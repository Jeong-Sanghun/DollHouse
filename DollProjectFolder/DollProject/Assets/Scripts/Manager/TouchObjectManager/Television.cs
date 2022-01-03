using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : TouchableObject
{
    [SerializeField]
    GameObject televisionLightObject;
    bool isCoroutineOn = false;
    public override void OnTouch()
    {
        base.OnTouch();
        //라이트가 켜져있나 꺼져있나. 
        
        televisionLightObject.SetActive(!televisionLightObject.activeSelf);
        mainSceneManager.TurnOnTV();
        if (mainSceneManager.watchingTV)
        {
            StartCoroutine(WatchingTVCor());
            Debug.Log("코루틴 시작");
        }
    }
    IEnumerator WatchingTVCor()
    {
        if (!isCoroutineOn)
        {
            isCoroutineOn = true;
            float timer = 0;
            while (timer < 5f)
            {
                timer += Time.deltaTime;
                if (!mainSceneManager.watchingTV)
                {
                    isCoroutineOn = false;
                    yield break;
                }
                if (timer >= 5f)
                {
                    Debug.Log("5초 경과");
                    mainSceneManager.energyPoint--;
                    mainSceneManager.exprLevel++;
                    mainSceneManager.Equalize();
                }
                yield return null;
            }
            if (mainSceneManager.energyPoint > 0)
            {
                isCoroutineOn = false;
                StartCoroutine(WatchingTVCor());
            }
            else
            {
                isCoroutineOn = false;
                yield break;
            }
        }
    }
}