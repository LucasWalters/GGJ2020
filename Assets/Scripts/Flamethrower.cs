using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
    }

    public void StartFlame() {
        coll.enabled = true;
    }

    public void StopFlame() {
        coll.enabled = false;
    }

    public void OnTriggerEnter(Collider coll) {
        // I set firreeeeeeeee to the hair
        // Watched it pour as I touched your face
        // Let it burn while I cried
        //'Cause I heard it screaming out your name, your name
    }

}
