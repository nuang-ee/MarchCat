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

    private int trackIndicator = 0;
    private AudioHelmClock clock;


    void Awake() {
        catPlayList = GameObject.Find("CatPlaylist").gameObject;

        Camera cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        start = new Vector3(cameraWidth * 0.3f, -cameraHeight * 0.05f, 0);
        goal = new Vector3(cameraWidth * -0.4f, -cameraHeight * 0.1f, 0);

        oneBarObject = catPlayList.transform.GetChild(0).gameObject;
        nextBarObject = catPlayList.transform.GetChild(1).gameObject;


        oneBarObject.transform.position = start; 
        for (int i = 0; i < oneBarObject.transform.childCount; i++) {
            oneBarObject.transform.GetChild(i).gameObject.SetActive(true);
            oneBarObject.transform.GetChild(i).localScale = new Vector3(3, 3, 3);
            oneBarObject.transform.GetChild(i).position = oneBarObject.transform.position + new Vector3(i * 0.5f, -i, 0);

        }
    }

    /*
    void Update() {
        clock = GameObject.Find("Clock").GetComponent<AudioHelmClock>();
        bpm = clock.bpm;
        timeToReach = 60f * 4f / bpm;

        t += Time.deltaTime / timeToReach;
        for (int i = 0; i < oneBarObject.transform.childCount; i++) {
            oneBarObject.transform.GetChild(i).position = Vector3.Lerp(start, goal, t);
        }

        if (oneBarObject.transform.GetChild(trackIndicator).position.x <= goal.x) {
            trackIndicator += 1;
            oneBarObject = nextBarObject;
            nextBarObject = catPlayList.transform.GetChild(trackIndicator).gameObject;
        }

    }
    */

}
