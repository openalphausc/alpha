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
    public Color normalColorOff;
    public Color normalColorOn;
    public Color neutralizedColorOff;
    public Color neutralizedColorOn;
    public float percentNeutralized = 100; // 0 means neutralized, 100 means normal (size 1), but it's a gradient
    public bool neutralized = false;
    public bool selected = false;

    private new SpriteRenderer renderer;

    private float startScale = 0.7f;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    void Update()
    {
      // green smudges grow slowly
      if(type == SmudgeType.SmudgeL && !neutralized && FloorManager.currentFloor.smudgeManager.allSmudges.Contains(this)) {
        float growPerSecond = 100f - (percentNeutralized % 100f);
        if(growPerSecond < 1f) growPerSecond = 1f;
        percentNeutralized += growPerSecond * Time.deltaTime;
      }


      // change size of smudge based on percentNeutralized
      if(percentNeutralized <= 40) percentNeutralized = 50;
      transform.localScale = new Vector3(startScale * percentNeutralized/100, startScale * percentNeutralized/100, startScale * percentNeutralized/100);
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

    public void Neutralize(int percent = 100)
    {
        percentNeutralized -= percent;
        if(percentNeutralized <= 0f) {
          percentNeutralized = 0f;
          neutralized = true;
          renderer.color = selected ? neutralizedColorOn : neutralizedColorOff;
        }
    }

    public void Clean()
    {
        Destroy(gameObject);
    }


}
