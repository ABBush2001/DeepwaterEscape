using System.Collections;  // Ensure this is at the top
using UnityEngine;

public class SpikeExplosion : MonoBehaviour
{
    public GameObject[] spikes;          // Array of your spike GameObjects
    public float explosionForce = 2000f; // Force applied to each spike (increase for faster explosion)
    public float upwardModifier = 1f;    // Modify if you want to give an upward push
    public float gravityDelay = 0.5f;    // Delay before gravity kicks back in
    public float gravityMultiplier = 2f; // Multiplier to make gravity stronger after delay

    void Start()
    {
        Explode();
    }

    void Explode()
    {
        foreach (GameObject spike in spikes)
        {
            if (spike.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                // Disable rotation to prevent spinning
                rb.freezeRotation = true;

                // Get a random direction from the center of the sphere
                Vector3 randomDirection = Random.onUnitSphere;

                // Optional: Modify the direction to give them some upward push
                randomDirection.y += upwardModifier;

                // Apply the force in that random direction for a fast explosion
                rb.useGravity = false;  // Disable gravity initially
                rb.velocity = randomDirection * explosionForce; // Push the spikes outward

                // Re-enable gravity and increase its strength after a short delay
                StartCoroutine(ReenableGravityAndRotation(rb));
            }
        }
    }

    // Coroutine to enable gravity and rotation after a small delay
    IEnumerator ReenableGravityAndRotation(Rigidbody rb)
    {
        yield return new WaitForSeconds(gravityDelay); // Wait for the delay before enabling gravity
        rb.useGravity = true; // Enable gravity
        rb.freezeRotation = false; // Allow the spike to rotate again

        // Apply a stronger gravity force by adjusting the drag or adding a gravity multiplier
        rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration); // Make gravity stronger
    }
}