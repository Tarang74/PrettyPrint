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

Below are example trees made up of nodes with labels: "AAAA", "BBBB", "CCCC", "DDDD", etc.

MWE:

```cs
IMovieCollection collection = new MovieCollection();

collection.Insert(new Movie("BBBB"));
collection.Insert(new Movie("IIII"));
collection.Insert(new Movie("KKKK"));
collection.Insert(new Movie("DDDD"));
collection.Insert(new Movie("JJJJ"));
collection.Insert(new Movie("CCCC"));
collection.Insert(new Movie("AAAA"));
collection.Insert(new Movie("GGGG"));
collection.Insert(new Movie("FFFF"));
collection.Insert(new Movie("HHHH"));
collection.Insert(new Movie("EEEE"));

// using default arguments (1, 1, false, true)
string tree = collectionPrettyPrint();
Console.WriteLine(tree);
```

```as
 B
╭╰──────╮
A       I
   ╭────╰─╮
   D      K
  ╭╰──╮  ╭╯
  C   G  J
     ╭╰╮
     F H
    ╭╯
    E
```

Examples with various arguments:

1. `(1, 1, true, false)`

   ```as
                   BBBB
   ╭───────────────╰───────────────╮
   AAAA                            IIII
                           ╭───────╰───────╮
                           DDDD            KKKK
                       ╭───╰───╮       ╭───╯
                       CCCC    GGGG    JJJJ
                             ╭─╰─╮
                             FFF HHHH
                            ╭╯
                            EEEE
   ```

2. `(1, 2, false, true)`

   ```as
     BB
   ╭─╯╰────────╮
   AA         II
         ╭────╯╰─────╮
         DD         KK
       ╭─╯╰────╮  ╭─╯
       CC     GG  JJ
            ╭─╯╰─╮
            FF  HH
           ╭╯
           EE
   ```

3. `(2, 1, false, false)`

   ```as
                                   B
   ╭───────────────────────────────╰───────────────╮
   A                                               I
                                   ╭───────────────╰───────╮
                                   D                       K
                           ╭───────╰───╮           ╭───────╯
                           C           G           J
                                   ╭───╰─╮
                                   F     H
                                 ╭─╯
                                 E
   ```
