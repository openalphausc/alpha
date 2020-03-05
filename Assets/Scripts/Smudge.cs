using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smudge : MonoBehaviour
{
    public enum SmudgeType
    {
        smudgeJ,
        smudgeK,
        smudgeL,
    }

    public SmudgeType type;
    public Material normalColorOff;
    public Material normalColorOn;
    public Material neutralizedColorOff;
    public Material neutralizedColorOn;
    public bool neutralized = false;
    public bool selected = false;

    private new MeshRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        SmudgeManager.allSmudges.Add(this);
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
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
