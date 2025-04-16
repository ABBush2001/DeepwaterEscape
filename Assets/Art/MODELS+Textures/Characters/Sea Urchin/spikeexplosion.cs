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

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;

    private void Start()
    {
        int i = 0;

        foreach(GameObject spike in spikes)
        {
            originalPositions[i] = spike.transform.position;
            originalRotations[i] = spike.transform.rotation;
            i++;
        }
    }

    private void resetSpikePositions()
    {
        int i = 0;

        foreach(GameObject spike in spikes)
        {
            spike.transform.SetPositionAndRotation(originalPositions[i], originalRotations[i]);
            i++;
        }
    }

    public void Explode()
    {
        Debug.Log("Explode called!");

        foreach (GameObject spike in spikes)
        {
            ShowSpike(spike);

            if (spike.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.freezeRotation = true;
                rb.useGravity = false;

                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y += upwardModifier;

                rb.velocity = randomDirection * explosionForce;

                StartCoroutine(ReenableGravity(rb));
                StartCoroutine(HideSpikeAfterDelay(spike));
            }
        }
    }

    IEnumerator ReenableGravity(Rigidbody rb)
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
        ResetSpikes();
    }

    public void ResetSpikes()
    {
        foreach (GameObject spike in spikes)
        {
            if (spike.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.freezeRotation = true;

                spike.transform.localPosition = Vector3.zero;
                spike.transform.localRotation = Quaternion.identity;
            }

            HideSpike(spike);

            resetSpikePositions();
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
        Debug.Log("Show Spike Called!");

        if (spike.TryGetComponent<MeshRenderer>(out MeshRenderer mr))
            mr.enabled = true;

        if (spike.TryGetComponent<Collider>(out Collider col))
            col.enabled = true;
    }
}
