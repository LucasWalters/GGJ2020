using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisherGrabAttach : OVRGrabbable {

    public bool beingGrabbed = false;
	
	public FireExtinguisherController fireExtinguisherController;
	
	public Transform handle;
	
	public AudioSource audio;
	
	public AudioClip startUp;
	public AudioClip loop;
	public AudioClip end;
	
	private bool lastSpout = false;

    /**
     * When grabbing the fire extinguisher, turn off the extinguisher body colliders and turn on the handle colliders.
     */
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
		beingGrabbed = true;
        base.GrabBegin(hand, grabPoint);
    }
    /**
     * When no longer grabbing the fire extinguisher, turn on the extinguisher body colliders and turn off the handle colliders. 
     * Also make the handle non kinematic.
     */
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
		beingGrabbed = false;
        base.GrabEnd(linearVelocity, angularVelocity);
    }
	
	private IEnumerator startSound()
	{
		audio.clip = startUp;
		audio.Play();
		while (beingGrabbed) {
			yield return new WaitForSeconds(audio.clip.length);
			audio.clip = loop;
			audio.Play();
		}
		
	}
	
	private void endSound() {
		StopAllCoroutines();
		audio.Stop();
		audio.clip = end;
		audio.Play();	
	}
		
		
	
	private void Update() {
		bool shouldSpout = beingGrabbed && (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger));
		fireExtinguisherController.SetSpouting(shouldSpout);
		handle.localEulerAngles = new Vector3(0, shouldSpout ? -27.141f : 0, 0);
		
		if (lastSpout != shouldSpout) {
			lastSpout = shouldSpout;
			if (shouldSpout) {
				StartCoroutine(startSound());
			} else {
				endSound();
			}
		}
	}

}
