using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy _instance ;

    void Awake() {
        if(!_instance) _instance = this;
        else Destroy(this.gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
}
