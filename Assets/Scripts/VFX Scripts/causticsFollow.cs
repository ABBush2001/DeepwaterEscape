using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class causticsFollow : MonoBehaviour
{
    public GameObject player;
    public int followX = 0;
    public int followY = 0;
    public int followZ = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - new Vector3(followX,followY,followZ);
    }
}
