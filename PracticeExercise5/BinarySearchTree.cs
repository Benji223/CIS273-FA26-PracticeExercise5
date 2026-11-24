namespace PracticeExercise5;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
    private BinarySearchTreeNode<T>? Root { get; set; }

    public bool IsEmpty => Root == null;

    public int Count {get; private set; }

    public int Height {
       get
        {
          if (Root == null)
          {
              return 0;
          }
          return HeightRecursive(Root);
        } 
    }

    private int HeightRecursive( BinarySearchTreeNode<T>? node) {
        if (node == null)
        {
            return -1;
        }
        return Math.Max(HeightRecursive(node.Left), HeightRecursive(node.Right)) + 1;
    }

    public void Add(int key, T value)
    {
        if (IsEmpty)
        {
            Root = new BinarySearchTreeNode<T>(key, value);
            Count++;
            return;
        }

        AddRecursive(key, value, Root!);
    }

    private void AddRecursive(int key, T value, BinarySearchTreeNode<T> parent) 
    {
        if (key == parent.Key)
        {
            // Update the value
            parent.Value = value;
        }
        
        if (key < parent.Key)
        {
            // Go left
            if (parent.Left == null)
            {
                var newNode = new BinarySearchTreeNode<T>(key, value);
                parent.Left = newNode;
                newNode.Parent = parent;
                Count++;
            }
            else
            {
                AddRecursive(key, value, parent.Left);
            }
        
        }
        else if (key > parent.Key)
        {
            // Go right
            if (parent.Right == null)
            {
                var newNode = new BinarySearchTreeNode<T>(key, value);
                parent.Right = newNode;
                newNode.Parent = parent;
                Count++;
            }
            else
            {
                AddRecursive(key, value, parent.Right);
            }
        }
        
    }

    public void Clear()
    {
        Root = null;
        Count = 0;
    }

    public void Remove(int key)
    {
        var node = GetNode(key);
        if (node == null)
        {
            throw new KeyNotFoundException($"Key {key} not found in the tree.");
        }

        if (node.Left != null && node.Right != null)
        {
            var successor = MinNode(node.Right);
            node.Key = successor.Key;
            node.Value = successor.Value;
            RemoveNode(successor);
        }
        else
        {
            RemoveNode(node);
        }
    }

    private void RemoveNode(BinarySearchTreeNode<T> node)
    {
        var child = node.Left ?? node.Right;

        if (node.Parent == null)
        {
            Root = child;
        }
        else if (node.Parent.Left == node)
        {
            node.Parent.Left = child;
        }
        else
        {
            node.Parent.Right = child;
        }

        if (child != null)
        {
            child.Parent = node.Parent;
        }

        Count--;
    }

    public T Search(int key)
    {
        var node = GetNode(key);
        
        if (node == null)
        {
            throw new KeyNotFoundException($"Key {key} not found in the tree.");
        }
        return node.Value;
    }

    public void Update(int key, T value)
    {
        Add(key,value);
    }

    private BinarySearchTreeNode<T>? GetNode(int key)
    {
        if (IsEmpty)
        {
            return null;
        }
        return GetNodeRecursive(key, Root);
    }

    private BinarySearchTreeNode<T>? GetNodeRecursive(int key, BinarySearchTreeNode<T>? node) 
    {
        if (node == null)
        {
            return null;
        }

        if (key == node.Key)
        {
            return node;
        }

        // recursively case
        if (key < node.Key)
        {
            return GetNodeRecursive(key, node.Left);
        }

        // recursively case
        if (key > node.Key)
        {
            return GetNodeRecursive(key, node.Right);
        }

        return null;
    }

    public bool Contains(int key)
    {
        return GetNode(key) != null;
    }

     public List<int> InOrderKeys {
        get {
            var keys = new List<int>();
            InOrderKeyRecursive(Root, keys);
            return keys;
        }
    }

    private void InOrderKeyRecursive(BinarySearchTreeNode<T>? node, List<int> keys)
    {
        // Base case
        if (node == null)
        {
            return;
        }

        // recursive case
        // In-order traversal: left, root, right
        InOrderKeyRecursive(node.Left, keys);
        keys.Add(node.Key);
        InOrderKeyRecursive(node.Right, keys);

    }

    public List<int> PreOrderKeys
    {
        get
        {
            var keys = new List<int>();
            PreOrderKeyRecursive(Root, keys);
            return keys;
        }
    }

    private void PreOrderKeyRecursive(BinarySearchTreeNode<T>? node, List<int> keys)
    {
        if (node == null)
        {
            return;
        }
        keys.Add(node.Key);
        PreOrderKeyRecursive(node.Left, keys);
        PreOrderKeyRecursive(node.Right, keys);
    }

    public List<int> PostOrderKeys
    {
        get
        {
            var keys = new List<int>();
            PostOrderKeyRecursive(Root, keys);
            return keys;
        }
    }

    private void PostOrderKeyRecursive(BinarySearchTreeNode<T>? node, List<int> keys)
    {
        if (node == null)
        {
            return;
        }
        PostOrderKeyRecursive(node.Left, keys);
        PostOrderKeyRecursive(node.Right, keys);
        keys.Add(node.Key);
    }

    

    /* ADVANCED METHODS and PROPERTIES */ 

    public int? MinKey => Root == null ? null : MinNode(Root).Key;

    public int? MaxKey => Root == null ? null : MaxNode(Root).Key;

    public Tuple<int, T>? Min
    {
        get
        {
            if (Root == null)
            {
                return null;
            }
            var node = MinNode(Root);
            return Tuple.Create(node.Key, node.Value);
        }
    }

    public Tuple<int, T>? Max
    {
        get
        {
            if (Root == null)
            {
                return null;
            }
            var node = MaxNode(Root);
            return Tuple.Create(node.Key, node.Value);
        }
    }

    public double? MedianKey
    {
        get
        {
            if (Root == null)
            {
                return null;
            }

            var keys = InOrderKeys;
            int middle = keys.Count / 2;
            if (keys.Count % 2 == 1)
            {
                return keys[middle];
            }
            return (keys[middle - 1] + keys[middle]) / 2.0;
        }
    }
    

    private BinarySearchTreeNode<T> MaxNode(BinarySearchTreeNode<T> node)
    {
        while (node.Right != null)
        {
            node = node.Right;
        }
        return node;
    }

    private BinarySearchTreeNode<T> MinNode(BinarySearchTreeNode<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    public int? NextKey(int key)
    {
        var node = GetNode(key);
        if (node == null)
        {
            throw new KeyNotFoundException($"Key {key} not found in the tree.");
        }

        if (node.Right != null)
        {
            return MinNode(node.Right).Key;
        }

        var parent = node.Parent;
        while (parent != null && parent.Right == node)
        {
            node = parent;
            parent = parent.Parent;
        }
        return parent?.Key;
    }

    public int? PrevKey(int key)
    {
        var node = GetNode(key);
        if (node == null)
        {
            throw new KeyNotFoundException($"Key {key} not found in the tree.");
        }

        if (node.Left != null)
        {
            return MaxNode(node.Left).Key;
        }

        var parent = node.Parent;
        while (parent != null && parent.Left == node)
        {
            node = parent;
            parent = parent.Parent;
        }
        return parent?.Key;
    }

    /// <summary>
    /// Return all keys between min and max (inclusive), in ascending order.
    /// Returns an empty list if no keys match, including when min is greater than max.
    /// </summary>
    /// <param name="min"> The minimum key</param>
    /// <param name="max">The maximum key</param>
    /// <returns></returns>
    public List<int> RangeSearch(int min, int max)
    {
        var result = new List<int>();
        if (min > max)
        {
            return result;
        }
        RangeSearchRecursive(Root, min, max, result);
        return result;
    }

    private void RangeSearchRecursive(BinarySearchTreeNode<T>? node, int min, int max, List<int> result)
    {
        if (node == null)
        {
            return;
        }
        if (node.Key > min)
        {
            RangeSearchRecursive(node.Left, min, max, result);
        }
        if (node.Key >= min && node.Key <= max)
        {
            result.Add(node.Key);
        }
        if (node.Key < max)
        {
            RangeSearchRecursive(node.Right, min, max, result);
        }
    }
    
}


