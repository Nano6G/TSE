using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SceneManagerScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    
    Resolution[] resolutions; //Create array for the resolutions.
    int selectscene;
    //Main Menu Code
    private void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray(); //Gather list of resolutions available
        resolutionDropdown.ClearOptions(); //Clear resolutions in option dropdown
        
        List<string> options = new List<string>(); //List of  strings containing resolutions

        int currentResolutionIndex = 0; //Index used to make sure the selected resolution is default

        //Loop through resolutions array 
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height; //Create a string for each resolution
            options.Add(option); //Add the string to the options list

            //Compare the resolution lenght and height to see if the correct resolution is being used
            if (resolutions[i].width == Screen.width && 
                resolutions[i].height == Screen.height) 
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options); //Add options list to resolutions dropdown
        resolutionDropdown.value = currentResolutionIndex; //Set the resolution to default
        resolutionDropdown.RefreshShownValue(); //Display the resolution

        DontDestroyOnLoad(gameObject);
    }
    public void SwitchToScene(int input)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(input);
    }
    public void SwitchToScenePlay()
    {
        if (selectscene > 0 && selectscene < 4)
            SceneManager.LoadScene(selectscene);
    }
    public void ChangeSceneSelect(Dropdown dropin)
    {
        selectscene = dropin.value + 1;
        Debug.Log("SELECTSCENE SET TO: " + selectscene);
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }

    //Settings Menu Code
    //Volume Slider Settings

    //Full screen toggle option
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Set the resolution of the game
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //Set the quality of the project
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); //Change the index based on the quality settings in the project settings
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Pause Menu
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    
    //Update the pause menu
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
