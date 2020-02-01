using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DartGrabAttach : MonoBehaviour {

    /**
     * The rotational pitch of the dart when it is grabbed.
     */
    public float dartPitch = -10;

    private Rigidbody rb;

    /**
     * Get the rigidbody on start
     */
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /**
     * Toggles whether the dart should be frozen in place.
     */
    public void ToggleDartFreeze(bool freeze)
    {
        Debug.Log("Dart frozen = " + freeze);
        rb.constraints = freeze ?
                RigidbodyConstraints.FreezeAll :
                RigidbodyConstraints.FreezeRotation;
    }
    

    /**
     * Overrides the StartGrab method to unfreeze the dart when picked up.
     */
    public void StartGrab()
    {
        transform.localEulerAngles = new Vector3(dartPitch, 0, -90);
        ToggleDartFreeze(false);
    }
    
}
