﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AudioHelm;

public class SequencerSaveHandler : MonoBehaviour
{

    public Button DrumSaveButton;
    public Button KeyboardSaveButton;
    public Button Drum32SaveButton;
    public Button Keyboard32SaveButton;
    private GameObject Drumsequencer;
    private GameObject Keyboardsequencer;

    private string CatInstrument;
    // Start is called before the first frame update
    void Awake()
    {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];
        bool UIselect = currentCat.instrumentType;
        CatInstrument = currentCat.instrumentName;

        if (CatInstrument == "bass")
        {
            Keyboardsequencer = GameObject.Find("BassSequencer");
        }
        else if (CatInstrument == "drum")
        {
            //Empty..
            Drumsequencer = GameObject.Find("DrumKit01Sequencer");
        }
        else if (CatInstrument == "guitar")
        {
            Keyboardsequencer = GameObject.Find("GuitarSequencer");
        }
        else if (CatInstrument == "synthesizer")
        {
            Keyboardsequencer = GameObject.Find("SynthSequencer");
        }
        else {
            //This is not good
        }

        DrumSaveButton.onClick.AddListener(delegate
        {
            SaveDrumSequenceDataToCat();
        });
        KeyboardSaveButton.onClick.AddListener(delegate
        {
            SaveKeyboardSequenceDataToCat();
        });
        Drum32SaveButton.onClick.AddListener(delegate
        {
            SaveDrumSequenceDataToCat();
        });
        Keyboard32SaveButton.onClick.AddListener(delegate
        {
            SaveKeyboardSequenceDataToCat();
        });
    }

    void SaveDrumSequenceDataToCat() {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Debug.Log("SaveSequenceDataToCat : " + catlist.catlist.Count);
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];

        Drumsequencer.name = Drumsequencer.name + (catlist.catlist.Count - 1);
        Drumsequencer.GetComponent<Sequencer>().enabled = false;
        DontDestroyOnLoad(Drumsequencer);
        currentCat.SetSequencer(Drumsequencer.GetComponent<Sequencer>());
        Drumsequencer.transform.parent = catlist.catObjectList[catlist.catObjectList.Count - 1].transform;
        Drumsequencer.GetComponent<Sequencer>().loop = false;
        SceneManager.LoadScene(0);
    }

    void SaveKeyboardSequenceDataToCat()
    {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Debug.Log("SaveSequenceDataToCat : " + catlist.catlist.Count);
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];

        Keyboardsequencer.name = Keyboardsequencer.name + (catlist.catlist.Count - 1);
        Keyboardsequencer.GetComponent<Sequencer>().enabled = false;
        DontDestroyOnLoad(Keyboardsequencer);
        currentCat.SetSequencer(Keyboardsequencer.GetComponent<Sequencer>());
        Keyboardsequencer.transform.parent = catlist.catObjectList[catlist.catObjectList.Count - 1].transform;
        Keyboardsequencer.GetComponent<Sequencer>().loop = false;
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
