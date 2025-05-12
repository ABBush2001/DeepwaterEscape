using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Script : MonoBehaviour
{
    public Animator playerAnim;
    public AudioSource jump;

    public float jumpHeight = 100f;
    public float jumpDuration = 0.5f;

    private Coroutine jumpCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            this.enabled = false;
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            jump.Play();
            playerAnim.SetTrigger("Landing");
            playerAnim.SetTrigger("Jumping");

            CommentedThirdPersonController playerC = other.GetComponent<CommentedThirdPersonController>();

            if (playerC != null)
            {
                playerC.isJumping = false;
                playerC.jumpElapsedTime = 0;
                playerC.jumpForce = jumpHeight;

                if (jumpCoroutine != null)
                {
                    StopCoroutine(jumpCoroutine);
                }

                jumpCoroutine = StartCoroutine(SmoothJump(other.transform, playerC));
            }
        }
    }

    IEnumerator SmoothJump(Transform playerTran, CommentedThirdPersonController playerC)
    {
        Vector3 startPos = playerTran.position;
        float startY = startPos.y;
        float targetY = startY + jumpHeight;
        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {

            float t = elapsedTime / jumpDuration;
            float newY = Mathf.SmoothStep(startY, targetY, t);

            Vector3 currentPos = playerTran.position;
            playerTran.position = new Vector3(currentPos.x, newY, currentPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPos = playerTran.position;
        playerTran.position = new Vector3(finalPos.x, targetY, finalPos.z);
    }
}
