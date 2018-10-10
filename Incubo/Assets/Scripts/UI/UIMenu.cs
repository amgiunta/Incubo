using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// [10-6-18] Adam Giunta <amgiunta.2016@mymail.becker.edu>
/// <summary>
/// A base class for all UI menus to use in the game.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIMenu : MonoBehaviour {
    /// <summary>
    /// Does this menu pause the game when it is opened?
    /// </summary>
    [Tooltip("Does this menu pause the game when it is opened?")]
    public bool pauseMenu;

    /// <summary>
    /// The rect transform component on the object.
    /// </summary>
    protected RectTransform rect;
    /// <summary>
    /// A list of all open menus that are a child to this one.
    /// </summary>
    protected List<UIMenu> openMenus = new List<UIMenu>();
    /// <summary>
    /// The parent menu object to this menu.
    /// </summary>
    protected UIMenu parent;

    private void Awake()
    {
        if (pauseMenu) { Time.timeScale = 0; }
        rect = GetComponent<RectTransform>();

        parent = transform.parent.GetComponent<UIMenu>();
    }

    /// <summary>
    /// Opens a menu as a child to this one given its name.
    /// </summary>
    /// <param name="menuName">The name of the desired menu</param>
    public void OpenMenu(string menuName) {
        if (GetOpenMenu(menuName)) { CloseMenu(menuName); }

        GameObject menuObject = Resources.Load<GameObject>("Menus/" + menuName);
        menuObject = Instantiate(menuObject, transform);
        menuObject.name = menuName;
        openMenus.Add(menuObject.GetComponent<UIMenu>());
    }

    /// <summary>
    /// Closes a menu that is a child to this one given its name.
    /// </summary>
    /// <param name="menuName">The name of a menu</param>
    public void CloseMenu(string menuName) {
        foreach (UIMenu menu in openMenus) {
            if (menu.gameObject.name == menuName) {
                CloseMenu(menu);
                return;
            }
        }
    }

    /// <summary>
    /// Closes the menu that is a child of this menu.
    /// </summary>
    /// <param name="menu">The desired menu to close</param>
    public void CloseMenu(UIMenu menu)
    {
        if (openMenus.Contains(menu)) {
            openMenus.Remove(menu);
            Destroy(menu);
            return;
        }   
    }

    /// <summary>
    /// Closes this menu on its parent menu.
    /// </summary>
    public void CloseThisMenu() {
        parent.CloseMenu(this);
    }

    /// <summary>
    /// Get the UIMenu object of the open menu given its name.
    /// </summary>
    /// <param name="menuName">Name of desired menu</param>
    /// <returns></returns>
    public UIMenu GetOpenMenu(string menuName) {
        foreach (UIMenu menu in openMenus) {
            if (menu.gameObject.name == menuName) { return menu; }
        }
        return null;
    }

    private void OnDestroy()
    {
        if (pauseMenu) { Time.timeScale = 1; }
        Destroy(gameObject);
    }
}
