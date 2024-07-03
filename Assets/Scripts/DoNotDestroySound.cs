using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroySound : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] soundObj = GameObject.FindGameObjectsWithTag("GameSound");
        if (soundObj.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
