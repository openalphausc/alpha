using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToShopInputScript : MonoBehaviour
{
    
    private float HoldADTime;
    public RectTransform HoldADFill;
    public string nextScene;

    public TMP_Text goText;
    // Start is called before the first frame update
    void Start()
    {
        if (PersistentManagerScript.Instance.levelIndex == 4 && SceneManager.GetActiveScene().name != "CreditsScene")
        {
            goText.fontSize = 20;
            goText.text = "GO TO CREDITS";
            nextScene = "CreditsScene";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            HoldADTime += Time.deltaTime;
            HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
            print("been holding for " + (int)HoldADTime);
            if (HoldADTime > 1f)
            {
                Change();
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            HoldADTime = 0;
            HoldADFill.transform.localScale = new Vector3(HoldADTime,1,1);
            print("been holding for " + (int)HoldADTime);
        }
    }

    public void Change()
    {
        ChangeSceneScript.ChangeScene(nextScene);
    }
}
