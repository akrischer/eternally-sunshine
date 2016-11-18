using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CollectionUtil {

    /// <summary>
    /// Does a "set check" for two lists. I.E. given two lists A, B, return if
    /// all elements in A are in B, and all elements in B are in A
    /// </summary>
    /// <returns></returns>
    public static bool ListsContainSameElements<T>(List<T> a, List<T> b)
    {
        return a.All(aElem => b.Contains(aElem)) &&
            b.All(bElem => a.Contains(bElem));
    }
}
