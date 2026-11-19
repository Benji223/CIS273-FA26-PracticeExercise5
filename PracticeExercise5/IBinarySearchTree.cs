namespace PracticeExercise5;

public interface IBinarySearchTree<T>
{
    public void Add(int key, T value);
    public bool Contains(int key);

    /// <summary>
    /// Remove the node with the given key.
    /// </summary>
    /// <exception cref="KeyNotFoundException">The key is not in the tree.</exception>
    public void Remove(int key);

    public void Update(int key, T value);

    /// <summary>
    /// Return the value stored with the given key.
    /// </summary>
    /// <exception cref="KeyNotFoundException">The key is not in the tree.</exception>
    public T Search(int key);

    public void Clear();

    public bool IsEmpty { get; }
    public int Count { get; }
    public int Height { get; }

    // Advanced
    public int? MinKey { get; }
    public int? MaxKey { get; }

    public Tuple<int, T>? Min { get; }
    public Tuple<int, T>? Max { get; }

    /// <summary>
    /// Return the median of all the keys, or null if the tree is empty.
    /// With an even number of keys, this is the average of the two middle keys.
    /// </summary>
    public double? MedianKey { get; }

    /// <summary>
    /// Return the next larger key in the tree (the in-order successor of key),
    /// or null if key is the largest key.
    /// </summary>
    /// <exception cref="KeyNotFoundException">The key is not in the tree.</exception>
    public int? NextKey(int key);

    /// <summary>
    /// Return the next smaller key in the tree (the in-order predecessor of key),
    /// or null if key is the smallest key.
    /// </summary>
    /// <exception cref="KeyNotFoundException">The key is not in the tree.</exception>
    public int? PrevKey(int key);

    /// <summary>
    /// Return all keys between min and max (inclusive), in ascending order.
    /// Returns an empty list if no keys match, including when min is greater than max.
    /// </summary>
    public List<int> RangeSearch(int min, int max);

    // Tree Traversal
    public List<int> InOrderKeys { get; }
    public List<int> PreOrderKeys { get; }
    public List<int> PostOrderKeys { get; }

}
