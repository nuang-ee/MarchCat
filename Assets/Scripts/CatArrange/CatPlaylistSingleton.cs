using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlaylistSingleton : MonoBehaviour
{
    private static CatPlaylistSingleton _instance = null;

    public static CatPlaylistSingleton Instance {
        get {
            if (_instance == null)
                _instance = new CatPlaylistSingleton();
            return _instance;
        }
    }

//Modify here to set Data Variables if you want to 

//=============================================
    void Awake() {
        if (CatPlaylistSingleton._instance == null){
            DontDestroyOnLoad (this);
            CatPlaylistSingleton._instance = this;

        } else {
            Destroy (this.gameObject);
        }
    }
}
