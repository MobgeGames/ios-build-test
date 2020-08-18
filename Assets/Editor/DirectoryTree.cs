using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class DirectoryTree {
    public static void PrintDirectoryTree(string path) {
        var items = SearchDirectory(new DirectoryInfo(path));
        foreach (var item in items)
            Debug.Log(string.Concat(Enumerable.Repeat('\t', item.Depth)) + item.Name);
    }
    private static IEnumerable<HierarchicalItem> SearchDirectory(DirectoryInfo directory, int deep = 0) {
        yield return new HierarchicalItem(directory.Name, deep);
        foreach (var subdirectory in directory.GetDirectories())
        foreach (var item in SearchDirectory(subdirectory , deep + 1))
            yield return item;
        foreach (var file in directory.GetFiles())
            yield return new HierarchicalItem(file.Name, deep + 1);
    }
}
public class HierarchicalItem {
    public readonly string Name;
    public readonly int Depth;
    public HierarchicalItem(string name, int depth) {
        this.Name = name;
        this.Depth = depth;
    }
}