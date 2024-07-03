using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Entry : MonoBehaviour
{
    public GameObject SettingsCanvas;
    public GameObject canvas;
    public GameObject RotateCanvas;

    private void Update()
    {
        if(Screen.height >= Screen.width)
        {
            RotateCanvas.SetActive(true);

        }
        else
        {
            RotateCanvas.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel" , 1));
    }
    public void Levels()
    {
        SceneManager.LoadScene(26);
    }
    public void SettingBtn()
    {
        SettingsCanvas.SetActive(true);
    }
    public void SettingBtnExit()
    {
        SettingsCanvas.SetActive(false);
    }
}