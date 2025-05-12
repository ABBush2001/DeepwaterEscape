using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundController : MonoBehaviour
{
    public AudioClip footStep;
    public AudioSource source;

    public void FootStep()
    {
        source.PlayOneShot(footStep);
    }
}
