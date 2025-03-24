using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallQuit : MonoBehaviour
{

    [SerializeField] GameObject mainCamera;

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
        StartCoroutine(fallTransition());
    }

    IEnumerator fallTransition()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("2.Oceanfloor");
    }
}
