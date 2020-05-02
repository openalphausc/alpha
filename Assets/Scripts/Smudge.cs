using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smudge : MonoBehaviour
{
    public enum SmudgeType
    {
        SmudgeJ,
        SmudgeK,
        SmudgeL,
        SmudgeNone,
    }

    public SmudgeType type;
    public GameObject sprite;
    public Color normalColorOff;
    public Color normalColorOn;
    public Color neutralizedColorOff;
    public Color neutralizedColorOn;
    public float percentNeutralized = 100f; // 0 means neutralized, 100 means normal (size 1), but it's a gradient
    public bool neutralized = false;
    public bool selected = false;
    public GameObject FloatingTextPrefab;

    private new SpriteRenderer renderer;

    private Vector3 SmudgePosition;
    private Quaternion SmudgeRotation;

    private float startScale = 0.7f;

    GameObject helpUI;

    void Start()
    {
        renderer = sprite.GetComponent<SpriteRenderer>();
        string letter;
        switch (type)
        {
            case SmudgeType.SmudgeJ:
                letter = "J";
                break;
            case SmudgeType.SmudgeK:
                letter = "K";
                break;
            case SmudgeType.SmudgeL:
                letter = "L";
                break;
            default:
                letter = "None";
                break;
        }

        string filename = "Sprites/Smudge" + letter + "/Sprite" + letter + "0"; //PersistentManagerScript.Instance.levelIndex;

        // print(filename);
        renderer.sprite = Resources.Load<Sprite>(filename);

        transform.localScale = new Vector3(startScale, startScale, startScale);

        SmudgePosition = gameObject.transform.position;
        SmudgeRotation = gameObject.transform.rotation;
        // Scene currentScene = SceneManager.GetActiveScene();
        // if(currentScene.name == "TutorialScene") {
        ShowFloatingText();
        // }
    }

    void Update()
    {
      // green smudges grow slowly
      if(type == SmudgeType.SmudgeL && !neutralized && FloorManager.currentFloor.smudgeManager.allSmudges.Contains(this) && percentNeutralized < 400f) {
        float growPerSecond = 100f - (percentNeutralized % 100f);
        if(growPerSecond < 1f) growPerSecond = 1f;
        percentNeutralized += growPerSecond * Time.deltaTime;
      }


      // change size of smudge based on percentNeutralized
      float neutralizeScale = percentNeutralized;
      if(neutralizeScale < 50) neutralizeScale = 50;
      transform.localScale = new Vector3(startScale * neutralizeScale/100, startScale * neutralizeScale/100, transform.localScale.z);
    }

    void ShowFloatingText()
    {
        helpUI = Instantiate(FloatingTextPrefab, SmudgePosition, SmudgeRotation, gameObject.transform);
        helpUI.transform.position += new Vector3(0, 0, 0.5f);
        string helperText = "";
        switch(this.type) {
            case SmudgeType.SmudgeJ :
                helperText = "J";
                break;
            case SmudgeType.SmudgeK :
                helperText = "K";
                break;
            case SmudgeType.SmudgeL :
                helperText = "L";
                break;
            case SmudgeType.SmudgeNone :
                break;
        }
        helpUI.GetComponent<TextMesh>().text = helperText;
    }

    void HideFloatingText() {
      helpUI.GetComponent<TextMesh>().text = "";
    }

    public void Select()
    {
        selected = true;
        renderer.color = neutralized ? neutralizedColorOn : normalColorOn;
    }

    public void Deselect()
    {
        selected = false;
        renderer.color = neutralized ? neutralizedColorOff : normalColorOff;
    }

    public void Neutralize(int percent)
    {
        percentNeutralized -= percent;
        if(percentNeutralized <= 0f) {
          percentNeutralized = 0f;
          neutralized = true;
          renderer.color = selected ? neutralizedColorOn : neutralizedColorOff;
          HideFloatingText();
        }
    }

    public void Clean()
    {
        Destroy(gameObject);
    }
}
