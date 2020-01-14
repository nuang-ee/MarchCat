using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;

public class MarchingUIHandler : MonoBehaviour
{
    private AudioHelmClock clock;
    private Slider BPMSlider;
    private Text BPMSliderText;
    // Start is called before the first frame update
    void Start()
    {
        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();
        SetBPMSlider();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBPMSlider() {
        BPMSlider = GameObject.Find("Slider").GetComponentInChildren<Slider>();
        BPMSliderText = GameObject.Find("BPMTitle").GetComponentInChildren<Text>();
        BPMSlider.value = 120;
        BPMSliderText.text = "BPM : 120";

        BPMSlider.onValueChanged.AddListener(delegate
        {
            clock.bpm = (int)BPMSlider.value;
            BPMSliderText.text = "BPM : " + clock.bpm;
        });
    }
}
