using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuitButton : CustomButton {

    // Use this for initialization
    protected override void Start()
    {
        clickEvent = _QuitAction;
        base.Start();
    }

    public void _QuitAction() {
        GameManager.gameManager.LoadLevel("mainMenu");
        return;
    }
}
