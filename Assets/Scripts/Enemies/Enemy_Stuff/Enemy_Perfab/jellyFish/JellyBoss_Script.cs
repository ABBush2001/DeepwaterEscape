using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JellyBoss_Script : MonoBehaviour
{

    //[SerializeField] Vector3 movement;
    //[SerializeField] Vector3 rotation;

    //[SerializeField] bool fullCycle;

    //[SerializeField] float completionTime = 3f;
    //[SerializeField] float endPointStallTime = 2f;

    //[SerializeField] float endPointDistanceThreshold = 0.05f;

    //float dwellTimer = 0f;
    //float t = 0f;

    //Vector3 startingPoint;
    //Vector3 targetPoint;


    //public int waveDmg = 999;

    public string sceneToLoad;

    // Start is called before the first frame update
    private void Start()
    {
        //targetPoint = (transform.position + movement);

        //if (fullCycle)
        //{
        //    transform.position = (transform.position - movement);
        //}

        //startingPoint = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //if(Vector3.Distance(transform.position, targetPoint) <= endPointDistanceThreshold)
        //{
        //    dwellTimer += Time.deltaTime;

        //    if (dwellTimer >= endPointStallTime)
        //    {
        //        Vector3 temp = targetPoint;
        //        targetPoint = startingPoint;
        //        startingPoint = temp;

        //        t = 0f;
        //        dwellTimer = 0f;
        //    }
        //}

        //t += (Time.deltaTime / completionTime);

        //transform.position = Vector3.Lerp(startingPoint, targetPoint, t);
        ////transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * speed);

        //transform.Rotate(rotation * Time.deltaTime);
    }


    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 initialPosition = Application.isPlaying ? transform.position : transform.position - movement;

    //    if (fullCycle)
    //    {
    //        Gizmos.DrawLine(initialPosition - movement, initialPosition + movement);
    //    }
    //    else
    //    {
    //        Gizmos.DrawLine(initialPosition, initialPosition + movement);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        if (other.CompareTag("Untagged"))
        {
            other.gameObject.tag = "Zap";
        }
    }
}
