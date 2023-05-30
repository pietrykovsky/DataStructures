namespace DataStructures
{
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
}