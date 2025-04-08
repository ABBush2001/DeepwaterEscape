using System.Collections;
using UnityEngine;

public class SpikeExplosion : MonoBehaviour
{
    public GameObject[] spikes;
    public float explosionForce = 2000f;
    public float upwardModifier = 1f;
    public float gravityDelay = 0.5f;
    public float gravityMultiplier = 2f;
    public float lifetimeAfterExplosion = 2f;

    public void Explode()
    {
        foreach (GameObject spike in spikes)
        {
            ShowSpike(spike);

            if (spike.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.useGravity = false;

                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y += upwardModifier;

                rb.velocity = randomDirection * explosionForce;

                StartCoroutine(ReenableGravityAndRotation(rb));
                StartCoroutine(HideSpikeAfterDelay(spike));
            }
        }
    }

    IEnumerator ReenableGravityAndRotation(Rigidbody rb)
    {
        yield return new WaitForSeconds(gravityDelay);
        rb.useGravity = true;
        rb.freezeRotation = false;
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
    }

    IEnumerator HideSpikeAfterDelay(GameObject spike)
    {
        yield return new WaitForSeconds(lifetimeAfterExplosion);
        HideSpike(spike);
    }

    public void ResetSpikes()
    {
        foreach (GameObject spike in spikes)
        {
            if (spike.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.freezeRotation = true;
                spike.transform.localPosition = Vector3.zero;
                spike.transform.localRotation = Quaternion.identity;
            }

            HideSpike(spike);
        }
    }

    void HideSpike(GameObject spike)
    {
        if (spike.TryGetComponent<MeshRenderer>(out MeshRenderer mr))
            mr.enabled = false;

        if (spike.TryGetComponent<Collider>(out Collider col))
            col.enabled = false;
    }

    void ShowSpike(GameObject spike)
    {
        if (spike.TryGetComponent<MeshRenderer>(out MeshRenderer mr))
            mr.enabled = true;

        if (spike.TryGetComponent<Collider>(out Collider col))
            col.enabled = true;
    }
}

