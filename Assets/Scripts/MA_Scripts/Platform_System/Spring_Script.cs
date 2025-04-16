using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Script : MonoBehaviour
{
    public float jumpHeight = 100f;
    public float jumpDuration = 0.5f;

    private Coroutine jumpCoroution;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CommentedThirdPersonController playerC = other.GetComponent<CommentedThirdPersonController>();

            if (playerC != null)
            {
                playerC.isJumping = false;
                playerC.jumpElapsedTime = 0;
                playerC.jumpForce = jumpHeight;

                if(jumpCoroution != null)
                {
                    StopCoroutine(jumpCoroution);
                }

                jumpCoroution = StartCoroutine(SmoothJump(other.transform, playerC));
                //jumpHeight = playerC.jumpForce;
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
            // When the c button is press the player will fall down fast 
            if(Input.GetKey(KeyCode.C))
            {
                playerC.isJumping = true;
                yield break;
            }

            float newY = Mathf.Lerp(startY, targetY, elapsedTime / jumpDuration);
            Vector3 currentPos = playerTran.position;
            playerTran.position = new Vector3(currentPos.x, newY, currentPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;

        }

        Vector3 finalPos = playerTran.position;
        playerTran.position = new Vector3(finalPos.x, targetY, finalPos.z);
    }
}