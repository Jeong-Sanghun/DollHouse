using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleTon;

    

    [SerializeField]
    AudioSource bgmSource;

    [SerializeField]
    AudioSource effectSource;
    [SerializeField]
    AudioClip pencilSound;
    private void Awake()
    {
        if(singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PencilSoundPlay()
    {
        effectSource.clip = pencilSound;
        effectSource.Play();
    }
   
}
