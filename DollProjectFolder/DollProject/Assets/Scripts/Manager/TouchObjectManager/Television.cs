using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : TouchableObject
{
    [SerializeField]
    GameObject televisionLightObject;

    public override void OnTouch()
    {
        base.OnTouch();
        //����Ʈ�� �����ֳ� �����ֳ�. 
        
        televisionLightObject.SetActive(!televisionLightObject.activeSelf);
        mainSceneManager.TurnOnTV();
    }
}
