using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script manages the collection of items in level 1, turn the
 * collectables on when appropriate
*/
public class LevelOneManager : MonoBehaviour
{
    //variables
    [SerializeField] private bool item1PickedUp;
    [SerializeField] private bool item2PickedUp;
    [SerializeField] private bool item3PickedUp;

    [SerializeField] private GameObject item1;
    [SerializeField] private GameObject item2;
    [SerializeField] private GameObject item3;

    [SerializeField] private GameObject playerGun;

    public bool alarmStart = false;

    //set all collectables to inactive on awake
    private void Awake()
    {
        item1PickedUp = false;
        item2PickedUp = false;
        item3PickedUp = false;

        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
    }

    //method to turn on collectables
    public void turnOnObjects()
    {
        item1.SetActive(true);
        item2.SetActive(true);
        item3.SetActive(true);
    }

    //if all items collected, turn on the player gun
    void Update()
    {
        if(item1PickedUp && item2PickedUp && item3PickedUp)
        {
            playerGun.SetActive(true);
        }
    }

    //setters and getters

    public void setItem1()
    {
        if(item1PickedUp == false)
        {
            item1PickedUp = true;
        }
    }
    public bool getItem1()
    {
        return item1PickedUp;
    }

    public void setItem2()
    {
        if(item2PickedUp == false)
        {
            item2PickedUp = true;
        }
    }
    public bool getItem2()
    {
        return item2PickedUp;
    }

    public void setItem3()
    {
        if (item3PickedUp == false)
        {
            item3PickedUp = true;
        }
    }
    public bool getItem3()
    {
        return item3PickedUp;
    }
}
