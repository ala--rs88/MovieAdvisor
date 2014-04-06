using System;

namespace MovieAdvisor.Business.Entities.Core.RatingPrediction
{
    public class MoviesSimilarity
    {
        public int SourceMovieId { get; set; }

        public int TargetMovieId { get; set; }
        
        public double SimilarityValue { get; set; }
    }
}
