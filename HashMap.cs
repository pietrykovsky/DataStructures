public class Node<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public Node<TKey, TValue> Next { get; set; }

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

    public HashMap(int capacity)
    {
        data = new Node<TKey, TValue>[capacity];
        count = 0;
    }

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

        Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value, data[index]);
        data[index] = newNode;
        count++;
    }

    public TValue Get(TKey key)
    {
        int index = GetHash(key);
        Node<TKey, TValue> node = data[index];

        while (node != null)
        {
            if (node.Key.Equals(key))
            {
                return node.Value;
            }
            node = node.Next;
        }

        throw new Exception("Key not found");
    }

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
                    data[index] = node.Next;
                }
                else
                {
                    prev.Next = node.Next;
                }
                count--;
                return;
            }
            prev = node;
            node = node.Next;
        }
    }

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

    public int Size()
    {
        return count;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public void Clear()
    {
        data = new Node<TKey, TValue>[data.Length];
        count = 0;
    }

    private int GetHash(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % data.Length;
    }
}
