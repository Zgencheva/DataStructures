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
            this.RoutesById = new Dictionary<string, Route>();
            this.Routes = new HashSet<Route>();
        }
        private Dictionary<string, Route> RoutesById { get; set; }
        private HashSet<Route> Routes { get; set; }

        public int Count => this.Routes.Count;

        public void AddRoute(Route route)
        {
            if (this.Routes.Contains(route))
            {
                throw new ArgumentException();
            }
            this.Routes.Add(route);
            this.RoutesById.Add(route.Id, route);
        }

        public void RemoveRoute(string routeId)
        {
            if (!this.RoutesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            var route = this.RoutesById[routeId];
            this.Routes.Remove(route);
            this.RoutesById.Remove(routeId);
        }

        public bool Contains(Route route)
       => this.Routes.Contains(route);

        public Route GetRoute(string routeId)
        {
            if (!this.RoutesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
           
            return this.RoutesById[routeId];
        }

        public void ChooseRoute(string routeId)
        {
            if (!this.RoutesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            this.RoutesById[routeId].Popularity += 1;
        }

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
        {
            return this.Routes
                .Where(x => x.IsFavorite && x.LocationPoints.IndexOf(destinationPoint) >= 1)
                .OrderBy(x => x.Distance)
                .ThenByDescending(x => x.Popularity);
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
        =>      this.Routes
                .OrderByDescending(x => x.Popularity)
                .ThenBy(x => x.Distance)
                .ThenBy(x=> x.LocationPoints.Count)
                .Take(5);



        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
        {
            var chosenRoutes = this.Routes
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
