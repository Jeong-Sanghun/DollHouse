using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleTon;
    public SaveDataClass saveData;


    // Start is called before the first frame update
    void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(gameObject);//이거만 존재하게 만듬
        }
        else
        {
            Destroy(gameObject);//두번째로 들어어오는거 삭제
        }

    }

    private void Start()
    {
        saveData = JsonManager.LoadSaveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
