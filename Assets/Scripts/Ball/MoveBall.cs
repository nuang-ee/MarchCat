using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public Vector2 minPos;
    public Vector2 maxPos;

    private Transform ballTf;
    private Vector3 ballPos;
    private Vector2 moveValue;
    private float moveSpeed = 0.30f;

    private float cameraHeight = new float();
    private float cameraWidth = new float();

    void Awake()
    {
        Camera cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        ballTf = transform;
        ballPos = ballTf.position;
        moveValue = Vector2.one;
        minPos = new Vector2(-cameraWidth/2f, -cameraHeight/2f);
        maxPos = new Vector2(cameraWidth/2, cameraHeight/2);
    }
    
    
    void FixedUpdate() {
        ballPos.x += moveSpeed * moveValue.x;
        ballPos.y += moveSpeed * moveValue.y;
        if (ballPos.x < minPos.x) {
            moveValue.x *= Random.Range(-1.3f, -0.5f);
            ballPos.x += minPos.x - ballPos.x;
        } else if (ballPos.x > maxPos.x) {
            moveValue.x *= Random.Range(-1.3f, -0.5f);
            ballPos.x += maxPos.x - ballPos.x;
        } 

        if (ballPos.y < minPos.y) {
            moveValue.y *= Random.Range(-1.3f, -0.5f);
            ballPos.y += minPos.y - ballPos.y;
        } else if (ballPos.y > maxPos.y) {
            moveValue.y *= Random.Range(-1.3f, -0.5f);
            ballPos.y += maxPos.y - ballPos.y;
        } 
        ballTf.position = ballPos;
    }
}
