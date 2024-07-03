using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    public GameObject SkipButton;
    public ParticleSystem exposion;
    Camera cam;
    public int restartcount;
    public GameObject NextlvlPanel;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    [HideInInspector] public Vector3 pos
    {
        get
        {
            PlayerPrefs.SetFloat("ballposx" + SceneManager.GetActiveScene().buildIndex, transform.position.x);
            PlayerPrefs.SetFloat("ballposy" + SceneManager.GetActiveScene().buildIndex, transform.position.y);
            PlayerPrefs.SetFloat("ballposz" + SceneManager.GetActiveScene().buildIndex, transform.position.z);

            return transform.position;
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        cam = Camera.main;

        if (PlayerPrefs.HasKey("LastLevel"))
        {
            if (SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("LastLevel"))
            {
                PlayerPrefs.SetInt("Ads", 0);
            }
        }

        if (PlayerPrefs.HasKey("Restart"))
        {
            restartcount = PlayerPrefs.GetInt("Restart");
            if(PlayerPrefs.GetInt("Restart") >= 3 && PlayerPrefs.GetInt("Ads",0) == 1)
            {
                SkipButton.SetActive(true);
            }
        }
        
    }
    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    public void ActivateRb()
    {
        rb.isKinematic = false;
    }
    public void DesactiveRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Restart")
        {
            restartcount++;
            Debug.Log(restartcount + "Collision");

            PlayerPrefs.SetInt("Restart", restartcount);
            if (PlayerPrefs.GetInt("Restart", 0) >= 3 && PlayerPrefs.GetInt("LastLevel", 1) < SceneManager.GetActiveScene().buildIndex+1 && PlayerPrefs.GetInt("Ads",0) == 0)
            {
                DesactiveRb();
                GameObject.Find("GameManager").GetComponent<GM>().AdCanvas.SetActive(true);
                Debug.Log(restartcount);
                PlayerPrefs.SetInt("Ads", 1);
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GM>().PlayOneShotRestart();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bucket" && collision.gameObject.transform.parent.GetComponent<SpriteRenderer>().color == gameObject.GetComponent<SpriteRenderer>().color)
        {
            cam.GetComponent<Animator>().SetTrigger("Shake");
            exposion.Play();
            GameObject.Find("GameManager").GetComponent<GM>().PlayOneShotWin();
            if(PlayerPrefs.GetInt("LastLevel") < (SceneManager.GetActiveScene().buildIndex + 1))
            {
                PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex + 1);
            }
            NextlvlPanel.SetActive(true);
            PlayerPrefs.SetInt("Ads", 0);
            PlayerPrefs.SetInt("Restart", 0);
            PlayerPrefs.DeleteKey("Restart");
            DesactiveRb();
        }
        else if(collision.gameObject.tag == "Bucket")
        {
            restartcount++;
            Debug.Log(restartcount + "Triger");

            PlayerPrefs.SetInt("Restart", restartcount);
            if (PlayerPrefs.GetInt("Restart",0) >= 3 && PlayerPrefs.GetInt("LastLevel",1) < SceneManager.GetActiveScene().buildIndex + 1 && PlayerPrefs.GetInt("Ads", 0) == 0)
            {
                DesactiveRb();
                GameObject.Find("GameManager").GetComponent<GM>().AdCanvas.SetActive(true);
                Debug.Log(restartcount);
                PlayerPrefs.SetInt("Ads", 1);

            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GM>().PlayOneShotRestart();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            

        }
    }
    public void NextLvlBtn()
    {
        PlayerPrefs.SetInt("Restart", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        DesactiveRb();
    }
    public void MainMenuBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void LevelsMenuBtn()
    {
        SceneManager.LoadScene(26);
    }
    public void DeletePref()
    {
        SkipButton.SetActive(true);
    }
}