using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hair : MonoBehaviour
{
    private enum Method
    {
        Shrink,
        Cut,
        Remove
    }

    public GameObject nextHair;

    [SerializeField] private Method method;

    BaseTool tool;

    public UnityAction GetMethod()
    {
        switch (method)
        {
            case Method.Shrink:
                {
                    return Shrink;
                }
            case Method.Cut:
                {
                    return Cut;
                }
            case Method.Remove:
                {
                    return Remove;
                }
        }
        return null;
    }

    public void Shrink()
    {
        if (nextHair != null)
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
            nextHair.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void Cut()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
    }

    public void Remove()
    {
        Debug.Log(this.gameObject.name);
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
