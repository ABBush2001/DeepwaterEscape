using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpProText;  // Using TMP_Text here
    string writer;
    [SerializeField] private Coroutine coroutine;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;
    [Space(10)][SerializeField] private bool startOnEnable = false;

    [Header("Collision-Based")]
    [SerializeField] private bool clearAtStart = false;
    [SerializeField] private bool startOnCollision = false;
    enum options { clear, complete }
    [SerializeField] options collisionExitOptions;

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
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
        }
    }
}
