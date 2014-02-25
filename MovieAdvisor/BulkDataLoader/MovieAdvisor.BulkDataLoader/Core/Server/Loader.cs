using System;
using System.IO;
using System.Linq;

namespace MovieAdvisor.BulkDataLoader.Core.Server
{
    public class Loader
    {
        public void LoadMovies(string moviesDataPath)
        {
            using (var streamReader = new StreamReader(moviesDataPath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    try
                    {
                        //// "6::Heat (1995)::Action|Crime|Thriller"

                    }
                    catch
                    {
                        
                    }
                    
                }
            }
        }
    }
}
