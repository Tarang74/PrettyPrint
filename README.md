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
collection.PrettyPrint(bool overflow = true);
```

Each node will print at least 2 characters from the start of the string.
The `overflow` parameter allows this string to overflow and occupy any whitespace
preceding the next node (if it exists).

## Examples

Below are example trees made up of nodes with labels: "A", "B2", "C33", "D444", etc.

```ru
                                                 D444
                 ╭───────────────────────────────╯╰───────────────────────────────╮
                 B2                                                              F66666
 ╭───────────────╯╰───────────────╮                              ╭───────────────╯╰───────────────╮
 A                               C33                             E5555                           J000000000
                                                                                         ╭───────╯╰───────╮
                                                                                         I99999999       K1111111111
                                                                                     ╭───╯
                                                                                     H8888888
                                                                                   ╭─╯
                                                                                   G777777
```

```ru
B2
  ╰───────╮
         I99999999
     ╭───╯╰───╮
     D444    K1111111111
   ╭─╯╰─╮
   C33 G777777
```

```ru
               E5555
       ╭───────╯╰───────╮
       C33             I99999999
   ╭───╯╰───╮      ╭───╯╰───╮
   B2      D444    G777777 J000000000
 ╭─╯
 A
```

Disabling the `overflow` option produces the following:

```ru
                                                 D4
                 ╭───────────────────────────────╯╰───────────────────────────────╮
                 B2                                                              F6
 ╭───────────────╯╰───────────────╮                              ╭───────────────╯╰───────────────╮
 A                               C3                              E5                              J0
                                                                                         ╭───────╯╰───────╮
                                                                                         I9              K1
                                                                                     ╭───╯
                                                                                     H8
                                                                                   ╭─╯
                                                                                   G7
```

```ru
 B2
  ╰───────╮
         I9
     ╭───╯╰───╮
     D4      K1
   ╭─╯╰─╮
   C3  G7
```

```ru
               E5
       ╭───────╯╰───────╮
       C3              I9
   ╭───╯╰───╮      ╭───╯╰───╮
   B2      D4      G7      J0
 ╭─╯
 A
```
