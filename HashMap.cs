public class Node<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public Node<TKey, TValue> Next { get; set; }

    /// <summary>
    /// Represents a node in the hash map.
    /// </summary>
    /// <param name="key">The key associated with the node.</param>
    /// <param name="value">The value associated with the node.</param>
    /// <param name="next">The next node in the linked list.</param>
    public Node(TKey key, TValue value, Node<TKey, TValue> next = null)
    {
        Key = key;
        Value = value;
        Next = next;
    }
}

public class HashMap<TKey, TValue>
{
    private Node<TKey, TValue>[] data;
    private int count;

    /// <summary>
    /// Initializes a new instance of the HashMap class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The initial capacity of the hash map.</param>
    public HashMap(int capacity)
    {
        data = new Node<TKey, TValue>[capacity];
        count = 0;
    }

    /// <summary>
    /// Inserts a key-value pair into the hash map.
    /// If the key already exists, the value is updated.
    /// </summary>
    /// <param name="key">The key to insert or update.</param>
    /// <param name="value">The value associated with the key.</param>
    public void Put(TKey key, TValue value)
    {
        int index = GetHash(key);
        Node<TKey, TValue> node = data[index];

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
        Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value, data[index]);
        data[index] = newNode;
        count++;
    }

    /// <summary>
    /// Retrieves the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>The value associated with the key.</returns>
    /// <exception cref="Exception">Thrown when the key is not found.</exception>
    public TValue Get(TKey key)
    {
        int index = GetHash(key);
        Node<TKey, TValue> node = data[index];

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
    public void Remove(TKey key)
    {
        int index = GetHash(key);
        Node<TKey, TValue> node = data[index];
        Node<TKey, TValue> prev = null;

        while (node != null)
        {
            if (node.Key.Equals(key))
            {
                if (prev == null)
                {
                    // Key found at the head of the linked list
                    data[index] = node.Next;
                }
                else
                {
                    // Key found in the middle or end of the linked list
                    prev.Next = node.Next;
                }
                count--;
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
    public bool ContainsKey(TKey key)
    {
        int index = GetHash(key);
        Node<TKey, TValue> node = data[index];

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
    /// Gets the number of key-value pairs in the hash map.
    /// </summary>
    /// <returns>The number of key-value pairs.</returns>
    public int Size()
    {
        return count;
    }

    /// <summary>
    /// Checks whether the hash map is empty.
    /// </summary>
    /// <returns>True if the hash map is empty, False otherwise.</returns>

    public bool IsEmpty()
    {
        return count == 0;
    }

    /// <summary>
    /// Removes all key-value pairs from the hash map, making it empty.
    /// </summary>
    public void Clear()
    {
        // Clear the hash map by creating a new array and resetting the count
        data = new Node<TKey, TValue>[data.Length];
        count = 0;
    }

    /// <summary>
    /// Computes the hash code for the specified key and maps it to an index in the underlying array.
    /// </summary>
    /// <param name="key">The key to compute the hash code for.</param>
    /// <returns>The index in the array where the key-value pair should be stored.</returns>
    private int GetHash(TKey key)
    {
        // Compute the hash code for the key and map it to an index in the array
        return Math.Abs(key.GetHashCode()) % data.Length;
    }
}
