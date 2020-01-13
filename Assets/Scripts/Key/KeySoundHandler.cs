using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;
using UnityEngine.EventSystems;

public class KeySoundHandler : MonoBehaviour
{
    public Button[] White;
    public Button[] Black;
    public int baseKey;
    public Sequencer sequencer;
    // Start is called before the first frame update
    void Start()
    {
        int[] whiteidx = { 0, 2, 4, 5, 7, 9, 11 };
        int[] blackidx = { 1, 3, 6, 8, 10 };

        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];
        if (currentCat.instrumentName == "drum")
        {
            //sequencer = GameObject.Find("DrumKit01Sequencer").GetComponent<Sequencer>();
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
        else
        {
            print("this is not good");
        }

        Button[] WhiteKeys = GameObject.Find("WhiteKey").GetComponentsInChildren<Button>();
        Button[] BlackKeys = GameObject.Find("BlackKey").GetComponentsInChildren<Button>();
        Debug.Log(WhiteKeys.Length);
        Debug.Log(BlackKeys.Length);
        for (int i = 0; i < WhiteKeys.Length; i++) {
            Debug.Log(WhiteKeys[i].name);
            SetKey(WhiteKeys[i], baseKey + whiteidx[i]);
        }
        for (int i = 0; i < BlackKeys.Length; i++)
        {
            Debug.Log(BlackKeys[i].name);
            SetKey(BlackKeys[i], baseKey + blackidx[i]);
        }
    }

    void SetKey(Button targetButton, int key) {
        //targetButton.OnPointerDown(PointerEventData eventData).AddListener(delegate
        //{
        //    Debug.Log("Button clicked!");
        //    sequencer.NoteOn(key);
        //});
        EventTrigger trigger = targetButton.gameObject.AddComponent<EventTrigger>();

        var pointerDown = new EventTrigger.Entry();
        var pointerUp = new EventTrigger.Entry();

        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => sequencer.NoteOn(key));

        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((e) => sequencer.NoteOff(key));

        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
