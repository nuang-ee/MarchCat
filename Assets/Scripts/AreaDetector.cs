using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaDetector : MonoBehaviour
{
    Collider2D m_Collider;

    public bool enter = true;
    public bool stay = true;
    public bool exit = true;

    public List<Cat> catList;
    private List<AudioHelm.Sequencer> sequencerList;
    public Button startCat;


    private void OnTriggerEnter2D(Collider2D other) {
        if (enter) {
            Debug.Log("entered");
            catList.Add(other.gameObject.GetComponent<Cat>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (exit) {
            Debug.Log("exit");
        }
    }

    public void OnPlay() {
        foreach(Cat cat in catList) {
            //sequencerList.Add(cat.sequencer);
            cat.sequencer.StartOnNextCycle();
        }
        //AudioHelm.AudioHelmClock clock = new AudioHelm.AudioHelmClock();

    }


}
