using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exam.Discord
{
    public class Message : IComparable<Message>
    {
        public Message(string id, string content, int timestamp, string channel)
        {
            this.Id = id;
            this.Content = content;
            this.Timestamp = timestamp;
            this.Channel = channel;
        }

        public string Id { get; set; }

        public string Content { get;set; }

        public int Timestamp { get; set; }

        public string Channel { get; set; }

        public List<string> Reactions { get; set; } = new List<string>();

        public int CompareTo([AllowNull] Message other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
