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
    private HelmPatch patch;
    private float velocity;
    private int beat;
    private Text positionText;
    private Cat currentCat;

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
        currentCat = Catlist.catlist[Catlist.catlist.Count - 1];
        positionText = GameObject.Find("Position").GetComponent<Text>();
        

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
            print("Get into the bass");
            sequencer = GameObject.Find("BassSequencer").GetComponent<Sequencer>();
            patch = GameObject.Find("basspatch").GetComponent<HelmPatch>();
            print(patch);

            print(GameObject.Find("BassSequencer").GetComponent<HelmController>());
            GameObject.Find("BassSequencer").GetComponent<HelmController>().LoadPatch(patch);
            key = key - 12;
        }
        else if (currentCat.instrumentName == "guitar")
        {
            sequencer = GameObject.Find("GuitarSequencer").GetComponent<Sequencer>();
        }
        else {
            print("this is not good");
        }

        switch (currentCat.catType) {
            case "plain":
                velocity = 0.7f;
                beat = 16;
                break;
            case "strong":
                velocity = 1.0f;
                beat = 16;
                break;
            case "sensitive":
                velocity = 0.4f;
                beat = 16;
                break;
            case "supreme":
                velocity = 0.7f;
                beat = 32;
                break;
            default:
                break;
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
                AudioHelm.Note note = sequencer.AddNote(key, selected_idx, selected_idx + 1, velocity);
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
        //count = ((int)sequencer.GetSequencerPosition()+3 + 15) % 16;
        count = ((int)sequencer.GetSequencerPosition() + 3) % beat;
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

    public void SetPositionText(Toggle toggle) {
        int selected_idx = -1;
        for (int i = 0; i < patternElements.Length; i++)
        {
            if (patternElements[i] == toggle)
            {
                selected_idx = i;
                break;
            }
        }
        Debug.Log("selected_idx : " + selected_idx.ToString());
        if (selected_idx == -1)
        {
            Debug.Log("PatternHandler : SetSequencerNoteErr");
        }
        else
        {
            int name = key % 12;
            int height = (key / 12) - 2;

            switch (name) {
                case 0:
                    if (currentCat.instrumentName == "drum")
                    {
                        positionText.text = "Bass Drum " + selected_idx;
                    }
                    else {
                        positionText.text = "C" + height + " " + selected_idx;
                    }
                    
                    break;
                case 1:
                    if (currentCat.instrumentName == "drum")
                    {
                        positionText.text = "Snare " + selected_idx;
                    }
                    else
                    {
                        positionText.text = "C#" + height + " " + selected_idx;
                    }
                    break;
                case 2:
                    positionText.text = "D" + height + " " + selected_idx;
                    break;
                case 3:
                    positionText.text = "D#" + height + " " + selected_idx;
                    break;
                case 4:
                    positionText.text = "E" + height + " " + selected_idx;
                    break;
                case 5:
                    positionText.text = "F" + height + " " + selected_idx;
                    break;
                case 6:
                    positionText.text = "F#" + height + " " + selected_idx;
                    break;
                case 7:
                    positionText.text = "G" + height + " " + selected_idx;
                    break;
                case 8:
                    if (currentCat.instrumentName == "drum")
                    {
                        positionText.text = "Hihat " + selected_idx;
                    }
                    else
                    {
                        positionText.text = "G#" + height + " " + selected_idx;
                    }
                    break;
                case 9:
                    if (currentCat.instrumentName == "drum")
                    {
                        positionText.text = "Crash " + selected_idx;
                    }
                    else
                    {
                        positionText.text = "A" + height + " " + selected_idx;
                    }
                    break;
                case 10:
                    positionText.text = "A#" + height + " " + selected_idx;
                    break;
                case 11:
                    positionText.text = "B" + height + " " + selected_idx;
                    break;
            }

        }
    }

    public void cleanPositionText() {
        positionText.text = "Position";
    }
}
