using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public GameObject stampToClone;
    public float stampCooldown = 1f;
    private float lastStamp;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastStamp < stampCooldown)
        {
            return;
        }
        lastStamp = Time.time;
        ContactPoint cp = collision.contacts[0];
        Instantiate(stampToClone, cp.point, Quaternion.LookRotation(cp.normal, stampToClone.transform.up));
    }
}
