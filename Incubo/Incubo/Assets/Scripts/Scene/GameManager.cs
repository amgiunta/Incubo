using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// [10-6-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A managment script to handle level changing, persistence, and other backend jobs.
/// </summary>
public class GameManager : MonoBehaviour {

    /// <summary>
    /// The single, always active GameManager instance.
    /// </summary>
    public static GameManager gameManager;
    public static UnityEvent OnSave;
    public static BinaryFormatter serializer = new BinaryFormatter();
    private static SavedData currentSave;

    // Use this for initialization
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if (OnSave == null) {
            OnSave = new UnityEvent();
        }

        Time.timeScale = 1;
    }

    private void Start()
    {
        currentSave = new SavedData(SceneManager.GetActiveScene().buildIndex);

        Directory.CreateDirectory(GetSaveDirectory());
    }

    public string GetSaveDirectory() {
        return Application.persistentDataPath + "\\Saves";
    }

    /// <summary>
    /// Loads a specific level given its name.
    /// </summary>
    /// <param name="levelName">The name of the desired level</param>
    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void LoadLevel(int buildIndex) {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void LoadSave(string saveName) {
        string filePath = GetSaveDirectory() + "\\" + saveName + ".sav";
        if (!File.Exists(filePath)) { Debug.LogError("The filepath " + filePath + " does not exist. " + saveName + ".sav did not load."); return; }

        FileStream file = File.Open(filePath, FileMode.Open);
        currentSave = (SavedData) serializer.Deserialize(file);
        file.Close();
        if (currentSave == null) { Debug.LogError("An error occured loading the save " + saveName + ".sav. The save was not loaded."); return; }

        gameManager.StartCoroutine(LoadLevelEnum(currentSave.currentLevel, FinishLoad));
    }

    public void LoadLastSave() {
        DirectoryInfo dirInfo = new DirectoryInfo(GetSaveDirectory());
        var files = dirInfo.GetFiles().OrderBy(f => f.LastWriteTime).ToList();

        if (files.Count < 1) { return; }
        string name = files[files.Count - 1].Name;
        name = (name.Split('.'))[0];
        LoadSave(name);
    }

    public void Save() {
        currentSave = new SavedData(SceneManager.GetActiveScene().buildIndex);

        if (OnSave != null) {
            OnSave.AddListener(FinishSave);
            Debug.Log("Saving the game...");
            OnSave.Invoke();
        }
    }

    public void AddToSavedData(Character character) {
        currentSave.AddObject(character);
    }

    private void FinishSave() {
        Debug.Log("Should add " + currentSave.objectList.Count + " objects to the saved data");
        string path = GetSaveDirectory() + "\\" + currentSave.name + ".sav";

        FileStream file = File.Open(path, FileMode.OpenOrCreate);
        serializer.Serialize(file, currentSave);
        file.Close();

        Debug.Log("The file was successfully saved at the path " + path);
        OnSave.RemoveAllListeners();
    }

    private void FinishLoad() {
        SaveableObject[] oldObjects = FindObjectsOfType<SaveableObject>();
        Debug.LogWarning("there are " + oldObjects.Length + " old objects in the scene");
        foreach (SaveableObject oldObject in oldObjects)
        {
            Destroy(oldObject.gameObject);
        }

        Debug.Log("there are " + currentSave.objectList.Count + " objects to create");
        foreach (SavedData.SavedObject s_obj in currentSave.objectList)
        {
            GameObject newObj = Instantiate<GameObject>(s_obj.LoadData());
            newObj.name = s_obj.prefabName;

            //This may need to be changed if PlayerDamageController is not the default class for players
            if (newObj.CompareTag("Player")) {

            }
        }
    }

    private IEnumerator LoadLevelEnum(int buildIndex, UnityAction callback = null) {
        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
        if (callback != null) { callback(); }
    }

    private IEnumerator LoadLevelEnum(string sceneName, UnityAction callback = null)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        if (callback != null) { callback(); }
    }
}

[Serializable]
public class SavedData
{
    public string name;
    public int currentLevel;
    public List<SavedObject> objectList = new List<SavedObject>();

    [Serializable]
    public class SavedObject
    {
        public SerializableCharacter characterInfo;
        public SerializableTransform transform;
        public string prefabName;

        public SavedObject()
        {
            characterInfo = new SerializableCharacter();
            transform = new SerializableTransform();
            prefabName = "";
        }

        public SavedObject(Character characterInfo, Transform transform, string prefabName)
        {
            this.characterInfo = characterInfo;
            this.transform = transform;
            this.prefabName = prefabName;
        }

        public SavedObject SaveObject(Character characterInfo)
        {
            SavedObject newData = new SavedObject(characterInfo, characterInfo.transform, characterInfo.gameObject.name);
            return newData;
        }

        public GameObject LoadData()
        {
            GameObject newObj = Resources.Load<GameObject>("Prefabs\\" + prefabName);
            newObj.transform.position = transform.position;
            newObj.transform.rotation = transform.rotation;
            newObj.transform.localScale = transform.localScale;

            Character objectCharacter = newObj.GetComponent<Character>();
            objectCharacter.maxFear = characterInfo.maxFear;
            objectCharacter.currentFear = characterInfo.currentFear;
            objectCharacter.baseDamage = characterInfo.baseDamage;
            objectCharacter.damageMultiplier = characterInfo.damageMultiplier;
            objectCharacter.fearMultiplier = characterInfo.fearMultiplier;

            return newObj;
        }

        public static explicit operator SavedObject(Character characterInfo) {
            SavedObject s_obj = new SavedObject();
            s_obj = s_obj.SaveObject(characterInfo);
            return s_obj;
        }
    }

    public SavedData() { name = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year; }

    public SavedData(int currentLevel) {
        this.currentLevel = currentLevel;
        name = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
    }

    public SavedData(SavedData other) {
        currentLevel = other.currentLevel;
        objectList = other.objectList;
        name = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
    }

    public void AddObject(Character character) {
        objectList.Add((SavedObject)character);
    }

    public void AddObject(SavedObject s_obj) {
        objectList.Add(s_obj);
    }

    public void RemoveObject(Character character) {
        objectList.Remove((SavedObject)character);
    }

    public void RemoveObject(SavedObject s_obj)
    {
        objectList.Remove(s_obj);
    }
}

[Serializable]
public class SerializableTransform {
    [Serializable]
    public class SerializableVector3 {
        public float x;
        public float y;
        public float z;

        public SerializableVector3() {
            x = 0;
            y = 0;
            z = 0;
        }

        public SerializableVector3(Vector3 old) {
            x = old.x;
            y = old.y;
            z = old.z;
        }

        public static implicit operator Vector3(SerializableVector3 other) {
            Vector3 vec = new Vector3();
            vec.x = other.x;
            vec.y = other.y;
            vec.z = other.z;
            return vec;
        }
    }
    [Serializable]
    public class SerializableQuaternion {
        float w;
        float x;
        float y;
        float z;

        public SerializableQuaternion() {
            w = 0;
            x = 0;
            y = 0;
            z = 0;
        }

        public SerializableQuaternion(Quaternion old) {
            w = old.w;
            x = old.x;
            y = old.y;
            z = old.z;
        }

        public static implicit operator Quaternion(SerializableQuaternion other) {
            Quaternion quat = new Quaternion();
            quat.w = other.w;
            quat.x = other.x;
            quat.y = other.y;
            quat.z = other.z;
            return quat;
        }
    }

    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public SerializableVector3 localScale;

    public SerializableTransform() {
        position = new SerializableVector3();
        rotation = new SerializableQuaternion();
        localScale = new SerializableVector3();
    }

    public SerializableTransform(Transform old) {
        position = new SerializableVector3(old.position);
        rotation = new SerializableQuaternion(old.rotation);
        localScale = new SerializableVector3(old.localScale);
    }

    public static implicit operator SerializableTransform(Transform other) {
        SerializableTransform s_Transform = new SerializableTransform(other);
        return s_Transform;
    }

    public static implicit operator Transform(SerializableTransform other) {
        Transform newTrans = new GameObject().transform;
        newTrans.position = other.position;
        newTrans.rotation = other.rotation;
        newTrans.localScale = other.localScale;

        return newTrans;
    }
}