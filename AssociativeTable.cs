public class AssociativeTable<TKey, TValue> : BaseMap<TKey, TValue>
{
    private int FirstFreeIndex = 0;
    
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
    /// Doubles (creates new array with doubled size) the size of an array, and copies old one into new one.
    /// </summary>
    private void DoubleArraySize()
    {
        int tempSize = Data.Length * 2;
        Node<TKey, TValue>[] tempArray = new Node<TKey, TValue>[tempSize];
        Array.Copy(Data, tempArray, Count);
        Data = tempArray;
    }
    
    /// <summary>
    /// Inserts a key-value pair into the array.
    /// If the key already exists, the value is updated.
    /// </summary>
    /// <param name="Key">The key to insert or update.</param>
    /// <param name="Value">The value associated with the key.</param>
    public override void Put(TKey Key, TValue Value)
    {
        if (FirstFreeIndex == Data.Length)
        {
            DoubleArraySize();
            FirstFreeIndex = Count;
        }
        // using ContainsKey method will make this block of function O(2n) instead of O(n)
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, Key))
            {
                Data[i].Value = Value;
                return;
            }
        }
        Data[FirstFreeIndex] = new Node<TKey, TValue>(Key, Value);
        Count++;
        for (int i = FirstFreeIndex; i < Data.Length; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, default(TKey)))
            {
                FirstFreeIndex = i;
                break;
            }
        }
    }
    
    /// <summary>
    /// Retrieves the value associated with the specified key.
    /// </summary>
    /// <param name="Key">The key to search for.</param>
    /// <returns>The value associated with the key.</returns>
    public override TValue Get(TKey Key)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, Key))
            {
                return Data[i].Value;
            }  
        }
        throw new Exception("Key not found");
    }
    
    /// <summary>
    /// Determines whether the hash map contains the specified key.
    /// </summary>
    /// <param name="Key">The key to search for.</param>
    /// <returns>True if the key exists, False otherwise.</returns>
    public override bool ContainsKey(TKey Key)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, Key))
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// Removes the specified key and its associated value from the hash map.
    /// </summary>
    /// <param name="Key">The key to remove.</param>
    public override void Remove(TKey Key)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, Key ))
            {
                Data[i] = new Node<TKey, TValue>(default(TKey), default(TValue));
                if (i < FirstFreeIndex)
                {
                    FirstFreeIndex = i;
                }
                Count--;
            }
        }
    }
}
