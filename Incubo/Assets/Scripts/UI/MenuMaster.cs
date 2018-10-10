using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [10-6-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A manager script for controlling a menu system based on the UIMenu object.
/// </summary>
public class MenuMaster : UIMenu {

    /// <summary>
    /// The one, always active Menu Master instance.
    /// </summary>
    public static MenuMaster menuMaster;
    /// <summary>
    /// Should this menu be hidden on startup?
    /// </summary>
    public bool startHidden;

    private void Start()
    {
        Debug.Log("the menu is being initialized");
        if (menuMaster)
        {
            Destroy(menuMaster);
            menuMaster = this;
        }
        else
            menuMaster = this;

        if (startHidden) {
            gameObject.SetActive(false);
        }

        if (transform.childCount > 0) {
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                if (child.GetComponent<UIMenu>()) {
                    openMenus.Add(child.GetComponent<UIMenu>());
                }
            }
        }
    }

    private void Update()
    {
        if (openMenus.Count == 0 && gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Toggle the menu visible/invisible.
    /// </summary>
    public void ToggleMenu() {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}