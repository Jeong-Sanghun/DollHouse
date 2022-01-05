using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : TouchableObject
{
    [SerializeField]
    GameObject televisionLightObject;
    [SerializeField]
    AudioSource tvSound;
    [SerializeField]
    AudioClip[] tvSoundArray;

    protected override void Start()
    {
        base.Start();
        if(GameManager.singleTon.saveData.tvPower == true)
        {
            mainSceneManager.watchingTV = false;
            televisionLightObject.SetActive(true);
            StartCoroutine(WatchingTVCor());
        }
    }

    public override void OnTouch()
    {
        base.OnTouch();
        //라이트가 켜져있나 꺼져있나. 
        //if (mainSceneManager.energyPoint > 0)
        //{
            televisionLightObject.SetActive(!televisionLightObject.activeSelf);
            if (televisionLightObject.activeSelf)
            {
                StartCoroutine(WatchingTVCor());
            }
            else
            {
                SoundManager.singleTon.StopTv();
            }
      //  }

    }
    IEnumerator WatchingTVCor()
    {
        if (!mainSceneManager.watchingTV)
        {
            SoundManager.singleTon.PlayTv();
            mainSceneManager.watchingTV = true;
            mainSceneManager.Equalize();
            float timer = 0;

            while (timer < 5f)
            {
                timer += Time.deltaTime;
                if (!televisionLightObject.activeSelf)
                {
                    mainSceneManager.watchingTV = false;
                    mainSceneManager.Equalize();
                    yield break;
                }
                if (timer >= 5f)
                {
                    Debug.Log("5초 경과");
                    mainSceneManager.energyPoint--;
                    if(mainSceneManager.exprLevel < 3)
                    {
                        mainSceneManager.exprLevel++;
                    }
                }
                yield return null;
            }
            if (mainSceneManager.energyPoint > 0)
            {
                mainSceneManager.watchingTV = false;
                mainSceneManager.Equalize();
                StartCoroutine(WatchingTVCor());
            }
            else
            {
                mainSceneManager.watchingTV = false;
                SoundManager.singleTon.StopTv();
                mainSceneManager.Equalize();
                televisionLightObject.SetActive(false);
                //StartCoroutine(mainSceneManager.ParentAppearCoroutine());
            }
        }
    }
}