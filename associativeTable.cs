﻿public class Node<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    /// <summary>
    /// Represents a node in the associative table.
    /// </summary>
    /// <param name="key">The key associated with the node.</param>
    /// <param name="value">The value associated with the node.</param>
    public Node(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public class AssociativeTable<TKey, TValue>
{
    private Node<TKey, TValue>[] Data;
    private int Count;
    
    
    /// <summary>
    /// Initializes a new instance of the associative table class.
    /// </summary>
    public AssociativeTable()
    {
        Data = new Node<TKey, TValue>[1];
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
    /// Inserts new Node element to array.
    /// </summary>
    public void Put(TKey tempKey, TValue tempValue)
    {
        if (Count == Data.Length)
        {
            DoubleArraySize();
        }

        Data[Count] = new Node<TKey, TValue>(tempKey, tempValue);
        Count++;
    }
    
    /// <summary>
    /// Returns all values of Node elements inside Data array with passed key value.
    /// </summary>
    /// <param name="tempKey">The key to search for.</param>
    public TValue Get(TKey tempKey)
    {
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(Data[i].Key, tempKey))
            {
                return Data[i].Value;
            }  
        }
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
            if (EqualityComparer<TValue>.Default.Equals(Data[i].Value, tempValue))
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