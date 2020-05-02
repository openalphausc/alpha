using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

// In charge of keeping track of each floor
public class FloorManager : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject playerObjects;
    public GameObject character;
    public GameObject background;
    [SerializeField] private float descentSpeed; // How quickly the platform transitions between floors
    [SerializeField] private int floorCount; // Number of floors
    [SerializeField] private int minimumSmudges; // Lowest number of possible smudges on a floor
    [SerializeField] private int maximumSmudges; // Highest number of possible smudges on a floor
    [SerializeField] private double randomness; // How much weight is placed on randomness: 0.0 (linear from min to max) to 1.0 (completely random)

    public List<Floor> allFloors;
    public static Floor currentFloor; // ACCESS VIA: FloorManager.currentFloor

    private const float FLOOR_HEIGHT = 7.5f;
    public static bool moving;
    public static int floorIndex; // current floor index in allFloors
    private WindowController windowController_;
    private CharacterMover characterMover_;

    public AudioSource tutorialmusic;
    public AudioSource volcanomusic;
    public AudioSource squidmusic;
    public AudioSource spaghettimusic;
    public AudioSource arcademusic;

    void Start()
    {
        List<Smudge.SmudgeType> availableTypes = new List<Smudge.SmudgeType>()
        {
            Smudge.SmudgeType.SmudgeNone,
            Smudge.SmudgeType.SmudgeJ,
            Smudge.SmudgeType.SmudgeK,
            Smudge.SmudgeType.SmudgeL,
        };
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "TutorialScene")
        {
            floorCount = 10 + 2 * PersistentManagerScript.Instance.levelIndex;
            GenerateSmudges(minimumSmudges, maximumSmudges, randomness, availableTypes);
            // play music based on the level
            if(PersistentManagerScript.Instance.levelIndex == 1) volcanomusic.Play();
            else if(PersistentManagerScript.Instance.levelIndex == 2) squidmusic.Play();
            else if(PersistentManagerScript.Instance.levelIndex == 3) spaghettimusic.Play();
            else arcademusic.Play();
        }
        else tutorialmusic.Play();
        allFloors.Clear();
        for(int i = 0; i < floorCount; i++)
        { // add each floor from the dataset to a new instance of a Floor
            GameObject floor = Instantiate(floorPrefab, new Vector3(0, (floorCount - 1 - i) * FLOOR_HEIGHT, 0), Quaternion.identity);
            floor.transform.parent = this.transform;
            allFloors.Add(floor.GetComponent<Floor>());
            allFloors[i].InitializeFloor(smudgeData[i]);
        }

        currentFloor = allFloors[0];
        floorIndex = 0;
        playerObjects.transform.position = new Vector3(0, (floorCount - 1) * FLOOR_HEIGHT, 0); // start at top floor

        PersistentManagerScript.Instance.levelProgress = 0f;

        windowController_ = GetComponent<WindowController>();
        characterMover_ = character.GetComponent<CharacterMover>();
    }

    void Update()
    {
        if (moving)
        { // move character, platform, camera, etc. down a floor
            playerObjects.transform.Translate(0, -descentSpeed * Time.deltaTime, 0);
            if (floorIndex < 16)
            {
                background.transform.Translate(0, descentSpeed / 35f * Time.deltaTime, 0);
            }

            if (playerObjects.transform.position.y <= (floorCount - floorIndex - 1) * FLOOR_HEIGHT)
            { // stop when arrived
                playerObjects.transform.position = new Vector3(0,  (floorCount - floorIndex - 1) * FLOOR_HEIGHT, 0);
                moving = false;
                // characterMover_.FindClosest();
            }
        }
        else
        {
            characterMover_.FindClosest();
        }
    }

    public bool NextFloor() // go to the next floor down. returns false if at bottom
    {
        floorIndex++;
        PersistentManagerScript.Instance.levelProgress = ((float) floorIndex) / floorCount;
        
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
            double progress = 1.0 * i / (floorCount - 1); // how far down we are
            double predictedCount = range * progress + minSmudges; // linear value according to increasing count
            double randomCount = range * random.NextDouble() + minSmudges; // totally random amount within range
            double actualCount = variance * randomCount + (1 - variance) * predictedCount; // weighted average of the two based on variance
            int roundedCount = (int) Math.Round(actualCount); // nearest int

            int countL = 0;
            for (int j = 0; j < roundedCount; j++)
            {
                double xSlot = 14.0 / roundedCount * j - 7; // each smudge gets its own horizontal segment
                double xShift = 14.0 / roundedCount * (random.NextDouble() * 0.8 + 0.1); // random spot within that slot, not too close to edge (10% margin)
                float xPos = (float)(xSlot + xShift); // add the two for the actual x value
                float yPos = 4 * (float)random.NextDouble() + 1; // totally random height

                int randomType = random.Next();
                if (countL < 2)
                {
                    randomType %= types.Count; // totally random smudge
                    if (randomType == 3)
                    {
                        countL++;
                    }
                }
                else
                {
                    randomType %= types.Count - 1;
                }

                positions.Add(new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(xPos, yPos, 0), types[randomType]));
            }
            smudgeData.Add(positions);
        }
    }

    // data for floor generation
    public List<List<Tuple<Vector3, Smudge.SmudgeType>>> smudgeData = new List<List<Tuple<Vector3, Smudge.SmudgeType>>>()

        //the following will be removed, but leaving in case we remove level generation, as a formatting template

    {
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 2, 0), Smudge.SmudgeType.SmudgeNone),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+5, 3, 0), Smudge.SmudgeType.SmudgeNone),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+3, 3, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-5, 5, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 1, 0), Smudge.SmudgeType.SmudgeNone),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-3, 2, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(4, 4, 0), Smudge.SmudgeType.SmudgeJ),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            // new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-5, 4, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 5, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-7, 2, 0), Smudge.SmudgeType.SmudgeJ),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 3, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 1, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 5, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 1, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 3, 0), Smudge.SmudgeType.SmudgeJ),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 2, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-7, 3, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+3, 1, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+1, 5, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 4, 0), Smudge.SmudgeType.SmudgeJ),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 5, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+4, 2, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-6, 1, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 3, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.SmudgeJ),
        },

        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(1, 5, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+5, 1, 0), Smudge.SmudgeType.SmudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-5, 4, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(3, 3, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-3, 1, 0), Smudge.SmudgeType.SmudgeNone),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 5, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+4, 2, 0), Smudge.SmudgeType.SmudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-6, 1, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 3, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 4, 0), Smudge.SmudgeType.SmudgeL),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-1, 3, 0), Smudge.SmudgeType.SmudgeNone),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 1, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-2, 5, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+2, 1, 0), Smudge.SmudgeType.SmudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-4, 3, 0), Smudge.SmudgeType.SmudgeJ),
        },
        new List<Tuple<Vector3, Smudge.SmudgeType>>
        {
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+6, 2, 0), Smudge.SmudgeType.SmudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(1, 5, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(+5, 1, 0), Smudge.SmudgeType.SmudgeL),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-5, 4, 0), Smudge.SmudgeType.SmudgeJ),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(3, 3, 0), Smudge.SmudgeType.SmudgeK),
            new Tuple<Vector3, Smudge.SmudgeType>(new Vector3(-3, 1, 0), Smudge.SmudgeType.SmudgeK),
        },
    }
        ;

}
