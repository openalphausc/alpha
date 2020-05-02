using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cityscape : MonoBehaviour
{
    private float elapsedTime = 0f;
    public float darkTime;

    public SpriteRenderer sprite;

    private float finalRed = 36f;
    private float finalGreen = 36f;
    private float finalBlue = 73f;

    // Start is called before the first frame update
    void Start()
    {
      sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      if(elapsedTime < darkTime) elapsedTime += Time.deltaTime;
      else return;

      float t = elapsedTime/darkTime;
      float ratio = t*t*t*t; // model a t^4 curve in approaching darkness
      // Debug.Log(ratio);
      float red = 1f - ratio*(1 - finalRed/255);
      // Debug.Log("red = " + red);
      float green = 1f - ratio*(1 - finalGreen/225);
      float blue = 1f - ratio*(1 - finalBlue/255);
      sprite.color = new Color(red, green, blue);
    }
}
