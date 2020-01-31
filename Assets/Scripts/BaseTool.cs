using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BaseTool : MonoBehaviour
{
    public UnityEvent eventToTriggerOnEnter;
    public UnityEvent eventToTriggerOnExit;
    public string tagToMatch;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == tagToMatch)
        {
            eventToTriggerOnEnter.Invoke();
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == tagToMatch)
        {
            eventToTriggerOnExit.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagToMatch)
        {
            eventToTriggerOnEnter.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tagToMatch)
        {
            eventToTriggerOnExit.Invoke();
        }
    }

}
