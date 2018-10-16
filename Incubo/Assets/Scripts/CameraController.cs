using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    private Vector3 offset;
    public CharaterSwap Charater;

	// Use this for initialization
	void Start () {
        offset = transform.position - player1.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Charater.swapped == 0)
            transform.position = player1.position + offset;
        if (Charater.swapped == 1)
            transform.position = player2.position + offset;
        if (Charater.swapped == 2)
            transform.position = player3.position + offset;
        if (Charater.swapped == 3)
            transform.position = player4.position + offset;
    }
}
