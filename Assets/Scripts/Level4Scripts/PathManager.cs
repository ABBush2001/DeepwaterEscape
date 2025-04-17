using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script manages the paths used for the bite attack in the Arena boss
 * fight. It uses a coroutine to actively switch between paths, randomly moving
 * from the enemy's normal circling to one of the two bite paths. After a bite
 * path is completed, the enemy returns to its circling path
*/
public class PathManager : MonoBehaviour
{
    [SerializeField] GameObject path1;
    [SerializeField] GameObject path2;
    [SerializeField] GameObject path3;

    public GameObject enemy;

    [SerializeField] private BossManager bossM;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(switchPaths());
    }


    //coroutine to cycle between paths
    IEnumerator switchPaths()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            //if a charge node is hit, randomly decide to charge or continue
            if(path1.GetComponent<FollowPath>().getNode().isChargeNode)
            {
                int randPath = Random.Range(1, 3);

                if(randPath == 1)
                {
                    continue;
                }
                //if charging, set charge path active, follow it, then
                //reset the charge path and update the original path node to
                //the charge end node
                else
                {
                
                    if(path1.GetComponent<FollowPath>().getNode().gameObject.name == "ChargeNode1")
                    {
                        StartCoroutine(bossM.ChaseAttack());
                    }
                    else if(path1.GetComponent<FollowPath>().getNode().gameObject.name == "ChargeNode2")
                    {
                        StartCoroutine(bossM.ChaseAttack());
                    }
                }

                yield return new WaitForSeconds(6f);
            }
        }
    }
}
