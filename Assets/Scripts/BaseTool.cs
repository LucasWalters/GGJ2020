using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BaseTool : MonoBehaviour
{
    public bool triggerHairEvent = true;
    public UnityEvent eventToTriggerOnEnter;
    public UnityEvent eventToTriggerOnExit;
    public string tagToMatch;

    void OnCollisionEnter(Collision coll)
    {
        CheckAndTriggerEvents(coll.gameObject, true);
    }
    void OnCollisionExit(Collision coll)
    {
        CheckAndTriggerEvents(coll.gameObject, false);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckAndTriggerEvents(other.gameObject, true);
    }
    void OnTriggerExit(Collider other)
    {
        CheckAndTriggerEvents(other.gameObject, false);
    }

    private void CheckAndTriggerEvents(GameObject collidedGO, bool enter)
    {
        if (collidedGO.tag == tagToMatch)
        {
            eventToTriggerOnExit.Invoke();

            if (enter && triggerHairEvent)
            {
                Hair hair = collidedGO.GetComponent<Hair>();
                if (hair != null)
                {
                    hair.hairEvent.Invoke();
                }
            }
        }
    }

}
