using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class catSelector : MonoBehaviour
{
    int index;
    public Animator animator;
    public Button button1, button2, button3, button4;
    public Cat cat;
    public void onCatTypeClick(int index) {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
        cat.SetCatType(index);
        animator.SetInteger("cat_Style", index);
        GetComponent<Button>().interactable = false;
        
        print("cat selected!\n");
    }
    

    public void onInstrumentTypeClick(int index) {
        
        cat.SetInstrument(index);
    }
}
