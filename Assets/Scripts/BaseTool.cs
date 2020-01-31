using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseTool : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public string tagToMatch;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == tagToMatch)
        {
            eventToTrigger.Invoke();
        }
    }
}
