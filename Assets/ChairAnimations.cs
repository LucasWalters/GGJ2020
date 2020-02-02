using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ChairAnimations : MonoBehaviour
{
    const float DurationAnimationInSeconds = 0.75f;
    private Vector3 MoveInterval = (Vector3.left * 4);
    private Vector3 StartPosition = new Vector3(0, 0, 0) - (Vector3.left * 4);
    public AudioSource audioSource;
    public GameObject chair;

    public void ResetChairPosition()
    {
        this.transform.position = StartPosition;
        if (!chair.activeSelf)
        {
            chair.SetActive(true);
        }
    }
    public TweenerCore<Vector3, Vector3, VectorOptions> MoveChair()
    {
        var currentPosition = this.transform.position;

        if (Vector3.Distance(this.transform.position, Vector3.zero) < 1)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }

        return this.transform.DOMove(currentPosition + MoveInterval, DurationAnimationInSeconds).SetEase(Ease.InOutQuad);
    }
}
