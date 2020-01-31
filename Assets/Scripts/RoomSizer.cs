using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSizer : MonoBehaviour
{
	public Transform room;
	public Transform maquettesLoc;
	public Transform logosLoc;
	
	public Transform maquettes;
	public Transform logos;
	
	public Transform[] playAreaCorners;
	
	public Text debugText;
	

    void Start()
    {

		Vector3[] paPos = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
		
		Vector3 midPoint = new Vector3((paPos[0].x + paPos[2].x) / 2, 0, (paPos[0].z + paPos[2].z) / 2);
		transform.position = midPoint;
		
		float dist1 = Vector3.Distance(paPos[0], paPos[1]);
		float dist2 = Vector3.Distance(paPos[1], paPos[2]);
		Vector3 playAreaDirection;
		if (dist1 > dist2) {
			playAreaDirection = (paPos[1] - paPos[0]).normalized;
		} else {
			playAreaDirection = (paPos[2] - paPos[1]).normalized;
		}
		Quaternion lookRotation = Quaternion.LookRotation(playAreaDirection);
		transform.localRotation = lookRotation;
		
		
		room.localScale = new Vector3(dist1 > dist2 ? dist2 : dist1, room.localScale.y, dist1 > dist2 ? dist1 : dist2);
		
		maquettes.position = maquettesLoc.position;
		logos.position = logosLoc.position;
		
	/*	
#region DEBUG PLAY AREA
		debugText.text += $"paPos[0] is blue, paPos[1] is red, paPos[2] is green and paPos[3] is yellow \n";
		for (int i = 0; i < paPos.Length; i++){
			debugText.text += $"paPos[{i}] = {paPos[i]}\n";
		}
		
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
*/
	}
}
