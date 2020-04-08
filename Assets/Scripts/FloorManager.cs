using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

// In charge of keeping track of each floor
public class FloorManager : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject playerObjects;
    public GameObject character;
    [SerializeField] private float descentSpeed; // How quickly the platform transitions between floors
    [SerializeField] private int floorCount; // Number of floors
    [SerializeField] private int minimumSmudges; // Lowest number of possible smudges on a floor
    [SerializeField] private int maximumSmudges; // Highest number of possible smudges on a floor
    [SerializeField] private double randomness; // How much weight is placed on randomness: 0.0 (linear from min to max) to 1.0 (completely random)
    
    public List<Floor> allFloors;
    public static Floor currentFloor; // ACCESS VIA: FloorManager.currentFloor    

    private const float FLOOR_HEIGHT = 7;
    public static bool moving;
    private int floorIndex; // current floor index in allFloors
    private WindowController windowController_;
    private CharacterMover characterMover_;


    void Start()
    {
        List<Smudge.SmudgeType> availableTypes = new List<Smudge.SmudgeType>()
        {
            Smudge.SmudgeType.SmudgeNone,
            Smudge.SmudgeType.SmudgeJ,
            Smudge.SmudgeType.SmudgeK,
            Smudge.SmudgeType.SmudgeL,
        };
        GenerateSmudges(minimumSmudges, maximumSmudges, randomness, availableTypes);
        allFloors.Clear();
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
                characterMover_.FindClosest();
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
    
    private void GenerateSmudges(int minSmudges, int maxSmudges, double variance, List<Smudge.SmudgeType> types)
    {
        int range = maxSmudges - minSmudges;
        Random random = new Random();
        smudgeData.Clear();
        for (int i = 0; i < floorCount; i++)
        {
            List<Tuple<Vector3, Smudge.SmudgeType>> positions = new List<Tuple<Vector3, Smudge.SmudgeType>>();
            double progress = 1.0 * i / (floorCount - 1);
            double predictedCount = range * progress + minSmudges;
            double randomCount = range * random.NextDouble() + minSmudges;
            double actualCount = variance * randomCount + (1 - variance) * predictedCount;
            int roundedCount = (int) Math.Round(actualCount);
            for (int j = 0; j < roundedCount; j++)
            {
                double xSlot = 14.0 / roundedCount * j - 7;
                double xShift = 14.0 / roundedCount * (random.NextDouble() * 0.8 + 0.1);
                float xPos = (float)(xSlot + xShift);
                float yPos = 4 * (float)random.NextDouble() + 1;
                int randomType = random.Next() % types.Count;
                positions.Add(new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(xPos, yPos, 0), types[randomType]));
            }
            smudgeData.Add(positions);
        }
    }
    
    // data for floor generation
    public List<List<Tuple<Vector3, Smudge.SmudgeType>>> smudgeData = new List<List<Tuple<Vector3, Smudge.SmudgeType>>>()
        
        //the following will be removed, but leaving in case we remove level generation, as a formatting template
        
    // {
    //     new List<Tuple<Vector3, Smudge.SmudgeType>>
    //     {
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 1, 0), Smudge.SmudgeType.SmudgeNone),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.SmudgeNone),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.SmudgeNone),
    //     },
    //     new List<Tuple<Vector3, Smudge.SmudgeType>>
    //     {
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 1, 0), Smudge.SmudgeType.SmudgeJ),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-6, 4, 0), Smudge.SmudgeType.SmudgeK),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 2, 0), Smudge.SmudgeType.SmudgeL),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+4, 3, 0), Smudge.SmudgeType.SmudgeNone),
    //     },
    //     new List<Tuple<Vector3, Smudge.SmudgeType>>
    //     {
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+3, 2, 0), Smudge.SmudgeType.SmudgeJ),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 5, 0), Smudge.SmudgeType.SmudgeJ),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 1, 0), Smudge.SmudgeType.SmudgeK),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-6, 4, 0), Smudge.SmudgeType.SmudgeK),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 3, 0), Smudge.SmudgeType.SmudgeL),
    //         new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-7, 1, 0), Smudge.SmudgeType.SmudgeL),
    //     },
    // }
        ;

}
