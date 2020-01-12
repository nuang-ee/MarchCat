using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class catSelector : MonoBehaviour
{
    int index;

    public static GameObject catObject;
    public GameObject catSprite;
    public GameObject instrumentSprite;
    

    public List<Button> instrumentButtons;
    public List<Button> catTypeButtons;
    public Cat cat;

    public UIHandler uihandler;


    public void onCatTypeClick(int index) {
        cat.characterSelected = true;

        foreach (Button b in catTypeButtons) {
            b.interactable = true;
        }
        catTypeButtons[index].interactable = false;
        cat.SetCatType(index);


        string path = "Animation/characters/cat_" + index.ToString();

        catSprite.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(path) as RuntimeAnimatorController;
    }
    

    public void onInstrumentTypeClick(int index) {
        cat.instrumentSelected = true;
        cat.instrumentIndex = index;
        //catObject.transform.Find("sprite_instrument").gameObject.SetActive(true);
        instrumentSprite.SetActive(true);
        

        foreach(Button b in instrumentButtons) {
            b.interactable = true;
        }
        instrumentButtons[index].interactable = false;
        if (instrumentSprite.GetComponent<Animator>() == null)
        {
            instrumentSprite.AddComponent<Animator>();
        }

        string path = "Animation/equipments/instrument_" + index.ToString();

        instrumentSprite.GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load(path) as RuntimeAnimatorController;

        uihandler.moving = true;
    }

    public void OnSceneCancel()
    {
        Destroy(GameObject.Find("Cat"));
    }
}
