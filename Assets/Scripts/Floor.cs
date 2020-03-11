using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to access objects on a given floor
public class Floor : MonoBehaviour
{
    public SmudgeManager smudgeManager;
    public WindowController windowController;

    void Awake()
    {
        
    }
    void Start()
    {}

    
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
