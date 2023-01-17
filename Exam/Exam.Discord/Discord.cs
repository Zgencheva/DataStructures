using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Discord
{
    public class Discord : IDiscord
    {
        public Discord()
        {
            this.Messages = new Dictionary<string, Message>();
            this.Channels = new Dictionary<string, SortedSet<Message>>();
        }
        public Dictionary<string, Message> Messages { get; set; }

        public Dictionary<string, SortedSet<Message>> Channels { get; set; }

        public int Count => this.Messages.Count;
        public void SendMessage(Message message)
        {
            if (!this.Messages.ContainsKey(message.Id))
            {
                this.Messages.Add(message.Id,message);
            }
            this.Messages[message.Id] = message;
            if (!this.Channels.ContainsKey(message.Channel))
            {
                this.Channels.Add(message.Channel, new SortedSet<Message>());
            }
            this.Channels[message.Channel].Add(message);
        }
        public bool Contains(Message message)
        {
            return this.Messages.ContainsKey(message.Id);
        }
        public Message GetMessage(string messageId)
        {
            if (!this.Messages.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }
            return this.Messages[messageId];
        }
        public void DeleteMessage(string messageId)
        {
            if (!this.Messages.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }
            var message = this.Messages[messageId];
            this.Messages.Remove(messageId);
            this.Channels[message.Channel].Remove(message);
        }
        public void ReactToMessage(string messageId, string reaction)
        {
            if (!this.Messages.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }
            var message = this.Messages[messageId];
            this.Messages[messageId].Reactions.Add(reaction);
            this.Channels[message.Channel]
                .FirstOrDefault(x => x.Id == messageId)
                .Reactions.Add(reaction);
        }

        public IEnumerable<Message> GetChannelMessages(string channel)
        {
            if (!Channels.ContainsKey(channel))
            {
                throw new ArgumentException();
            }
            return this.Channels[channel];
        }
        public IEnumerable<Message> GetMessagesByReactions(List<string> reactions)
        {
            return this.Messages
                .Select(x=> x.Value)
                .Where(x => x.Reactions.All(r => reactions.Contains(r)))
                .OrderByDescending(x => x.Reactions.Count)
                .ThenBy(x => x.Timestamp);
        }
        public IEnumerable<Message> GetMessageInTimeRange(int lowerBound, int upperBound)
        {
            var result = this.Channels
                .OrderByDescending(x => x.Value.Count)
                .SelectMany(x => x.Value
                        .Where(m => lowerBound <= m.Timestamp && m.Timestamp <= upperBound));

            return result;
                
        }
        public IEnumerable<Message> GetTop3MostReactedMessages()
        {
           return this.Messages.Select(x => x.Value)
                .OrderByDescending(x => x.Reactions.Count)
                .Take(3);
                
        }
        public IEnumerable<Message> GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent()
        {
            return this.Messages.Select(x => x.Value)
                .OrderByDescending(x => x.Reactions.Count)
                .ThenBy(x => x.Timestamp)
                .ThenBy(x => x.Content.Length)
                .ToList();
        }


    }
}
