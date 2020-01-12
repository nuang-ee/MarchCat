using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCatInformation : MonoBehaviour
{
    public GameObject loadingImage;
    

    private GameObject catObject;
    private Cat cat;
    private CatList catList;

    void Awake() {
        catObject = GameObject.Find("Cat");
        cat = catObject.GetComponent<Cat>();
        catList = GameObject.Find("CatList").GetComponent<CatList>();
    }

    public void LoadScene(int level) //index of level on build settings
    {
        loadingImage.SetActive(true);
        //Destroy(this.gameObject);
        catObject.name = "Cat" + catList.catlist.Count.ToString();
        catObject.SetActive(false);
        SceneManager.LoadScene(level);
        catObject.transform.parent = GameObject.Find("CatList").transform;
        catList.catlist.Add(cat);
        catList.catObjectList.Add(catObject);
    }
}
