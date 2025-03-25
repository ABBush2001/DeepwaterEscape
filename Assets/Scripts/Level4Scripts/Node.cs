using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script represents a Node, used for the enemy pathing in the Arena scene.
 * Each node has a index on its path, and can be denoted if it is a 'charge'
 * node, meaning where the enemy can charge to bite from, or a 'charge end'
 * node, where their attack path ends.
*/
public class Node : MonoBehaviour
{
    public bool isChargeNode;
    public bool isChargeEndNode;
    public bool isCornerNode;
    public bool lookAtNode;
    public int NodeIndex;

}
