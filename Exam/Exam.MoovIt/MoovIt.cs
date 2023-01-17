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
            this.RoutesByStartAndEndPoint = 
                new Dictionary<(string, string), SortedSet<Route>>();
        }
        public Dictionary<string, Route> Routes { get; set; }

        public Dictionary<(string start, string end), SortedSet<Route>> RoutesByStartAndEndPoint { get; set; }

        public int Count => this.Routes.Count;

        public void AddRoute(Route route)
        {
            if (this.Routes.ContainsKey(route.Id))
            {
                throw new ArgumentException();
            }
            this.Routes[route.Id] = route;
            var startEndPoint = (route.LocationPoints[0], route.LocationPoints[route.LocationPoints.Count - 1]);
            if (!this.RoutesByStartAndEndPoint.ContainsKey(startEndPoint))
            {
                this.RoutesByStartAndEndPoint.Add(startEndPoint, new SortedSet<Route>());
            }
            this.RoutesByStartAndEndPoint[startEndPoint].Add(route);
        }

        public void ChooseRoute(string routeId)
        {
            if (!this.Routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            this.Routes[routeId].Popularity += 1;
            var route = this.Routes[routeId];
            var kvp = (route.LocationPoints[0], route.LocationPoints[route.LocationPoints.Count - 1]);
            this.RoutesByStartAndEndPoint[kvp]
                .FirstOrDefault(x => x.Id == routeId)
                .Popularity += 1;
        }

        public bool Contains(Route route)
         => this.RoutesByStartAndEndPoint
            .ContainsKey((route.LocationPoints[0], route.LocationPoints[route.LocationPoints.Count - 1]));

        
        public Route GetRoute(string routeId)
        {
            if (!this.Routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            return this.Routes[routeId];
        }

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
        {
            return this.Routes.Values
                .Where(x => x.LocationPoints[x.LocationPoints.Count - 1] == destinationPoint)
                .OrderByDescending(x => x.Popularity);
        }

        
        public void RemoveRoute(string routeId)
        {
            if (!this.Routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            this.Routes.Remove(routeId);
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
=>          this.Routes
                .Values
                .OrderByDescending(x => x.Popularity)
                .ThenBy(x => x.Distance)
                .ThenBy(x=> x.LocationPoints.Count)
                .Take(5);



        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
        => this.RoutesByStartAndEndPoint[(startPoint, endPoint)];
    }
}
