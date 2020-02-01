using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseTool : MonoBehaviour
{
    public bool isTriggerBased;

    public virtual void ExecuteAction(GameObject hairObject)
    {
        Debug.Log("Method not implemented yet.");
    }
}
