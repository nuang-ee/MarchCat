using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatArrangerGoback : MonoBehaviour
{
    public Slider loadingBar;
    public GameObject loadingImage;
    public List<GameObject> CatCloneList;
    public List<List<Cat>> CatPlaylist;

    private AsyncOperation async;
    public void ClickAsync(int level) 
    {
        GameObject CatPlaylistObject = GameObject.Find("CatPlaylist").gameObject;
        CatPlaylist = CatPlaylistObject.GetComponent<CatPlaylist>().catPlaylist;
        DontDestroyOnLoad(CatPlaylistObject);

        foreach (GameObject clone in CatCloneList) {
            clone.SetActive(false);
        }
        int CatPlaylistCount = GameObject.Find("SortingTileList").transform.childCount;
        print(CatPlaylistCount.ToString());
        for (int i = 0; i < CatPlaylistCount - 1; i++) {
            Transform SortingTileList = GameObject.Find("SortingTileList").transform;
            if (SortingTileList != null) {
                CatPlaylist.Add(
                    SortingTileList.GetChild(i)
                .GetChild(1).GetComponent<AreaDetector>().catList
                );
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
