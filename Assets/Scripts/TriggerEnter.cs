using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerEnter : MonoBehaviour
{
    [Tooltip("The tag that will be checked against the trigger to see if it should fire the given event")]
    public string tagToCheck = null;
    public UnityEvent eventToTrigger;
	
    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(tagToCheck) || other.tag == tagToCheck)
        {
            eventToTrigger.Invoke();
        }
    }
}
