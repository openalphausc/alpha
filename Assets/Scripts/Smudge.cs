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
    }

    public SmudgeType type;
    public Material normalColorOff;
    public Material normalColorOn;
    public Material neutralizedColorOff;
    public Material neutralizedColorOn;
    public bool neutralized = false;
    public bool selected = false;

    private new MeshRenderer renderer;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
    }

    public void Select()
    {
        selected = true;
        renderer.material = neutralized ? neutralizedColorOn : normalColorOn;
    }

    public void Deselect()
    {
        selected = false;
        renderer.material = neutralized ? neutralizedColorOff : normalColorOff;
    }

    public void Neutralize()
    {
        neutralized = true;
        renderer.material = selected ? neutralizedColorOn : neutralizedColorOff;
    }

    public void Clean()
    {
        Destroy(gameObject);
    }

}
