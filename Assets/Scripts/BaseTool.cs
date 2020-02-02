using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BaseTool : MonoBehaviour
{
    public bool triggerHairEvent = true;
    public bool triggeredByPlayer = false;
    public UnityEvent eventToTriggerOnEnter;
    public UnityEvent eventToTriggerOnExit;
    public string tagToMatch = "Hair";

    private bool triggerDown, colliding;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == tagToMatch)
        {
            coll.gameObject.GetComponent<Hair>().GetMethod().Invoke();
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

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == tagToMatch)
        {
            coll.gameObject.GetComponent<Hair>().GetMethod().Invoke();
            eventToTriggerOnEnter.Invoke();
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == tagToMatch)
        {
            eventToTriggerOnExit.Invoke();
        }
    }

    void Update()
    {
        /*if (triggerHairEvent)
        {
            if (colliding)
            {
                if (triggeredByPlayer)
                {

                    if (triggerDown == false && (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.E)))
                    {
                        triggerDown = true;
                        eventToTriggerOnEnter.Invoke();
                        Debug.Log("Invoked Enter");
                    }
                    if (triggerDown == true && (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyUp(KeyCode.E)))
                    {
                        triggerDown = false;
                        eventToTriggerOnExit.Invoke();
                        Debug.Log("Invoked Exit");
                    }
                }
            }
        }
        else
        {
            if (triggeredByPlayer)
            {
                if (triggerDown == false && (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.E)))
                {
                    triggerDown = true;
                    eventToTriggerOnEnter.Invoke();
                    Debug.Log("Invoked Enter");
                }
                if (triggerDown == true && (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyUp(KeyCode.E)))
                {
                    triggerDown = false;
                    eventToTriggerOnExit.Invoke();
                    Debug.Log("Invoked Exit");
                }
            }
        }*/
    }

    // private void CheckAndTriggerEvents(GameObject collidedGO, bool enter)
    // {
    //     if (collidedGO.tag == tagToMatch)
    //     {
    //         if (triggeredByPlayer) {

    //         }
    //         if (enter) {
    //             eventToTriggerOnEnter.Invoke();
    //         } else {
    //             eventToTriggerOnExit.Invoke();
    //         }


    //     }
    // }

}
