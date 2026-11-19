# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Context

CIS273 (Data Structures) practice exercise: implement a generic binary search tree in C#. The `UnitTests` project is the spec. Many members of `BinarySearchTree<T>` are still stubs (`throw new NotImplementedException()`), so failing tests are expected until those members are implemented. The `in-class-work` branch holds code written during lecture; `main` is the starter version.

## Commands

Both projects target .NET 9 (`net9.0`). Tests use MSTest 4.x: use `Assert.ThrowsExactly<T>` (`Assert.ThrowsException` no longer exists in MSTest 4).

```bash
dotnet build                                              # build solution
dotnet test                                               # run all MSTest tests
dotnet test --filter "FullyQualifiedName~BasicTests"      # one test class
dotnet test --filter "Name=RemoveRoot1"                   # single test
dotnet run --project PracticeExercise5                    # console stub (Hello World only)
```

## Architecture

- `PracticeExercise5/IBinarySearchTree.cs`: the interface contract. Its members are grouped into basic operations, "Advanced" members (Min/Max, Median, NextKey/PrevKey, RangeSearch), and traversals. **The public API is key/value-only.** No public member takes or returns a node, the same design as `SortedDictionary`/`TreeMap`. Keep it that way.
- `PracticeExercise5/BinarySearchTree.cs`: the implementation. Keys are always `int`, and `T` is the value type. Most operations use a public or property entry point that checks `IsEmpty` and then calls a private `XxxRecursive(node, ...)` helper. Follow this pattern for new members.
- `PracticeExercise5/BinarySearchTreeNode.cs`: an `internal` class, so the test project can't see it. The node has `Key`, `Value`, `Left`, `Right` **and `Parent`**. `GetNode`, `MinNode` and `MaxNode` are `private` helpers in the tree class.
- `IsEmpty` has `[MemberNotNullWhen(false, nameof(Root))]`, so code after an `if (IsEmpty) return …;` guard can use `Root` without nullable warnings. Both projects currently build with 0 warnings.

## Behavior the tests expect (not obvious from the interface)

- **Height** counts edges, so an empty tree and a single-node tree both have height `0`. A degenerate 50-node chain has height `49`.
- **Add with an existing key** overwrites the value and does not increment `Count`. `Update` is implemented as `Add`, so updating a missing key inserts it.
- **Tree shape is checked through `PreOrderKeys`**, compared with an exact list, because the tests can't reach nodes. A BST's preorder determines its shape. Shape checks go through `UnitTests/TreeAssert.cs`: `HasShape` calls `IsValid` first, which checks that `InOrderKeys` is strictly increasing and agrees with `Count`/`IsEmpty`. Use these helpers in new tests rather than comparing `PreOrderKeys` directly. The exception is the `PreOrderKeys` traversal test itself. Most tests start from `SampleTree.Create()` (`UnitTests/SampleTree.cs`, with its diagram in the doc comment; values are key + 1). The two-children tests then `Add(9, 10)` to give 10 a left child.
- **Remove of a missing key** throws `KeyNotFoundException`. A node with two children is replaced by its **in-order successor** (the minimum of its right subtree). Using the predecessor gives a different shape and fails the `RemoveIntW2Children*`/`RemoveRoot*` tests. No test reads `Parent` directly; `NextAndPrevAfterRemove` catches broken `Parent` links through `NextKey`/`PrevKey`.
- **Search** returns the value for a key (see `AddInt`, `AdvancedTests.Search`). For a missing key it throws `KeyNotFoundException` (`SearchMissingKey`).
- **MedianKey** is computed over the sorted keys and averages the two middle keys when `Count` is even (for example, keys 0..51 give `25.5`). It is `double?` and returns `null` for an empty tree, like `MinKey`/`MaxKey`/`Min`/`Max` (`MedianKeyEmpty`).
- **NextKey/PrevKey(key)** return the in-order successor or predecessor key, or `null` at either end. They throw `KeyNotFoundException` if `key` isn't in the tree. They are not like `TreeMap.higherKey`, which accepts any key.
- **RangeSearch(min, max)** returns a `List<int>` of keys, inclusive at both ends and in ascending order (the tests compare exact lists). It returns an empty list, not `null`, when no keys match. When `min > max` it also returns an empty list (`RangeMinGreaterThanMax`), with no swapping and no exception.
- `Min` is implemented on top of `MinNode`, which is still a stub, so `Min` throws until `MinNode` is implemented.
