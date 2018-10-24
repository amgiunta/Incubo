using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {
   public void loadLevel() {
        SceneManager.LoadScene(1);
    }
   public void QuitGame() {
        Application.Quit();
    }

}
