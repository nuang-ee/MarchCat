using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;

public class SequencerUIHandler : MonoBehaviour
{

    public Sequencer DrumSequencer;
    public Sequencer KeyboardSequencer;
    public GameObject SequencerUI;
    void Awake()
    {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];
        bool UIselect = currentCat.instrumentType;
        if (UIselect)
        {
            GameObject.Find("KeyboardPanel").SetActive(true);
            GameObject.Find("DrumPanel").SetActive(false);
        }
        else {
            GameObject.Find("KeyboardPanel").SetActive(false);
            GameObject.Find("DrumPanel").SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
