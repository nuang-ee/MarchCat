using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using AudioHelm;

public class PatternHandler : MonoBehaviour
{
    public int key;
    public string InstName;
    public Button ResetButton;

    private Toggle[] patternElements;
    private List<AudioHelm.Note> existingNotes = new List<AudioHelm.Note>();
    private int count;
    private Color initColor;
    private Sequencer sequencer;
    private AudioHelmClock clock;

    // Start is called before the first frame update
    void Start()
    {
        patternElements = GetComponentsInChildren<Toggle>();
        Debug.Log("NoteSet # of toggle : " + patternElements.Length);
        count = 0;

        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();

        //GameObject DrumSequencer = GameObject.Find("DrumKit01Sequencer");
        //GameObject SynthSequencer = GameObject.Find("SynthSequencer");
        //GameObject BassSequencer = GameObject.Find("BassSequencer");
        //GameObject GuitarSequencer = GameObject.Find("GuitarSequencer");


        CatList Catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Cat currentCat = Catlist.catlist[Catlist.catlist.Count - 1];

        

        print(currentCat.instrumentName);

        if (currentCat.instrumentName == "drum")
        {
            sequencer = GameObject.Find("DrumKit01Sequencer").GetComponent<Sequencer>();
        }
        else if (currentCat.instrumentName == "synthesizer")
        {
            sequencer = GameObject.Find("SynthSequencer").GetComponent<Sequencer>();
        }
        else if (currentCat.instrumentName == "bass")
        {
            sequencer = GameObject.Find("BassSequencer").GetComponent<Sequencer>();
        }
        else if (currentCat.instrumentName == "guitar")
        {
            sequencer = GameObject.Find("GuitarSequencer").GetComponent<Sequencer>();
        }
        else {
            print("this is not good");
        }

        ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();

        if (sequencer == null) print("null!");
        sequencer.Clear();
        Color normalColor = patternElements[0].colors.normalColor;
        initColor = new Color(normalColor.r, normalColor.g, normalColor.b);

        ResetButton.onClick.AddListener(delegate
        {
            ResetNote();
        });
        sequencer.beatEvent.AddListener(delegate
        {
            ToggleBackgroundChange();
        });
        //sequencer.
        //Pattern Color Selection
        //if (InstName == "Bass Drum") {
        //    for (int )
        //}
    }
    
    public List<AudioHelm.Note> GetListofNote() {
        return existingNotes;
    }

    public void SetSequencerNote(Toggle toggle) {
        int selected_idx = -1;
        for (int i = 0; i < patternElements.Length; i++) {
            if (patternElements[i] == toggle) {
                selected_idx = i;
                break;
            }
        }
        Debug.Log("selected_idx : " + selected_idx.ToString());
        if (selected_idx == -1)
        {
            Debug.Log("PatternHandler : SetSequencerNoteErr");
        }
        else {
            if (toggle.isOn == true)
            {
                AudioHelm.Note note = sequencer.AddNote(key, selected_idx, selected_idx + 1, 1.0f);
                existingNotes = sequencer.GetAllNotes();
                //Debug.Log("After AddNote : " + existingNotes.Count);
            }
            else {
                
                AudioHelm.Note existing_note = sequencer.GetNoteInRange(key, selected_idx, selected_idx + 1);
                sequencer.RemoveNote(existing_note);
                existingNotes.Remove(existing_note);
            }
        }
    }

    void ToggleBackgroundChange() {
        //ColorBlock newColorBlock;
        print(count);
        if (count == 0) {
            ColorBlock prevcolor = patternElements[15].colors;
            prevcolor.normalColor = initColor;
            patternElements[15].colors = prevcolor;
        }
        else
        {
            ColorBlock prevcolor = patternElements[count - 1].colors;
            prevcolor.normalColor = initColor;
            patternElements[count - 1].colors = prevcolor;
        }
        ColorBlock currentColor = patternElements[count].colors;
        currentColor.normalColor = new Color(initColor.r + 0.2f, initColor.g + 0.2f, initColor.b + 0.2f);
        patternElements[count].colors = currentColor;

        //count = (count + 1) % 16;
        count = ((int)sequencer.GetSequencerPosition()+3 + 15) % 16;
    }

    public void ResetNote() {
        for (int i = 0; i < patternElements.Length; i++)
        {
            if (patternElements[i].isOn) {
                Debug.Log("Reset Toggle : " + i.ToString());
                patternElements[i].isOn = false;
            }
        }
        //existingNotes = new List<AudioHelm.Note>();
    }
}
