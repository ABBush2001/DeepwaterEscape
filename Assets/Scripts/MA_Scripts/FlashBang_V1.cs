using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashBang_V1 : MonoBehaviour
{
    // The screen go white
    private float fuseTime = 4f; // Time when the bome blow
    public Image whiteImage;
    private Camera cam;

    // this is the glowing affect material
    private Material objectmat;
    private bool isGlowing = true;
    private object glowMat;

    public GameObject QAF;

    private AudioSource WhiteNoise;

    private void Start()
    {
        // This it to find the wight image in the hierachy also in need to be tag by WhiteImage
        //whiteImage = GameObject.FindGameObjectWithTag("WhiteImage").GetComponent<Image>();

        if (whiteImage == null)
        {
            // if the image is not found
            Debug.LogError("WhiteImage is not found ");
            return;
        }



        // it for the camera 
        cam = Camera.main;
        // if the Camera is not found
        if (cam == null)
        {
            Debug.LogError("camera not found!");
            return;
        }


        // to render the material
        Renderer renderer = QAF.GetComponent<Renderer>();


        // check if the material is in the object
        if (renderer != null)
        {
            objectmat = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer or material not found");
            return;
        }

        if (objectmat.name != "QAFTEX (Instance)")
        {
            Debug.LogError("Renderer or material is not emission_Glow");
            return;
        }


        startFlashbang();
    }


    public void startFlashbang()
    {
        StartCoroutine(GlowEffect());



        // This audio to trigger
        // StartCoroutine(AudioEffect());

        // this is important this is to set off the explodion and when it activate
        Invoke(nameof(Explode), fuseTime);
    }

    // to show the explosion and to determen that it was seen or not
    private void Explode()
    {
        // it for it to stop glowing
        isGlowing = false;

        // check if the camera is looking at the object
        if (CheckVisibility())
        {
            Debug.Log("go blind!");

            // this where the screen on blind and fade
            StartCoroutine(WhiteFade());

            // This audio to trigger
            //StartCoroutine(AudioEffect());
        }
        else
        {
            // If you dont see it
            Debug.Log("don't get affected!");
        }
    }

    // this is to determan if the camera is not looking or something is blocking the view
    private bool CheckVisibility()
    {
        // this if there no camera this won't work
        if (cam == null)
        {
            return false;
        }

        // camera determination position
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        // Check if the the flashbang is within the screen area
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            Ray ray = new Ray(cam.transform.position, transform.position - cam.transform.position);

            // Check if there flashbang is on screen with nothing blocking the camera
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.transform.gameObject == gameObject;
            }
        }
        return false;
    }

    // This is screen affect and the fade out
    private IEnumerator WhiteFade()
    {
        // Set the screen to fully white
        whiteImage.color = new Color(1, 1, 1, 1);

        float fadeDuration = 15f; // Total duration of the fade
        float fadeStep = 0.0025f; // the fade step by step (it take make the screen visiable)
        float waitTime = fadeDuration * fadeStep;

        while (whiteImage.color.a > 0)
        {
            // Gradually reduce the white screen
            whiteImage.color = new Color(1, 1, 1, whiteImage.color.a - fadeStep);
            yield return new WaitForSeconds(waitTime);
        }

        // set screen back to normal
        whiteImage.color = new Color(1, 1, 1, 0);

        // to deactive the White Image 
        whiteImage.gameObject.SetActive(false);
    }

    private IEnumerator GlowEffect()
    {
        float glowSpeed = 2f; // Speed of the glow pulseing 
        float maxEmn = 1.5f; // how bright the glow is
        float minEmn = 0.5f; // how dim the glow is
        Color baseColor = objectmat.GetColor("_EmissionColor"); // Base color of emission

        while (isGlowing)
        {
            // it will make the glow like a wave brighten up then dim between max and min emission
            float emission = Mathf.PingPong(Time.time * glowSpeed, maxEmn - minEmn) + minEmn;
            Color finalC = baseColor * Mathf.LinearToGammaSpace(emission);
            objectmat.SetColor("_EmissionColor", finalC);

            yield return null; // wait for next frame
        }
        // reset the glow after it stop
        objectmat.SetColor("_EmissionColor", baseColor * Mathf.LinearToGammaSpace(minEmn));
    }

    //private IEnumerator AudioEffect()
    //{
    //    WhiteNoise.Play();

    //    yield return null;
    //}
}
