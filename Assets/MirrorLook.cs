using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLook : MonoBehaviour
{
    public Transform mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 diff = (mainCamera.position - transform.position).normalized;
        Vector3 lookDirection = new Vector3(diff.x, -diff.y, -diff.z);

        transform.localRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}
