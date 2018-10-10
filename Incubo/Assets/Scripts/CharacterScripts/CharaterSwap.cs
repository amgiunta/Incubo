using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterSwap : MonoBehaviour {

    public int swapped = 0;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
       if(Input.GetKeyDown("r"))
            swap();
    }
    void swap()
    {
        if (swapped == 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).transform.position = gameObject.transform.GetChild(0).transform.position;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            swapped++;
        }
        else if (swapped == 1)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).transform.position = gameObject.transform.GetChild(1).transform.position;
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            swapped = 2;
        }
        else if (swapped == 2)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).transform.position = gameObject.transform.GetChild(2).transform.position;
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
            swapped = 3;
        }
        else if (swapped == 3)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).transform.position = gameObject.transform.GetChild(3).transform.position;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            swapped = 0;
        }
    }
}
