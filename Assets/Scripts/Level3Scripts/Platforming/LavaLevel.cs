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
    public float timeToRise = 4;
    public float bottomWaitTime = 3f;
    public float topWaitTime = 3f;
    public AnimationCurve moveCurve;
    private float moveTime = 0;
   // private Keyframe[] frames;

    private float initialPosition;

    //sets the initial position of the lava and starts the movement
    void Start()
    {
        //frames[0] = new Keyframe(0, 0);
        //frames[1] = new Keyframe(timeToRise/2, moveSpeed);
        //frames[2] = new Keyframe(timeToRise, 0);
        //moveCurve = new AnimationCurve(frames);
        initialPosition = transform.position.y;
        StartCoroutine(MoveLava());
    }

    //moves the lava up and down
    IEnumerator MoveLava()
    {
        while (true)
        {
            while (moveTime < timeToRise)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition + maxMoveUp, transform.position.z), Time.deltaTime * moveCurve.Evaluate(moveTime));
                moveTime += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(topWaitTime);
            moveTime = 0;

            while (moveTime < timeToRise)
            {   
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialPosition, transform.position.z), Time.deltaTime * moveCurve.Evaluate(moveTime));
                moveTime += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(bottomWaitTime);
            moveTime = 0;
        }
    }
}
