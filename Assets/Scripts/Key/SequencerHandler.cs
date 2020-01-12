using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using UnityEngine.UI;

public class SequencerHandler : MonoBehaviour
{
    public AudioHelmClock clock;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        

    }
    public void SetBPM()
    {
        text.text = ((int)clock.bpm).ToString();
    }
    // Update is called once per frame
    void Update()
    {
        SetBPM();
    }
}
