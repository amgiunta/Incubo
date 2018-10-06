using System;
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

    // Use this for initialization
    private void Awake()
    {
        if (gameManager) {
            Destroy(gameManager);
        }
        gameManager = this;
    }

    /// <summary>
    /// Loads a specific level given its name.
    /// </summary>
    /// <param name="levelName">The name of the desired level</param>
    [Obsolete("This function will be completely re-written once the savegame system is implemented.")]
    public void LoadLevel(string levelName) {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }
}
