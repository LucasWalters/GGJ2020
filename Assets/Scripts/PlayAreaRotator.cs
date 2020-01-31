using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAreaRotator : MonoBehaviour
{
	public Transform player;
	public Transform world;
	
	public Transform trackingSpace;
	public Transform centerEye;
	
	public bool debug = false;
	public Transform[] debugPlayAreaPoints;
	public Vector3 posToMoveTo;
	public Vector3 diffToMove;
	public Vector3 closestWallCenter;
	public Vector3 playAreaDirection;
	
	public Text debugText;
	public Text dynamicDebugText;
	public float degreesToTurn;
	
	public Vector3[] paPos;
	
    void Start()
    {
        //Vector3[] paPos;  // Play Area Positions
		if (debug) {
			paPos = new Vector3[4];
			for (int i = 0; i < debugPlayAreaPoints.Length; i++) {
				Debug.Log("f");
				paPos[i] = debugPlayAreaPoints[i].position; // 4 transforms used for debugging instead of play area
			}
		} else {
			paPos = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea); 
			// Should be list of 4 positions which is the largest possible rectangle in the player defined boundary.
			for (int i = 0; i < paPos.Length; i++){
				//paPos[i] += fixer.endPosition;
			}
		}
		
		
		float dist1 = Vector3.Distance(paPos[0], paPos[1]); // The distance between the first and second transform
		float dist2 = Vector3.Distance(paPos[1], paPos[2]); // The distance between the second and third transform
		Vector3 longWall; // Defined by comparing dist1 and dist2 to see which one is higher/lower
		Vector3 shortWall; // idem
		
		Vector3 playerPos = player.position;
		
		
		if (dist1 > dist2) {
			shortWall = paPos[2] - paPos[1]; // If dist1 is the largest, then shortwall is equal to the difference between paPos[1] and paPos[2]
			
			if (Vector3.Distance(playerPos, paPos[2]) > Vector3.Distance(playerPos, paPos[3])) { // Checks what shortwall the player is closest to
				longWall = paPos[1] - paPos[0]; 
				closestWallCenter = paPos[0] + shortWall/2;
			} else {
				longWall = paPos[0] - paPos[1];
				closestWallCenter = paPos[1] + shortWall/2;
			}
		} 
		else 
		{
			shortWall = paPos[1] - paPos[0];
			
			if (Vector3.Distance(playerPos, paPos[1]) > Vector3.Distance(playerPos, paPos[2])) {
				longWall = paPos[1] - paPos[2];
				closestWallCenter = paPos[3] + shortWall/2;
			} else {
				longWall = paPos[2] - paPos[1];
				closestWallCenter = paPos[0] + shortWall/2;
			}
		}
		
		playAreaDirection = longWall.normalized;
		
		Vector3 directionProj = ProjectionVector(world.forward, playAreaDirection);
		float amountToRotate = Mathf.Rad2Deg * Mathf.Acos(directionProj.magnitude);
		if (Vector3.Dot(world.forward, playAreaDirection) < 0) {
			amountToRotate += 180;
		}
		Vector3 worldRotation = world.localEulerAngles;
		Vector3 worldRotationOrginal = worldRotation;
		worldRotation.y += amountToRotate;
		
		world.localEulerAngles = worldRotation;
		
		Vector3 centerDiff = closestWallCenter - world.position;
		Vector3 projection = ProjectionVector(centerDiff, shortWall.normalized);
		Vector3 worldPositionOriginal = world.position;
		world.position += projection;
		
		if (debug) {
			posToMoveTo = world.position + projection;
			diffToMove = projection;
			degreesToTurn = amountToRotate;
		}

#region DEBUGPLAYAREA

		debugText.text += $"paPos[0] is blue, paPos[1] is red, paPos[2] is green and paPos[3] is yellow \n";
		for (int i = 0; i < paPos.Length; i++){
			debugText.text += $"paPos[{i}] = {paPos[i]}\n";
		}
		
		debugText.text += $"PlayerPos: {playerPos} \n";
		
		debugText.text += $"Dist1: {dist1} \n";
		debugText.text += $"Dist2: {dist2} \n";
		debugText.text += $"ClosestWallCenter: {closestWallCenter} \n";
		debugText.text += $"Shortwall: {shortWall.ToString()} \n";
		debugText.text += $"Longwall: {longWall.ToString()} \n";
		debugText.text += $"playAreaDirection: {playAreaDirection} \n";
		debugText.text += $"AmountToRotate: {amountToRotate.ToString()} \n";
		debugText.text += $"worldRotation ORIGINAL: {worldRotationOrginal} \n";
		debugText.text += $"worldRotation NEW: {world.localEulerAngles} \n";
		debugText.text += $"worldPosition ORIGINAL: {worldPositionOriginal} \n";
		debugText.text += $"worldPosition NEW: {world.position} \n";
		
		GameObject paPos1Debug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		GameObject paPos2Debug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		GameObject paPos3Debug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		GameObject paPos4Debug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		
		paPos1Debug.transform.position = paPos[0];
		paPos1Debug.GetComponent<Renderer>().material.color = Color.blue;
		
		paPos2Debug.transform.position = paPos[1];
		paPos2Debug.GetComponent<Renderer>().material.color = Color.red;
		
		paPos3Debug.transform.position = paPos[2];
		paPos3Debug.GetComponent<Renderer>().material.color = Color.green;
		
		paPos4Debug.transform.position = paPos[3];
		paPos4Debug.GetComponent<Renderer>().material.color = Color.yellow;
#endregion

    }
	
	private void Update()
	{
		dynamicDebugText.text = $"OVRCamera: {player.position} \n TrackingSpace: {trackingSpace.position} \n Center eye: {centerEye.position} \n";
	}
	
	private Vector3 ProjectionVector(Vector3 source, Vector3 normalizedTarget) {
		return Vector3.Dot(source, normalizedTarget) * normalizedTarget;
	}
	
	public void OnDrawGizmos() {
		if (!debug)
			return;
		
		Gizmos.color = Color.red;
		for (int i = 0; i < debugPlayAreaPoints.Length; i++) {
			Gizmos.DrawLine(debugPlayAreaPoints[i].position, debugPlayAreaPoints[(i + 1) == debugPlayAreaPoints.Length ? 0 : (i + 1)].position);
		}
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.zero, playAreaDirection);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(Vector3.zero, diffToMove);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(closestWallCenter, 0.3f);
	}
}
