using PracticeExercise5;

namespace UnitTests;

/// <summary>
/// The tree that many tests start from.
/// </summary>
internal static class SampleTree
{
    /// <summary>
    /// Build this tree by adding 8, 10, 6, 5, 22, 26, 7, 23 in that order.
    /// Each key's value is key + 1.
    /// <code>
    ///          8
    ///        /   \
    ///       6     10
    ///      / \      \
    ///     5   7      22
    ///                  \
    ///                   26
    ///                  /
    ///                23
    /// </code>
    /// Pre-order: 8, 6, 5, 7, 10, 22, 26, 23. Height: 4.
    /// </summary>
    public static BinarySearchTree<int> Create()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        foreach (int key in new[] { 8, 10, 6, 5, 22, 26, 7, 23 })
        {
            tree.Add(key, key + 1);
        }
        return tree;
    }
}
