using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject catObject;
    public GameObject nextPanel;
    public GameObject instrumentSprite;
    
    public Button moveToInstrumentsButton;

    public float speed = 500.0f;

    private static Vector2 target1;
    private static Vector2 target2;

    private Vector2 target;

    public bool moving;

    
    private float cameraHeight = new float();
    private float cameraWidth = new float();

    private void Awake()
    {
        Camera cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        target1 = new Vector2(-cameraWidth * 0.1f, -cameraHeight * 0.1f);
        target2 = new Vector2(cameraWidth * 0.2f , -cameraHeight * 0.1f);
        print(cameraHeight.ToString() + " || " + cameraWidth.ToString());
        catObject.transform.position = target2;
    }


    void Update() {
        if (moving) {
            //float step = speed * Time.deltaTime;
            catObject.transform.position = target;
            print(catObject.transform.position.x.ToString() + "||" + catObject.transform.position.y.ToString());

            if (catObject.GetComponent<Cat>().instrumentIndex != -1)
            {
                int i = catObject.GetComponent<Cat>().instrumentIndex;
                Vector2 temp = new Vector2(0, 0);
                switch (i)
                {
                    case 0:
                        temp = new Vector2(-cameraWidth * 0.02f, 0);
                        break;
                    case 1:
                        temp = new Vector2(-cameraWidth * 0.2f * 0.1f, 0);
                        break;
                    case 2:
                        temp = new Vector2(-cameraWidth * 0.02f, 0);
                        break;
                    case 3:
                        temp = new Vector2(-cameraWidth * 0.02f, - cameraHeight / 8);
                        break;
                    default:
                        print("error");
                        break;
                }
                Vector2 parentPosition = target;
                instrumentSprite.transform.position = temp + target;
                moving = false;
            }
        }

        if (catObject.GetComponent<Cat>().characterSelected) {
            moveToInstrumentsButton.interactable = true;
        }
        else moveToInstrumentsButton.interactable = false;
    }
    public void OnClick(int mode) 
    {
        if (mode == 1) { //character -> instrument
            previousPanel.gameObject.SetActive(false);
            nextPanel.gameObject.SetActive(true);
            moving = true;
            target = target1;
        }
        else if (mode == 2) {
            previousPanel.gameObject.SetActive(true);
            nextPanel.gameObject.SetActive(false);
            moving = true;
            target = target2;
        }
    }
}
