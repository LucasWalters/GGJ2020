using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hair : MonoBehaviour
{
    public void Shrink()
    {
        this.transform.localScale = new Vector3(
                this.transform.localScale.x * 0.9f,
                this.transform.localScale.y * 0.9f,
                this.transform.localScale.z * 0.9f
            );
    }

    public void Cut()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
    }

    public void Remove()
    {
        this.gameObject.SetActive(false);
    }
}
