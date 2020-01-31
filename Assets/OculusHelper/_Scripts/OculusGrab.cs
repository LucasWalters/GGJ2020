using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Orientation {
	[Header("Matching Setup")]
					    public string    tagIs;
					    public Transform parentBelow;

	[Header("Hand Setup")]
						public bool      isPointing    = false;
						public bool      isGivingThumb = false;
	[Range(-1.0f,1.0f)] public float     flex          = 1.0f;
	[Range(-1.0f,1.0f)] public float     pinch         = 1.0f;
}

public class OculusGrab : MonoBehaviour
{
	[Header("Hand Situation")]
	public OculusObject  inReach;
	public OculusObject  isHolding;

	[Header("Orientation List")]
	public Orientation[] orientations;

	[Header("Movement Delta")]
	public Vector3       lastPosition;
	public Vector3       lastDelta;
	public Orientation   currentOrientation = null;

	void OnTriggerEnter(Collider o) {
		if (isHolding != null)
			return;

		OculusObject grabbable = o.GetComponentInChildren<OculusObject>();
		if (grabbable == null)
			return;

		inReach                = grabbable;
	}

	void OnTriggerExit(Collider o) {
		if (isHolding != null)
			return;

		OculusObject grabbable = o.GetComponentInChildren<OculusObject>();
		
		if (grabbable == null)
			return;
		if (grabbable == inReach)
			inReach = null;
	}

	public void StartGrab() {
		if (inReach   == null ||
            isHolding != null)
			return;

		Transform parentBelow      = transform;
		currentOrientation         = null;
		for (int i = 0; i < orientations.Length; i++)
			if (orientations[i]       != null &&
                orientations[i].tagIs == inReach.tag) {
				parentBelow        = orientations[i].parentBelow;
				currentOrientation = orientations[i];
			}

		inReach.Grab(parentBelow, this);
		isHolding                  = inReach;
	}

	public void StopGrab() {
		if (isHolding == null)
			return;
		isHolding.Release(lastDelta);
		isHolding = null;
	}

	public void Update() {
		if (transform.position == lastPosition)
			return;
		lastDelta    = transform.position - lastPosition;
		lastPosition = transform.position;
	}

	public bool        IsHoldingSomething() { return isHolding != null;  }
	public Orientation GetOrientation    () { return currentOrientation; }
}
