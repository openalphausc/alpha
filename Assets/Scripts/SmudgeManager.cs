using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmudgeManager : MonoBehaviour
{
    public GameObject character;
    
    public static List<Smudge> allSmudges = new List<Smudge>();
    
    private static int currentTarget = -1;
    private static CharacterMover characterMover;

    void Start()
    {
        characterMover = character.GetComponent<CharacterMover>();
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
                characterMover.FindClosest();
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
            DeselectSmudge();
            currentTarget = smudgeIndex;
            allSmudges[smudgeIndex].Select();
        }
    }

    public static void DeselectSmudge()
    {
        if (currentTarget > -1 && currentTarget < allSmudges.Count)
        {
            allSmudges[currentTarget].Deselect();
        }
        currentTarget = -1;
    }
}
