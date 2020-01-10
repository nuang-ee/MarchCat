using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class Instrument : MonoBehaviour
{
    public string instrumentName;
    public bool instrumentType; //true when melodic, false when rhythmic.
    public AudioHelm.HelmSequencer sequencer;
}
