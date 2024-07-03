using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.01f, 0.3f)] float dotMinScale;
    [SerializeField] [Range(0.3f, 1f)] float dotMaxScale;


    Vector2 pos;
    float timeStamp;
    Transform[] dotsList;

    Transform[] lastdotslist;
    [SerializeField] GameObject lastdotParent;
    [SerializeField] GameObject lastdotPrefab;

    private void Start()
    {
        Hide();
        PrepareDots();
        if (PlayerPrefs.HasKey("ballposx" + SceneManager.GetActiveScene().buildIndex))
        {
            LastDotsPrepare(new Vector2(PlayerPrefs.GetFloat("forcex"+ SceneManager.GetActiveScene().buildIndex), PlayerPrefs.GetFloat("forcey"+ SceneManager.GetActiveScene().buildIndex)));
        }
    }

    void PrepareDots()
    {
       
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab , null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if(scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    void LastDotsPrepare(Vector2 forceApplied2)
    {
        lastdotslist = new Transform[dotsNumber];
        lastdotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            lastdotslist[i] = Instantiate(lastdotPrefab, null).transform;
            lastdotslist[i].parent = lastdotParent.transform;

            lastdotslist[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (PlayerPrefs.GetFloat("ballposx"+ SceneManager.GetActiveScene().buildIndex) + PlayerPrefs.GetFloat("forcex"+ SceneManager.GetActiveScene().buildIndex) * timeStamp);
            pos.y = (PlayerPrefs.GetFloat("ballposy"+ SceneManager.GetActiveScene().buildIndex) + PlayerPrefs.GetFloat("forcey"+ SceneManager.GetActiveScene().buildIndex) * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;
            lastdotslist[i].position = pos;

            timeStamp += dotSpacing;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {

        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;
            dotsList[i].position = pos;

            timeStamp += dotSpacing;
        }


    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
