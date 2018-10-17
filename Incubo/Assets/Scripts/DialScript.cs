using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for the Dial. Use Function "SetDialAngle" when dial needs to be changed.
/// </summary>
public class DialScript : MonoBehaviour {

    public static DialScript dialScript;

    private void Awake()
    {
        if(dialScript == null) { dialScript = this; }
        else if(dialScript != null) { Destroy(gameObject); }
    }

    Image dialArrow;
    [Tooltip("Angle at which the arrow if offset from straight up")]
    public float arrowOffsetAngle = -45f;
    [Tooltip("Speed at which the dial rotates")]
    public float rotateSpeed = .1f;
    float finalAngle;

    bool rotateRunning;

	// Use this for initialization
	void Start () {
        dialArrow = GameObject.Find("DialArrow").GetComponent<Image>();
        rotateRunning = false;
        SetDialAngle(0f);
	}

    /// <summary>
    ///  Sets the Dial Angle to a float between 0 and 180. Clamps Values to posssable range between 0 and 180.
    /// </summary>
    /// <param name="angle"></param>
    public void SetDialAngle(float angle)
    {
        angle = -(Mathf.Clamp(angle, 0f, 180f)); //Clamps angle to possable values of dial
        angle += arrowOffsetAngle; //Sets angle to final angle;
        
        finalAngle = angle;
        if(!rotateRunning) { StartCoroutine(SmoothRotate()); }
    }

    IEnumerator SmoothRotate()
    {
        rotateRunning = true;

        while (dialArrow.transform.rotation.z != finalAngle)
        {
            while (dialArrow.transform.rotation.z < finalAngle)
            {
                dialArrow.transform.rotation = Quaternion.Slerp(dialArrow.transform.rotation, Quaternion.Euler(0, 0, finalAngle), rotateSpeed * Time.fixedDeltaTime);
                yield return new WaitForEndOfFrame();
            }
            while (dialArrow.transform.rotation.z > finalAngle)
            {
                dialArrow.transform.rotation = Quaternion.Slerp(dialArrow.transform.rotation, Quaternion.Euler(0, 0, finalAngle), rotateSpeed * Time.fixedDeltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        dialArrow.transform.rotation = Quaternion.Euler(0, 0, finalAngle);
        rotateRunning = false;
        yield return null;
    }
}
