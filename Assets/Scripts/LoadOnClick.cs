using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnClick : MonoBehaviour
{
    public GameObject loadingImage;

    public void LoadScene(int level) //index of level on build settings
    {
        loadingImage.SetActive(true);
        Destroy(this.gameObject);
        Application.LoadLevel(level);
    }
}
