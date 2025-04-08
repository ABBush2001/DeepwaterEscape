using System.Collections;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpProText;
    private string writer;
    [SerializeField] private Coroutine coroutine;

    [SerializeField] private float delayBeforeStart = 0f;
    [SerializeField] private float timeBtwChars = 0.1f;
    [SerializeField] private string leadingChar = "";
    [SerializeField] private bool leadingCharBeforeDelay = false;
    [SerializeField] private bool startOnEnable = false;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;  // Assign in Inspector
    [SerializeField] private AudioClip typeSound;  // Assign in Inspector
    [SerializeField] private bool randomPitch = true;  // Enable pitch variation

    void Awake()
    {
        if (tmpProText != null)
        {
            writer = tmpProText.text;
        }
    }

    private void OnEnable()
    {
        if (startOnEnable) StartTypewriter();
    }

    public void SetText(string text)
    {
        writer = text;
        StartTypewriter();  // Start the typewriter effect when new text is set
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void StartTypewriter()
    {
        StopAllCoroutines();

        if (tmpProText != null)
        {
            tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";
            StartCoroutine(TypeWriterTMP());
        }
    }

    IEnumerator TypeWriterTMP()
    {
        tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";
        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (tmpProText.text.Length > 0)
            {
                tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
            }
            tmpProText.text += c;
            tmpProText.text += leadingChar;

            // Play typing sound
            PlayTypeSound();

            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
        }
    }

    private void PlayTypeSound()
    {
        if (audioSource && typeSound) // Ensure AudioSource & Clip are assigned
        {
            if (randomPitch)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);  // Slight pitch variation
            }
            audioSource.PlayOneShot(typeSound);
        }
    }
}
