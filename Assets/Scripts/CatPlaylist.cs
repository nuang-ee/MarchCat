using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlaylist : MonoBehaviour
{
    private static CatPlaylist _instance = null;

    public static CatPlaylist Instance {
        get {
            if (_instance == null)
                _instance = new CatPlaylist();
            return _instance;
        }
    }

//Modify here to set Data Variables if you want to 
    public List<List<Cat>> catPlaylist;
//=============================================
    void Awake() {
        if (CatPlaylist._instance == null){
            DontDestroyOnLoad (this);
            CatPlaylist._instance = this;

        } else {
            Destroy (this.gameObject);
        }
    }
}
