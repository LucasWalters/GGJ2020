using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LaunchObject : MonoBehaviour {

	[Tooltip("The launch force of the object.")]
	public float force = 5f;
	Rigidbody rb;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void LaunchAt(Transform target)
	{
        rb.isKinematic = false;
		rb.AddForce((target.position - transform.position) * force);
	}
}
