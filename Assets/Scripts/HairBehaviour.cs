using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider coll)
    {
        Debug.Log("OnTriggerEnter");
        this.GetComponent<MeshRenderer>().material.color = Color.green;
        if (coll.gameObject.tag.ToLower() == "tool")
        {
            coll.GetComponent<MeshRenderer>().material.color = Color.red;
            if (coll.GetComponent<BaseTool>().isTriggerBased)
            {
                coll.GetComponent<BaseTool>().ExecuteAction(this.gameObject);
            }
        }
    }
}
