using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public GameObject prickToClone;
    public float prickCooldown = 1f;
    public int maxPricks = 100;
    private GameObject[] pricks;
    private int currentPrickIndex = 0;
    private float lastPrick;

    private void Start()
    {
        pricks = new GameObject[maxPricks];
        for (int i = 0; i < maxPricks; i++)
        {
            pricks[i] = Instantiate(prickToClone);
            pricks[i].SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastPrick < prickCooldown)
        {
            return;
        }
        lastPrick = Time.time;
        ContactPoint cp = collision.contacts[0];
        if (++currentPrickIndex == pricks.Length)
        {
            currentPrickIndex = 0;
        }
        pricks[currentPrickIndex].transform.position = cp.point;
        pricks[currentPrickIndex].transform.rotation = Quaternion.LookRotation(cp.normal);
        pricks[currentPrickIndex].SetActive(true);
    }
}
