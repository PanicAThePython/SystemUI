using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsMenu : MonoBehaviour
{
    public int index;

    public ResItem[] resolutions;

    public Text resolutionLabel;

    public Toggle fullscreenTog, vsyncTog;

    public AudioMixer theMixer;

    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterLabel, musicLabel, sfxLabel;

    public AudioSource sfxLoop;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;

        for(int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                index = i;
                UpdateResLabel();
            }
        }
        if (!foundRes)
        {
            resolutionLabel.text = Screen.width.ToString() + " X " + Screen.height.ToString();
        }

        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }
        masterLabel.text = (masterSlider.value + 80).ToString();
        musicLabel.text = (musicSlider.value + 80).ToString();
        sfxLabel.text = (sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LessIndex()
    {
        index--;
        if (index < 0)
        {
            index = 0;
        }
        UpdateResLabel();
    }

    public void PlusIndex()
    {
        index++;
        if(index > resolutions.Length - 1)
        {
            index = resolutions.Length - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[index].horizontal.ToString() + " X " + resolutions[index].vertical.ToString();
    }

    public void ApplyGraphics()
    {

        //Screen.fullScreen = fullscreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[index].horizontal, resolutions[index].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVol()
    {
        masterLabel.text = (masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value); //salva no sistema as alterações
    }
    public void SetMusicVol()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    public void SetSFXVol()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

    public void PlaySFXLoop()
    {
        sfxLoop.Play();
    }

    public void StopSFXLoop()
    {
        sfxLoop.Stop();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
