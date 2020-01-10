using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject nextPanel;
    public Animator animator;
    public Button moveToInstrumentsButton;
    public float speed = 500.0f;
    private static Vector2 target1 = new Vector2(-4, -1);
    private static Vector2 target2 = new Vector2(4, -1);
    private Vector2 target;
    private bool moving;
    void Update() {
        if (moving) {
            float step = speed * Time.deltaTime;
            animator.transform.position = target;
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
        }
        else if (mode == 2) {
            previousPanel.gameObject.SetActive(true);
            nextPanel.gameObject.SetActive(false);
            moving = true;
            target = target2;
        }
    }
}
