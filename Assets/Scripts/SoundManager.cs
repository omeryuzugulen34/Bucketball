using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider VolumeSlider;
    public GameObject ObjectMusic;

    public GameObject MusicOnIcon;
    public GameObject MusicOffIcon;

    private float MusicVolume = 0f;
    private AudioSource Audiosource;

    void Start()
    {

        ObjectMusic = GameObject.FindWithTag("GameSound");
        Audiosource = ObjectMusic.GetComponent<AudioSource>();

        MusicVolume = PlayerPrefs.GetFloat("volumeS", 0.4f);
        Audiosource.volume = MusicVolume;
        VolumeSlider.value = MusicVolume;
    }


    void Update()
    {
        if (MusicVolume != VolumeSlider.value)
        {
            MusicVolume = VolumeSlider.value;
            PlayerPrefs.SetFloat("volumeS", VolumeSlider.value);
        }
        Audiosource.volume = MusicVolume;

        if (VolumeSlider.value == 0)
        {
            MusicOffIcon.SetActive(true);
            MusicOnIcon.SetActive(false);
        }
        else
        {
            MusicOffIcon.SetActive(false);
            MusicOnIcon.SetActive(true);
        }
    }

    public void VolumeUpdater(float volume)
    {
        MusicVolume = volume;
    }

    public void MusicOff()
    {
        if (VolumeSlider.value == 0)
        {
            if(PlayerPrefs.GetFloat("volumeS") > 0)
            {
                VolumeSlider.value = PlayerPrefs.GetFloat("volumeS", 0.4f);
            }
            else
            {
                VolumeSlider.value = 0.1f;
            }
        }
        else
        {
            VolumeSlider.value = 0;
        }
    }

}
