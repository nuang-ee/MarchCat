﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class MusicPlayHandler : MonoBehaviour
{
    public AudioHelmClock clock;

    void Awake() {
        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.transform.GetChild(2).GetComponent<SampleSequencer>() != null) {
            if (clock.pause) {
                clock.pause = false;
            }
            other.gameObject.transform.GetChild(2).GetComponent<SampleSequencer>().enabled = true;
            Debug.Log(other.gameObject.GetComponent<Cat>().sequencer.loop);
            other.gameObject.GetComponent<Cat>().sequencer.loop = true;
            Debug.Log("After : " + other.gameObject.transform.GetChild(2).GetComponent<SampleSequencer>().loop.ToString());
            other.gameObject.GetComponent<Cat>().sequencer.StartOnNextCycle();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.transform.GetChild(2).GetComponent<SampleSequencer>() != null) {
            //other.gameObject.transform.GetChild(2).GetComponent<SampleSequencer>().loop = false;
            //other.gameObject.transform.GetChild(2).GetComponent<SampleSequencer>().enabled = false;
        }
    }   
}
