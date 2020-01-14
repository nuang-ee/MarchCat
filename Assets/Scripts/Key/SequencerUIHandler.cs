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
    private Sequencer sequencer;
    void Awake()
    {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];
        

        bool UIselect = currentCat.instrumentType;

        GameObject DrumSequencer = GameObject.Find("DrumKit01Sequencer");
        GameObject SynthSequencer = GameObject.Find("SynthSequencer");
        GameObject BassSequencer = GameObject.Find("BassSequencer");
        GameObject GuitarSequencer = GameObject.Find("GuitarSequencer");

        if (UIselect && currentCat.catType == "supreme")
        {
            print("supreme keyboard");
            GameObject.Find("KeyboardPanel").SetActive(false);
            GameObject.Find("DrumPanel").SetActive(false);
            GameObject.Find("KeyboardPanel_32").SetActive(true);
            GameObject.Find("DrumPanel_32").SetActive(false);
        }
        else if (UIselect && currentCat.catType != "supreme")
        {
            print("normal keyboard");
            GameObject.Find("KeyboardPanel").SetActive(true);
            GameObject.Find("DrumPanel").SetActive(false);
            GameObject.Find("KeyboardPanel_32").SetActive(false);
            GameObject.Find("DrumPanel_32").SetActive(false);
        }
        else if (!UIselect && currentCat.catType == "supreme")
        {
            print("supreme drum");
            GameObject.Find("KeyboardPanel").SetActive(false);
            GameObject.Find("DrumPanel").SetActive(false);
            GameObject.Find("KeyboardPanel_32").SetActive(false);
            GameObject.Find("DrumPanel_32").SetActive(true);
        }
        else {
            print("normal drum");
            GameObject.Find("KeyboardPanel").SetActive(false);
            GameObject.Find("DrumPanel").SetActive(true);
            GameObject.Find("KeyboardPanel_32").SetActive(false);
            GameObject.Find("DrumPanel_32").SetActive(false);
        }

        if (currentCat.instrumentName == "drum")
        {
            DrumSequencer.SetActive(true);
            SynthSequencer.SetActive(false);
            BassSequencer.SetActive(false);
            GuitarSequencer.SetActive(false);
            sequencer = DrumSequencer.GetComponent<Sequencer>();
        }
        else if (currentCat.instrumentName == "synthesizer")
        {
            DrumSequencer.SetActive(false);
            SynthSequencer.SetActive(true);
            BassSequencer.SetActive(false);
            GuitarSequencer.SetActive(false);
            sequencer = SynthSequencer.GetComponent<Sequencer>();
        }
        else if (currentCat.instrumentName == "bass")
        {
            DrumSequencer.SetActive(false);
            SynthSequencer.SetActive(false);
            BassSequencer.SetActive(true);
            GuitarSequencer.SetActive(false);
            sequencer = BassSequencer.GetComponent<Sequencer>();
        }
        else if (currentCat.instrumentName == "guitar")
        {
            DrumSequencer.SetActive(false);
            SynthSequencer.SetActive(false);
            BassSequencer.SetActive(false);
            GuitarSequencer.SetActive(true);
            sequencer = GuitarSequencer.GetComponent<Sequencer>();
        }
        else
        {
            print("UIHandler : this is not good");
        }

        if (currentCat.catType == "supreme")
        {
            sequencer.length = 16;
        }
        else {
            sequencer.length = 16;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
