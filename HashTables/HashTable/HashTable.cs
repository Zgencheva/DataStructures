namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        public LinkedList<KeyValue<TKey, TValue>>[] slots { get; set; }
        public int Count { get; private set; }

        public int Capacity => this.slots.Length;

        public const float FillFactor = 0.75f;

        public const int DefaultCapacity = 16;

        public HashTable() 
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public HashTable(int capacity = DefaultCapacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            foreach (var kvp in this.slots[slotNumber])
            {
                if (kvp.Key.Equals(key))
                {
                    throw new ArgumentException("Key already exists: " + key);
                }
            }
            var newKvp = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newKvp);
            this.Count++;
        }

        private int FindSlotNumber(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % this.Capacity;
        }

        private void GrowIfNeeded()
        {
            if ((float)(this.Count +1)/ this.Capacity > FillFactor)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);
            foreach (var elements in this.slots)
            {
                if (elements != null)
                {
                    foreach (var kvp in elements)
                    {
                        newHashTable.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            this.Count = newHashTable.Count;
            this.slots = newHashTable.slots;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            foreach (var kvp in this.slots[slotNumber])
            {
                if (kvp.Key.Equals(key))
                {
                    kvp.Value = value;
                    return true;
                }
            }
            var newKvp = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newKvp);
            this.Count++;
            return true;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);
            if (element == null)
            {
                throw new KeyNotFoundException();
            }
            return element.Value;

        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.AddOrReplace(key, value);  
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if(element != null)
            {
                value = element.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            KeyValue<TKey, TValue> result = null;
            var slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] != null)
            {
                foreach (var kvp in this.slots[slotNumber])
                {
                    if (kvp.Key.Equals(key))
                    {
                        result = kvp;
                    }
                }
            }
            return result;
        }

        public bool ContainsKey(TKey key)
        {
            var element = this.Find(key);
            return element != null;
        }

        public bool Remove(TKey key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];
            if (elements == null)
            {
                return false;
            }
            var currentElement = elements.First;
            while (currentElement != null)
            {
                if (currentElement.Value.Key.Equals(key))
                {
                    elements.Remove(currentElement);
                    this.Count--;
                    return true;
                }
                currentElement = currentElement.Next;
            }
            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(x => x.Key);

        public IEnumerable<TValue> Values
        {
            get
            {
                return this.Select(x => x.Value);
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var elements in this.slots)
            {
                if (elements != null)
                {
                    foreach (var item in elements)
                    {
                        yield return item;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
