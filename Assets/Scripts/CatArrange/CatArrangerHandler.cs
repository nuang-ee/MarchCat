using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatArrangerHandler : MonoBehaviour
{
    public GameObject slotPrefab;
    //private static CatList = 
    private static GameObject catListObject;
    private List<GameObject> catObjectList;
    public CatArrangerGoback catArrangerGoback;
    public GameObject CatPlaylist;

    void Awake()
    {
        CatPlaylist = GameObject.Find("CatPlaylist");
        catListObject = GameObject.Find("CatList");
        catObjectList = catListObject.GetComponent<CatList>().catObjectList;
        //Initiate Slot Elements
        foreach (GameObject cat in catObjectList) {
            GameObject newSlot = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newSlot.transform.localScale = new Vector3(1,1,1);
            newSlot.transform.SetParent(GameObject.Find("CatSelectionContainer").transform, false);
            Transform itembutton = newSlot.transform.GetChild(0);
            //set cat sprite
            itembutton.GetChild(0).GetComponent<Image>().sprite = 
                cat.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            //set instrument sprite
            itembutton.GetChild(1).GetComponent<Image>().sprite = 
                cat.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
            //adjust position for synth
            if (cat.transform.GetComponent<Cat>().instrumentIndex == 3) {
                Transform tempPosition = itembutton.GetChild(1);
                itembutton.GetChild(1).position = new Vector3(tempPosition.position.x, tempPosition.position.y - 0.2f , 0);
            }
            itembutton.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{Onclick(cat);});
        }
        
        for (int i = 0; i < CatPlaylist.transform.childCount; i++) {
            List<GameObject> templist = CatPlaylist.transform.GetChild(i).GetComponent<CatPlaylist>().catObjectList;
            foreach (GameObject temp in templist) {
                //Loads cat clones again!
                //temp.GetComponent<CapsuleCollider2D>().enabled = true;
                temp.GetComponent<Dragger>().enabled = true;
                temp.GetComponent<CatInstanceRemove>().enabled = true;
                temp.GetComponent<Rigidbody2D>().gravityScale = 1;
                temp.GetComponent<CatInstanceRemove>().meow_scream = Resources.Load("AudioHelm/meow") as AudioSource;
                temp.SetActive(true);
            }
        }
    }

    public void Onclick(GameObject catObject) {
        GameObject catInstance = Instantiate(catObject, new Vector3(0, 2.5f, 0), Quaternion.identity);
        catInstance.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        catInstance.transform.GetChild(1).localScale = new Vector3(1, 1, 1);
        catInstance.transform.GetChild(1).position = new Vector3(0, 2.5f, 0);
        catInstance.SetActive(true);
        catInstance.AddComponent<CatInstanceRemove>();
        catInstance.GetComponent<CatInstanceRemove>().meow_scream = 
            GameObject.Find("meow_scream").GetComponent<AudioSource>();
        GameObject hinge = GameObject.Find("HingePoint").gameObject;
        if (hinge == null) Debug.Log("no hinge");
        GameObject newHingePoint = Instantiate(hinge, catInstance.transform.position, Quaternion.identity);
        newHingePoint.transform.SetParent(catInstance.transform, false);
        
        catInstance.AddComponent<Rigidbody2D>();
        catInstance.AddComponent<CapsuleCollider2D>();
        catInstance.GetComponent<CapsuleCollider2D>().size = new Vector2(0.318f, 0.392f);
        catInstance.AddComponent<Dragger>();
        catInstance.GetComponent<Dragger>().rb = catInstance.GetComponent<Rigidbody2D>();
    }
}
