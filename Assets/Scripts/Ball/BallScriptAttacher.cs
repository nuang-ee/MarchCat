using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScriptAttacher : MonoBehaviour
{
    
    Random r = new Random();
    
    void Awake() {
        bool found = false;
        foreach (GameObject catObject in GameObject.Find("CatList").GetComponent<CatList>().catObjectList){
            if(catObject != null) {
                if (catObject.GetComponent<MoveBall>() == null) {
                    //catObject.transform.position = new Vector2(Random.Range(-23, 23), Random.Range(-11, 11));
                    catObject.AddComponent<MoveBall>();
                    //catObject.GetComponent<MoveBall>().minPos = new Vector2(-23, -11);
                    //catObject.GetComponent<MoveBall>().maxPos = new Vector2(23, 11);
                }
                catObject.SetActive(true);
            }
            
        }
        print(found.ToString());
    }
}