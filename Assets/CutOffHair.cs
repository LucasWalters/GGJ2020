using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOffHair : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CutHair()
    {
        rb.useGravity = true;
    }
}
