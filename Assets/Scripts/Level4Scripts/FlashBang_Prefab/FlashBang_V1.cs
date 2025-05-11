using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * This script handles the flashbang effect for the boss fight. It does
 * this by fading out a white image on the screen
*/
public class FlashBang_V1 : MonoBehaviour
{
    // The screen go white
    public float fuseTime = 4f; // Time when the bomb blow
        [Tooltip("Speed at which the white flashbang overlay fades")] 
    public float flashFadeSpeed = 15f;

    [SerializeField][Range(0f,1f)] private float flashHitVol = .7f;
    [SerializeField][Range(0f,1f)] private float flashMissVol = .4f;

    public Image whiteImage;
    private Camera cam;

    // this is the glowing affect material
    private Material objectmat;
    private bool isGlowing = true;
    //private object glowMat;

    public GameObject QAF;
    public TextMeshProUGUI timerText;// display timer

    public AudioSource WhiteNoise;

    

    private void Start()
    {
        // This it to find the wight image in the hierachy also in need to be tag by WhiteImage
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
        
        // check if the material is in the object
        if (QAF.TryGetComponent<Renderer>(out var renderer))
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

        if (timerText == null)
        {
            Debug.LogError("Timer text is not assigned");
            return;
        }

        flashFadeSpeed /= 10; // So that inspector values don't need too many zeroes .0025
    }

    public void StartFlashbang()
    {
        timerText.gameObject.SetActive(true);
        //StopAllCoroutines(); // Stops all coroutines to prevent overlapping behavior
        //^ This also stops the flashbang from fading. AKA it can permanently blind you.
        StartCoroutine(ShowWarningAndStartTimer());
    }

    private IEnumerator ShowWarningAndStartTimer()
    {
        // Display the warning message
        timerText.text = "FlashBang Warning";
        yield return new WaitForSeconds(1.5f); // Show warning for X amount seconds

        // Start the timer countdown
        StartCoroutine(UpdateTimer());
        StartCoroutine(GlowEffect());
        Invoke(nameof(Explode), fuseTime);
    }

    private IEnumerator UpdateTimer()
    {
        float remainingTime = fuseTime;

        while (remainingTime > 0)
        {
            timerText.text = $"{remainingTime:F1}s";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        timerText.text = "0.0s";
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
            StopCoroutine(WhiteFade()); // Multiple coroutines can run at once, restart whitefade to avoid things breaking.
            StartCoroutine(WhiteFade());
            PlayFlashSound(flashHitVol);
        }
        else
        {
            // If you dont see it
            Debug.Log("don't get affected!");
            PlayFlashSound(flashMissVol);
        }
        
        timerText.gameObject.SetActive(false);
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
            Ray ray = new(cam.transform.position, transform.position - cam.transform.position);

            // Check if there flashbang is on screen with nothing blocking the camera
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return hit.transform.gameObject == gameObject;
            }
        }
        return false;
    }

    private void PlayFlashSound(float volume = 1f)
    {
        WhiteNoise.volume = volume;
        WhiteNoise.Play();
    }

    // This is screen affect and the fade out
    private IEnumerator WhiteFade()
    {
        //in case flashbang gets stuck
        StopCoroutine(BreakOutOfFlash()); // also restart it incase you get flashed several times in a row.
        StartCoroutine(BreakOutOfFlash());

        // Set the screen to fully white
        whiteImage.color = new Color(1, 1, 1, 1);

        //float fadeDuration = 15f; // Total duration of the fade !NOTUSED
        //float fadeStep = 0.025f; // the fade step by step (it take make the screen visiable) !NOTUSED
        //float waitTime = fadeDuration * fadeStep; !NOTUSED

        while (whiteImage.color.a > 0.01)
        {
            // Gradually reduce the white screen
            whiteImage.color = new Color(1, 1, 1, whiteImage.color.a - (flashFadeSpeed * Time.deltaTime));
            yield return null;
        }

        // set screen back to normal
        whiteImage.color = new Color(1, 1, 1, 0);

        // to deactive the White Image 
        //whiteImage.gameObject.SetActive(false);
    }

    private IEnumerator BreakOutOfFlash()
    {
        yield return new WaitForSeconds(15f);
        StopCoroutine(WhiteFade());
        whiteImage.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator GlowEffect()
    {
        float glowSpeed = 2f; // Speed of the glow pulseing 
        float maxEmn = 10; // how bright the glow is
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

}
