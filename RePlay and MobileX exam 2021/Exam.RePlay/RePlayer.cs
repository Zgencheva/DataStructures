using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.RePlay
{
    public class RePlayer : IRePlayer
    {
        public Queue<Track> ListeningQueue { get; set; }
        public SortedDictionary<string, Dictionary<string,Track>> Albums { get; set; }
        public Dictionary<string, Track> Tracks { get; set; }

        public RePlayer()
        {
            this.Albums = new SortedDictionary<string, Dictionary<string, Track>>();
            this.ListeningQueue = new Queue<Track>();
            this.Tracks = new Dictionary<string, Track>();
        }
        public int Count => this.Tracks.Count;
        public void AddTrack(Track track, string album)
        {
            track.Album = album;
            if (!this.Albums.ContainsKey(album))
            {
                this.Albums.Add(album, new Dictionary<string, Track>());
            }
            if (!this.Albums[album].ContainsKey(track.Title))
            {
                this.Albums[album].Add(track.Title, track);
            }

            if (!this.Tracks.ContainsKey(track.Id))
            {
                this.Tracks.Add(track.Id, track);
            }
            
        }

        public bool Contains(Track track)
        => this.Tracks.ContainsKey(track.Id);

        public Track GetTrack(string title, string albumName)
        {
            if (!this.Albums.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            if (!this.Albums[albumName].ContainsKey(title))
            {
                throw new ArgumentException();
            }
           var track = this.Albums[albumName][title];
           
            return track;
        }
        public IEnumerable<Track> GetAlbum(string albumName)
        {
            if (!this.Albums.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            return this.Albums[albumName]
                .Values
                .OrderByDescending(x => x.Plays);
        }

        public void AddToQueue(string trackName, string albumName)
        {
            if (!this.Albums.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            if (!this.Albums[albumName].ContainsKey(trackName))
            {
                throw new ArgumentException();
            }
            var track = this.Albums[albumName][trackName];
            this.ListeningQueue.Enqueue(track);
        }

        public Track Play()
        {
            if (this.ListeningQueue.Count == 0)
            {
                throw new ArgumentException();
            }
            var track = this.ListeningQueue.Dequeue();
            this.Tracks[track.Id].Plays += 1;
            return track;
        }
        public void RemoveTrack(string trackTitle, string albumName)
        {
            if (!this.Albums.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }
            if (!this.Albums[albumName].ContainsKey(trackTitle))
            {
                throw new ArgumentException();
            }
            var track = this.Albums[albumName][trackTitle];
            this.Tracks.Remove(track.Id);
            this.Albums[albumName].Remove(track.Title);
            if (this.ListeningQueue.Contains(track))
            {
                this.ListeningQueue = new Queue<Track>(ListeningQueue.Where(x => x.Title != trackTitle));
            }
        }
        public IEnumerable<Track> GetTracksInDurationRangeOrderedByDurationThenByPlaysDescending(int lowerBound, int upperBound)
        => this.Tracks
            .Values
            .Where(x=> lowerBound <= x.DurationInSeconds && x.DurationInSeconds <= upperBound)
            .OrderBy(x => x.DurationInSeconds)
            .ThenByDescending(x => x.Plays)
            .ToList();

        public IEnumerable<Track> GetTracksOrderedByAlbumNameThenByPlaysDescendingThenByDurationDescending()
        {
            var result = this.Tracks
                .Values
                .OrderBy(x=> x.Album)
            .ThenByDescending(x => x.Plays)
            .ThenByDescending(x => x.DurationInSeconds)
            .ToList();

            return result;
        }
        public Dictionary<string, List<Track>> GetDiscography(string artistName)
        {
            if (!this.Tracks.Values.Any(x=> x.Artist == artistName))
            {
                throw new ArgumentException();
            }
            var songs = this.Tracks.Values.Where(x => x.Artist == artistName);
            var result = new Dictionary<string, List<Track>>();
            foreach (var song in songs)
            {
                if (!result.ContainsKey(song.Album))
                {
                    result.Add(song.Album, new List<Track>());
                }
                result[song.Album].Add(song);
            }

            return result;
        }

    }
}
