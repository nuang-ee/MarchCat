using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScriptAttacher : MonoBehaviour
{
    
    Random r = new Random();
    
    void Awake() {
        bool found = false;
        for (int i = 0; i < 10; i++){
            GameObject catObject = GameObject.Find("Cat" + i.ToString()); 
            print("Cat"+i.ToString());
            if(catObject != null) {
                found = true;
                catObject.transform.position = new Vector2(Random.Range(-85, 85), Random.Range(-47, 47));
                catObject.AddComponent<MoveBall>();
                catObject.SetActive(true);
            }
            
        }
        print(found.ToString());
    }
}