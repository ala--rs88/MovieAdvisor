namespace TestApp
{
    using System;
    using Microsoft.Practices.Unity;
    using MovieAdvisor.Business.Interfaces.Facades;
    using UnityConfiguration;

    public class Program
    {
        public static void Main(string[] args)
        {
            var unityContainer = new UnityContainer();
            new CompositeRegistry().ApplyToUnityContainer(unityContainer);

            var movieAdvisor = unityContainer.Resolve<IMovieAdvisorFacade>();

            Console.WriteLine("[{0}] Prediction calculation started.", DateTime.Now);

            var p = movieAdvisor.GetRatingPrediction(3, 4);
            
            Console.WriteLine("[{0}] Prediction calculation finished.", DateTime.Now);
            Console.WriteLine();

            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
