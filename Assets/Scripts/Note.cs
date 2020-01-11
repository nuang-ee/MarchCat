using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public Toggle toggle;
    public PatternHandler patternHandler;

    private void Start()
    {
        toggle.onValueChanged.AddListener(delegate
        {
            patternHandler.SetSequencerNote(toggle);
        });
    }

    private void Update()
    {
        
    }

}
