using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// keeps track of the smudges on its floor
public class SmudgeManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip jingle;
    public GameObject prefabJ;
    public GameObject prefabK;
    public GameObject prefabL;

    public List<Smudge> allSmudges = new List<Smudge>(); // ACCESS VIA: FloorManager.currentFloor.smudgeManager.allSmudges

    private static int currentTarget = -1; // index in allSmudges that is being selected
    private CharacterMover characterMover;
    private FloorManager floorManager;
    private int initialSmudges = 0; //initial number of total window smudges when spawned
    private int currTotalSmudges = 0; //current amount of smudges on window


    void Start()
    {
        floorManager = GameObject.Find("FloorParent").GetComponent<FloorManager>();
        characterMover = GameObject.Find("Character").GetComponent<CharacterMover>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (allSmudges.Count <= 0)
        {
            if (!floorManager.NextFloor()) source.PlayOneShot(jingle, 0.1f);
            Destroy(this);
        }
    }

    // creates a smudge object at the given position with the given type
    public void AddSmudge(Tuple<Vector3, Smudge.SmudgeType> smudgeInfo)
    {
        GameObject prefab;
        switch (smudgeInfo.Item2)
        {
            case Smudge.SmudgeType.SmudgeJ:
                prefab = prefabJ;
                break;
            case Smudge.SmudgeType.SmudgeK:
                prefab = prefabK;
                break;
            case Smudge.SmudgeType.SmudgeL:
            default:
                prefab = prefabL;
                break;
        }

        GameObject smudge = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        smudge.transform.parent = this.transform;
        smudge.transform.localPosition = smudgeInfo.Item1;
        allSmudges.Add(smudge.GetComponent<Smudge>());
        initialSmudges++;
        currTotalSmudges++;
    }

    public bool WipeSmudge()
    {
        if (currentTarget > -1)
        {
            if (allSmudges[currentTarget].neutralized)
            {
                allSmudges[currentTarget].Clean();
                allSmudges.RemoveAt(currentTarget);
                currentTarget = -1;
                characterMover.FindClosest();
                currTotalSmudges--;
                return true;
            }
        }
        return false;
    }

    public float Progress
    {
        get{return (float)currTotalSmudges/(float)initialSmudges;}
    }

    public void SpraySmudge(Smudge.SmudgeType spray)
    {
        if (spray == allSmudges[currentTarget].type)
        {
            allSmudges[currentTarget].Neutralize();
        }
    }

    public void SelectSmudge(int smudgeIndex)
    {
        if (currentTarget != smudgeIndex)
        {
            DeselectSmudge();
            currentTarget = smudgeIndex;
            allSmudges[smudgeIndex].Select();
        }
    }

    public void DeselectSmudge()
    {
        if (currentTarget > -1 && currentTarget < allSmudges.Count)
        {
            allSmudges[currentTarget].Deselect();
        }

        currentTarget = -1;
    }
}