using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public GameObject stampToClone;
    public float stampCooldown = 1f;
    public int maxStamps = 100;
    public float nextLevelDelay = 2f;
    public LevelManager levelManager;
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

    private IEnumerator DelayedGoToNextLevel()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        levelManager.GoToNextLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Customer" || Time.time - lastStamp < stampCooldown)
        {
            return;
        }
        if (levelManager != null)
        {
            StopAllCoroutines();
            StartCoroutine(DelayedGoToNextLevel());
        }
        lastStamp = Time.time;
        ContactPoint cp = collision.contacts[0];
        if (++currentStampIndex == stamps.Length)
        {
            currentStampIndex = 0;
        }
        stamps[currentStampIndex].transform.position = cp.point;
        stamps[currentStampIndex].transform.rotation = Quaternion.LookRotation(cp.normal, stampToClone.transform.up);
        stamps[currentStampIndex].transform.parent = collision.transform;
        stamps[currentStampIndex].SetActive(true);
    }
}
