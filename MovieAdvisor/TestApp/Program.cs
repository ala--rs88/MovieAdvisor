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

            var p = movieAdvisor.GetRatingPrediction(3, 3);

            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
