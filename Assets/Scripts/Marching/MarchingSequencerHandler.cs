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
        CatPlayList = GameObject.Find("CatPlaylist").GetComponentsInChildren<CatList>();
        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();
        print("catplaylist : " + CatPlayList.Length);
        clock.pause = true;
        order = 0;
        Started = false;

        PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
        PauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        StopButton = GameObject.Find("StopButton").GetComponent<Button>();

        PlayButton.onClick.AddListener(delegate //If press start button
        {
            if (!Started)//Initialize sequencers and start to play
            {
                print("lets start : "+order);
                
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
                clock.pause = false;
            }
            else {//Resume
                print("resume");
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
        print("extractedCatList : " +  extractedCatList.Count);
        foreach (Cat cat in extractedCatList) {
            print("get in to the extraction part");
            extractedSequencer.Add(cat.sequencer);
        }
        print("catlist.catlist = " + extractedCatList.Count);
        return extractedSequencer;
    }


    void attachNextSequencer(Sequencer sequencer) {
        int beat_index = ((int)sequencer.GetSequencerPosition()) % 16;
        print(beat_index);
        if (beat_index == 15) // beat index = 15 : prepare next sequencers
        {
            if (order + 1 < CatPlayList.Length) {
                print("next order : " + (order));
                nextSequencer = ExtractSequencersFromCatList(CatPlayList[order]); //Prepare next sequencers
                foreach (Sequencer seq in nextSequencer)
                {
                    SequencerReadyToStart(seq);
                }
            }
            
            
        }
        else if (beat_index == 0) { // beat index = 0 : set current sequencer's active = false (except pivot)
            print("it this true?");
            order = order + 1;
            foreach (Sequencer seq in currentSequencer) {
                if (pivot_sequencer != seq)
                {
                    print("this is not pivot sequencer");
                    seq.gameObject.SetActive(false);
                }
                else {
                    seq.loop = false;
                }
            }
            currentSequencer = nextSequencer;
        }
    }


    void SequencerReadyToStart(Sequencer sequencer) {
        sequencer.gameObject.SetActive(true);
        sequencer.loop = true;
        sequencer.StartOnNextCycle();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
