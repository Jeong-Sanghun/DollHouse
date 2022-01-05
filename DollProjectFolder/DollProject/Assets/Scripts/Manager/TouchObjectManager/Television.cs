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

    public override void OnTouch()
    {
        base.OnTouch();
        //����Ʈ�� �����ֳ� �����ֳ�. 
        if (mainSceneManager.energyPoint > 0)
        {
            televisionLightObject.SetActive(!televisionLightObject.activeSelf);
            if (televisionLightObject.activeSelf)
            {
                StartCoroutine(WatchingTVCor());
            }
            else
            {
                SoundManager.singleTon.StopTv();
            }
        }

    }
    IEnumerator WatchingTVCor()
    {
        if (!mainSceneManager.watchingTV)
        {
            SoundManager.singleTon.PlayTv();
            mainSceneManager.watchingTV = true;
            float timer = 0;

            while (timer < 5f)
            {
                timer += Time.deltaTime;
                if (!televisionLightObject.activeSelf)
                {
                    mainSceneManager.watchingTV = false;
                    yield break;
                }
                if (timer >= 5f)
                {
                    Debug.Log("5�� ���");
                    mainSceneManager.energyPoint--;
                    if(mainSceneManager.exprLevel < 3)
                    {
                        mainSceneManager.exprLevel++;
                    }

                    mainSceneManager.Equalize();
                }
                yield return null;
            }
            if (mainSceneManager.energyPoint > 0)
            {
                mainSceneManager.watchingTV = false;
                StartCoroutine(WatchingTVCor());
            }
            else
            {
                mainSceneManager.watchingTV = false;
                SoundManager.singleTon.StopTv();
                televisionLightObject.SetActive(false);
                //StartCoroutine(mainSceneManager.ParentAppearCoroutine());
            }
        }
    }
}