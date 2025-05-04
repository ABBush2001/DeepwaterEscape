using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForJellyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<Jellyfish_Spark_script>().enabled)
        {
            if (!this.gameObject.GetComponent<E_Health>())
            {
                this.gameObject.AddComponent<E_Health>();
                this.gameObject.GetComponent<E_Health>().EnemyDmg = 100;
            }
        }
    }
}
