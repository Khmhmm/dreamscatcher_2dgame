using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuHandler : Deser
{
    private GameObject metaGameData;
    // Start is called before the first frame update
    void Start()
    {
        metaGameData = GameObject.Find("GameData");
    }

    public void SetRu(){
      metaGameData.GetComponent<MetaGameData>().lang = "ru";
    }

    public void SetEn(){
      metaGameData.GetComponent<MetaGameData>().lang = "en";
    }

    public void Back(){
      metaGameData.GetComponent<MetaGameData>().SaveGame(this);
      SceneManager.LoadScene(1);
    }
}
