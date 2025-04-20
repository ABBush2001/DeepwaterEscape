using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script allows the lava in level 3 to move up and down, submerging some of the
 * platforms when it does.
*/
public class LavaLevel : MonoBehaviour
{
    //variables
    public float maxMoveUp = 10;
    public float moveSpeed = 1;

    private float initialPosition;

    //sets the initial position of the lava and starts the movement
    void Start()
    {
        initialPosition = transform.position.y;
        StartCoroutine(moveLava());
    }

    //moves the lava up and down
    IEnumerator moveLava()
    {
        while (true)
        {
            while (transform.position.y < initialPosition + maxMoveUp)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition + maxMoveUp, transform.position.z), Time.deltaTime * moveSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(3f);

            while (transform.position.y > initialPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition, transform.position.z), Time.deltaTime * moveSpeed);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
