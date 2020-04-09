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
    public int percentNeutralized = 0; // 0 means not neutralized, 100 means neutralized, but it's a gradient
    public bool neutralized = false;
    public bool selected = false;

    private new SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

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
        percentNeutralized += percent;
        if(percentNeutralized >= 100) {
          percentNeutralized = 100;
          neutralized = true;
          renderer.color = selected ? neutralizedColorOn : neutralizedColorOff;
        }
    }

    public void Clean()
    {
        Destroy(gameObject);
    }


}
