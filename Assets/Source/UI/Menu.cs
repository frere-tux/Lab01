using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Loading")]
    public GameObject loadingMenu;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    [Header("Options")]
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;


    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        fullscreenToggle.isOn = Screen.fullScreen;

        List<string> options = new List<string>();
        for (int i = 0 ; i < resolutions.Length ; ++i)
        {
            Resolution resolution = resolutions[i];

            string option = resolution.width + " x " + resolution.height;
            options.Add(option);

            if (resolution.width == Screen.currentResolution.width
                && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;

        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void LoadLevel(string scene)
    {
        StartCoroutine(AsyncLoadLevel(scene));
    }

    private IEnumerator AsyncLoadLevel(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        loadingMenu.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            progressBar.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex != currentResolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
