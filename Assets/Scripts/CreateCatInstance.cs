using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCatInstance : MonoBehaviour
{
    public GameObject catObject;
    public void Onclick() {
        GameObject catInstance = Instantiate(catObject, new Vector3(0, 2.5f, 0), Quaternion.identity);
        catInstance.AddComponent<CatInstanceRemove>();
        catInstance.GetComponent<CatInstanceRemove>().meow_scream = 
            GameObject.Find("meow_scream").GetComponent<AudioSource>();
        GameObject newHingePoint = Instantiate(GameObject.Find("HingePoint").gameObject, catInstance.transform.position, Quaternion.identity);
        catInstance.AddComponent<Rigidbody2D>();
        catInstance.AddComponent<CapsuleCollider2D>();
        catInstance.GetComponent<CapsuleCollider2D>().size = new Vector2(0.318f, 0.392f);
        catInstance.AddComponent<Dragger>();
        catInstance.GetComponent<Dragger>().rb = catInstance.GetComponent<Rigidbody2D>();
    }
}
