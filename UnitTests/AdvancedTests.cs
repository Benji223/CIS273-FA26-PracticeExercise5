using PracticeExercise5;

namespace UnitTests;

[TestClass]
public class AdvancedTests
{

    [TestMethod]
    public void Search()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(8, 9);
        tree.Add(10, 11);
        tree.Add(6, 7);
        tree.Add(5, 6);
        Assert.AreEqual(6, tree.Search(5));
        Assert.AreEqual(9, tree.Search(8));
    }

    [TestMethod]
    public void MinKey1()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(10, 10);
        tree.Add(5, 5);
        tree.Add(20, 10);
        tree.Add(15, 10);
        tree.Add(25, 10);
        tree.Add(1, 10);
        tree.Add(12, 10);

        Assert.AreEqual(1, tree.MinKey);
    }

    [TestMethod]
    public void MinKey2()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(10, 10);
        tree.Add(5, 5);
        tree.Add(20, 10);
        tree.Add(15, 10);
        tree.Add(25, 10);
        tree.Add(12, 10);

        Assert.AreEqual(5, tree.MinKey);
    }

    [TestMethod]
    public void MinKey3()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(10, 10);
        tree.Add(20, 10);
        tree.Add(15, 10);
        tree.Add(25, 10);
        tree.Add(12, 10);

        Assert.AreEqual(10, tree.MinKey);
    }
    [TestMethod]
    public void MedianKey1()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 51; i++)
        {
            tree.Add(i, i + 1);
        }
        Assert.AreEqual(25.0, tree.MedianKey);
    }

    [TestMethod]
    public void MedianKey2()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 52; i++)
        {
            tree.Add(i, i + 1);
        }
        Assert.AreEqual(25.5, tree.MedianKey);
    }

    [TestMethod]
    public void MedianKey3()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(1, 3);
        Assert.AreEqual(1.0, tree.MedianKey);
    }

    [TestMethod]
    public void MedianKey4()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(1, 3);
        tree.Add(2, 4);
        Assert.AreEqual(1.5, tree.MedianKey);
    }

    [TestMethod]
    public void MedianKey5()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(1, 3);
        tree.Add(2, 3);
        tree.Add(3, 3);
        tree.Add(10, 3);
        tree.Add(30, 3);
        Assert.AreEqual(3.0, tree.MedianKey);
    }

    [TestMethod]
    public void MedianKey6()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(10, 3);
        tree.Add(30, 3);
        tree.Add(35, 3);
        tree.Add(1, 3);
        tree.Add(2, 3);
        tree.Add(3, 3);
        Assert.AreEqual(6.5, tree.MedianKey);
    }

    [TestMethod]
    public void MedianKeyEmpty()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        Assert.IsNull(tree.MedianKey);

        // A tree emptied by Remove has no median either.
        tree.Add(4, 5);
        tree.Remove(4);
        Assert.IsNull(tree.MedianKey);
    }

    [TestMethod]
    public void MaxKey()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 52; i++)
        {
            tree.Add(i, i + 1);
        }
        Assert.AreEqual(51, tree.MaxKey);
    }

    [TestMethod]
    public void MaxKey2()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 3);
        tree.Add(2, 4);
        tree.Add(5, 7);
        tree.Add(7, 2);
        tree.Add(6, 4);
        tree.Add(3, 4);
        Assert.AreEqual(7, tree.MaxKey);
    }

    [TestMethod]
    public void Next1()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }

        for (int i = 0; i < 49; i++)
        {
            Assert.AreEqual(i + 1, tree.NextKey(i));
        }
    }

    [TestMethod]
    public void Next2()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(5, 5);
        tree.Add(4, 5);
        Assert.AreEqual(5, tree.NextKey(4));
    }

    [TestMethod]
    public void Next3()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 5);
        tree.Add(5, 5);
        Assert.AreEqual(5, tree.NextKey(4));
    }

    [TestMethod]
    public void Next4Edge()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 5);
        tree.Add(5, 5);
        Assert.IsNull(tree.NextKey(5));
    }

    [TestMethod]
    public void Next5MissingKey()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 5);
        tree.Add(9, 5);
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.NextKey(6));
    }

    [TestMethod]
    public void Prev1()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }

        for (int i = 1; i < 50; i++)
        {
            Assert.AreEqual(i - 1, tree.PrevKey(i));
        }
    }

    [TestMethod]
    public void Prev2()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 5);
        tree.Add(5, 5);
        Assert.AreEqual(4, tree.PrevKey(5));
    }

    [TestMethod]
    public void Prev3()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(5, 5);
        tree.Add(4, 5);
        Assert.AreEqual(4, tree.PrevKey(5));
    }

    [TestMethod]
    public void Prev4Edge()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 5);
        tree.Add(5, 5);
        Assert.IsNull(tree.PrevKey(4));
    }

    [TestMethod]
    public void Prev5MissingKey()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 5);
        tree.Add(9, 5);
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.PrevKey(6));
    }

    [TestMethod]
    public void NextPrevDescendingChain()
    {
        // A chain of left children: every NextKey must walk up a Parent link,
        // and every PrevKey comes from a left subtree.
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 49; i >= 0; i--)
        {
            tree.Add(i, i);
        }
        for (int i = 0; i < 49; i++)
        {
            Assert.AreEqual(i + 1, tree.NextKey(i));
            Assert.AreEqual(i, tree.PrevKey(i + 1));
        }
        Assert.IsNull(tree.NextKey(49));
        Assert.IsNull(tree.PrevKey(0));
    }

    [TestMethod]
    public void NextPrevAscendingChain()
    {
        // A chain of right children: every PrevKey must walk up a Parent link,
        // and every NextKey comes from a right subtree.
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            tree.Add(i, i);
        }
        for (int i = 0; i < 49; i++)
        {
            Assert.AreEqual(i + 1, tree.NextKey(i));
            Assert.AreEqual(i, tree.PrevKey(i + 1));
        }
        Assert.IsNull(tree.NextKey(49));
        Assert.IsNull(tree.PrevKey(0));
    }

    [TestMethod]
    public void Range1()
    {
        int min = 3;
        int max = 40;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range2()
    {
        int min = 0;
        int max = 49;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range3()
    {
        int min = 48;
        int max = 49;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range4()
    {
        int min = 49;
        int max = 49;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range5()
    {
        int min = 0;
        int max = 0;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range6()
    {
        int min = 23;
        int max = 33;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range7()
    {
        int min = 7;
        int max = 37;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = (i + 13) % 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        CollectionAssert.AreEqual(Enumerable.Range(min, max - min + 1).ToList(), range);
    }

    [TestMethod]
    public void Range8Edge()
    {
        int min = -1;
        int max = 37;
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        int index = 0;
        for (int i = 0; i < 50; i++)
        {
            index = i + 50;
            tree.Add(index, index);
        }
        List<int> range = tree.RangeSearch(min, max);
        Assert.AreEqual(0, range.Count);
    }

    [TestMethod]
    public void RangeMinGreaterThanMax()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        for (int i = 0; i < 50; i++)
        {
            int index = (i + 13) % 50;
            tree.Add(index, index);
        }
        // No key can be >= 30 and <= 10 at the same time.
        List<int> range = tree.RangeSearch(30, 10);
        Assert.IsNotNull(range);
        Assert.AreEqual(0, range.Count);
    }

    [TestMethod]
    public void Range9Gaps()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        foreach (int key in new[] { 10, 4, 16, 2, 8, 12, 20, 6 })
        {
            tree.Add(key, key);
        }
        CollectionAssert.AreEqual(new List<int> { 4, 6, 8 }, tree.RangeSearch(3, 9));
    }

    [TestMethod]
    public void NextAndPrevAfterRemove()
    {
        // Removing nodes must leave the links (including Parent) consistent,
        // so walking the tree with NextKey/PrevKey still visits every key.
        BinarySearchTree<int> tree = SampleTree.Create();
        tree.Add(9, 10);   // 9 becomes the left child of 10
        tree.Remove(10);
        tree.Remove(6);
        tree.Remove(8);

        List<int> expected = new List<int> { 5, 7, 9, 22, 23, 26 };
        for (int i = 0; i < expected.Count - 1; i++)
        {
            Assert.AreEqual(expected[i + 1], tree.NextKey(expected[i]));
            Assert.AreEqual(expected[i], tree.PrevKey(expected[i + 1]));
        }
        Assert.IsNull(tree.NextKey(26));
        Assert.IsNull(tree.PrevKey(5));
    }

    [TestMethod]
    public void MinMaxTuple()
    {
        BinarySearchTree<string> tree = new BinarySearchTree<string>();
        tree.Add(8, "eight");
        tree.Add(10, "ten");
        tree.Add(6, "six");
        tree.Add(5, "five");
        tree.Add(22, "twenty-two");
        tree.Add(26, "twenty-six");
        tree.Add(7, "seven");
        tree.Add(23, "twenty-three");
        Assert.AreEqual(new Tuple<int, string>(5, "five"), tree.Min);
        Assert.AreEqual(new Tuple<int, string>(26, "twenty-six"), tree.Max);
    }

    [TestMethod]
    public void MinMaxTupleSingleNode()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        tree.Add(4, 40);
        Assert.AreEqual(new Tuple<int, int>(4, 40), tree.Min);
        Assert.AreEqual(new Tuple<int, int>(4, 40), tree.Max);
    }

    [TestMethod]
    public void MinMaxAfterRemove()
    {
        BinarySearchTree<int> tree = SampleTree.Create();
        tree.Remove(5);
        tree.Remove(26);
        Assert.AreEqual(6, tree.MinKey);
        Assert.AreEqual(23, tree.MaxKey);
        Assert.AreEqual(new Tuple<int, int>(6, 7), tree.Min);
        Assert.AreEqual(new Tuple<int, int>(23, 24), tree.Max);
    }

    [TestMethod]
    public void MinMaxEmpty()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        Assert.IsNull(tree.MinKey);
        Assert.IsNull(tree.MaxKey);
        Assert.IsNull(tree.Min);
        Assert.IsNull(tree.Max);
    }

    [TestMethod]
    public void RangeSearchEmpty()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        List<int> range = tree.RangeSearch(0, 100);
        Assert.IsNotNull(range);
        Assert.AreEqual(0, range.Count);
    }

    [TestMethod]
    public void NextPrevEmpty()
    {
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.NextKey(5));
        Assert.ThrowsExactly<KeyNotFoundException>(() => tree.PrevKey(5));
    }

    [TestMethod]
    public void NextAndPrevAfterDeepSuccessorRemove()
    {
        // Same tree as BasicTests.RemoveRootDeepSuccessor. After the removal,
        // 57's Parent must be 60, which only happens if Remove relinks it correctly.
        BinarySearchTree<int> tree = new BinarySearchTree<int>();
        foreach (int key in new[] { 50, 30, 70, 60, 80, 55, 57 })
        {
            tree.Add(key, key);
        }
        tree.Remove(50);

        List<int> expected = new List<int> { 30, 55, 57, 60, 70, 80 };
        for (int i = 0; i < expected.Count - 1; i++)
        {
            Assert.AreEqual(expected[i + 1], tree.NextKey(expected[i]));
            Assert.AreEqual(expected[i], tree.PrevKey(expected[i + 1]));
        }
        Assert.IsNull(tree.NextKey(80));
        Assert.IsNull(tree.PrevKey(30));
    }
}
