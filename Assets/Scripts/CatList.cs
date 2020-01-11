using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class CatList : MonoBehaviour
{
    private static CatList _instance = null;

    public static CatList Instance {
        get {
            if (_instance == null)
                _instance = new CatList();
            return _instance;
        }
    }

//Modify here to set Data Variables if you want to 
    public List<Cat> catlist;
//=============================================
    void Awake() {
        if (CatList._instance == null){
            DontDestroyOnLoad (this);
            CatList._instance = this;

        } else {
            Destroy (this.gameObject);
        }
    }
}
