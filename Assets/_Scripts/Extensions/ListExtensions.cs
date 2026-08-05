using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T Draw<T>(this List<T> list)
    {
        if (list.Count == 0)
            return default;
        T t = list[Random.Range(0, list.Count)];
        list.Remove(t);
        return t;
    }
}
