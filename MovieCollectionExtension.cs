using System.Reflection;
using System.Text.RegularExpressions;

public static class MovieCollectionExtension
{
    public enum Direction
    {
        L = 0,
        R = 1
    };

    public static void PrettyPrint(this IMovieCollection collection, int nodeSpacing = 1, int nodeLabelWidth = 1, bool overflow = true)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Get private field "root"
        var field = collection.GetType().GetField("root", BindingFlags.NonPublic | BindingFlags.Instance);
        BTreeNode root = (BTreeNode)field!.GetValue(collection)!;

        if (root == null)
        {
            Console.WriteLine("Tree is empty.");
            return;
        }

        Dictionary<int, List<(string text, uint directions)>> treeNodes = new();

        // First pass, traverse tree
        Queue<(BTreeNode, int, uint)> treeQueue = new();
        treeQueue.Enqueue((root, 0, 0));

        while (treeQueue.Count > 0)
        {
            (BTreeNode node, int depth, uint direction) = treeQueue.Dequeue();

            if (!treeNodes.ContainsKey(depth)) treeNodes.Add(depth, new List<(string, uint)>());
            treeNodes[depth].Add((node.Movie.Title, direction));

            if (node.LChild is not null)
                treeQueue.Enqueue((node.LChild, depth + 1, (direction << 1) + (uint)Direction.L));
            if (node.RChild is not null)
                treeQueue.Enqueue((node.RChild, depth + 1, (direction << 1) + (uint)Direction.R));
        }

        int treeDepth = treeNodes.Count;

        int top = Console.GetCursorPosition().Top + 2 * treeDepth - 2;
        int margin = nodeSpacing;

        string textToPrint = "";

        Func<int, int, int, int, int> calculateLeft = (int margin, int node, int gap, int labelWidth) => margin + node * (gap + labelWidth);

        // Second pass, generate text
        foreach ((int depth, List<(string text, uint directions)> row) in treeNodes.Reverse())
        {
            uint currentNode = 0;

            List<uint> nodesList = row.Select(x => x.directions).ToList();
            Queue<uint> nodesQueue = new(nodesList);

            string labelText = "";
            string edgeText = "";

            Queue<uint> parentNodesQueue = new();

            if (depth > 0)
                parentNodesQueue = new(treeNodes[depth - 1].Select(x => x.directions).ToList());

            int parentMargin;
            int parentGap;

            parentMargin = 2 * margin;
            parentGap = 2 * nodeSpacing + nodeLabelWidth;

            int parentLeft = 0;
            bool parentLeftDefined = false;

            while (nodesQueue.Count > 0)
            {
                if (currentNode == nodesQueue.Peek())
                {
                    int left = calculateLeft(margin, (int)currentNode, nodeSpacing, nodeLabelWidth);

                    if (row[0].text.Length > nodeLabelWidth)
                    {
                        if (!overflow)
                            labelText = InsertInto(labelText, row[0].text[..nodeLabelWidth], left);
                        else
                            labelText = InsertInto(labelText, ' ' + row[0].text + new string(' ', 100), left - 1);
                    }
                    else
                    {
                        if ((nodesQueue.Peek() & 1) == 0)
                            labelText = InsertInto(labelText, row[0].text + new string(' ', nodeLabelWidth - row[0].text.Length), left);
                        else
                            labelText = InsertInto(labelText, new string(' ', nodeLabelWidth - row[0].text.Length) + row[0].text, left);
                    }

                    if (depth > 0)
                    {
                        // Ignore any leaf nodes that precede the parent of the current node
                        while (parentNodesQueue.Count > 0)
                            if (parentNodesQueue.Peek() < (currentNode >> 1))
                                parentNodesQueue.Dequeue();
                            else
                            {
                                if (parentNodesQueue.Peek() == (currentNode >> 1))
                                    parentLeftDefined = false;

                                break;
                            }

                        if (!parentLeftDefined)
                        {
                            parentLeft = calculateLeft(parentMargin, (int)parentNodesQueue.Dequeue(), parentGap, nodeLabelWidth);
                            parentLeftDefined = true;
                        }

                        // If LChild
                        if ((nodesQueue.Peek() & 1) == 0)
                        {
                            edgeText = InsertInto(edgeText, "╭", left);

                            if (parentLeft - left < 1)
                                edgeText = InsertInto(edgeText, "╯", left + nodeLabelWidth - 2);
                            else
                                edgeText = InsertInto(edgeText, new string('─', parentLeft - left - 1) + "╯", left + 1);
                        }
                        // If RChild
                        else
                        {
                            edgeText = InsertInto(edgeText, "╮", left + nodeLabelWidth - 1);

                            if (left - parentLeft < 1)
                                edgeText = InsertIntoReverse(edgeText, "╰", left + nodeLabelWidth - 1);
                            else
                                edgeText = InsertIntoReverse(edgeText, new string('─', left - parentLeft - 1) + "╰", left + nodeLabelWidth - 1);
                        }
                    }

                    nodesQueue.Dequeue();
                    row.RemoveAt(0);
                }
                currentNode++;
            }

            margin = parentMargin;
            nodeSpacing = parentGap;

            textToPrint += labelText + "\n" + edgeText + "\n";
        }
        Print(textToPrint, overflow);
    }

    private static void Print(string text, bool trimEnd)
    {
        List<string> lines = text.Split('\n').Reverse().ToList();
        lines.RemoveAll(s => string.IsNullOrEmpty(s));

        int minSpaces = int.MaxValue;
        foreach (string line in lines)
            if (line.TakeWhile(c => c == ' ').Count() < minSpaces)
                minSpaces = line.TakeWhile(c => c == ' ').Count();

        if (trimEnd)
            for (int i = 0; i < lines.Count; i++)
                lines[i] = lines[i].TrimEnd();

        string pattern = @$"^( {{{minSpaces}}})";
        Console.WriteLine(Regex.Replace(string.Join('\n', lines), pattern, "", RegexOptions.Multiline));
    }

    public static List<T> Join<T>(List<T> list1, List<T> list2)
    {
        List<T> combinedList = new();
        combinedList.AddRange(list1);
        combinedList.AddRange(list2);
        return combinedList;
    }

    public static string InsertInto(string text, string newText, int index)
    {
        if (text.Length <= index + newText.Length)
            text += new string(' ', index + newText.Length - text.Length + 1);
        return text[..index] + newText + text[(index + newText.Length)..];
    }
    public static string InsertIntoReverse(string text, string newText, int index)
    {
        index -= newText.Length;
        if (index < 0)
            throw new IndexOutOfRangeException("Reverse inserted text goes beyond index 0.");

        if (text.Length <= index + newText.Length)
            text += new string(' ', index + newText.Length - text.Length + 1);

        char[] reversed = newText.ToCharArray();
        Array.Reverse(reversed);

        return text[..index] + new string(reversed) + text[(index + newText.Length)..];
    }
}
