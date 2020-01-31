using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public enum TrackingSpace { WORLD, LOCAL }

public class BasicTransformAnimation : MonoBehaviour
{
    [HideInInspector] public bool[] followDoFs = new bool[] { true, false, false };
    [HideInInspector] public bool[] useStartingValues = new bool[] { true, true, true };
    [HideInInspector] public string[] dofNames = new string[] { "Position", "Rotation", "Scale" };
    [HideInInspector] public string[] axisNames = new string[] { "X", "Y", "Z" };
    [HideInInspector] public Vector3 posValuesTo = Vector3.zero;
    [HideInInspector] public Vector3 rotValuesTo = Vector3.zero;
    [HideInInspector] public Vector3 scaleValuesTo = Vector3.one;
    [HideInInspector] public Vector3 posValuesFrom = Vector3.zero;
    [HideInInspector] public Vector3 rotValuesFrom = Vector3.zero;
    [HideInInspector] public Vector3 scaleValuesFrom = Vector3.one;
    [HideInInspector] public TrackingSpace trackingSpace = TrackingSpace.WORLD;

    [Header("Animation settings")]
    public float animationTime = 1f;
    public Transform transformToAnimate;

    [Header("Optional animation curves")]
    public AnimationCurve positionCurve;
    public AnimationCurve rotationCurve;
    public AnimationCurve scaleCurve;

	[Header("Debug")]
	[SerializeField]
    private bool _animateNow = false;
	[SerializeField, Range(0f, 1f)]
    private float _animationAt = 0.0f;

	public void Start()
    {
        if (transformToAnimate == null)
        {
            transformToAnimate = transform;
        }

		//Set starting values
        if (useStartingValues[0])
            posValuesFrom = trackingSpace == TrackingSpace.WORLD ? 
                transformToAnimate.position : 
                transformToAnimate.localPosition;
        if (useStartingValues[1])
            rotValuesFrom = transformToAnimate.localEulerAngles;
        if (useStartingValues[2])
            scaleValuesFrom = transformToAnimate.localScale;
    }

    public void Update()
    {
		//Changes animation time, goes backwards when animate is false
        _animationAt += Time.deltaTime / animationTime * (_animateNow ? 1 : -1);
        _animationAt = Mathf.Clamp01(_animationAt);

		//Update values
		if (followDoFs[0])
            SetPositionOnTransform();
        if (followDoFs[1])
            SetRotationOnTransform();
        if (followDoFs[2])
            SetScaleOnTransform();
    }

	//Starts the animation
	public void StartAnimation() { _animateNow = true; }
	//Reverses the animation back to start
	public void StopAnimation() { _animateNow = false; }
	//Resets the animation to warp back to start
	public void ResetAnimation() { _animateNow = false; _animationAt = 0.0f; }
	//Warps the animation to end state
    public void InstantEndAnimation() { _animateNow = true; _animationAt = 1.0f; }
    //Starts/Stops the animation
    public void ToggleAnimation() { _animateNow = !_animateNow; }

    private void SetPositionOnTransform()
    {
        Vector3 temp = Vector3.zero;
		//LerpAt is the current animation time if no curve is defined, otherwise evaluates on curve
        float lerpAt = (positionCurve == null || positionCurve.length == 0) ? 
			_animationAt : positionCurve.Evaluate(_animationAt);
        if (trackingSpace == TrackingSpace.LOCAL)
        {
            transformToAnimate.localPosition = posValuesFrom + posValuesTo * lerpAt;
            return;
        }
        for (int i = 0; i < 3; i++)
        {
			//Sets position based on difference between start and finish
			temp[i] = posValuesFrom[i] + (posValuesTo[i] - posValuesFrom[i]) * lerpAt;
        }
        transformToAnimate.position = temp;
    }

    private void SetRotationOnTransform()
    {
        Vector3 temp = Vector3.zero;
		//LerpAt is the current animation time if no curve is defined, otherwise evaluates on curve
		float lerpAt = (rotationCurve == null || rotationCurve.length == 0) ? 
			_animationAt : rotationCurve.Evaluate(_animationAt);
        for (int i = 0; i < 3; i++)
        {
			//Sets rotation based on difference between start and finish
            temp[i] = rotValuesFrom[i] + (rotValuesTo[i] - rotValuesFrom[i]) * lerpAt;
        }
        transformToAnimate.localEulerAngles = temp;
    }

    private void SetScaleOnTransform()
    {
        Vector3 temp = Vector3.zero;
		//LerpAt is the current animation time if no curve is defined, otherwise evaluates on curve
		float lerpAt = (scaleCurve == null || scaleCurve.length == 0) ? 
			_animationAt : scaleCurve.Evaluate(_animationAt);
        for (int i = 0; i < 3; i++) 
		{
			//Sets scale based on difference between start and finish
			temp[i] = scaleValuesFrom[i] + (scaleValuesTo[i] - scaleValuesFrom[i]) * lerpAt;
        }
        transformToAnimate.localScale = temp;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BasicTransformAnimation)), CanEditMultipleObjects]
public class BasicTransformAnimationEditor : Editor
{
	//Draws handle for setting the target position
	protected virtual void OnSceneGUI()
	{
		BasicTransformAnimation script = target as BasicTransformAnimation;
		if (script == null || !script.followDoFs[0]) {
			return;
		}
		Transform transform = script.transformToAnimate;

		Vector3 pos = script.posValuesTo;

        if (script.trackingSpace == TrackingSpace.LOCAL)
        {
            pos += transform.position;
        }

		EditorGUI.BeginChangeCheck();

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.green;
		Handles.Label(pos, "Position Target", style);

		Vector3 newTargetPosition = Handles.PositionHandle(pos, Quaternion.identity);

		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(script, "Change Transform animation Target Position");
			script.posValuesTo = newTargetPosition;
            if (script.trackingSpace == TrackingSpace.LOCAL)
            {
                script.posValuesTo -= transform.position;
            }
        }
	}

	//Inspector view for setting the DoF values
	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BasicTransformAnimation script = target as BasicTransformAnimation;
        if (script == null)
        {
            return;
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Target Settings", EditorStyles.boldLabel);

        for (int dof = 0; dof < 3; dof++)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

			EditorGUILayout.PrefixLabel(script.dofNames[dof]);
			script.followDoFs[dof] = EditorGUILayout.Toggle("", script.followDoFs[dof]);

			EditorGUILayout.EndHorizontal();

			if (script.followDoFs[dof]) {
                if (dof == 0)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.PrefixLabel("Target Space");
                    script.trackingSpace = (TrackingSpace)EditorGUILayout.EnumPopup(script.trackingSpace);

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel("From starting value");
                script.useStartingValues[dof] = EditorGUILayout.Toggle("", script.useStartingValues[dof]);

                EditorGUILayout.EndHorizontal();
                if (!script.useStartingValues[dof])
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("From");
                    switch (dof)
                    {
                        case 0:
                            script.posValuesFrom = EditorGUILayout.Vector3Field("", script.posValuesFrom);
                            break;
                        case 1:
                            script.rotValuesFrom = EditorGUILayout.Vector3Field("", script.rotValuesFrom);
                            break;
                        case 2:
                            script.scaleValuesFrom = EditorGUILayout.Vector3Field("", script.scaleValuesFrom);
                            break;
                    }
                    EditorGUILayout.EndHorizontal();
                }


                EditorGUILayout.BeginHorizontal();
				EditorGUILayout.PrefixLabel("To");
				switch (dof) {
					case 0:
						script.posValuesTo = EditorGUILayout.Vector3Field("", script.posValuesTo);
						break;
					case 1:
						script.rotValuesTo = EditorGUILayout.Vector3Field("", script.rotValuesTo);
						break;
					case 2:
						script.scaleValuesTo = EditorGUILayout.Vector3Field("", script.scaleValuesTo);
						break;
				}
				EditorGUILayout.EndHorizontal();
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Debug Buttons", EditorStyles.boldLabel);

		//Debug buttons
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Start Animation")) script.StartAnimation();
		if (GUILayout.Button("Stop Animation")) script.StopAnimation();
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Warp to Start")) script.ResetAnimation();
		if (GUILayout.Button("Warp to End")) script.InstantEndAnimation();
		EditorGUILayout.EndHorizontal();
	}
}
#endif // UNITY_EDITOR