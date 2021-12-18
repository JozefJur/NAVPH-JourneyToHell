using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Inspired by https://www.youtube.com/watch?v=YOaYQrN1oYQ&ab_channel=Brackeys
public class SettingsMenuScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Dropdown resDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;
    public bool isFullScreen;
    Resolution[] resolutions;

    private float volumeToSave;
    private bool fullScreenToSave;
    private Resolution resolutionToSave;

    public void SetSettings()
    {

        if(resolutions != null)
        {
            int loopIndex = 0;
            foreach (Resolution res in resolutions)
            {

                if (res.Equals(Screen.currentResolution))
                {
                    resolutionToSave = resolutions[loopIndex];
                    resDropdown.value = loopIndex;
                    resDropdown.RefreshShownValue();
                    break;
                }
                loopIndex++;
            }
            bool result = audioMixer.GetFloat("volume", out volumeToSave);
            volumeSlider.value = result ? volumeToSave : 0;
            fullScreenToSave = Screen.fullScreen;
            fullScreenToggle.isOn = fullScreenToSave;
        }

    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> resLabels = new List<string>();

        int loopIndex = 0;
        int resIndex = 0;

        foreach(Resolution res in resolutions){
            resLabels.Add(res.width + "x" + res.height);

            if(res.Equals(Screen.currentResolution))
            {
                resIndex = loopIndex;
            }

            loopIndex++;

        }
        resDropdown.AddOptions(resLabels);
        SetSettings();
    }

    public void SetVolume(float volume)
    {
        volumeToSave = volume;
        Debug.Log(volume);
        //audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool fullscreen)
    {
        fullScreenToSave = fullscreen;
        //Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int index)
    {
        resolutionToSave = resolutions[index];
        //Screen.SetResolution(resolutions[index].width, resolutions[index].height, isFullScreen);
    }

    public void Save()
    {
        Screen.fullScreen = fullScreenToSave;
        audioMixer.SetFloat("volume", volumeToSave);
        Screen.SetResolution(resolutionToSave.width, resolutionToSave.height, Screen.fullScreen);
    }
}
