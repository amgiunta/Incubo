using System;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// [10-6-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A managment script to handle level changing, persistence, and other backend jobs.
/// </summary>
[ExecuteInEditMode]
public class GameManager : MonoBehaviour {

    /// <summary>
    /// The single, always active GameManager instance.
    /// </summary>
    public static GameManager gameManager;

    public delegate SavedData SaveObjectData();
    public delegate void LoadObjectData();
    public static event SaveObjectData SaveGame;
    public static event LoadObjectData LoadGame;

    public static BinaryFormatter serializer = new BinaryFormatter();

    private SavedData currentSave;

    // Use this for initialization
    private void Awake()
    {
        if (gameManager) {
            Destroy(gameManager);
        }
        gameManager = this;
    }

    public Dictionary<Transform, List<Component>> GetTransformComponentHierarchy(GameObject gm) {
        Dictionary<Transform, List<Component>> hierarchy = new Dictionary<Transform, List<Component>>();
        foreach (Transform child in gm.transform) {
            KeyValuePair<Transform, List<Component>> componentList = GetComponantList(child);
            hierarchy.Add(componentList.Key, componentList.Value);
        }

        return hierarchy;
    }

    private KeyValuePair<Transform, List<Component>> GetComponantList(Transform trans) {
        List<Component> componentList = new List<Component>(trans.GetComponents<Component>());
        KeyValuePair<Transform, List<Component>> keyValuePair = new KeyValuePair<Transform, List<Component>>(trans.parent, componentList);
        return keyValuePair;
    }

    /// <summary>
    /// Loads a specific level given its name.
    /// </summary>
    /// <param name="levelName">The name of the desired level</param>
    [Obsolete("This function will be completely re-written once the savegame system is implemented.")]
    public void LoadLevel(string levelName) {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }

    public void LoadSave(string saveName) {
        //TODO: load a save and instantiate respective contents
    }

    public void Save() {
        currentSave = new SavedData();
        AsyncCallback callback = new AsyncCallback(AppendResult);

        SaveGame.BeginInvoke(callback, "invoking {0}");

        Thread.Sleep(3000);

        FileStream file = new FileStream(Application.persistentDataPath + "\\Saves\\" + currentSave.name + ".sav", FileMode.OpenOrCreate);
        serializer.Serialize(file, currentSave);
        file.Close();
    }

    private void AppendResult(IAsyncResult newData) {
        SavedData format = (SavedData) newData.AsyncState;
        currentSave = currentSave.CombineData(format);
        AsyncResult delegateResult = (AsyncResult)newData;
        SaveObjectData delegateInstance = (SaveObjectData)delegateResult.AsyncDelegate;

        Debug.Log(format + " ------ " + delegateInstance.EndInvoke(newData));
    }
}

public class SavedData {
    public string name;
    public int levelBuildIndex;
    public Dictionary<Transform, List<Component>> objectList;

    public SavedData() {
        objectList = new Dictionary<Transform, List<Component>>();
        name = "" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        levelBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public SavedData(Dictionary<Transform, List<Component>> initialList) {
        objectList = initialList;
        name = "" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        levelBuildIndex = SceneManager.GetActiveScene().buildIndex;
    } 

    public void AddList(KeyValuePair<Transform, List<Component>> newList) {
        objectList.Add(newList.Key, newList.Value);
    }

    public SavedData CombineData(SavedData other) {
        if (other == this) { return this; }

        SavedData newSavedData = new SavedData();
        foreach (KeyValuePair<Transform, List<Component>> list in objectList) {
            newSavedData.AddList(list);
        }

        foreach (KeyValuePair<Transform, List<Component>> list in other.objectList) {
            if (newSavedData.objectList.ContainsValue(list.Value)) { continue; }
            newSavedData.AddList(list);
        }

        return newSavedData;
    }
}
