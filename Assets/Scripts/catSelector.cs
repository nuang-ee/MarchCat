using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class catSelector : MonoBehaviour
{
    int index;
    public Animator catAnimator;
    public List<GameObject> instrumentAnimator;
    public List<Button> buttons;
    public Cat cat;
    public void onCatTypeClick(int index) {
        foreach (Button b in buttons) {
            b.interactable = true;
        }
        buttons[index].interactable = false;
        cat.SetCatType(index);
        catAnimator.SetInteger("cat_Style", index);
        print("cat selected!\n");
    }
    

    public void onInstrumentTypeClick(int index) {
        cat.SetInstrument(index);
        foreach (GameObject item in instrumentAnimator) {
            item.SetActive(false);
        }
        instrumentAnimator[index].SetActive(true);
    }
}
