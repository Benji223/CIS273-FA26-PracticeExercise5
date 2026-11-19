namespace PracticeExercise5;

public class BinarySearchTree<T> : IBinarySearchTree<T>
{
    private BinarySearchTreeNode<T>? Root { get; set; }

    public bool IsEmpty => Root == null;

    public int Count {get; private set; }

    public int Height {
       get
        {
          throw new NotImplementedException();  
        } 
    }

    private int HeightRecursive( BinarySearchTreeNode<T>? node) {
        throw new NotImplementedException(); 
    }

    public void Add(int key, T value)
    {
        throw new NotImplementedException(); 
    }

    private void AddRecursive(int key, T value, BinarySearchTreeNode<T> parent) 
    {
        throw new NotImplementedException(); 
    }

    public void Clear()
    {
        throw new NotImplementedException(); 
    }

    public void Remove(int key)
    {

        throw new NotImplementedException();
    }

    public T Search(int key)
    {
        throw new NotImplementedException();
    }

    public void Update(int key, T value)
    {
        Add(key,value);
    }

    private BinarySearchTreeNode<T>? GetNode(int key)
    {
        throw new NotImplementedException(); 
    }

    private BinarySearchTreeNode<T>? GetNodeRecursive(int key, BinarySearchTreeNode<T>? node) 
    {
        throw new NotImplementedException(); 
    }

    public bool Contains(int key)
    {
        return GetNode(key) != null;
    }

     public List<int> InOrderKeys {
        get {
            throw new NotImplementedException(); 
        }
    }

    private void InOrderKeyRecursive(BinarySearchTreeNode<T>? node, List<int> keys)
    {
        throw new NotImplementedException(); 

    }

    public List<int> PreOrderKeys => throw new NotImplementedException();

    public List<int> PostOrderKeys => throw new NotImplementedException();

    

    /* ADVANCED METHODS and PROPERTIES */ 

    public int? MinKey => throw new NotImplementedException();

    public int? MaxKey => throw new NotImplementedException();

    public Tuple<int, T>? Min => throw new NotImplementedException(); 

    public Tuple<int, T>? Max => throw new NotImplementedException();

    public double? MedianKey => throw new NotImplementedException();
    

    private BinarySearchTreeNode<T> MaxNode(BinarySearchTreeNode<T> node)
    {
        throw new NotImplementedException();
    }

    private BinarySearchTreeNode<T> MinNode(BinarySearchTreeNode<T> node)
    {
        throw new NotImplementedException();
    }

    public int? NextKey(int key)
    {
        throw new NotImplementedException();
    }

    public int? PrevKey(int key)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    
}


