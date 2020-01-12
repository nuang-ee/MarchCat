using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    private Toggle toggle;
    private PatternHandler patternHandler;

    private void Start()
    {
        toggle = GetComponentInChildren<Toggle>();
        patternHandler = GetComponentInParent<PatternHandler>();
        toggle.onValueChanged.AddListener(delegate
        {
            patternHandler.SetSequencerNote(toggle);
        });
    }

    private void Update()
    {
        
    }

}
