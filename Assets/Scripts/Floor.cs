using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public SmudgeManager smudgeManager;
    public WindowController windowController;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void InitializeFloor(List<Tuple<Vector3, Smudge.SmudgeType>> smudges)
    {
        foreach (Tuple<Vector3, Smudge.SmudgeType> smudgeInfo in smudges)
        {
            smudgeManager.AddSmudge(smudgeInfo);
        }
    }
}
