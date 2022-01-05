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

    [SerializeField]
    AudioSource tvSource;
    [SerializeField]
    AudioClip[] tvClip;
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

    public void PlayTv()
    {
        if (!tvSource.isPlaying)
        {
            tvSource.clip = tvClip[Random.Range(0, 5)];
            tvSource.Play();
        }
    }

    public void StopTv()
    {
        tvSource.Stop();
    }

    public IEnumerator BgmPitchDownCoroutine()
    {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            bgmSource.pitch -= Time.deltaTime / 4;
            yield return null;
        }
    }

    public void BgmPitchOne()
    {
        bgmSource.pitch = 1;
    }
   
}
