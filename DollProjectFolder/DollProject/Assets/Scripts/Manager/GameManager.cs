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
            DontDestroyOnLoad(gameObject);//�̰Ÿ� �����ϰ� ����
        }
        else
        {
            Destroy(gameObject);//�ι�°�� ������°� ����
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
