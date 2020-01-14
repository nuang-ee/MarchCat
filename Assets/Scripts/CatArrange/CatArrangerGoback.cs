using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatArrangerGoback : MonoBehaviour
{
    public Slider loadingBar;
    public GameObject loadingImage;
    public CatArrangerHandler catArrangerHandler;

    private AsyncOperation async;
    public void ClickAsync(int level) 
    {
        GameObject CatPlaylistObject = GameObject.Find("CatPlaylist").gameObject;
        int CatPlayListCount = CatPlaylistObject.transform.childCount;
        DontDestroyOnLoad(CatPlaylistObject);

        for (int i = 0; i < CatPlayListCount; i++) {
            DontDestroyOnLoad(CatPlaylistObject.transform.GetChild(i).gameObject);

            Transform SortingTileList = GameObject.Find("SortingTileList").transform;
            if (SortingTileList != null) {
                CatPlaylistObject.transform.GetChild(i).GetComponent<CatPlaylist>().catlist.AddRange(
                    SortingTileList.GetChild(i).GetChild(1).GetComponent<AreaDetector>().catList);
                CatPlaylistObject.transform.GetChild(i).GetComponent<CatPlaylist>().catObjectList.AddRange(
                    SortingTileList.GetChild(i).GetChild(1).GetComponent<AreaDetector>().catObjectList);
                foreach (GameObject clone in SortingTileList.GetChild(i).GetChild(1).GetComponent<AreaDetector>().catObjectList) {
                    //making Clones invisible and not falling!
                    clone.SetActive(false);
                    clone.transform.SetParent(CatPlaylistObject.transform.GetChild(i), false);
                    clone.GetComponent<Rigidbody2D>().gravityScale = 0;
                    //clone.GetComponent<CapsuleCollider2D>().enabled = false;
                    clone.GetComponent<Dragger>().enabled = false;
                    clone.transform.rotation = Quaternion.identity;
                    clone.GetComponent<CatInstanceRemove>().meow_scream = null;
                    clone.GetComponent<CatInstanceRemove>().enabled = false;
                }
            }
        }
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(level));
    }


    IEnumerator LoadLevelWithBar (int level)
    {
        async = SceneManager.LoadSceneAsync(level);
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }
    }
}
