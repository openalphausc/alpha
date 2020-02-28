using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmudgeManager : MonoBehaviour
{
    public static List<Smudge> allSmudges = new List<Smudge>();
    
    private static int currentTarget = -1;

    void Start()
    {
        
    }

    public static void WipeSmudge()
    {
        if (currentTarget > -1)
        {
            if (allSmudges[currentTarget].neutralized)
            {
                allSmudges[currentTarget].Clean();
                allSmudges.RemoveAt(currentTarget);
                currentTarget = -1;
            }
        }
    }

    public static void SpraySmudge(Smudge.SmudgeType spray)
    {
        if (spray == allSmudges[currentTarget].type)
        {
            allSmudges[currentTarget].Neutralize();
        }
    }

    public static void SelectSmudge(int smudgeIndex)
    {
        if (currentTarget != smudgeIndex)
        {
            print("new target");
            DeselectSmudge();
            currentTarget = smudgeIndex;
            allSmudges[smudgeIndex].Select();
        }
    }

    public static void DeselectSmudge()
    {
        if (currentTarget > -1)
        {
            allSmudges[currentTarget].Deselect();
            currentTarget = -1;
        }
    }
}
