namespace DataStructures
{
    public abstract class BaseHashMap<TKey, TValue> : BaseMap<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the HashMap class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the hash map.</param>
        public BaseHashMap(int capacity)
        {
            Data = new Node<TKey, TValue>[capacity];
            Count = 0;
        }

        public override IEnumerator<Node<TKey, TValue>> GetEnumerator()
        {
            // Iterate over all buckets.
            for (int i = 0; i < Data.Length; i++)
            {
                var current = Data[i];

                // Iterate over all nodes within a bucket.
                while (current != null)
                {
                    yield return new Node<TKey, TValue>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        /// <summary>
        /// Inserts a key-value pair into the hash map.
        /// If the key already exists, the value is updated.
        /// </summary>
        /// <param name="key">The key to insert or update.</param>
        /// <param name="value">The value associated with the key.</param>
        public override void Put(TKey key, TValue value)
        {
            int index = GetHash(key);
            Node<TKey, TValue> node = Data[index];

            while (node != null)
            {
                if (node.Key.Equals(key))
                {
                    // Update value if key already exists
                    node.Value = value;
                    return;
                }
                node = node.Next;
            }
            // Key does not exist, create a new node and add it to the linked list
            Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value, Data[index]);
            Data[index] = newNode;
            Count++;
        }

        /// <summary>
        /// Retrieves the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="Exception">Thrown when the key is not found.</exception>
        public override TValue Get(TKey key)
        {
            int index = GetHash(key);
            Node<TKey, TValue> node = Data[index];

            while (node != null)
            {
                if (node.Key.Equals(key))
                {
                    // Key found, return the corresponding value
                    return node.Value;
                }
                node = node.Next;
            }

            throw new Exception("Key not found");
        }

        /// <summary>
        /// Removes the specified key and its associated value from the hash map.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public override void Remove(TKey key)
        {
            int index = GetHash(key);
            Node<TKey, TValue> node = Data[index];
            Node<TKey, TValue> prev = null;

            while (node != null)
            {
                if (node.Key.Equals(key))
                {
                    if (prev == null)
                    {
                        // Key found at the head of the linked list
                        Data[index] = node.Next;
                    }
                    else
                    {
                        // Key found in the middle or end of the linked list
                        prev.Next = node.Next;
                    }
                    Count--;
                    return;
                }
                prev = node;
                node = node.Next;
            }
        }

        /// <summary>
        /// Determines whether the hash map contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>True if the key exists, False otherwise.</returns>
        public override bool ContainsKey(TKey key)
        {
            int index = GetHash(key);
            Node<TKey, TValue> node = Data[index];

            while (node != null)
            {
                if (node.Key.Equals(key))
                {
                    return true;
                }
                node = node.Next;
            }

            return false;
        }
        /// <summary>
        /// Computes the hash code for the specified key and maps it to an index in the underlying array.
        /// </summary>
        /// <param name="key">The key to compute the hash code for.</param>
        /// <returns>The index in the array where the key-value pair should be stored.</returns>
        protected abstract int GetHash(TKey key);
    }

    public class SimpleHashMap<TKey, TValue> : BaseHashMap<TKey, TValue>
    {
        public SimpleHashMap(int capacity) : base(capacity) { }

        protected override int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % Data.Length;
        }
    }

    public class LengthHashMap<TKey, TValue> : BaseHashMap<TKey, TValue>
    {
        public LengthHashMap(int capacity) : base(capacity) { }

        protected override int GetHash(TKey key)
        {
            // convert TKey to string and then get the length
            string keyString = key.ToString();
            return keyString.Length % Data.Length;
        }
    }

    public class CharValueHashMap<TKey, TValue> : BaseHashMap<TKey, TValue>
    {
        public CharValueHashMap(int capacity) : base(capacity) { }

        protected override int GetHash(TKey key)
        {
            // convert TKey to string and then sum the char values
            string keyString = key.ToString();
            int sum = 0;
            foreach (char c in keyString)
            {
                sum += (int)c;
            }
            return sum % Data.Length;
        }
    }
}