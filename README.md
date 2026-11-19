# Practice Exercise 5: Binary Search Tree

In this exercise you will implement a generic **binary search tree** in C#. Each node in the tree stores an `int` key and a value of type `T`.

The unit tests in the `UnitTests` project are the specification. You are done when every test passes.

## Getting started

You need the .NET 9 SDK.

```bash
dotnet build                                            # compile everything
dotnet test                                             # run all tests
dotnet test --filter "FullyQualifiedName~BasicTests"    # run only the basic tests
dotnet test --filter "Name=RemoveRoot1"                 # run a single test
```

You can also use the Test Explorer in VS Code or Visual Studio.

## Project layout

| File | What it is | Edit it? |
|---|---|---|
| `PracticeExercise5/IBinarySearchTree.cs` | The interface: the public operations every BST must support | No |
| `PracticeExercise5/BinarySearchTreeNode.cs` | A node with `Key`, `Value`, `Left`, `Right` and `Parent` | No |
| `PracticeExercise5/BinarySearchTree.cs` | Your implementation | **Yes** |
| `PracticeExercise5/Program.cs` | A console app you can use for experiments | Optional |
| `UnitTests/BasicTests.cs` | Tests for the core operations | No |
| `UnitTests/AdvancedTests.cs` | Tests for the advanced operations | No |

### Nodes are private to the tree

Nothing in the public interface accepts or returns a node. Code that uses the tree only ever sees keys and values. The node class is `internal`, and the methods in `BinarySearchTree.cs` that work with nodes (`GetNode`, `MinNode`, `MaxNode`) are `private` helpers for your own use. Libraries such as .NET's `SortedDictionary` and Java's `TreeMap` work the same way.

You may add more private helper methods. Many of these operations are easiest to write recursively: the public method handles the empty-tree case and then calls a private recursive helper on the root.

## What to implement

### Private helpers

Write these first. Most of the public members are built on them.

| Member | Requirement |
|---|---|
| `GetNode(key)` | Return the node with that key, or `null` if there isn't one. |
| `MinNode(node)`, `MaxNode(node)` | Return the node with the smallest or largest key in the subtree rooted at `node`. |

### Basic operations (`BasicTests`)

| Member | Requirement |
|---|---|
| `Add(key, value)` | Insert a new node. If the key already exists, replace its value; this does not add a node, so `Count` stays the same. |
| `Contains(key)` | Return `true` if the key is in the tree. |
| `Search(key)` | Return the value stored with the key. If the key isn't in the tree, throw `KeyNotFoundException`. |
| `Update(key, value)` | Set the value for the key. If the key isn't in the tree, insert it. |
| `Remove(key)` | Delete the node with that key. If the key isn't in the tree, throw `KeyNotFoundException`. |
| `Clear()` | Remove every node. |
| `IsEmpty`, `Count` | Report whether the tree is empty and how many nodes it holds. |
| `Height` | The number of **edges** on the longest path from the root to a leaf. An empty tree and a tree with one node both have height `0`. |
| `InOrderKeys`, `PreOrderKeys`, `PostOrderKeys` | Return the keys as a `List<int>` in that traversal order. |

### Advanced operations (`AdvancedTests`)

| Member | Requirement |
|---|---|
| `MinKey`, `MaxKey` | Return the smallest or largest key, or `null` if the tree is empty. |
| `Min`, `Max` | Return the smallest or largest `(key, value)` pair as a `Tuple<int, T>`, or `null` if the tree is empty. |
| `MedianKey` | Return the median of all the keys. If there is an even number of keys, average the two middle keys (for example, keys 0 through 51 give `25.5`). If the tree is empty, return `null`, just like `MinKey` and `MaxKey`. |
| `NextKey(key)`, `PrevKey(key)` | Return the next larger or next smaller key in the tree (the in-order successor or predecessor of `key`). Return `null` if `key` is the largest or smallest key. If `key` isn't in the tree, throw `KeyNotFoundException`. |
| `RangeSearch(min, max)` | Return every key between `min` and `max`, including both ends, in **ascending order**. If no keys match, return an **empty list**, not `null`. If `min` is greater than `max`, no key can be in the range, so return an empty list too. Don't swap the arguments or throw an exception. |

## How the tests check your tree's shape

Many tests compare `PreOrderKeys` with an exact list. The pre-order traversal of a BST determines its shape, so this one check confirms that every node ended up in the right place. If `PreOrderKeys` is wrong, you'll fail many of the `Add` and `Remove` tests too, so make sure it works early.

These checks live in `UnitTests/TreeAssert.cs`. Before comparing shapes, it confirms that `InOrderKeys` is sorted and that `Count` matches the number of keys. Read the failure message: it tells you whether the problem is the tree's order, `Count`, or the traversal.

## Things to watch out for

- **Removal has three cases:** removing a leaf, removing a node with one child, and removing a node with two children. Each case needs separate handling, and removing the root is a special version of each one. The `RemoveRoot*` and `RemoveIntW2Children*` tests cover them.
- **When removing a node with two children, replace it with its in-order successor**, the node with the smallest key in its right subtree. Using the predecessor instead also produces a valid BST, but the tree has a different shape, so the tests will fail.
- **Keep `Parent` pointers correct.** No test reads `Parent` directly. But the standard successor and predecessor algorithms walk up `Parent` links, and `NextAndPrevAfterRemove` checks `NextKey`/`PrevKey` after several removals. Whenever `Add` or `Remove` moves a node, update its `Parent` as well as the child links.
- **Keep `Count` accurate** after `Add` (but not when a duplicate key replaces a value), after `Remove` and after `Clear`.
- **Some tests build long degenerate trees** by adding the keys 0 through 49 in order, which produces a tree of height 49. Your recursion should handle that.

## Suggested order

1. `GetNode`, then `Contains`, `Search`, `Add`, `Count`, `IsEmpty`, `Clear`
2. `Height` and the three traversals, especially `PreOrderKeys`
3. `MinNode`/`MaxNode`, then `MinKey`/`MaxKey` and `Min`/`Max`
4. `Remove`. Do it after `MinNode`, because finding the successor uses `MinNode`.
5. `MedianKey`, `NextKey`/`PrevKey` and `RangeSearch`
