using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trimmer : MonoBehaviour
{
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Hair")
        {
            coll.gameObject.SetActive(false);
        }
    }
}
