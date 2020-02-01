using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DieBehaviour
{
    Destroy, Deactivate, MakeKinematic
}
public class DieAfterAWhile : MonoBehaviour {
	[Tooltip("Max lifetime of the object in seconds.")]
	[Range(2,10)] public float dieAfter = 8.0f;							// max lifetime in seconds. will be multiplied with random range [0,1]

    public DieBehaviour dieBehaviour = DieBehaviour.MakeKinematic;
    

    private float timeAlive = 0;
    private float maxTimeAlive = 0;

	void Start () { maxTimeAlive = Random.value * dieAfter; }              // calculate dieing timestamp for reference in Update()
    void Update() {
        if ((timeAlive += Time.deltaTime) > maxTimeAlive)                   // wallclock is past dieing timestamp?
        {
            gameObject.SetActive(false);                                    // deactivate object
            timeAlive = 0;
        }											
	}
}
