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
    private string debugMessage = "";

    void OnCollisionEnter(Collision coll)
    {
        colliding = true;
    }
    void OnCollisionExit(Collision coll)
    {
        eventToTriggerOnExit.Invoke();
        colliding = false;
    }

    void OnTriggerEnter(Collider other)
    {
        colliding = true;
    }
    void OnTriggerExit(Collider other)
    {
        eventToTriggerOnExit.Invoke();
        colliding = false;
    }

    void Update() {
        if (colliding) {
            debugMessage = "Colliding.";
            if (triggeredByPlayer) {
                
                if (triggerDown == false && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f) {
                    triggerDown = true;
                    debugMessage += " Triggered";
                    eventToTriggerOnEnter.Invoke();
                }
                if (triggerDown == true && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.5f) {
                    triggerDown = false;
                    eventToTriggerOnExit.Invoke();
                }
            }
            else {
                eventToTriggerOnEnter.Invoke();
            }
        }
        else {
            debugMessage = "Not Colliding";
        }
        QuestDebug.Instance.Log(debugMessage);
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
