using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;

public class AreaDetector : MonoBehaviour
{
    Collider2D m_Collider;

    public bool enter = true;
    public bool stay = true;
    public bool exit = true;

    public int index = -1;

    public List<Cat> catList = new List<Cat>();
    private List<AudioHelm.Sequencer> sequencerList;
    public Button startCat;


    private void OnTriggerEnter2D(Collider2D other) {
        if (enter) {
            if (other.gameObject.name != "PlayerCat") {
                Debug.Log("entered");
                catList.Add(other.gameObject.GetComponent<Cat>());
            }
            if (other.gameObject.name == "PlayerCat") {
                OnPlay();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (exit) {
            if (other.gameObject.name != "PlayerCat") {
                Debug.Log("exit");
                other.gameObject.GetComponent<Cat>().sequencer.enabled = false;
                catList.Remove(other.gameObject.GetComponent<Cat>());
                Debug.Log(AudioHelmClock.GetGlobalBeatTime().ToString());
            }
            if (other.gameObject.name == "PlayerCat") {
                OnPause();
            }
        }
    }

    public void OnPlay() {
        Debug.Log("onplay");
        foreach(Cat cat in catList) {
            cat.sequencer.loop = true;
            cat.sequencer.enabled = true;
            cat.sequencer.StartOnNextCycle();
        }
        //AudioHelm.AudioHelmClock clock = new AudioHelm.AudioHelmClock();
    }


    public void OnPause() {
        Debug.Log("onPause");
        foreach(Cat cat in catList) {
            //sequencerList.Add(cat.sequencer);
            cat.sequencer.loop = false;
            cat.sequencer.enabled = false;
            //cat.sequencer.StartOnNextCycle();
        }
    }
}
