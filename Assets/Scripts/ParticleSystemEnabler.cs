using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemEnabler : MonoBehaviour
{
    ParticleSystem ps;

    void Start() {
        ps = this.GetComponent<ParticleSystem>();
    }
    public void StartParticleSystem() {
        ps.Play();
    }

    public void StopParticleSystem() {
        ps.Stop();
    }
}
