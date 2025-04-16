using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCutscene : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneJF;
    public GameObject cutsceneCamera;

    public GameObject splineAnimationObject; // Reference to the object with the SplineAnimation script
    //public GameObject DialogueManager;


    private Animator jellyFishAnimator;
    private MonoBehaviour splineAnimationScript; // General reference for the spline animation script

   // [SerializeField] private TextAsset inkJson;
    private void Start()
    {
        // Get the Animator component from the JellyFish object
        if (cutsceneJF != null)
        {
            jellyFishAnimator = cutsceneJF.GetComponent<Animator>();
        }

        // Get the SplineAnimation script from the splineAnimationObject
        if (splineAnimationObject != null)
        {
            splineAnimationScript = splineAnimationObject.GetComponent<MonoBehaviour>(); // Replace MonoBehaviour with the actual script type if known
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers this
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            cutsceneCamera.SetActive(true);
            cutsceneJF.SetActive(true);
            thePlayer.SetActive(false);

            // Play the JellyFish animation
            if (jellyFishAnimator != null)
            {
                jellyFishAnimator.Play("JellyFish_Animation");
                StartCoroutine(DisableJellyFishAnimator());
            }
            else
            {
                Debug.LogWarning("No Animator found on cutsceneJF!");
            }

            // DialogueManager.GetComponent<DialogueManager>().EnterDialogueMode(inkJson);
            StartCoroutine(FinishCut());
        }
    }

    private IEnumerator DisableJellyFishAnimator()
    {
        if (jellyFishAnimator != null)
        {
            AnimatorStateInfo stateInfo = jellyFishAnimator.GetCurrentAnimatorStateInfo(0);

            // Wait for the animation to finish playing
            while (stateInfo.normalizedTime < 1f || !stateInfo.IsName("JellyFish_Animation"))
            {
                yield return null;
                stateInfo = jellyFishAnimator.GetCurrentAnimatorStateInfo(0);
            }

            // Disable the Animator component
            jellyFishAnimator.enabled = false;
        }
    }

    private IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(3f);

        // Enable the player
        thePlayer.SetActive(true);

        // Deactivate the cutscene camera
        cutsceneCamera.SetActive(false);

        // Enable the spline animation from another object
        if (splineAnimationScript != null)
        {
            splineAnimationScript.enabled = true; // Enable the spline animation script
        }
        else
        {
            Debug.LogWarning("No animation script found on the specified object!");
        }
    }
}
