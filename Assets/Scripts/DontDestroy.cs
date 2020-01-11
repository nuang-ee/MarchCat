using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy _instance ;

    public static DontDestroy Instance {
        get {
            if (_instance == null)
                _instance = new DontDestroy();
            return _instance;
        }
    }

    void Awake(){
        //SINGLETON PATTERN
        if (DontDestroy._instance == null){
            DontDestroyOnLoad (this);
            DontDestroy._instance = this;

        } else {
            Destroy (this.gameObject);
        }
    }
}
