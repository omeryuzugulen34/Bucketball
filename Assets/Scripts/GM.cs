using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GM : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject AdCanvas;
    public static GM Instance;
    private void Awake()
    {
        ball.DesactiveRb();
        if (Instance == null)
        {
            Instance = this;
        }
    }
    Camera cam;
    public Ball ball;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;
    public bool isDragging = false;
    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;
    public int shotCount;
    public bool wait = true;
    GameObject[] soundObj;
    AudioSource sound;
    public AudioClip ButtonSound;
    public AudioClip WinSound;
    public AudioClip RestartSound;
    void Start()
    {
        cam = Camera.main;
        ball.DesactiveRb();
        shotCount = 1;
        soundObj = GameObject.FindGameObjectsWithTag("GameSound");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotCount>0)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                isDragging = true;
                OnDragStart();
            }
        }
        if (Input.GetMouseButtonUp(0) && shotCount > 0 && isDragging == true)
        {
            if (EventSystem.current.currentSelectedGameObject == null && isDragging == true)
            {
                isDragging = false;
                shotCount = 0;
                OnDragEnd();
            } 
        }
        if (isDragging)
        {
            OnDrag();
        }
    }
    void OnDragStart()
    {
        ball.DesactiveRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();
    }
    void OnDragEnd()
    {
        ball.ActivateRb();
        ball.Push(force);
        trajectory.Hide();
        PlayerPrefs.SetFloat("forcex" + SceneManager.GetActiveScene().buildIndex, force.x);
        PlayerPrefs.SetFloat("forcey" + SceneManager.GetActiveScene().buildIndex, force.y);
    }
    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = (direction * distance  * pushForce);
        trajectory.UpdateDots(ball.pos, force);
    }
    public void Pause()
    {
        PauseCanvas.SetActive(true);
    }
    public void PauseExit()
    {
        PauseCanvas.SetActive(false);
    }
    public void Restart()
    {
        GameObject.Find("Ball").GetComponent<Ball>().restartcount++;
        PlayerPrefs.SetInt("Restart", GameObject.Find("Ball").GetComponent<Ball>().restartcount);
        if (PlayerPrefs.GetInt("Restart", 0) >= 3 && PlayerPrefs.GetInt("LastLevel", 1) < SceneManager.GetActiveScene().buildIndex + 1 && PlayerPrefs.GetInt("Ads", 0) == 0)
        {
            AdCanvas.SetActive(true);
            ball.DesactiveRb();
            Debug.Log(GameObject.Find("Ball").GetComponent<Ball>().restartcount + "GM");
            PlayerPrefs.SetInt("Ads", 1);

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void PlayOneSHOT()
    {
        sound = soundObj[0].GetComponent<AudioSource>();
        sound.PlayOneShot(ButtonSound, sound.volume);
    }
    public void PlayOneShotWin()
    {
        sound = soundObj[0].GetComponent<AudioSource>();
        sound.PlayOneShot(WinSound, sound.volume);
    }
    public void PlayOneShotRestart()
    {
        sound = soundObj[0].GetComponent<AudioSource>();
        sound.PlayOneShot(RestartSound, sound.volume);
    }
    public void RewardedAds()
    {
        PlayerPrefs.SetInt("Ads", 0);
        PlayerPrefs.SetInt("Restart", 0);
        if (PlayerPrefs.GetInt("LastLevel") < (SceneManager.GetActiveScene().buildIndex + 1))
        {
            PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex + 1);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NoThanks()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}