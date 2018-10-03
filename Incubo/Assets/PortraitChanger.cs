using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitChanger : MonoBehaviour {
    public Sprite player1;
    public Sprite player2;
    public Sprite player3;
    public Sprite player4;
    int playernumber;
    Image Portrait;
	// Use this for initialization
	void Start () {
        playernumber = 0;
        Portrait = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
            swap();
    }
    void swap()
    {
        if (playernumber == 0)
        {
            Portrait.sprite = player2;
            playernumber++;
        }
        else if (playernumber== 1)
        {
            Portrait.sprite = player3;
            playernumber = 2;
        }
        else if (playernumber == 2)
        {
            Portrait.sprite = player4;
            playernumber = 3;
        }
        else if (playernumber == 3)
        {
            Portrait.sprite = player1;
            playernumber = 0;
        }
    }
}
