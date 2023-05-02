# PrettyPrint

A C# utility method to print binary trees.

Made for CAB301 Assessment 2 in Semester 1, 2023.

## Usage

Place this extension class in your project, then call the print method on any collection.

```cs
IMovieCollection collection = new();

// Populate tree
...

// Print tree
string tree = collection.PrettyPrint(int nodeSpacing = 1, int nodeLabelWidth = 1, bool overflow = false, bool optimise = true);
Console.WriteLine(tree);
```

The first two parameters `nodeSpacing` and `nodeLabelWidth` define dimensions for the deepest nodes in the tree.
Both arguments must be greater than 0.

If you use a small `nodeLabelWidth`, you may want to enable the `overflow` flag
to allow some node labels to overflow any unoccupied whitespace to the right of the node.

The `optimise` parameter reduces the overall width of a tree. This is particularly
useful when printing deep sparse trees that do not need to be spaced uniformally.

## Examples

Below are example trees made up of nodes with labels: "AAA", "BBB", "CCC", "DDD", etc.

1. `(1, 1, true)`

   ```as
                   BBB
   ╭───────────────╰───────────────╮
   AAA                             III
                           ╭───────╰───────╮
                           DDD             KKK
                       ╭───╰───╮       ╭───╯
                       CCC     GGG     JJJ
                             ╭─╰─╮
                             FFF HHH
                            ╭╯
                            EEE
   ```

2. `(1, 1, false)`

   ```as
                   B
   ╭───────────────╰───────────────╮
   A                               I
                           ╭───────╰───────╮
                           D               K
                       ╭───╰───╮       ╭───╯
                       C       G       J
                             ╭─╰─╮
                             F   H
                            ╭╯
                            E
   ```

3. `(3, 1, true)`

   ```as
                                                   BBB
   ╭───────────────────────────────────────────────╰───────────────╮
   AAA                                                             III
                                           ╭───────────────────────╰───────╮
                                           DDD                             KKK
                               ╭───────────╰───╮               ╭───────────╯
                               CCC             GGG             JJJ
                                         ╭─────╰─╮
                                         FFF     HHH
                                      ╭──╯
                                      EEE
   ```

4. `(3, 1, false)`

   ```as
                                                   B
   ╭───────────────────────────────────────────────╰───────────────╮
   A                                                               I
                                           ╭───────────────────────╰───────╮
                                           D                               K
                               ╭───────────╰───╮               ╭───────────╯
                               C               G               J
                                         ╭─────╰─╮
                                         F       H
                                      ╭──╯
                                      E
   ```

5. `(1, 3, true)`

   ```as
                   BBB
   ╭───────────────╯ ╰───────────────────────────────────────────────╮
   AAA                                                             III
                                                           ╭───────╯ ╰───────────────────────╮
                                                           DDD                             KKK
                                                       ╭───╯ ╰───────────╮             ╭───╯
                                                       CCC             GGG             JJJ
                                                                     ╭─╯ ╰─────╮
                                                                     FFF     HHH
                                                                    ╭╯
                                                                    EEE
   ```

6. `(1, 3, false)`

   ```as
                   BBB
   ╭───────────────╯ ╰───────────────────────────────────────────────╮
   AAA                                                             III
                                                           ╭───────╯ ╰───────────────────────╮
                                                           DDD                             KKK
                                                       ╭───╯ ╰───────────╮             ╭───╯
                                                       CCC             GGG             JJJ
                                                                     ╭─╯ ╰─────╮
                                                                     FFF     HHH
                                                                    ╭╯
                                                                    EEE
   ```
