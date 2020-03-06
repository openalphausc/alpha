using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject playerObjects;
    [SerializeField] private float descentSpeed;
    
    public List<Floor> allFloors;
    public static Floor currentFloor;
    

    private const float FLOOR_HEIGHT = 7;
    private bool moving;
    private int floorIndex;
    private int floorCount;

    void Start()
    {
        allFloors.Clear();
        floorCount = smudgeData.Count;
        for(int i = 0; i < floorCount; i++)
        {
            GameObject floor = Instantiate(floorPrefab, new Vector3(0, (floorCount - 1 - i) * FLOOR_HEIGHT, 0), Quaternion.identity);
            allFloors.Add(floor.GetComponent<Floor>());
            allFloors[i].InitializeFloor(smudgeData[i]);
        }

        currentFloor = allFloors[0];
        floorIndex = 0;
        playerObjects.transform.position = new Vector3(0, (floorCount - 1) * FLOOR_HEIGHT, 0);
    }

    void Update()
    {
        if (moving)
        {
            playerObjects.transform.Translate(0, -descentSpeed * Time.deltaTime, 0);
            if (playerObjects.transform.position.y <= (floorCount - floorIndex - 1) * FLOOR_HEIGHT)
            {
                playerObjects.transform.position = new Vector3(0,  (floorCount - floorIndex - 1) * FLOOR_HEIGHT, 0);
                moving = false;
            }
        }
    }

    public bool NextFloor()
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
    
    
    // data for floor generation (kinda messy?)
    public List<List<Tuple<Vector3, Smudge.SmudgeType>>> smudgeData = new List<List<Tuple<Vector3, Smudge.SmudgeType>>>
    {
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 1, 0), Smudge.SmudgeType.smudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.smudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.smudgeL),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 1, 0), Smudge.SmudgeType.smudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.smudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.smudgeL),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+3, 2, 0), Smudge.SmudgeType.smudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 5, 0), Smudge.SmudgeType.smudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 1, 0), Smudge.SmudgeType.smudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-6, 4, 0), Smudge.SmudgeType.smudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 3, 0), Smudge.SmudgeType.smudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-7, 1, 0), Smudge.SmudgeType.smudgeL),
        },
    };
    
}
