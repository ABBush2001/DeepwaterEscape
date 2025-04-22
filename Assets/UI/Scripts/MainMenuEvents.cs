using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _startButton;
    private Button _exitButton;
    private Button _howToPlayButton;
    private Button _creditsButton;
    private Button _levelButton;
    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;

    private bool _isTransitioning = false;

    public GameObject loading;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        // Query buttons by name (match them in your UI Toolkit)
        _startButton = _document.rootVisualElement.Q<Button>("Start");
        _exitButton = _document.rootVisualElement.Q<Button>("Exit");
        _howToPlayButton = _document.rootVisualElement.Q<Button>("HowToPlay");
        _creditsButton = _document.rootVisualElement.Q<Button>("Credits");
        _levelButton = _document.rootVisualElement.Q<Button>("Levels");

        if (_startButton != null)
            _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);

        if (_exitButton != null)
            _exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);

        if (_howToPlayButton != null)
            _howToPlayButton.RegisterCallback<ClickEvent>(OnHowToPlayClick);

        if (_creditsButton != null)
            _creditsButton.RegisterCallback<ClickEvent>(OnCreditsClick);

        if (_levelButton != null)
            _levelButton.RegisterCallback<ClickEvent>(OnLevelClick);

        // Collect all buttons for sound + effect
        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        foreach (var button in _menuButtons)
        {
            button.RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        if (_startButton != null)
            _startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        if (_exitButton != null)
            _exitButton.UnregisterCallback<ClickEvent>(OnExitGameClick);

        if (_howToPlayButton != null)
            _howToPlayButton.UnregisterCallback<ClickEvent>(OnHowToPlayClick);

        if (_creditsButton != null)
            _creditsButton.UnregisterCallback<ClickEvent>(OnCreditsClick);

        if (_levelButton != null)
            _levelButton.RegisterCallback<ClickEvent>(OnLevelClick);

        foreach (var button in _menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Start Button");
        StartCoroutine(PlayStart());
    }

    private void OnHowToPlayClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the HowToPlay Button");
        StartCoroutine(PlayHowto());
    }

    private void OnExitGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Exit Button");
        StartCoroutine(PlayExit());
    }

    private void OnCreditsClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Credits Button");
        StartCoroutine(PlayCredits()); 
    }
    private void OnLevelClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Level Button");
        StartCoroutine(PlayLevel());
    }


    private void OnAllButtonsClick(ClickEvent evt)
    {
        if (_audioSource != null)
            _audioSource.Play();
    }

    private IEnumerator PlayStart()
    {
        if (_isTransitioning) yield break;
        _isTransitioning = true;

        if (_audioSource != null)
            _audioSource.Play();

        yield return new WaitForSeconds(_audioSource != null ? _audioSource.clip.length : 0.5f);
        //SceneManager.LoadScene("1.Submarine");
        loading.GetComponent<loading>().LoadNextScene(6);
    }

    private IEnumerator PlayHowto()
    {
        if (_isTransitioning) yield break;
        _isTransitioning = true;

        if (_audioSource != null)
            _audioSource.Play();

        yield return new WaitForSeconds(_audioSource != null ? _audioSource.clip.length : 0.5f);
        SceneManager.LoadScene("Howto");
    }

    private IEnumerator PlayExit()
    {
        if (_isTransitioning) yield break;
        _isTransitioning = true;

        if (_audioSource != null)
            _audioSource.Play();

        yield return new WaitForSeconds(_audioSource != null ? _audioSource.clip.length : 0.5f);
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfwdjz4HT0iWeojGLPPhOp7fo7Z4mVy0J8iz__-lf81F_aDhA/viewform?usp=header");
        Application.Quit();
    }

    private IEnumerator PlayCredits()
    {
        if (_isTransitioning) yield break;
        _isTransitioning = true;

        if (_audioSource != null)
            _audioSource.Play();

        yield return new WaitForSeconds(_audioSource != null ? _audioSource.clip.length : 0.5f);
        SceneManager.LoadScene("Credits"); // Make sure the scene name matches exactly!
    }

    private IEnumerator PlayLevel()
    {
        if (_isTransitioning) yield break;
        _isTransitioning = true;

        if (_audioSource != null)
            _audioSource.Play();

        yield return new WaitForSeconds(_audioSource != null ? _audioSource.clip.length : 0.5f);
        SceneManager.LoadScene("leveltest"); // Make sure the scene name matches exactly!
    }


}
