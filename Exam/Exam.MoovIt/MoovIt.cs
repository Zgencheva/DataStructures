using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Exam.MoovIt
{
    public class MoovIt : IMoovIt
    {
        public MoovIt()
        {
            this.Routes = new Dictionary<string, Route>();
        }
        public Dictionary<string, Route> Routes { get; set; }

        public int Count => this.Routes.Count;

        public void AddRoute(Route route)
        {
            if (this.Routes.ContainsKey(route.Id))
            {
                throw new ArgumentException();
            }
            this.Routes[route.Id] = route;         
        }

        public void RemoveRoute(string routeId)
        {
            if (!this.Routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            this.Routes.Remove(routeId);
        }

        public bool Contains(Route route)
       => this.Routes.Values
            .Any(x=> x.LocationPoints[0] == route.LocationPoints[0]
            && x.LocationPoints[x.LocationPoints.Count - 1] == route.LocationPoints[route.LocationPoints.Count-1]);

        public Route GetRoute(string routeId)
        {
            if (!this.Routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            return this.Routes[routeId];
        }

        public void ChooseRoute(string routeId)
        {
            if (!this.Routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            this.Routes[routeId].Popularity += 1;
        }

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
        {
            return this.Routes.Values
                .Where(x => x.IsFavorite && x.LocationPoints.IndexOf(destinationPoint) >= 1)
                .OrderBy(x => x.Distance)
                .ThenByDescending(x => x.Popularity);
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
=>          this.Routes
                .Values
                .OrderByDescending(x => x.Popularity)
                .ThenBy(x => x.Distance)
                .ThenBy(x=> x.LocationPoints.Count)
                .Take(5);



        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
        {
            var chosenRoutes = this.Routes
                .Select(kvp => kvp.Value)
                .Where(r =>
                         r.LocationPoints.Contains(startPoint) &&
                         r.LocationPoints.Contains(endPoint)
                         && r.LocationPoints.IndexOf(startPoint) < r.LocationPoints.IndexOf(endPoint))
               .OrderBy(r => r.IsFavorite)
               .ThenBy(x => x.LocationPoints.IndexOf(endPoint) - x.LocationPoints.IndexOf(startPoint))
               .ThenByDescending(r => r.Popularity);

            return chosenRoutes;
        }
    }
}
