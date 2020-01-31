using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusHands : MonoBehaviour
{
	[Header("Parameters")]
					    public bool                showLeft  = true;
					    public bool                showRight = true;

	[Header("Graphics Setup")]	
					    public Animator            animatorLeft;
					    public Animator            animatorRight;
					    public GameObject          physicsLeft;
					    public GameObject          physicsRight;
					    public GameObject          grabLeft;
					    public GameObject          grabRight;

	[Header("Oculus Input")]
					    public OVRInput.Controller controllerLeft;
					    public OVRInput.Controller controllerRight;
	[Range( 0.1f,1.0f)] public float               poseChangeInSeconds = 0.1f;

	[Header("Grab Setup")]
					    public OculusGrab[]        grabScript;
	[Range( 0.0f,1.0f)] public float               grabThreshold       = 0.5f;

	[Header("Internal")]
	[Range( 0.0f,1.0f)] public float[]             pointing            = new float[2];
	[Range( 0.0f,1.0f)] public float[]             thumb               = new float[2];
					    public bool                grabStarted         = false;
						       OVRHapticsClip      vibration           = null;

	[Header("Debug Hand Positions")]
					    public bool                debugHandPositions  = false;
	[Range(-1.0f,1.0f)] public float               debugFlex           = 0.0f;
	[Range(-1.0f,1.0f)] public float               debugPinch          = 0.0f;
					    public bool                debugIsPointing     = false;
					    public bool                debugIsGivingThumb  = false;

    void Start()
    {
		if (animatorLeft  != null) animatorLeft .gameObject.SetActive(showLeft );
		if (physicsLeft   != null) physicsLeft             .SetActive(showLeft );
		if (grabLeft      != null) grabLeft                .SetActive(showLeft );

		if (animatorRight != null) animatorRight.gameObject.SetActive(showRight);
		if (physicsRight  != null) physicsRight            .SetActive(showRight);
		if (grabRight     != null) grabRight               .SetActive(showRight);

		grabStarted    = false;

		int    samples = 100;
		byte[] noize   = new byte[samples];
		for (int i = 0; i < samples; i++)
			noize[i]   = 255;
		vibration      = new OVRHapticsClip(noize, samples);
    }
	
    void Update()
    {
        UpdateAnimation();
    }

	public bool FreeHandIsGrabbing() {
		if      (! grabStarted                                  ) return false;
		else if (showLeft  && grabScript[0].IsHoldingSomething()) return false;
		else if (showRight && grabScript[1].IsHoldingSomething()) return false;
		return true;
	}

	public void Vibrate() {
		if (showLeft ) OVRHaptics.LeftChannel .Preempt(vibration);
		if (showRight) OVRHaptics.RightChannel.Preempt(vibration);
	}

	void UpdateAnimation() {
		for (int i = 0; i < 2; i++) {
			Animator            A =  (i == 0 ? animatorLeft   : animatorRight  );
			OVRInput.Controller C =  (i == 0 ? controllerLeft : controllerRight);
		
			bool  isPointing      =  ! OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, C);
			bool  isGivingThumb   =  ! OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, C);
			float flex            =    OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger,     C);
			float pinch           =    OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger,    C);
			bool  debugGrab       =    Input.GetKey(KeyCode.G);

			if (grabScript        != null &&
				grabScript.Length >  i    &&
                grabScript[i]     != null &&
                grabScript[i].gameObject.activeSelf) {
				if (flex >= grabThreshold ||
                    debugGrab) {
					if (! grabStarted) {
						grabScript[i].StartGrab();
						grabStarted = true;
					}
				}
				else {
					grabScript[i].StopGrab();
					grabStarted     = false;
				}

				if (grabScript[i].IsHoldingSomething()) {
					Orientation o     = grabScript[i].GetOrientation();
					if (o != null) {
						isPointing    = o.isPointing;
						isGivingThumb = o.isGivingThumb;
						flex          = o.flex;
						pinch         = o.pinch;
					}
				}
			}

			A.SetFloat("Flex",  (debugHandPositions ? debugFlex  : flex ));
			A.SetFloat("Pinch", (debugHandPositions ? debugPinch : pinch));
						
			float timeIncrement =  (Time.deltaTime / poseChangeInSeconds);
			bool  statePointing =  (debugHandPositions ? debugIsPointing    : isPointing   );
			bool  stateThumb    =  (debugHandPositions ? debugIsGivingThumb : isGivingThumb);

			pointing[i]         += timeIncrement * (statePointing ? 1 : -1);
			pointing[i]         =  Mathf.Clamp01(pointing[i]);
			thumb   [i]         += timeIncrement * (stateThumb    ? 1 : -1);
			thumb   [i]         =  Mathf.Clamp01(thumb   [i]);
			
			A.SetLayerWeight(A.GetLayerIndex("Point Layer"), pointing[i]);
			A.SetLayerWeight(A.GetLayerIndex("Thumb Layer"), thumb   [i]);
		}
	}
}
