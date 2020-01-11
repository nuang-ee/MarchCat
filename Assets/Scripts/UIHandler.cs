using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject nextPanel;
    public Animator animator;
    public List<Animator> instrumentAnimators;
    public Button moveToInstrumentsButton;
    public float speed = 500.0f;
    private static Vector2 target1 = new Vector2(-4, -1);
    private static Vector2 target2 = new Vector2(4, -1);
    private static Vector2 instrumetTarget1 = new Vector2(-4.3f, -1);
    private static Vector2 instrumetTarget2 = new Vector2(3.7f, -1);
    private Vector2 target;
    private Vector2 instrumentTarget;
    private bool moving;
    void Update() {
        if (moving) {
            float step = speed * Time.deltaTime;
            animator.transform.position = target;
            for (int i=0; i < instrumentAnimators.Count; i++) {
                Vector2 temp = new Vector2(0, 0);
                switch (i) {
                    case 0:
                        temp = new Vector2(-0.2f, -0.5f);
                        break;
                    case 1:
                        temp = new Vector2(0, 0);
                        break;
                    case 2:
                        temp = new Vector2(-0.2f, -0.2f);
                        break;
                    case 3:
                        temp = new Vector2(-0.3f, -1.3f);
                        break;
                    default:
                        print("error");
                        break;
                }
                temp = temp + instrumentTarget;
                instrumentAnimators[i].transform.position = temp;
            }
            moving = false;
        }
        if (animator.GetInteger("cat_Style") >= 0) {
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
            instrumentTarget = instrumetTarget1;
        }
        else if (mode == 2) {
            previousPanel.gameObject.SetActive(true);
            nextPanel.gameObject.SetActive(false);
            moving = true;
            target = target2;
            instrumentTarget = instrumetTarget2;
        }
    }
}
