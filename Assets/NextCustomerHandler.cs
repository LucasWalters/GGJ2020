using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCustomerHandler : MonoBehaviour {

    public GameObject chair;
    private BasicTransformAnimation animationScript;

    public void Start () {
        animationScript = chair.GetComponent<BasicTransformAnimation> ();
    }

    public void Update () {
        if (Input.GetKeyDown (KeyCode.X)) {
            animationScript.StartAnimation ();
        } else if (Input.GetKeyDown (KeyCode.Y)) {
            animationScript.StopAnimation ();
        }
    }
}