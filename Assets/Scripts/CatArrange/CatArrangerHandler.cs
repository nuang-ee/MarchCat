using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AudioHelm;

public class CatArrangerHandler : MonoBehaviour
{
    public GameObject slotPrefab;
    //private static CatList = 
    private static GameObject catListObject;
    private List<GameObject> catObjectList;
    public CatArrangerGoback catArrangerGoback;
    public GameObject CatPlaylist;

    private Cat pointedCat;
    private List<List<GameObject>> createdInstance;
    void Awake()
    {
        CatPlaylist = GameObject.Find("CatPlaylist");
        catListObject = GameObject.Find("CatList");
        catObjectList = catListObject.GetComponent<CatList>().catObjectList;
        createdInstance = new List<List<GameObject>>();
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
            itembutton.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{Onclick(cat, newSlot);});
            //Set remove button
            itembutton.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate 
                {
                    OnRemoveButtonClicked(cat, newSlot);
                }
            );

            //Play their sound while mouse is on the object.
            EventTrigger trigger = itembutton.GetChild(0).gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry1 = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            entry1.callback.AddListener((data) => { OnMouseEnter_fun(cat); });
            print(trigger);
            print(trigger.triggers);
            print(entry1);
            trigger.triggers.Add(entry1);

            //When the mouse gone, the sound will be stop.
            EventTrigger.Entry entry2 = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            entry2.callback.AddListener((data) => { OnMouseExit_fun(cat); });
            print(trigger);
            print(trigger.triggers);
            print(entry2);
            trigger.triggers.Add(entry2);
        }
        
        for (int i = 0; i < CatPlaylist.transform.childCount; i++) {
            List<GameObject> templist = CatPlaylist.transform.GetChild(i).GetComponent<CatPlaylist>().catObjectList;
            foreach (GameObject temp in templist) {
                //Loads cat clones again!
                //temp.GetComponent<CapsuleCollider2D>().enabled = true;

                //Load instances to createdInstance list
                bool alreadyin = false;
                for (int j = 0; j < createdInstance.Count; j++)
                {
                    print(j + "th createdInstance List size" + createdInstance[j].Count);
                    if (createdInstance[j][0].name == temp.name)
                    { // Compared with name
                        createdInstance[j].Add(temp);
                        alreadyin = true;
                        break;
                    }
                }
                if (!alreadyin)
                {
                    print("insert new list!");
                    List<GameObject> newObjectList = new List<GameObject>();
                    newObjectList.Add(temp);
                    createdInstance.Add(newObjectList);
                }

                temp.GetComponent<Dragger>().enabled = true;
                temp.GetComponent<CatInstanceRemove>().enabled = true;
                temp.GetComponent<Rigidbody2D>().gravityScale = 1;
                temp.GetComponent<CatInstanceRemove>().meow_scream = Resources.Load("AudioHelm/meow") as AudioSource;
                temp.GetComponentInChildren<Sequencer>().enabled = false;
                //temp.GetComponent<Sequencer>().loop = false;
                temp.SetActive(true);
            }
        }
    }

    public void Onclick(GameObject catObject, GameObject newSlot) {

        catObject.transform.GetChild(0).gameObject.SetActive(true);
        catObject.transform.GetChild(1).gameObject.SetActive(true);
        GameObject catInstance = Instantiate(catObject, new Vector3(0, 2.5f, 0), Quaternion.identity);
        catObject.transform.GetChild(0).gameObject.SetActive(false);
        catObject.transform.GetChild(1).gameObject.SetActive(false);

        bool alreadyin = false;
        //catInstance.transform.SetParent(newSlot.transform.GetChild(1), true);

        for (int i = 0; i < createdInstance.Count; i++) {
            print(i + "th createdInstance List size" + createdInstance[i].Count);
            if (createdInstance[i][0].name == catInstance.name) { // Compared with name
                createdInstance[i].Add(catInstance);
                alreadyin = true;
                break;
            }
        }
        
        if (!alreadyin) {
            print("insert new list!");
            List<GameObject> newObjectList = new List<GameObject>();
            newObjectList.Add(catInstance);
            createdInstance.Add(newObjectList);
        }

        catInstance.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        catInstance.transform.GetChild(1).localScale = new Vector3(1, 1, 1);
        catInstance.transform.GetChild(1).position = new Vector3(0, 2.5f, 0);
        //catInstance.transform.GetChild(1).localPosition = new Vector3(0, 2.5f, 0);
        catInstance.SetActive(true);
        catInstance.AddComponent<CatInstanceRemove>();
        catInstance.GetComponent<CatInstanceRemove>().meow_scream = 
            GameObject.Find("meow_scream").GetComponent<AudioSource>();
        catInstance.GetComponent<CatInstanceRemove>().Handler =
            this;
        GameObject hinge = GameObject.Find("HingePoint").gameObject;
        if (hinge == null) Debug.Log("no hinge");
        GameObject newHingePoint = Instantiate(hinge, catInstance.transform.position, Quaternion.identity);
        newHingePoint.transform.SetParent(catInstance.transform, false);
        
        catInstance.AddComponent<Rigidbody2D>();
        catInstance.AddComponent<CapsuleCollider2D>();
        catInstance.GetComponent<CapsuleCollider2D>().size = new Vector2(0.318f, 0.392f);
        catInstance.AddComponent<Dragger>();
        catInstance.GetComponent<Dragger>().rb = catInstance.GetComponent<Rigidbody2D>();
        catInstance.GetComponentInChildren<Sequencer>().enabled = false;

    }

    void OnMouseEnter_fun(GameObject catObject) {
        catObject.SetActive(true);
        pointedCat = catObject.GetComponent<Cat>();
        catObject.transform.GetChild(0).gameObject.SetActive(false);
        catObject.transform.GetChild(1).gameObject.SetActive(false);
        //clock = new AudioHelmClock();
        //clock.Reset();
        //clock.pause = false;
        Sequencer sequencer = pointedCat.sequencer;
        print(sequencer);
        sequencer.loop = true;
        sequencer.enabled = true;
        sequencer.StartOnNextCycle();
        
    }

    void OnMouseExit_fun(GameObject catObject)
    {
        pointedCat.sequencer.loop = false;
        pointedCat.sequencer.enabled = false;
        print("exited");
        //clock.pause = true;
        //clock.Reset();
        //clock = null;
        pointedCat = null;
        catObject.transform.GetChild(0).gameObject.SetActive(true);
        catObject.transform.GetChild(1).gameObject.SetActive(true);
        catObject.SetActive(false);
    }

    void OnRemoveButtonClicked(GameObject catObject, GameObject newSlot)
    {
        List<GameObject> removeList = null;
        //Remove instanciated objects.
        for (int i = 0; i < createdInstance.Count; i++) {
            print(catObject.name);
            print(createdInstance[i][0].name);
            print(createdInstance[i][0].name.Substring(0, createdInstance[i][0].name.Length - 7));
            if (createdInstance[i][0].name.Substring(0, createdInstance[i][0].name.Length - 7) == catObject.name) { // Compare name except (Clone) suffix
                print("good");
                removeList = createdInstance[i];
                createdInstance.Remove(removeList);
                break;
            }
        }
        if (removeList == null) {
            print("remove button will not work.");
        }
        else
        {
            //print("Destroy them all");
            print(removeList.Count);
            for (int i = 0; i < removeList.Count; i++)
            {
                print("Destroy them all");
                Destroy(removeList[i]);
            }
        }
        //Transform instances =  newSlot.transform.GetChild(1);
        //Cat[] catInstances = instances.GetComponentsInChildren<Cat>();
        //for (int i = 0; i < catInstances.Length; i++) {
        //    Destroy(catInstances[i].gameObject);
        //}

        //Remove slot object.
        //catListObject = GameObject.Find("CatList");
        //catObjectList = catListObject.GetComponent<CatList>().catObjectList;
        catObjectList.Remove(catObject);
        Destroy(newSlot);
        Destroy(catObject);
    }

    public void catListRemovebyexit(GameObject gameobject) {
        bool flag = false;
        bool another_flag = false;
        List<GameObject> removeCatList = null;
        for (int i = 0; i < createdInstance.Count; i++)
        {
            print(gameobject.name);
            print(createdInstance[i][0].name);
            print(createdInstance[i][0].name.Substring(0, createdInstance[i][0].name.Length - 7));
            if (createdInstance[i][0].name == gameobject.name)
            { // Compare name except (Clone) suffix
                print("good");
                flag = true;
                removeCatList = createdInstance[i];
                //createdInstance[i].Remove(gameobject);
                break;
            }
        }
        if (!flag)
        {
            print("remove button is not work.");
        }
        else {
            for (int i = 0; i < removeCatList.Count; i++)
            {
                print(gameobject.GetInstanceID());
                print(removeCatList[i].GetInstanceID());
                //print(createdInstance[i][0].name.Substring(0, createdInstance[i][0].name.Length - 7));
                if (gameobject.GetInstanceID() == removeCatList[i].GetInstanceID())
                { // Compare name except (Clone) suffix
                    print("good");
                    removeCatList.Remove(removeCatList[i]);
                    if (removeCatList.Count == 0) {
                        createdInstance.Remove(removeCatList);
                    }
                    another_flag = true;
                    //createdInstance[i].Remove(gameobject);
                    break;
                }
            }
        }
        if (!another_flag) {
            print("Something wrong...");
        }
    }
}
