using System;
using System.Diagnostics.CodeAnalysis;

namespace Exam.RePlay
{
    public class Track : IComparable<Track>
    {
        public Track(string id, string title, string artist, int plays, int durationInSeconds)
        {
            this.Id = id;
            this.Title = title;
            this.Artist = artist;
            this.Plays = plays;
            this.DurationInSeconds = durationInSeconds;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public int Plays { get; set; }

        public int DurationInSeconds { get; set; }

        public int CompareTo([AllowNull] Track other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
