using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for the Dial. Use Function "SetDialAngle" when dial needs to be changed.
/// </summary>
public class DialScript : MonoBehaviour {

    public Image dialArrow;
    [Tooltip("Angle at which the arrow if offset from straight up")]
    public float arrowOffsetAngle = -45f;
    [Tooltip("Speed at which the dial rotates")]
    public float rotateSpeed = .1f;
    float finalAngle;

	// Use this for initialization
	void Start () {
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
        StartCoroutine(SmoothRotate());
    }

    IEnumerator SmoothRotate()
    {
        if (dialArrow.transform.rotation.z < finalAngle)
        {
            while (dialArrow.transform.rotation.z < finalAngle)
            {
                dialArrow.transform.rotation = Quaternion.Slerp(dialArrow.transform.rotation, Quaternion.Euler(0, 0, finalAngle), rotateSpeed * Time.fixedDeltaTime);
                yield return null;
            }
        }
        else
        {
            while (dialArrow.transform.rotation.z > finalAngle)
            {
                dialArrow.transform.rotation = Quaternion.Slerp(dialArrow.transform.rotation, Quaternion.Euler(0, 0, finalAngle), rotateSpeed * Time.fixedDeltaTime);
                yield return null;
            }
        }
        dialArrow.transform.rotation = Quaternion.Euler(0, 0, finalAngle);
        yield return null;
    }
}
