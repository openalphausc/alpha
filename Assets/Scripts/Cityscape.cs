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

      float red = 255f - ((255f - finalRed) * elapsedTime/darkTime);
      float green = 255f - ((255f - finalGreen) * elapsedTime/darkTime);
      float blue = 255f - ((255f - finalBlue) * elapsedTime/darkTime);
      sprite.color = new Color(red/255, green/255, blue/255);
    }
}
