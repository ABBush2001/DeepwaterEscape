using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    public GameObject jellyfish;
    public Animator jellyAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(switchAnim());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator switchAnim()
    {
        yield return new WaitForSeconds(2f);
        jellyAnim.SetBool("IntroDone", true);
    }
}
