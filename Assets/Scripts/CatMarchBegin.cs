using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class CatMarchBegin : MonoBehaviour
{
    public GameObject catPlayList; //whole playlist to play
    public GameObject oneBarObject; //one bar to play, contains cat clone objects in child
    public GameObject nextBarObject;

    private float cameraHeight = new float();
    private float cameraWidth = new float();
    private float timeToReach = new float();
    private float t = new float();

    private float bpm = new float();

    private static Vector3 start;
    private static Vector3 goal;
    private List<Vector3> startList = new List<Vector3>();
    private List<Vector3> goalList = new List<Vector3>();

    private int trackIndicator = 0;
    private AudioHelmClock clock;


    void Awake() {
        catPlayList = GameObject.Find("CatPlaylist").gameObject;

        Camera cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        start = new Vector3(cameraWidth * 0.5f, 0, 0);
        goal = new Vector3(cameraWidth * -0.6f, 0, 0);

        catPlayList.transform.position = start;

        for (int i = 0; i < catPlayList.transform.childCount; i++) {
            for (int j = 0; j < catPlayList.transform.GetChild(i).childCount; j++) {
                Transform temp = catPlayList.transform.GetChild(i).GetChild(j);
                temp.gameObject.SetActive(true);
                temp.localPosition = new Vector3(j * 0.5f, -j, 0);
                temp.localScale = new Vector3(3, 3, 3);
                if (temp.GetComponent<Cat>().instrumentName == "synthesizer") {
                    temp.GetChild(1).localPosition = new Vector3(-0.05f, -0.15f, 0);    //adjust instrument position
                }
                else {
                    temp.GetChild(1).localPosition = new Vector3(-0.03f, 0, 0);    //adjust instrument position
                }
            }
            catPlayList.transform.GetChild(i).localPosition = new Vector3((1.1f / 4f) * i * cameraWidth, -cameraHeight * 0.05f, 0);
        }

        oneBarObject = catPlayList.transform.GetChild(0).gameObject;
        nextBarObject = catPlayList.transform.GetChild(1).gameObject;
    }

    
    void Update() {
        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();
        bpm = clock.bpm;
        timeToReach = 60f * 4f * 4 / bpm;

        t += Time.deltaTime / timeToReach;

        //Moves one Row Together
        catPlayList.transform.position = Vector3.Lerp(start, goal, t);
        //Debug.Log(Vector3.Lerp(start, goal, t).x.ToString());

        if (catPlayList.transform.position.x <= -cameraWidth * 0.55f) {
            catPlayList.transform.position = start;
            t = 0;
            for (int i = 0; i < catPlayList.transform.childCount; i++) {
                catPlayList.transform.GetChild(i).localPosition = new Vector3(-cameraWidth * 1.05f + (1.1f / 4f) * i * cameraWidth, -cameraHeight * 0.05f, 0);
            }
        }

        
        for (int i = 0; i < catPlayList.transform.childCount; i++) {
            //set One Row's Position back to 0, and play music.
            if (catPlayList.transform.GetChild(i).position.x <= goal.x) {
                catPlayList.transform.GetChild(i).localPosition = new Vector3((1.1f / 4f) * i * cameraWidth, -cameraHeight * 0.05f, 0);
            }
            /*
            if (catPlayList.transform.GetChild(i).position.x > Vector3.Lerp(start, goal, 0.25f).x) {
                for (int j = 0; j < catPlayList.transform.GetChild(i).childCount; j++) {
                    catPlayList.transform.GetChild(i).GetChild(j).GetChild(2).GetComponent<SampleSequencer>().loop = false;
                }
                
            }
            */
        }
    }
    

    private void SetOneRow() {
        oneBarObject.SetActive(true);
        startList.Clear();
        goalList.Clear();
        for (int i = 0; i < oneBarObject.transform.childCount; i++) {
            oneBarObject.transform.GetChild(i).gameObject.SetActive(true);
            
            oneBarObject.transform.GetChild(i).localScale = new Vector3(3, 3, 3);
            oneBarObject.transform.GetChild(i).localPosition = oneBarObject.transform.localPosition + new Vector3(i * 0.5f, -i, 0);
            startList.Add(oneBarObject.transform.localPosition + new Vector3(i * 0.5f, -i, 0));
            goalList.Add(goal + new Vector3(i * 0.5f, -i, 0));
        }
    }
    

}
