using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrap_Script : MonoBehaviour
{
    [SerializeField] private Animator Trap = null;

    [SerializeField] private bool Platform_Trap = false;
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
        if(other.CompareTag("Player"))
        {
            if (Platform_Trap)
            {
                Trap.Play("Platform_Trap", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
