using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisherController : MonoBehaviour {

    [Header("Spout Settings")]

    [Tooltip("The particle system of the spout.")]
    public ParticleSystem pSystem;

    [Tooltip("Foam prefab that gets cloned and spammed.")]
    public GameObject foamBallPrefab;

    [Tooltip("Speed at which the foamballs get launched.")]
    public float foamBallSpeed = 10;

    [Tooltip("Amount of foamballs launched per minute.")]
    public float foamBallFrequency = 120;

    [Tooltip("Maximum amount of foamballs.")]
    [Range(0, 100000)]
    public int maxFoamBalls = 1000;

    [Tooltip("Foamball growth multiplier over time.")]
    public float foamBallGrowScale = 1.0f;

    [Tooltip("The Transfrom that the foamballs will be emitted from.")]
    public Transform foamNozzle;

    [Tooltip("The Transform that the foamballs will be fired at.")]
    public Transform foamTarget;

    [Tooltip("Size of target sphere around foamTarget.")]
    [Range(0.001f, 10)]
    public float foamTargetScale = 1f;

    [Tooltip("Parent of the foamballs in the hierarchy.")]
    public GameObject foamBallsParent;


    private bool isExtinguisherHeld = false;
    private bool isSpoutHeld = false;
    public bool isSpouting = false;
    private float timeSinceLastFoamBall = 0f;
    private List<Transform> foamBalls  = new List<Transform>();
    private List<Rigidbody> foamBallsR = new List<Rigidbody>();
    private int foamBallIndex = 0;


    /**
     * Turn off particle system at start.
     */
    public void Start()
    {
        SetParticleSystemActive(false);
        for(int i = 0; i < maxFoamBalls; i++)
        {
            GameObject g = Instantiate(foamBallPrefab);
            foamBalls.Add(g.transform);
            foamBallsR.Add(g.GetComponent<Rigidbody>());
            foamBalls[i].parent = foamBallsParent.transform;
            foamBalls[i].localPosition = Vector3.zero;
            g.SetActive(false);
        }
    }

    /**
     * Update loop for launching foamballs
     */
    public void Update()
    {
        //Loop for foamball growth over time.
        foreach (Transform foamBall in foamBalls)
        {
            if (foamBall.gameObject.activeSelf)
            {
                float randomScale = Random.Range(0f, 1);
                foamBall.localScale += new Vector3(Time.deltaTime * foamBallGrowScale * randomScale,
                                                   Time.deltaTime * foamBallGrowScale * randomScale,
                                                   Time.deltaTime * foamBallGrowScale * randomScale);
            }
        }
        if (isSpouting)
        {
            for (int i = 0; i < 3; i++)
            {

                //Creating new foamball
                //  if ((timeSinceLastFoamBall += Time.deltaTime) > (60/foamBallFrequency))
                // {
                // timeSinceLastFoamBall = 0f;
                if (++foamBallIndex >= maxFoamBalls)
                {
                    foamBallIndex = 0;
                }
                foamBalls[foamBallIndex].position = foamNozzle.position;
                float size = Random.Range(0.2f, 1f);
                foamBalls[foamBallIndex].localScale = new Vector3(size, size, size);
                foamBalls[foamBallIndex].localRotation = Random.rotation;
                foamBalls[foamBallIndex].gameObject.SetActive(true);
                Vector3 forceTarget = foamTarget.position + Random.insideUnitSphere * foamTargetScale;
                foamBallsR[foamBallIndex].isKinematic = false;
                foamBallsR[foamBallIndex].AddForce((forceTarget - pSystem.transform.position) * foamBallSpeed);
                // }
            }
        }
    }

    
    public void OnDrawGizmos()
    {
        if (foamTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(foamTarget.position, foamTargetScale * 0.5f);
            Gizmos.DrawLine(foamNozzle.position, foamTarget.position + foamTarget.right * foamTargetScale * 0.5f);
            Gizmos.DrawLine(foamNozzle.position, foamTarget.position + -foamTarget.right  * foamTargetScale * 0.5f);
            Gizmos.DrawLine(foamNozzle.position, foamTarget.position + foamTarget.up * foamTargetScale * 0.5f);
            Gizmos.DrawLine(foamNozzle.position, foamTarget.position + -foamTarget.up * foamTargetScale * 0.5f);
        }
    }

    /**
     * Toggle the particle effect.
     */
    private void SetParticleSystemActive(bool setActive)
    {
        if (setActive) //If it's turned off and needs to be turned on play it
        {
            pSystem.Play();
        }
        else if (! setActive) //If it's turned on and needs to be turned off stop it
        {
            pSystem.Stop();
        }
    }

    /**
     * Toggle the spouting particle effect + foamballs.
     */
    public void SetSpouting(bool setActive)
    {
        if (isSpouting != setActive)
        {
            SetParticleSystemActive(setActive);
            isSpouting = setActive;
        }
    }
}
