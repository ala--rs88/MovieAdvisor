namespace MovieAdvisor.BulkDataLoader.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Core.DataAccess.Entities;
    using Neo4jClient;
    using Neo4jClient.Cypher;

    public class Neo4jDataManager : ITargetStorageDataManager
    {
        private readonly IGraphClient neo4jClient;

        public Neo4jDataManager(IGraphClient neo4jClient)
        {
            if (neo4jClient == null)
            {
                throw new ArgumentNullException("neo4jClient");
            }

            this.neo4jClient = neo4jClient;
        }

        public void CreateUsersByBatch(IEnumerable<UserData> usersBatch)
        {
            if (usersBatch == null)
            {
                throw new ArgumentOutOfRangeException("usersBatch");
            }

            neo4jClient.Cypher
                .Create("(:User {users})")
                .WithParam("users", usersBatch.ToArray())
                .ExecuteWithoutResults();
        }

        public void CreateMoviesByBatch(IEnumerable<MovieData> moviesBatch)
        {
            if (moviesBatch == null)
            {
                throw new ArgumentOutOfRangeException("moviesBatch");
            }

            neo4jClient.Cypher
                .Create("(:Movie {movies})")
                .WithParam("movies", moviesBatch.ToArray())
                .ExecuteWithoutResults();
        }

        public void CreateRatingRecordsByBatch(IEnumerable<RatingRecordData> ratingRecordsBatch)
        {
            if (ratingRecordsBatch == null)
            {
                throw new ArgumentOutOfRangeException("ratingRecordsBatch");
            }

            ratingRecordsBatch = ratingRecordsBatch.ToList();

            var matchStatement = ratingRecordsBatch.Select(
                    (r, i) => string.Format(CultureInfo.InvariantCulture, "(u{0}:User),(m{0}:Movie)", i))
                    .Aggregate((a, b) => a + "," + b);

            var createStatement = ratingRecordsBatch.Select(
                (r, i) => string.Format(CultureInfo.InvariantCulture, "u{0}-[:Rated {{ratingProperties{0}}}]->m{0}", i))
                .Aggregate((a, b) => a + "," + b);

            var parameters = new Dictionary<string, object>();

            ratingRecordsBatch.SelectMany((r, i) => new[]
                {
                    new Tuple<string, object>(string.Format(CultureInfo.InvariantCulture, "ratingProperties{0}", i), new { Value = (int)r.Value, r.Timestamp }), 
                    new Tuple<string, object>(string.Format(CultureInfo.InvariantCulture, "u{0}Id", i), r.UserId),
                    new Tuple<string, object>(string.Format(CultureInfo.InvariantCulture, "m{0}Id", i), r.MovieId)
                }).ToList().ForEach(tuple => parameters.Add(tuple.Item1, tuple.Item2));

            var matchQuery = neo4jClient.Cypher.Match(matchStatement);

            var whereQuery = ApplyWhereStatements(matchQuery, ratingRecordsBatch.Count());

            whereQuery
                .CreateUnique(createStatement)
                .WithParams(parameters)
                .ExecuteWithoutResults();
        }

        private static ICypherFluentQuery ApplyWhereStatements(ICypherFluentQuery initialQuery, int numberOfTimes)
        {
            if (numberOfTimes < 1)
            {
                throw new ArgumentOutOfRangeException("numberOfTimes");
            }

            var whereQuery = initialQuery.Where("u0.UserId = {u0Id}").AndWhere("m0.MovieId = {m0Id}");

            for (int i = 1; i < numberOfTimes; i++)
            {
                whereQuery = whereQuery
                    .AndWhere(string.Format(CultureInfo.InvariantCulture, "u{0}.UserId = {{u{0}Id}}", i))
                    .AndWhere(string.Format(CultureInfo.InvariantCulture, "m{0}.MovieId = {{m{0}Id}}", i));
            }

            return whereQuery;
        }
    }
}
