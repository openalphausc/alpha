using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In charge of keeping track of each floor
public class FloorManager : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject playerObjects;
    [SerializeField] private float descentSpeed; // How quickly the platform transitions between floors
    
    public List<Floor> allFloors;
    public static Floor currentFloor; // ACCESS VIA: FloorManager.currentFloor    

    private const float FLOOR_HEIGHT = 7;
    private bool moving;
    private int floorIndex; // current floor index in allFloors
    private int floorCount;
    private WindowController windowController_;


    void Start()
    {
        allFloors.Clear();
        floorCount = smudgeData.Count;
        for(int i = 0; i < floorCount; i++)
        { // add each floor from the dataset to a new instance of a Floor
            GameObject floor = Instantiate(floorPrefab, new Vector3(0, (floorCount - 1 - i) * FLOOR_HEIGHT, 0), Quaternion.identity);
            allFloors.Add(floor.GetComponent<Floor>());
            allFloors[i].InitializeFloor(smudgeData[i]);
        }

        currentFloor = allFloors[0];
        floorIndex = 0;
        playerObjects.transform.position = new Vector3(0, (floorCount - 1) * FLOOR_HEIGHT, 0); // start at top floor

        windowController_ = GetComponent<WindowController>();
    }

    void Update()
    {
        if (moving)
        { // move character, platform, camera, etc. down a floor
            playerObjects.transform.Translate(0, -descentSpeed * Time.deltaTime, 0);
            if (playerObjects.transform.position.y <= (floorCount - floorIndex - 1) * FLOOR_HEIGHT)
            { // stop when arrived
                playerObjects.transform.position = new Vector3(0,  (floorCount - floorIndex - 1) * FLOOR_HEIGHT, 0);
                moving = false;
            }
        }
    }

    public bool NextFloor() // go to the next floor down. returns false if at bottom
    {
        floorIndex++;
        if (floorIndex >= floorCount)
        {
            return false;
        }
        currentFloor = allFloors[floorIndex];
        moving = true;
        return true;
    }
    
    
    // data for floor generation
    public List<List<Tuple<Vector3, Smudge.SmudgeType>>> smudgeData = new List<List<Tuple<Vector3, Smudge.SmudgeType>>>
    {
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 1, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.SmudgeL),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 1, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.SmudgeL),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+3, 2, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 5, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 1, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-6, 4, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 3, 0), Smudge.SmudgeType.SmudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-7, 1, 0), Smudge.SmudgeType.SmudgeL),
        },
    };
    
}
