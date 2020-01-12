using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;

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
        targetButton.onClick.AddListener(delegate
        {
            Debug.Log("Button clicked!");
            sequencer.NoteOn(key);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
