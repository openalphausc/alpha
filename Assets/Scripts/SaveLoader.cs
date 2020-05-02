using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoader
{

  private static SaveState CreateSaveState() {
    SaveState save = new SaveState();
    save.levelIndex = PersistentManagerScript.Instance.levelIndex;
    save.money = PersistentManagerScript.Instance.money;
    foreach (Item item in PersistentManagerScript.Instance.inventory) {
      save.inventory.Add(item.itemName);
    }
    return save;
  }

  public static void SaveGame() {
    SaveState save = CreateSaveState();

    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
    bf.Serialize(file, save);
    file.Close();

    Debug.Log("Game Saved");
  }

  public static void LoadGame() {
    if (File.Exists(Application.persistentDataPath + "/gamesave.save")) {
      // clear current data
      PersistentManagerScript.Instance.levelIndex = 0;
      PersistentManagerScript.Instance.money = 0;
      PersistentManagerScript.Instance.inventory = new List<Item>();

      // load SaveState from file
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
      SaveState save = (SaveState)bf.Deserialize(file);
      file.Close();

      // load SaveState data into actual data
      PersistentManagerScript.Instance.levelIndex = save.levelIndex;
      PersistentManagerScript.Instance.money = save.money;
      foreach (string name in save.inventory) {
        Item item = Resources.Load<Item>("Items/" + name);
        PersistentManagerScript.Instance.inventory.Add(item);
        Debug.Log(" data loaded");
      }

      Debug.Log("Game Loaded");
    }
    else {
      Debug.Log("No game saved!");
    }
  }

}
