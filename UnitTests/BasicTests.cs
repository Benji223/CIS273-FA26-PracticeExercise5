using PracticeExercise5;

namespace UnitTests;

[TestClass]
public class BasicTests
{
    [TestMethod]
    public void Add()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(3, 30);
        Assert.AreEqual(1, tree.Count);
        Assert.IsTrue(tree.Contains(3));
        Assert.AreEqual(30, tree.Search(3));

        BinarySearchTree<string> tree2 = new BinarySearchTree<string>();
        tree2.Add(3, "leaf");
        Assert.AreEqual(1, tree2.Count);
        Assert.IsTrue(tree2.Contains(3));
        Assert.AreEqual("leaf", tree2.Search(3));

        BinarySearchTree<char> tree3 = new BinarySearchTree<char>();
        tree3.Add(3, 'c');
        Assert.AreEqual(1, tree3.Count);
        Assert.IsTrue(tree3.Contains(3));
        Assert.AreEqual('c', tree3.Search(3));
    }


    [TestMethod]
    public void AddSame()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(2, 30);
        tree.Add(3, 30);
        tree.Add(3, 20);
        Assert.AreEqual(1, tree.Height);
        Assert.AreEqual(2, tree.Count);
        Assert.AreEqual(20, tree.Search(3));
    }


    [TestMethod]
    public void AddInt()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            // value differs from the key, so mixing them up is caught
            int value = i * 7 + 3;
            tree.Add(i, value);
            Assert.AreEqual(value, tree.Search(i));
        }
        Assert.AreEqual(50, tree.Count);
    }

    [TestMethod]
    public void HeightZero()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(0, 0);
        Assert.AreEqual(0, tree.Height);

        tree = new BinarySearchTree<int>();
        Assert.AreEqual(0, tree.Height);
    }

    [TestMethod]
    public void HeightInt()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            tree.Add(i, i + 1);
        }
        Assert.AreEqual(49, tree.Height);
    }

    [TestMethod]
    public void HeightIntDescending()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 49; i >= 0; i--)
        {
            tree.Add(i, i + 1);
        }
        // Adding keys in decreasing order builds a chain of left children.
        Assert.AreEqual(49, tree.Height);
        TreeAssert.HasShape(tree, Enumerable.Range(0, 50).Reverse().ToList());
    }

    [TestMethod]
    public void HeightInt2()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        // longest path: 8 -> 10 -> 22 -> 26 -> 23
        Assert.AreEqual(4, tree.Height);
    }

    [TestMethod]
    public void AddString()
    {
        BinarySearchTree<string> tree = new BinarySearchTree<string>();
        for (int i = 0; i < 50; i++)
        {
            string value = $"value {i}";
            tree.Add(i, value);
            Assert.AreEqual(value, tree.Search(i));
        }
        Assert.AreEqual(50, tree.Count);
    }

    [TestMethod]
    public void AddChar()
    {
        BinarySearchTree<char> tree = new BinarySearchTree<char>();
        char random = ' ';
        for (int i = 0; i < 50; i++)
        {
            random = (char)i;
            tree.Add(i, random);
            Assert.AreEqual(random, tree.Search(i));
        }
    }

    [TestMethod]
    public void ShapeInt()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            tree.Add(i, i + 1);
        }
        // Adding keys in increasing order builds a chain of right children,
        // so the pre-order traversal visits the keys in the same order.
        TreeAssert.HasShape(tree, Enumerable.Range(0, 50).ToList());
        for (int i = 0; i < 50; i++)
        {
            Assert.AreEqual(i + 1, tree.Search(i));
        }
    }

    [TestMethod]
    public void ShapeInt2()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 5, 7, 10, 22, 26, 23 });
    }

    [TestMethod]
    public void RemoveMissingKey()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 17; i++)
        {
            tree.Add(i, i + 1);
        }
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.Remove(20));
        // a failed Remove must not change the tree
        Assert.AreEqual(17, tree.Count);
        TreeAssert.IsValid(tree);
    }

    [TestMethod]
    public void RemoveFromEmptyTree()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.Remove(5));
        Assert.IsTrue(tree.IsEmpty);
        Assert.AreEqual(0, tree.Count);
    }

    [TestMethod]
    public void RemoveIntWHeightAndShape()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            tree.Add(i, i + 1);
        }
        tree.Remove(20);
        Assert.AreEqual(48, tree.Height);
        Assert.AreEqual(49, tree.Count);
        Assert.IsFalse(tree.Contains(20));
        TreeAssert.HasShape(tree, Enumerable.Range(0, 50).Where(k => k != 20).ToList());
    }

    [TestMethod]
    public void RemoveIntWHeightAndShape2()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            tree.Add(i, i + 1);
        }
        tree.Remove(20);
        tree.Remove(4);
        tree.Remove(40);
        Assert.AreEqual(46, tree.Height);
        Assert.AreEqual(47, tree.Count);
        TreeAssert.HasShape(tree, Enumerable.Range(0, 50).Where(k => k != 20 && k != 4 && k != 40).ToList());
    }

    [TestMethod]
    public void RemoveFromDescendingChain()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 49; i >= 0; i--)
        {
            tree.Add(i, i + 1);
        }
        // 20 has one child (19, on the left), so 19 takes its place under 21.
        tree.Remove(20);
        Assert.AreEqual(48, tree.Height);
        Assert.IsFalse(tree.Contains(20));
        Assert.AreEqual(20, tree.Search(19));
        TreeAssert.HasShape(tree, Enumerable.Range(0, 50).Reverse().Where(k => k != 20).ToList());

        // 0 is the leaf at the bottom of the chain.
        tree.Remove(0);
        Assert.AreEqual(47, tree.Height);
        TreeAssert.HasShape(tree, Enumerable.Range(1, 49).Reverse().Where(k => k != 20).ToList());
    }

    [TestMethod]
    public void RemoveIntWHeightAndShape3()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        // 10 has one child (22), so 22 takes its place under 8.
        tree.Remove(10);
        Assert.AreEqual(3, tree.Height);
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 5, 7, 22, 26, 23 });
    }

    [TestMethod]
    public void RemoveLeaf()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        // Leaves: 23 (left child of 26), 5 (left child of 6), 7 (right child of 6).
        tree.Remove(23);
        Assert.AreEqual(3, tree.Height);
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 5, 7, 10, 22, 26 });

        tree.Remove(5);
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 7, 10, 22, 26 });

        tree.Remove(7);
        Assert.AreEqual(3, tree.Height);
        Assert.IsFalse(tree.Contains(7));
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 10, 22, 26 });
    }

    [TestMethod]
    public void RemoveIntW2Children()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        tree.Add(9, 10);   // 9 becomes the left child of 10
        // 10 has two children, so it is replaced by its in-order successor, 22.
        tree.Remove(10);
        Assert.AreEqual(8, tree.Count);
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 5, 7, 22, 9, 26, 23 });
        Assert.AreEqual(23, tree.Search(22));
    }

    [TestMethod]
    public void RemoveIntW2Children2()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        tree.Add(9, 10);   // 9 becomes the left child of 10
        // 6 has two children, so it is replaced by its in-order successor, 7.
        tree.Remove(6);
        Assert.AreEqual(8, tree.Count);
        TreeAssert.HasShape(tree, new List<int> { 8, 7, 5, 10, 9, 22, 26, 23 });
        Assert.AreEqual(8, tree.Search(7));
    }

    [TestMethod]
    public void RemoveIntW2ChildrenDeepSuccessor()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(10, 11);
        tree.Add(5, 6);
        tree.Add(30, 31);
        tree.Add(20, 21);
        tree.Add(50, 51);
        tree.Add(40, 41);
        tree.Add(45, 46);
        //       10
        //      /  \
        //     5    30
        //         /  \
        //       20    50
        //            /
        //          40
        //            \
        //             45
        // 30's successor (40) is not its right child: it is the leftmost node of
        // 30's right subtree, and it has a right child (45) that must be kept.
        tree.Remove(30);
        Assert.AreEqual(6, tree.Count);
        TreeAssert.HasShape(tree, new List<int> { 10, 5, 40, 20, 50, 45 });
        Assert.AreEqual(41, tree.Search(40));
        Assert.AreEqual(46, tree.Search(45));
    }

    [TestMethod]
    public void RemoveRootDeepSuccessor()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(50, 51);
        tree.Add(30, 31);
        tree.Add(70, 71);
        tree.Add(60, 61);
        tree.Add(80, 81);
        tree.Add(55, 56);
        tree.Add(57, 58);
        //       50
        //      /  \
        //    30    70
        //         /  \
        //       60    80
        //      /
        //    55
        //      \
        //       57
        // The root's successor (55) is deep in the right subtree and has a right child (57).
        tree.Remove(50);
        Assert.AreEqual(6, tree.Count);
        TreeAssert.HasShape(tree, new List<int> { 55, 30, 70, 60, 57, 80 });
        Assert.AreEqual(56, tree.Search(55));
        Assert.AreEqual(58, tree.Search(57));
    }


    [TestMethod]
    public void RemoveRoot1()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        tree.Remove(8);
        Assert.AreEqual(3, tree.Height);
        Assert.IsFalse(tree.Contains(8));
        TreeAssert.HasShape(tree, new List<int> { 10, 6, 5, 7, 22, 26, 23 });
    }

    [TestMethod]
    public void RemoveRoot2()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        tree.Remove(8);
        tree.Remove(10);
        Assert.AreEqual(2, tree.Height);
        Assert.IsFalse(tree.Contains(10));
        TreeAssert.HasShape(tree, new List<int> { 22, 6, 5, 7, 26, 23 });
    }

    [TestMethod]
    public void RemoveRoot3()
    {
        BinarySearchTree<int> tree = SampleTree.Create();

        tree.Remove(8);
        tree.Remove(10);
        tree.Remove(6);
        tree.Remove(5);
        tree.Remove(23);
        tree.Remove(7);
        tree.Remove(22);
        Assert.AreEqual(0, tree.Height);
        Assert.AreEqual(1, tree.Count);
        Assert.IsFalse(tree.Contains(8));
        Assert.IsTrue(tree.Contains(26));
        TreeAssert.IsValid(tree);
        tree.Remove(26);
        Assert.IsTrue(tree.IsEmpty);
        Assert.AreEqual(0, tree.Count);
    }

    [TestMethod]
    public void RemoveRoot4()
    {
        // Root with only a left subtree
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(6, 7);
        tree.Add(5, 6);
        tree.Add(7, 8);
        tree.Remove(8);
        Assert.AreEqual(1, tree.Height);
        Assert.AreEqual(3, tree.Count);
        TreeAssert.HasShape(tree, new List<int> { 6, 5, 7 });
    }

    [TestMethod]
    public void RemoveThenAddAgain()
    {
        BinarySearchTree<int> tree = SampleTree.Create();

        // 10 has one child (22), so 22 takes its place under 8.
        tree.Remove(10);
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 5, 7, 22, 26, 23 });

        // 10 comes back as a new leaf (the left child of 22), with its new value.
        tree.Add(10, 99);
        Assert.AreEqual(8, tree.Count);
        Assert.AreEqual(99, tree.Search(10));
        TreeAssert.HasShape(tree, new List<int> { 8, 6, 5, 7, 22, 10, 26, 23 });
    }

    [TestMethod]
    public void RemoveRootThenAddAgain()
    {
        BinarySearchTree<int> tree = SampleTree.Create();

        // The root (8) has two children, so its successor (10) becomes the root.
        tree.Remove(8);
        TreeAssert.HasShape(tree, new List<int> { 10, 6, 5, 7, 22, 26, 23 });

        // 8 comes back as a new leaf: the right child of 7.
        tree.Add(8, 100);
        Assert.AreEqual(8, tree.Count);
        Assert.AreEqual(100, tree.Search(8));
        TreeAssert.HasShape(tree, new List<int> { 10, 6, 5, 7, 8, 22, 26, 23 });
    }

    [TestMethod]
    public void RemoveLastKeyThenAddAgain()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(5, 6);
        tree.Remove(5);
        Assert.IsTrue(tree.IsEmpty);
        Assert.AreEqual(0, tree.Count);

        // The emptied tree must work like a new one.
        tree.Add(5, 50);
        tree.Add(3, 30);
        Assert.AreEqual(2, tree.Count);
        Assert.AreEqual(1, tree.Height);
        Assert.AreEqual(50, tree.Search(5));
        TreeAssert.HasShape(tree, new List<int> { 5, 3 });
    }

    [TestMethod]
    public void Clear()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Clear();
        Assert.IsTrue(tree.IsEmpty);
        Assert.AreEqual(0, tree.Count);
        Assert.AreEqual(0, tree.Height);
        Assert.IsFalse(tree.Contains(8));
        Assert.AreEqual(0, tree.InOrderKeys.Count);

        // The tree must still work after being cleared.
        tree.Add(4, 5);
        Assert.AreEqual(1, tree.Count);
        Assert.AreEqual(5, tree.Search(4));
        CollectionAssert.AreEqual(new List<int> { 4 }, tree.InOrderKeys);
    }

    [TestMethod]
    public void Update()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Add(5, 6);
        tree.Add(22, 23);
        tree.Add(26, 27);
        tree.Update(22, 30);
        tree.Add(7, 8);
        tree.Add(23, 24);
        tree.Update(8, 1);
        Assert.AreEqual(30, tree.Search(22));
        Assert.AreEqual(1, tree.Search(8));
    }

    [TestMethod]
    public void UpdateMissingKeyInserts()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Add(5, 6);
        tree.Add(26, 27);
        Assert.AreEqual(4, tree.Count);

        // 22 is not in the tree, so Update inserts it
        tree.Update(22, 30);
        Assert.AreEqual(5, tree.Count);
        Assert.AreEqual(30, tree.Search(22));

        tree.Add(7, 8);
        tree.Add(23, 24);
        tree.Update(8, 1);
        Assert.AreEqual(8, tree.Count);
        Assert.AreEqual(1, tree.Search(8));
        TreeAssert.IsValid(tree);
    }

    [TestMethod]
    public void PostOrderKeys()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        CollectionAssert.AreEqual(new List<int> { 5, 7, 6, 23, 26, 22, 10, 8 }, tree.PostOrderKeys);
    }

    [TestMethod]
    public void PreOrderKeys()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        CollectionAssert.AreEqual(new List<int> { 8, 6, 5, 7, 10, 22, 26, 23 }, tree.PreOrderKeys);
    }

    [TestMethod]
    public void InOrderKeys()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        CollectionAssert.AreEqual(new List<int> { 5, 6, 7, 8, 10, 22, 23, 26 }, tree.InOrderKeys);
    }

    [TestMethod]
    public void TraversalsEmpty()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        Assert.AreEqual(0, tree.InOrderKeys.Count);
        Assert.AreEqual(0, tree.PreOrderKeys.Count);
        Assert.AreEqual(0, tree.PostOrderKeys.Count);
    }



    [TestMethod]
    public void Contains()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Add(5, 6);
        Assert.AreEqual(true, tree.Contains(5));
        Assert.AreEqual(false, tree.Contains(22));
    }



    [TestMethod]
    public void IsEmpty()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        Assert.AreEqual(true, tree.IsEmpty);
        tree.Add(8, 9);
        Assert.AreEqual(false, tree.IsEmpty);
    }

    [TestMethod]
    public void Count()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Add(5, 6);
        Assert.AreEqual(4, tree.Count);
    }

    [TestMethod]
    public void SearchMissingKey()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Add(5, 6);
        Assert.AreEqual(6, tree.Search(5));
        Assert.AreEqual(9, tree.Search(8));
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.Search(22));
    }

}

