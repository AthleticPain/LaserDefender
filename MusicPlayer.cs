using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float gameVolume = 0.5f;
    [Range(0, 1)] [SerializeField] float menuVolume = 0.25f;

    string sceneName;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //sceneName = SceneManager.GetActiveScene().name;
        //VolumeAdjust();   
    }

    /*private void VolumeAdjust()
    {
        if(sceneName == "Game")
        {
            myAudioSource.volume = gameVolume;
        }
        else
        {
            myAudioSource.volume = menuVolume;
        }
    }*/
}
