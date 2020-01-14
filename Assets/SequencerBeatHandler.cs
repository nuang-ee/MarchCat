using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class SequencerBeatHandler : MonoBehaviour
{
    private CatList catlist;
    private Cat currentCat;
    private Sequencer sequencer;
    // Start is called before the first frame update
    void Awake()
    {
        catlist = GameObject.Find("CatList").GetComponent<CatList>();
        currentCat = catlist.catlist[catlist.catlist.Count - 1];
        //if (currentCat.catType) {

        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
