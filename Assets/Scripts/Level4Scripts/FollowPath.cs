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
    Node[] PathNode;
    public GameObject enemy;
    public float moveSpeed;
    float timer;
    int currentNode;
    static Vector3 currentPositionHolder;
    static Vector3 startPosition;


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

    //set the current node
    void CheckNode()
    {
        timer = 0;
        currentPositionHolder = PathNode[currentNode].transform.position;
        startPosition = enemy.transform.position;
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
            //enemy.transform.Rotate(new Vector3(enemy.transform.rotation.x, enemy.transform.rotation.y - 90, enemy.transform.rotation.z));
        }
        if(path == 2)
        {
            currentNode = GameObject.Find("ChargeEndNode2").GetComponent<Node>().NodeIndex;
            //enemy.transform.Rotate(new Vector3(enemy.transform.rotation.x, enemy.transform.rotation.y - 90, enemy.transform.rotation.z));
        }
    }

    // Lerp the enemy between nodes
    void Update()
    {
        Debug.Log(currentNode);
        timer += Time.deltaTime * moveSpeed;

        if(PathNode[currentNode].GetComponent<Node>().isCornerNode)
        {
            enemy.transform.Rotate(new Vector3(enemy.transform.rotation.x, enemy.transform.rotation.y - 90, enemy.transform.rotation.z));
        }

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
