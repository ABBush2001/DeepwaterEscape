using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

/*
 * This script handles the closing cutscene for the submarine level.
 * It fades the camera out, starts the explosion effect and then loads
 * the next level.
*/
public class ExitTrigger : MonoBehaviour
{
    //variables
    public LevelOneManager levelManger;

    public GameObject gun;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject outroCamera;

    public AudioSource ExplosionSound1;

    public GameObject explosion;
    public GameObject explosion1;
    public GameObject explosion2;

    public float shakeAmount;

    public GameObject loading;

    //checks to see if the player has entered the exit trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jumped!");

            gun.SetActive(false);
            StartCoroutine(fadeToNextScene());
        }
    }


    //switches cameras, plays explosions w/sfx, shakes the camera and loads the next scene
    IEnumerator fadeToNextScene()
    {
        levelManger.StopTimer();

        ExplosionSound1.Play();
        //StartCoroutine(cameraShake(1f, shakeAmount));
        explosion.GetComponent<VisualEffect>().Play();
        /*yield return new WaitForSeconds(0.5f);
        ExplosionSound2.Play();
        StartCoroutine(cameraShake(1f, shakeAmount));
        explosion1.GetComponent<VisualEffect>().Play();
        yield return new WaitForSeconds(0.2f);
        ExplosionSound3.Play();
        StartCoroutine(cameraShake(1f, shakeAmount));
        explosion2.GetComponent<VisualEffect>().Play();
        yield return new WaitForSeconds(0.3f);
        ExplosionSound4.Play();
        StartCoroutine(cameraShake(5f, shakeAmount));
        explosion.GetComponent<VisualEffect>().Play();*/

        gun.SetActive(false);
        DontDestroyOnLoad(gun);
        Destroy(gun);


        yield return new WaitForSeconds(2);

        //SceneManager.LoadScene("UpdatedOceanFloor");
        loading.GetComponent<loading>().LoadNextScene("UpdatedOceanFloor");
    }

    //shakes the camera
    IEnumerator cameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = outroCamera.transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            outroCamera.transform.localPosition += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        outroCamera.transform.localPosition = originalPos;
    }
}
