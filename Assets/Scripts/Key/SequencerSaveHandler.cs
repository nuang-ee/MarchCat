using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AudioHelm;

public class SequencerSaveHandler : MonoBehaviour
{

    public Button DrumSaveButton;
    public Button KeyboardSaveButton;
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
    }

    void SaveDrumSequenceDataToCat() {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Debug.Log("SaveSequenceDataToCat : " + catlist.catlist.Count);
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];

        Drumsequencer.name = Drumsequencer.name + (catlist.catlist.Count - 1);
        Drumsequencer.SetActive(false);
        DontDestroyOnLoad(Drumsequencer);
        currentCat.SetSequencer(Drumsequencer.GetComponent<Sequencer>());
        Drumsequencer.transform.parent = catlist.catObjectList[catlist.catObjectList.Count - 1].transform;
        SceneManager.LoadScene(0);
    }

    void SaveKeyboardSequenceDataToCat()
    {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Debug.Log("SaveSequenceDataToCat : " + catlist.catlist.Count);
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];

        Keyboardsequencer.name = Keyboardsequencer.name + (catlist.catlist.Count - 1);
        Keyboardsequencer.SetActive(false);
        DontDestroyOnLoad(Keyboardsequencer);
        currentCat.SetSequencer(Keyboardsequencer.GetComponent<Sequencer>());
        Keyboardsequencer.transform.parent = catlist.catObjectList[catlist.catObjectList.Count - 1].transform;
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
