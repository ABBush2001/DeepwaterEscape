using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used as part of the bite mechanic for the Arena boss fight.
 * It allows the enemy to follow along a path, consisting of a series of 'nodes.'
 * It does this by creating an array of nodes, then Lerping the enemy from one
 * node to the next. It also has functionalities for moving between paths.
*/
public class FollowPath : MonoBehaviour
{
    //variables
    private Node[] PathNode;
    public GameObject enemy;
    //public GameObject lookerObj;
    public float moveSpeed;
    private float timer;
    private int currentNode;
    private static Vector3 currentPositionHolder;
    private static Vector3 startPosition;

    private BossManager bossManager;
    private Boss_health bHealth;

    // Initialize list of nodes
    void Start()
    {
        PathNode = GetComponentsInChildren<Node>();

        bHealth = enemy.GetComponent<Boss_health>();

        CheckNode();

        for(int i = 0; i < PathNode.Length - 1; i++)
        {
            PathNode[i].NodeIndex = i;
        }
    }

    //move the boss back to the closest node on the path
    public void SnapToClosestNode()
    {
        float closestDist = float.MaxValue;
        int closestIndex = 0;

        for (int i = 0; i < PathNode.Length; i++)
        {
            float dist = Vector3.Distance(enemy.transform.position, PathNode[i].transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        currentNode = closestIndex;
        CheckNode();
    }

    //make sure the boss is synced to the correct posit
    public void SyncToCurrentPosition()
    {
        startPosition = enemy.transform.position;
        currentPositionHolder = PathNode[currentNode].transform.position;
        timer = 0;
    }

    //set the current node
    void CheckNode()
    {
        timer = 0;
        currentPositionHolder = PathNode[currentNode].transform.position;
        startPosition = enemy.transform.position;

        if (currentNode < PathNode.Length - 1)
        {
            Vector3 directionNode = PathNode[currentNode + 1].transform.position - enemy.transform.position;
            if (directionNode != Vector3.zero)
            {

                enemy.transform.rotation = Quaternion.LookRotation(directionNode);
                enemy.transform.rotation.Set(0f, enemy.transform.rotation.y, 0f, enemy.transform.rotation.w);
                
                //lookerObj.transform.rotation = Quaternion.LookRotation(directionNode);
            }
        }
    }

    //return the current node
    public Node getNode()
    {
        return PathNode[currentNode];
    }

    //reset the path to the first node
    public void resetNode()
    {
        currentNode = 0;
    }

    // return after wave finish
    public void SetNode(int nodeIndex)
    {
        if (nodeIndex >= 0 && nodeIndex < PathNode.Length)
        {
            currentNode = nodeIndex;
            CheckNode();
        }
    }

    //set the current node based on the last path traveled
    public void setCurrentNode(int path)
    {
        if(path == 3)
        {
            currentNode = GameObject.Find("ChargeEndNode1").GetComponent<Node>().NodeIndex;
        }
        if(path == 2)
        {
            currentNode = GameObject.Find("ChargeEndNode2").GetComponent<Node>().NodeIndex;
        }
    }

    // Lerp the enemy between nodes
    void Update()
    {
        if (BossManager.isMoving)
        {
            return;
        }

        Debug.Log(currentNode);
        timer += Time.deltaTime * moveSpeed;

        if(enemy == null || bHealth == null || bHealth.BossHealth < 0)
        {
            return;
        }

        if (enemy.transform.position != currentPositionHolder)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, currentPositionHolder, timer);
        }
        else
        {
            if(currentNode < PathNode.Length - 1)
            {
                currentNode++;
                CheckNode();
            }
            else if(currentNode == PathNode.Length - 1)
            {
                currentNode = 0;
            }
        }
    }
}
