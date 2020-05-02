using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// keeps track of the smudges on its floor
public class SmudgeManager : MonoBehaviour
{
    public AudioSource buildingcompletesound;
    public GameObject prefabJ;
    public GameObject prefabK;
    public GameObject prefabL;
    public GameObject prefabNone;

    public GameObject leaveButton;

    public List<Smudge>
        allSmudges = new List<Smudge>(); // ACCESS VIA: FloorManager.currentFloor.smudgeManager.allSmudges

    public static int currentTarget = -1; // index in allSmudges that is being selected
    private CharacterMover characterMover;
    private FloorManager floorManager;
    private TimerScript timerScript;
    private int initialSmudges = 0; //initial number of total window smudges when spawned
    private int currTotalSmudges = 0; //current amount of smudges on window
    public int minIncomePerFloor = 5;

    void Start()
    {
        floorManager = GameObject.Find("FloorParent").GetComponent<FloorManager>();
        characterMover = GameObject.Find("Character").GetComponent<CharacterMover>();
        timerScript = GameObject.Find("TimeControl").GetComponent<TimerScript>();
    }

    void Update()
    {
        if (allSmudges.Count <= 0)
        {
            buildingcompletesound.Play();
            if (!floorManager.NextFloor())
            {
                buildingcompletesound.volume = 1f;
                GameObject button = Instantiate(leaveButton) as GameObject;
            }
            else buildingcompletesound.volume = 0.3f;

            Destroy(this);
            timerScript.runTimer = false;
            timerScript.addTime();
            //add income!;
            int baseIncome = minIncomePerFloor + Math.Max(0,
                10 - (int) (4 * timerScript.trackSplits.Last()));
            // print("base income:" + baseIncome);
            int income = (int) (baseIncome * (1 + (PersistentManagerScript.Instance.InvIncomeIncrease()/100.0)));
            // print("total income: " + income);
            PersistentManagerScript.Instance.money += income;
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
                prefab = prefabL;
                break;
            case Smudge.SmudgeType.SmudgeNone:
                prefab = prefabNone;
                break;
            default:
                prefab = prefabNone;
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
        get { return (float) currTotalSmudges / (float) initialSmudges; }
    }

    public void SpraySmudge(Smudge.SmudgeType spray)
    {
        // spray matches exactly - neutralize perfectly
        if (spray == allSmudges[currentTarget].type)
        {
            allSmudges[currentTarget].Neutralize(100);
            return;
        }

        // spray is not an exact match - neutralize either kinda or bad
        int kinda = 34;
        int bad = 20;
        Smudge.SmudgeType smudge = allSmudges[currentTarget].type;
        Smudge.SmudgeType red = Smudge.SmudgeType.SmudgeJ;
        Smudge.SmudgeType yellow = Smudge.SmudgeType.SmudgeK;
        Smudge.SmudgeType green = Smudge.SmudgeType.SmudgeL;
        if (smudge == red)
        {
            if (spray == yellow) allSmudges[currentTarget].Neutralize(kinda);
            else if (spray == green) allSmudges[currentTarget].Neutralize(bad);
        }
        else if (smudge == yellow)
        {
            if (spray == green) allSmudges[currentTarget].Neutralize(kinda);
            else if (spray == red) allSmudges[currentTarget].Neutralize(bad);
        }

        if (smudge == green)
        {
            if (spray == red) allSmudges[currentTarget].Neutralize(kinda);
            else if (spray == yellow) allSmudges[currentTarget].Neutralize(bad);
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