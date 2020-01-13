using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;

public class SequencerUIHandler : MonoBehaviour
{

    //public Sequencer DrumSequencer;
    //public Sequencer KeyboardSequencer;
    //public GameObject SequencerUI;
    void Awake()
    {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];

        bool UIselect = currentCat.instrumentType;

        GameObject DrumSequencer = GameObject.Find("DrumKit01Sequencer");
        GameObject SynthSequencer = GameObject.Find("SynthSequencer");
        GameObject BassSequencer = GameObject.Find("BassSequencer");
        GameObject GuitarSequencer = GameObject.Find("GuitarSequencer");

        if (UIselect)
        {
            GameObject.Find("KeyboardPanel").SetActive(true);
            GameObject.Find("DrumPanel").SetActive(false);
        }
        else {
            GameObject.Find("KeyboardPanel").SetActive(false);
            GameObject.Find("DrumPanel").SetActive(true);
        }

        if (currentCat.instrumentName == "drum")
        {
            DrumSequencer.SetActive(true);
            SynthSequencer.SetActive(false);
            BassSequencer.SetActive(false);
            GuitarSequencer.SetActive(false);
        }
        else if (currentCat.instrumentName == "synthesizer")
        {
            DrumSequencer.SetActive(false);
            SynthSequencer.SetActive(true);
            BassSequencer.SetActive(false);
            GuitarSequencer.SetActive(false);
        }
        else if (currentCat.instrumentName == "bass")
        {
            DrumSequencer.SetActive(false);
            SynthSequencer.SetActive(false);
            BassSequencer.SetActive(true);
            GuitarSequencer.SetActive(false);
        }
        else if (currentCat.instrumentName == "guitar")
        {
            DrumSequencer.SetActive(false);
            SynthSequencer.SetActive(false);
            BassSequencer.SetActive(false);
            GuitarSequencer.SetActive(true);
        }
        else
        {
            print("UIHandler : this is not good");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
