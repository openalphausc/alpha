using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public SmudgeManager smudgeManager; // access this smudgeManager (already linked in editor)

    private float dirtiness = 1; //should be from 1 to 0, starts out dirty
    private SpriteRenderer windowSprite;
    
    void Start()
    {
        windowSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckSmudges();
    }

    void CheckSmudges()
    {
        Color windowColor = windowSprite.color;
        windowSprite.color = new Color(windowColor.r, windowColor.g, windowColor.b, smudgeManager.Progress);
    }
}
