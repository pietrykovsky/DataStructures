using System.Collections;

namespace DataStructures
{
    public abstract class BaseMap<TKey, TValue> : IEnumerable<Node<TKey, TValue>>
    {
        protected Node<TKey, TValue>[] Data;
        protected int Count;

        public abstract IEnumerator<Node<TKey, TValue>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Inserts a key-value pair into the map.
        /// If the key already exists, the value is updated.
        /// </summary>
        /// <param name="key">The key to insert or update.</param>
        /// <param name="value">The value associated with the key.</param>
        public abstract void Put(TKey key, TValue value);

        /// <summary>
        /// Retrieves the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>The value associated with the key.</returns>
        public abstract TValue Get(TKey key);

        /// <summary>
        /// Removes the specified key and its associated value from the hash map.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public abstract void Remove(TKey key);

        /// <summary>
        /// Determines whether the hash map contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>True if the key exists, False otherwise.</returns>
        public abstract bool ContainsKey(TKey key);

        /// <summary>
        /// Gets the number of key-value pairs in the hash map.
        /// </summary>
        /// <returns>The number of key-value pairs.</returns>
        public int Size()
        {
            return Count;
        }

        /// <summary>
        /// Checks whether the hash map is empty.
        /// </summary>
        /// <returns>True if the hash map is empty, False otherwise.</returns>

        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Removes all key-value pairs from the hash map, making it empty.
        /// </summary>
        public void Clear()
        {
            // Clear the hash map by creating a new array and resetting the count
            Data = new Node<TKey, TValue>[Data.Length];
            Count = 0;
        }
    }
}