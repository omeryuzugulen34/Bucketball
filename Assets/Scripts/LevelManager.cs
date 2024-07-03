using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public GameObject[] Locks;
    GameObject[] soundObj;
    AudioSource sound;
    public AudioClip ButtonSound;
    public void LevelBtn(int lvl)
    {
        if (lvl <= PlayerPrefs.GetInt("LastLevel", 1))
        {
            SceneManager.LoadScene(lvl);
        }
    }
    private void Start()
    {
        soundObj = GameObject.FindGameObjectsWithTag("GameSound");
        for (int a = 0; a < PlayerPrefs.GetInt("LastLevel", 1) - 1; a++)
        {
            Locks[a].SetActive(false);
        }
    }
    public void EntryBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayOneSHOT()
    {
        sound = soundObj[0].GetComponent<AudioSource>();
        sound.PlayOneShot(ButtonSound, sound.volume);
    }
}