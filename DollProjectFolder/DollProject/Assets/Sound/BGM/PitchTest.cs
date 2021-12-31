using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchTest : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.time = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(source.pitch>0.4f)
        source.pitch -= Time.deltaTime / 30f;
    }
}
