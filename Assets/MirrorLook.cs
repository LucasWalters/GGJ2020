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
        if (Camera.current == null)
        {
            return;
        }
        mainCamera = Camera.current.transform;
        Vector3 diff = mainCamera.position - transform.position;
        //transform.position = transform.position + new Vector3
        Vector3 lookDirection = new Vector3(diff.x, diff.y, -diff.z);

        transform.localRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}
