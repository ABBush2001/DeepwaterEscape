using UnityEngine;

/*
 * This script handles toggling the noise object off.
 * There were many ways to handle clams "listening" for gunshots, but this one was the funniest.
 */

public class GunNoise : MonoBehaviour
{
    public short timeToLive = 5;
    private short curTime;
    private void Awake()
    {
        curTime = timeToLive;
    }

    private void FixedUpdate()
    {
        curTime--;
        if (curTime>= 0)
        {
            curTime = timeToLive;
            gameObject.SetActive(false);
        }
    }
}
