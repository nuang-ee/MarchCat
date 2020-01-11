using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class Cat : MonoBehaviour
{
    public static readonly string[] catTypeArray = { "plain", "strong", "sensitive", "supreme" };
    public static readonly string[] instrumentNames = { "bass", "drum", "guitar", "synthesizer"};
    
    //Note Information of cat to play
    public List<AudioHelm.Note> notes; 
    public int sequenceLength;
    
    public string catType; //Type of Cat, references catTypeArray
    public string instrumentName; //Name of Instrument, references instrumentNames array
    public bool instrumentType; //true when melodic, false when drum.

    public AudioHelm.Sequencer sequencer;

    public void SetInstrument(int index) {
        this.instrumentName = instrumentNames[index];
        this.instrumentType = (this.instrumentName == "drum") ? false : true;
    }

    public void SetCatType(int index) {
        this.catType = catTypeArray[index];
    }

    public void SetSequencer(AudioHelm.Sequencer seq) {
        this.sequencer = seq;
    }

    void Awake() {
        DontDestroyOnLoad (this);
    }

}
