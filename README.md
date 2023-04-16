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

