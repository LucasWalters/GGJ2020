using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Hair")
        {
            coll.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
