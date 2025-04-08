using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script allows the lava in level 3 to move up and down, submerging some of the
 * platforms when it does.
*/
public class LavaLevel : MonoBehaviour
{
    public float maxMoveUp = 10;
    public float moveSpeed = 1;

    private float initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.y;
        StartCoroutine(moveLava());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, maxPosition.position, moveSpeed * Time.deltaTime);
    }

    IEnumerator moveLava()
    {
        while (true)
        {
            while (transform.position.y < initialPosition + maxMoveUp)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition + maxMoveUp, transform.position.z), Time.deltaTime * moveSpeed);
                //transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z), transform.rotation);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(3f);

            while (transform.position.y > initialPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition, transform.position.z), Time.deltaTime * moveSpeed);
                //transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z), transform.rotation);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(3f);
        }
    }
}
