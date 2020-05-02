using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls the left-right movement of the character
// as well as finding the closest smudge to the player
public class CharacterMover : MonoBehaviour
{
    public static float baseSpeed = 4;
    //make these private later
    public float currentSpeed;
    public float movementSpeed;
    public float slowedSpeed;
    public Animator animator;
    public GameObject leftArm;
    public GameObject rightArm;

    public int speedState = 0; // 0 is free, 1 is slow
    public float timeCleaningUp;

    public static int closestSmudge = -1;
    public static Vector3 closestRelativePosition = Vector3.positiveInfinity;
    public static bool targeting = false;
    public float targetRange;

    private bool startedwalking = false;
    public AudioSource walksound;

    void Start()
    {
        float speedBonus = (PersistentManagerScript.Instance.InvSpeedBonus() / 100.0f);
        movementSpeed = baseSpeed;
        movementSpeed *= (1 + speedBonus);
        currentSpeed = movementSpeed;
        // slowedSpeed = 0.2f * movementSpeed;
        slowedSpeed = 0f;
        //increase target range based on extended reach from inventory
        targetRange *= (float) (1 + PersistentManagerScript.Instance.InvRangeIncrease()/100);
    }

    void Update()
    {
        if (speedState == 1)
        {
            walksound.Stop();
            currentSpeed = slowedSpeed;
            timeCleaningUp -= Time.deltaTime;
            if (timeCleaningUp <= 0)
            {
                speedState = 0;
            }
            animator.SetBool("broken", true);
            leftArm.SetActive(false);
            rightArm.SetActive(false);
        }
        else if (speedState == 0)
        {
            currentSpeed = movementSpeed;
            animator.SetBool("broken", false);
            leftArm.SetActive(true);
            rightArm.SetActive(true);
        }

        if (Input.GetKey(KeyCode.A))
        {
          print("A");
            if(!startedwalking) {
              walksound.Play();
              startedwalking = true;
            }
            MovePlayer(-currentSpeed);
            animator.SetFloat("HorizontalSpeed", -1.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if(!startedwalking) {
              walksound.Play();
              startedwalking = true;
            }
            MovePlayer(currentSpeed);
            animator.SetFloat("HorizontalSpeed", 1.0f);
        }
        else {
          startedwalking = false;
          walksound.Stop();
          animator.SetFloat("HorizontalSpeed", 0.0f);
        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void MovePlayer(float distance)
    {
        transform.Translate(distance * Time.deltaTime, 0, 0);
        float posX = transform.position.x;
        if (posX > 7)
        {
            SetPositionX(7);
        }
        else if (posX < -7)
        {
            SetPositionX(-7);
        }
        //FindClosest();
    }

    void SetPositionX(float x)
    {
        var characterTransform = transform;
        Vector3 position = characterTransform.position;
        position = new Vector3(x, position.y, position.z);
        characterTransform.position = position;
    }

    // calculates the nearest smudge
    public void FindClosest()
    {
        Vector3 closestPosition = Vector3.positiveInfinity;
        int i = 0;
        foreach (Smudge smudge in FloorManager.currentFloor.smudgeManager.allSmudges)
        {
            Vector3 relative = smudge.transform.position - this.transform.position;
            if (Mathf.Abs(relative.x) < Mathf.Abs(closestPosition.x))
            {
                closestPosition = relative;
                closestSmudge = i;
            }

            i++;
        }

        closestRelativePosition = closestPosition;

        if (closestRelativePosition.magnitude <= targetRange)
        {
            FloorManager.currentFloor.smudgeManager.SelectSmudge(CharacterMover.closestSmudge);
            targeting = true;
        }
        else
        {
            FloorManager.currentFloor.smudgeManager.DeselectSmudge();
            targeting = false;
        }
    }
}
