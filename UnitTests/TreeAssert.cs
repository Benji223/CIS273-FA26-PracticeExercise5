using PracticeExercise5;

namespace UnitTests;

/// <summary>
/// Shared checks for the whole tree. They run before shape checks so that a
/// failure points at the actual problem (a broken BST, a wrong Count, or a
/// broken traversal) instead of only reporting "wrong shape".
/// </summary>
internal static class TreeAssert
{
    /// <summary>
    /// Check that the keys are in BST order and that Count and IsEmpty agree
    /// with the number of keys in the tree.
    /// </summary>
    public static void IsValid<T>(BinarySearchTree<T> tree)
    {
        List<int> keys = tree.InOrderKeys;

        for (int i = 1; i < keys.Count; i++)
        {
            Assert.IsTrue(keys[i - 1] < keys[i],
                $"InOrderKeys is not strictly increasing at index {i} ({keys[i - 1]}, then {keys[i]}). " +
                "Either the tree is not a valid BST (check Add/Remove) or InOrderKeys is wrong.");
        }

        Assert.AreEqual(keys.Count, tree.Count,
            "Count does not match the number of keys in the tree. Check where Add, Remove and Clear update Count.");

        Assert.AreEqual(keys.Count == 0, tree.IsEmpty,
            "IsEmpty does not agree with the number of keys in the tree.");
    }

    /// <summary>
    /// Check that the tree is valid and has exactly the shape described by its
    /// pre-order traversal. (A BST's pre-order traversal determines its shape.)
    /// </summary>
    public static void HasShape<T>(BinarySearchTree<T> tree, List<int> expectedPreOrder)
    {
        IsValid(tree);

        CollectionAssert.AreEqual(expectedPreOrder, tree.PreOrderKeys,
            "PreOrderKeys does not match the expected tree shape. " +
            "If the PreOrderKeys test also fails, fix PreOrderKeys first.");
    }
}
