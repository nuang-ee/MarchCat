using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PatternHandler : MonoBehaviour
{
    public Toggle[] patternElements;
    public AudioHelm.AudioHelmClock clock;
    public AudioHelm.Sequencer sequencer;
    private List<AudioHelm.Note> existingNotes = new List<AudioHelm.Note>();
    public int key;
    public string InstName;

    // Start is called before the first frame update
    void Start()
    {
        //sequencer.Clear();

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
        if (selected_idx == -1)
        {
            Debug.Log("PatternHandler : SetSequencerNoteErr");
        }
        else {
            if (toggle.isOn == true)
            {
                AudioHelm.Note note = sequencer.AddNote(key, selected_idx, selected_idx + 1, 1.0f);
                existingNotes = sequencer.GetAllNotes();
                Debug.Log("After AddNote : " + existingNotes.Count);
            }
            else {
                
                AudioHelm.Note existing_note = sequencer.GetNoteInRange(key, selected_idx, selected_idx + 1);
                sequencer.RemoveNote(existing_note);
                existingNotes.Remove(existing_note);
            }
        }
    }
}
