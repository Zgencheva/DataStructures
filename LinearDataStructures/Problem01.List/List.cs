namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            this.items = new T[capacity];
        }

        public T this[int index] { get => this.Get(index); set => this.SetValueToIndex(index, value); }

        private void SetValueToIndex(int index, T value)
        {
            this.ValidateIndex(index);
            this.items[index] = value;
        }

        private T Get(int index)
        {
            this.ValidateIndex(index);
            return this.items[index];
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ResizeIfNeeded();
            items[Count] = item;
            Count++;
        }

        private void ResizeIfNeeded()
        {
            if (this.Count + 1 > this.items.Length)
            {
                var newArray = new T[this.items.Length * 2];
                Array.Copy(this.items, newArray, this.Count);
                this.items = newArray;
            }
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1 ? true : false;
            //foreach (var element in this)
            //{
            //    if (element.Equals(item))
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);

            this.ResizeIfNeeded();
            var newArray = new T[this.items.Length];

            var first = this.items.Take(index).ToArray();
            var second = this.items.Skip(index).Take(this.Count - first.Length).ToArray();
            first.CopyTo(newArray, 0);
            newArray[index] = item;
            second.CopyTo(newArray, first.Length + 1);
            this.items = newArray;
            this.Count++;
        }

        public bool Remove(T item)
        {
            if (!this.Contains(item))
            {
                return false;
            }
            var index = this.IndexOf(item);
            this.RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);
            var newArray = new T[this.items.Length];
            var first = this.items.Take(index).ToArray();
            var second = this.items.Skip(index + 1).Take(this.Count - first.Length).ToArray();
            first.CopyTo(newArray, 0);
            second.CopyTo(newArray, first.Length);
            this.items = newArray;
            this.Count--;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}