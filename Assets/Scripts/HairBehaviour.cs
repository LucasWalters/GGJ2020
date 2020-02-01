using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTriggerEnter");
        this.GetComponent<MeshRenderer>().material.color = Color.green;

        QuestDebug.Instance.Log(collider.name + " Tag: " + collider.tag);

        if (collider.tag.ToLower() == "tool")
        {
            collider.GetComponent<MeshRenderer>().material.color = Color.red;
            // if (collider.GetComponent<BaseTool>().isTriggerBased)
            // {
            //     collider.GetComponent<BaseTool>().ExecuteAction(this.gameObject);
            // }
        }
    }
}
