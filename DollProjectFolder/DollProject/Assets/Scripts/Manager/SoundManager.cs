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
    AudioClip doorSound;
    [SerializeField]
    AudioClip openLocketSound;
    [SerializeField]
    AudioClip locektKeyboardSound;
    [SerializeField]
    AudioClip buttonSound;
    [SerializeField]
    AudioClip noteOpenSound;
    [SerializeField]
    AudioClip parentFootSound;

    [SerializeField]
    AudioSource walkingSource;
    [SerializeField]
    AudioClip[] walkingSoundArray;

    [SerializeField]
    AudioSource crySource;
    [SerializeField]
    AudioClip[] crySoundArray;


    [SerializeField]
    AudioSource tvSource;
    [SerializeField]
    AudioClip[] tvClip;

    bool isWalkingIndexOne;

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

    public void PauseTv()
    {
        tvSource.Pause();
    }

    public void ResumeTv()
    {
        tvSource.Play();
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

    public void DoorSoundPlay()
    {
        effectSource.clip = doorSound;
        effectSource.Play();
    }

    public void OpenLocketPlay()
    {
        effectSource.clip = openLocketSound;
        effectSource.Play();
    }
    public void LocketKeyboardPlay()
    {
        if(effectSource.clip != locektKeyboardSound)
        {
            effectSource.clip = locektKeyboardSound;
            effectSource.Play();
        }
        else
        {
            if(effectSource.isPlaying == false)
                effectSource.Play();
        }
    }
    public void ButtonPlay()
    {
        effectSource.clip = buttonSound;
        effectSource.Play();
    }
    public void NoteOpenPlay()
    {
        effectSource.clip = noteOpenSound;
        effectSource.Play();
    }
    public void ParentFootPlay()
    {
        effectSource.clip = parentFootSound;
        effectSource.Play();
    }

    public void WalkingPlay()
    {
        if (walkingSource.isPlaying)
        {
            return;
        }
        if(isWalkingIndexOne == true)
        {
            isWalkingIndexOne = false;
            walkingSource.clip = walkingSoundArray[0];
        }
        else
        {
            isWalkingIndexOne = true;
            walkingSource.clip = walkingSoundArray[1];
        }
        walkingSource.Play();
    }


    public void CryPlay(int index)
    {
        crySource.clip = crySoundArray[index];
        crySource.Play();
    }

    public void Mute()
    {
        crySource.volume = 0;
        bgmSource.volume = 0;
        effectSource.volume = 0;
        tvSource.volume = 0;
        walkingSource.volume = 0;
    }

    public void UnMute()
    {
        crySource.volume = 1;
        bgmSource.volume = 0.8f;
        effectSource.volume = 1;
        tvSource.volume = 1;
        walkingSource.volume = 1;
    }
}
