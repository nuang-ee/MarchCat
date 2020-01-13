using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioHelm;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MarchingSequencerHandler : MonoBehaviour
{
    
    private AudioHelmClock clock;
    private List<Sequencer> currentSequencer;
    private List<Sequencer> nextSequencer;

    private CatList[] CatPlayList;
    private CatList currentCatList;

    private Button PlayButton;
    private Button PauseButton;
    private Button StopButton;

    private int order;
    private bool Started;

    private Sequencer pivot_sequencer;

    // Start is called before the first frame update
    void Awake()
    {
        //CatPlayList = GameObject.Find("CatPlaylist").catPlaylist;
        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();
        order = 0;
        Started = false;

        PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
        PauseButton = GameObject.Find("PlayButton").GetComponent<Button>();
        StopButton = GameObject.Find("PlayButton").GetComponent<Button>();

        PlayButton.onClick.AddListener(delegate //If press start button
        {
            if (!Started)//Initialize sequencers and start to play
            {
                Started = true;
                currentCatList = CatPlayList[order];
                currentSequencer = ExtractSequencersFromCatList(currentCatList);
                foreach (Sequencer sequencer in currentSequencer) {
                    SequencerReadyToStart(sequencer);
                }
                //To listen beat event, choose pivot sequencer.
                pivot_sequencer = currentSequencer[0];
                pivot_sequencer.beatEvent.AddListener(delegate
                {
                    attachNextSequencer(pivot_sequencer);
                });
            }
            else {//Resume
                clock.pause = false;
            }
        });

        PauseButton.onClick.AddListener(delegate
        {
            clock.pause = true;
        });

        StopButton.onClick.AddListener(delegate
        {
            Started = false;
            order = 0;
            pivot_sequencer = null;
        });
    }


    List<Sequencer> ExtractSequencersFromCatList(CatList catlist) {
        List<Sequencer> extractedSequencer = new List<Sequencer>();
        List<Cat> extractedCatList = catlist.catlist;
        foreach (Cat cat in extractedCatList) {
            extractedSequencer.Add(cat.sequencer);
        }
        return extractedSequencer;
    }


    void attachNextSequencer(Sequencer sequencer) {
        int beat_index = ((int)sequencer.GetSequencerPosition()) % 16;
        if (beat_index == 15) // beat index = 15 : prepare next sequencers
        {
            nextSequencer = ExtractSequencersFromCatList(CatPlayList[order + 1]); //Prepare next sequencers
            foreach (Sequencer seq in nextSequencer) {
                SequencerReadyToStart(seq);
            }
        }
        else if (beat_index == 0) { // beat index = 0 : set current sequencer's active = false (except pivot)
            order = order + 1;
            foreach (Sequencer seq in currentSequencer) {
                if (pivot_sequencer != seq) {
                    seq.gameObject.SetActive(false);
                }
            }
            currentSequencer = nextSequencer;
        }
    }


    void SequencerReadyToStart(Sequencer sequencer) {
        sequencer.loop = false;
        sequencer.StartOnNextCycle();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
