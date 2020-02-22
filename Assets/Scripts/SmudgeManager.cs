using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmudgeManager : MonoBehaviour
{
    public static HashSet<GameObject> allSmudges = new HashSet<GameObject>();
    
    void Start()
    {
        foreach (Transform smudge in transform)
        {
            allSmudges.Add(smudge.gameObject);
        }
    }

    public static void RemoveSmudge(GameObject smudge)
    {
        allSmudges.Remove(smudge);
        Destroy(smudge);
    }
}
