using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TouchableObject
{
    public override void OnTouch()
    {
        base.OnTouch();
        SoundManager.singleTon.DoorSoundPlay();

    }
}
