using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class CatMarchBegin : MonoBehaviour
{
    public GameObject catPlayList; //whole playlist to play
    public GameObject onebarObject; //one bar to play
    public static AudioHelm.AudioHelmClock clock;

    private float cameraHeight = new float();
    private float cameraWidth = new float();

    private static float bpm = clock.bpm;

    private static Vector2 start;
    private static Vector2 goal;


    void Awake() {
        Camera cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        start = new Vector2(cameraWidth * 0.4f, -cameraHeight * 0.1f);
        goal = new Vector2(cameraWidth * -0.4f, -cameraHeight * 0.1f);
    }

    private void CalculateSpeed() {
        
    }
}
