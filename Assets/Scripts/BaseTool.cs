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
        if (triggerHairEvent) {
            if (colliding) {
                debugMessage = "Colliding.";
                if (triggeredByPlayer) {
                    
                    if (triggerDown == false && (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.E))) {
                        triggerDown = true;
                        eventToTriggerOnEnter.Invoke();
                    }
                    if (triggerDown == true && (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyUp(KeyCode.E))) {
                        triggerDown = false;
                        eventToTriggerOnExit.Invoke();
                    }
                }
            } 
            else {
                debugMessage = "Not Colliding";
            }
        }
        else {
            if (triggeredByPlayer) {
                if (triggerDown == false && (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyDown(KeyCode.E))) {
                        triggerDown = true;
                        eventToTriggerOnEnter.Invoke();
                    }
                    if (triggerDown == true && (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) || Input.GetKeyUp(KeyCode.E))) {
                        triggerDown = false;
                        eventToTriggerOnExit.Invoke();
                }
            } 
            else {
                eventToTriggerOnEnter.Invoke();
            }
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
