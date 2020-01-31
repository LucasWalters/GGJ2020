using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OculusObject : MonoBehaviour
{
	[Header("Physics Setup")]
					    public Rigidbody  body;
	[Range(0.0f, 1.0f)] public float      forceFactor       = 0.5f;
	[Range(2.0f,20.0f)] public float      resetAfterSeconds = 10.0f;

	[Header("Events")]
					    public UnityEvent onStart;
					    public UnityEvent onGrab;
					    public UnityEvent onRelease;
					    public UnityEvent onReset;

	[Header("INTERNAL")]
					    public Transform  startParent;
					    public bool       startIsKinematic;
					    public OculusGrab heldByWhat;
					    public float      resetAt           = 0.0f;
					    public bool       wasReset          = true;

	void Start() {
		startParent      = transform.parent;
		startIsKinematic = body.isKinematic;

		resetAt          = 0.0f;
		wasReset         = true;

		onStart.Invoke();
	}

	public void Grab(Transform toWhat, OculusGrab byWhat) {
		if (heldByWhat != null) {
			if (heldByWhat == byWhat)
				return;
			else
				heldByWhat.StopGrab();
		}

		ParentUnder(toWhat, true, true);
		heldByWhat              = byWhat;
		wasReset                = false;

		onGrab.Invoke();
	}

	public void Release(Vector3 force) {
		if (heldByWhat == null)
			return;

		ParentUnder(null, false, false);
		heldByWhat              = null;
		wasReset                = false;

		body.AddForce(force * 10000.0f * forceFactor);

		onRelease.Invoke();
	}

	void ParentUnder(Transform parentIs, bool isKinematicToSet, bool resetPosition) {
		transform.SetParent(parentIs);
		body.isKinematic        = isKinematicToSet;

		if (! resetPosition)
			return;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}

	void Update() {
		if (wasReset == true)
            return;

		if (heldByWhat != null)
			resetAt = Time.time + resetAfterSeconds;
		if (Time.time < resetAt)
			return;

		ParentUnder(startParent, startIsKinematic, true);
		wasReset = true;

		onReset.Invoke();
	}
}
