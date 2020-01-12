using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveDestroyer : MonoBehaviour
{
    public void Onclick()
    {
        Destroy(GameObject.Find("Cat"));
    }
    
}
