﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exam.MoovIt
{
    public class Route : IComparable<Route>
    {
        public Route(string id, 
            double distance, 
            int popularity, 
            bool isFavorite, 
            List<string> locationPoints) 
        {
            this.Id = id;
            this.Distance = distance;
            this.Popularity = popularity;
            this.IsFavorite = isFavorite;
            this.LocationPoints = locationPoints;
        }
        public string Id { get; set; }

        public double Distance { get; set; }

        public int Popularity { get; set; }

        public bool IsFavorite { get; set; }

        public List<string> LocationPoints { get; set; } = new List<string>();

        public int CompareTo([AllowNull] Route other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
