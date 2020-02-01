using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DartTip : MonoBehaviour {

    [Tooltip("Whether the dart only sticks on static objects.")]
    public bool onlyHitStatic = true;

    [Tooltip("The dart's required velocity before it will be stuck to the object.")]
    [Range(0, 10)]public float velocityMagnitudeOnFreeze = 1;

    [Tooltip("The dart's grab attack script.")]
    public DartGrabAttach dartGrabAttach;

    /**
     * Check whether dartTip exists.
     */
    private void Start()
    {
        if (dartGrabAttach == null)
        {
            Debug.LogError("DartGrabAttach is unassigned.");
        }
    }

    /**
     * When a collision is detected check whether the dart should be locked.
     */
    private void OnCollisionEnter(Collision col)
    {
        if ((!onlyHitStatic || col.gameObject.isStatic)
             && col.relativeVelocity.magnitude > velocityMagnitudeOnFreeze)
        {
            dartGrabAttach.ToggleDartFreeze(true);
        }
    }
}
