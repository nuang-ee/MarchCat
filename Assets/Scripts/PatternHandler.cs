using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using AudioHelm;

public class PatternHandler : MonoBehaviour
{
    public Toggle[] patternElements;
    public AudioHelm.AudioHelmClock clock;
    public AudioHelm.Sequencer sequencer;
    private List<AudioHelm.Note> existingNotes = new List<AudioHelm.Note>();
    public int key;
    public string InstName;
    public Button ResetButton;


    private int count;
    private Color initColor;

    

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        sequencer.Clear();
        Color normalColor = patternElements[0].colors.normalColor;
        initColor = new Color(normalColor.r, normalColor.g, normalColor.b);
        
        sequencer.beatEvent.AddListener(delegate
        {
            ToggleBackgroundChange();
        });
        ResetButton.onClick.AddListener(delegate
        {
            ResetNote();
        });
        //sequencer.
        //Pattern Color Selection
        //if (InstName == "Bass Drum") {
        //    for (int )
        //}
    }

    void PatternColorSelect() {
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

        count = (count + 1) % 16;
    }

    void ResetNote() {
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
