namespace DataStructures
{
    public class AssociativeTable<TKey, TValue> : BaseMap<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the associative table class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the associative table can contain.</param>
        public AssociativeTable(int capacity)
        {
            Data = new Node<TKey, TValue>[capacity];
            Count = 0;
        }

        /// <summary>
        /// Doubles size of associative table when there is no place for adding new element.
        /// </summary>
        private void DoubleArraySize()
        {
            int tempSize = Data.Length * 2;
            Node<TKey, TValue>[] tempArray = new Node<TKey, TValue>[tempSize];
            Array.Copy(Data, tempArray, Count);
            Data = tempArray;
        }

        /// <summary>
        /// Inserts new Tuple element to array, if Key already exists - it will be overwritten.
        /// </summary>
        /// <param name="tempKey">The key associated with the Tuple.</param>
        /// <param name="tempValue">The value associated with the Tuple.</param>
        public override void Put(TKey tempKey, TValue tempValue)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, tempKey))
                {
                    Data[i].Value = tempValue;
                    return;
                }
            }
            if (Count == Data.Length)
            {
                DoubleArraySize();
            }
            Data[Count] = new Node<TKey, TValue>(tempKey, tempValue);
            Count++;
        }

        /// <summary>
        /// Returns all values of Tuple elements inside Data array with passed key value.
        /// </summary>
        /// <param name="tempKey">The key to search for.</param>
        public override TValue Get(TKey tempKey)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, tempKey))
                {
                    return Data[i].Value;
                }
            }
            throw new Exception("Key not found");
        }

        public override bool ContainsKey(TKey tempKey)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, tempKey))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the specified key and its associated value from the hash map.
        /// </summary>
        /// <param name="tempKey">The key to remove - method will loop through whole array..</param>
        public override void Remove(TKey tempKey)
        {
            int tempIndex = -1;
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, tempKey))
                {
                    tempIndex = i;
                    for (int j = tempIndex; j < Data.Length; j++)
                    {
                        Data[i] = Data[i + 1];
                        Count--;
                    }
                }
            }
        }
    }
}