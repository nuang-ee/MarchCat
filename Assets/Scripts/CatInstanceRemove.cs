using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInstanceRemove : MonoBehaviour
{   
    public AudioSource meow_scream;
    private Vector2 minPos;
    private Vector2 maxPos;
    private Transform catTf;
    private Vector3 catPos;
    private float cameraHeight = new float();
    private float cameraWidth = new float();

    void Awake() {
        Camera cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        minPos = new Vector2(-cameraWidth/2f, -cameraHeight/2f);
        maxPos = new Vector2(cameraWidth/2f, cameraHeight/2f);
    }

    void Update() {
        catTf = transform;
        catPos = catTf.position;
        if (catPos.x < minPos.x || catPos.x > maxPos.x || catPos.y < minPos.y || catPos.y > maxPos.y)
        {
            Debug.Log("mewo");
            meow_scream.Play();
            Destroy(gameObject);
        }
    }
    
}
