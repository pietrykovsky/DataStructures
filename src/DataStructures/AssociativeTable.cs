/*public class AssociativeTable<TKey, TValue>
{
    private Tuple<TKey, TValue>[] Data;
    private int Count;
    
    
    /// <summary>
    /// Initializes a new instance of the associative table class.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the associative table can contain.</param>
    public AssociativeTable(int capacity)
    {
        Data = new Tuple<TKey, TValue>[capacity];
        Count = 0;
    }
    
    /// <summary>
    /// Doubles size of associative table when there is no place for adding new element.
    /// </summary>
    private void DoubleArraySize()
    {
        int tempSize = Data.Length * 2;
        Tuple<TKey, TValue>[] tempArray = new Tuple<TKey, TValue>[tempSize];
        Array.Copy(Data, tempArray, Count);
        Data = tempArray;
    }
    
    /// <summary>
    /// Inserts new Tuple element to array, if Key already exists - it will be overwritten.
    /// </summary>
    /// <param name="tempKey">The key associated with the Tuple.</param>
    /// <param name="tempValue">The value associated with the Tuple.</param>
    public void Put(TKey tempKey, TValue tempValue)
    {
        for(int i = 0; i < Count; i++)
        {
            if(EqualityComparer<TKey>.Default.Equals(Data[i].Key, tempKey))
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
    public TValue Get(TKey tempKey)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Item1, tempKey))
            {
                return Data[i].Item2;
            }  
        }
        throw new Exception("Key not found");
    }
    
    /// <summary>
    /// Removes the specified key and its associated value from the hash map.
    /// </summary>
    /// <param name="tempValue">The Value to remove - method will loop through whole array.</param>
    public void RemoveByValue(TValue tempValue)
    {
        int tempIndex = -1;
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TValue>.Default.Equals(Data[i].Item2, tempValue))
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
    
    /// <summary>
    /// Removes the specified key and its associated value from the hash map.
    /// </summary>
    /// <param name="tempKey">The key to remove - method will loop through whole array..</param>
    public void RemoveByKey(TKey tempKey)
    {
        int tempIndex = -1;
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Item1, tempKey))
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
    
    /// <summary>
    /// Checks whether the associative table is empty.
    /// </summary>
    /// <returns>True if the associative table is empty, False otherwise.</returns>
    public bool IsEmpty()
    {
        return Count == 0;
    }

    /// <summary>
    /// Gets the number of elements in associative table's array.
    /// </summary>
    /// <returns>Number of elements in array.</returns>
    public int GetSize()
    {
        return Count;
    }
    
}
*/