using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public GameObject stampToClone;
    public float stampCooldown = 1f;
    public int maxStamps = 100;
    private GameObject[] stamps;
    private int currentStampIndex = 0;
    private float lastStamp;

    private void Start()
    {
        stamps = new GameObject[maxStamps];
        for (int i = 0; i < maxStamps; i++)
        {
            stamps[i] = Instantiate(stampToClone);
            stamps[i].SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastStamp < stampCooldown)
        {
            return;
        }
        lastStamp = Time.time;
        ContactPoint cp = collision.contacts[0];
        if (++currentStampIndex == stamps.Length)
        {
            currentStampIndex = 0;
        }
        stamps[currentStampIndex].transform.position = cp.point;
        stamps[currentStampIndex].transform.rotation = Quaternion.LookRotation(cp.normal, stampToClone.transform.up);
        stamps[currentStampIndex].SetActive(true);
    }
}
