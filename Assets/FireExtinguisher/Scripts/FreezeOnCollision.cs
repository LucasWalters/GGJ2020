using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FreezeOnCollision : MonoBehaviour {

    [Tooltip("Whether it will only freeze on static objects.")]
    public bool onlyHitStatic = true;

    [Tooltip("Whether the object should move to the collision position on hit.")]
    public bool moveToCollisionPoint = true;

    [Tooltip("The required velocity before it will be stuck to the object.")]
    [Range(0, 10)]public float velocityMagnitudeOnFreeze = 1;

    [Tooltip("The rigidbody that will be frozen.")]
    public Rigidbody rBody;

    /**
     * When a collision is detected check whether the object should be frozen.
     */
    private void OnCollisionEnter(Collision col)
    {
        if ((!onlyHitStatic || col.gameObject.isStatic) 
             && col.gameObject.tag != "FoamBall"
             && col.relativeVelocity.magnitude > velocityMagnitudeOnFreeze)
        {
            // rBody.constraints = RigidbodyConstraints.FreezeAll;
            rBody.isKinematic = true;
            if (moveToCollisionPoint)
            {
                transform.position = col.contacts[0].point;
            }
        }
    }
}
