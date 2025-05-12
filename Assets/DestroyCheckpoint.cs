using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint delete to: " + name);
            Destroy(other.gameObject);
        }
    }
}
