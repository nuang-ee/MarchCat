using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AudioHelm;

public class SequencerSaveHandler : MonoBehaviour
{

    public Button SaveButton;
    public Sequencer sequencer;
    // Start is called before the first frame update
    void Start()
    {
        SaveButton.onClick.AddListener(delegate
        {
            SaveSequenceDataToCat();
        });
    }

    void SaveSequenceDataToCat() {
        CatList catlist = GameObject.Find("CatList").GetComponent<CatList>();
        Debug.Log("SaveSequenceDataToCat : " + catlist.catlist.Count);
        Cat currentCat = catlist.catlist[catlist.catlist.Count - 1];
        DontDestroyOnLoad(sequencer);
        currentCat.SetSequencer(sequencer);
        sequencer.transform.parent = catlist.catObjectList[catlist.catObjectList.Count - 1].transform;
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
