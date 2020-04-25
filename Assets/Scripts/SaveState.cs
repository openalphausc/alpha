using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
  public int levelIndex = 0;
  public int money = 0;
  public List<string> inventory = new List<string>();
}
