using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour {

    [Tooltip("The tags of objects that can activate this trigger when entering it.")]
    /// <summary>
    /// The tags of objects that can activate this trigger when entering it.
    /// </summary>
    public List<string> tagMask;
    [Tooltip("The name of the level you want to load.")]
    /// <summary>
    /// The name of the level you want to load.
    /// </summary>
    public string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the object that entered the trigger has a tag in the list of valid tags...
        if (tagMask.Contains(collision.gameObject.tag))
        {
            // Try to load the desired scene from the stored build index.
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}
