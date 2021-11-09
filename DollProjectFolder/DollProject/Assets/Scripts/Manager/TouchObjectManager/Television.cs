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
        //라이트가 켜져있나 꺼져있나. 
        
        televisionLightObject.SetActive(!televisionLightObject.activeSelf);
        mainSceneManager.TurnOnTV();
    }
}
