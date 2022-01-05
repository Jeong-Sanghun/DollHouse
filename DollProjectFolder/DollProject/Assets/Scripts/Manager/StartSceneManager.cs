using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    PostProcessVolume blurVolume;
    [SerializeField]
    ModuleManager moduleManager;
    [SerializeField]
    GameObject upRect;
    [SerializeField]
    GameObject downRect;

    [SerializeField]
    Button startButton;
    [SerializeField]
    Button loadButton;

    public void StartGameButton(bool isLoaded)
    {
        StartCoroutine(GameStartCoroutine(isLoaded));
    }

    IEnumerator GameStartCoroutine(bool isLoaded)
    {
        startButton.interactable = false;
        loadButton.interactable = false;
        StartCoroutine(moduleManager.VolumeModule(blurVolume, false, 1));
        StartCoroutine(moduleManager.MoveModuleRect_Linear(upRect, new Vector3(0, 850, 0), 1));
        StartCoroutine(moduleManager.MoveModuleRect_Linear(downRect, new Vector3(0, -600, 0), 1));
        yield return new WaitForSeconds(1f);
        GameManager.singleTon.GameStart(isLoaded);
    }
}
