using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Static class for generally helpful methods
public static class Helpers
{
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
}
