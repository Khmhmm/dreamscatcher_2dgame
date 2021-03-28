using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : Deser
{
    MetaGameData gameData;
    public GameObject musicObject;

    protected void Start(){
      if (SceneManager.GetActiveScene().name == "Menu"){
        gameData = transform.GetComponentInChildren<MetaGameData>();
      }

      // Needed when you go to main menu from another scene with already setted game data
      if(GameObject.FindGameObjectsWithTag("GameData").Length > 1){
        if (SceneManager.GetActiveScene().name == "Menu"){
          Destroy(gameData.gameObject);
          Destroy(musicObject);
        }
        gameData = GameObject.FindWithTag("GameData").GetComponent<MetaGameData>();
        musicObject = GameObject.FindWithTag("MenuAudio");
        return;
      }

      try{
        base.Start();
      } catch(ArgumentException _e){
        gameData.SaveGame(this);
      } catch(SerializationException _e){
        gameData.SaveGame(this);
      }
      finally{
        gameData.transform.parent = null;
        DontDestroyOnLoad(gameData.gameObject);
        DontDestroyOnLoad(musicObject);
      }

    }

    public void ButtonPlay(){
      Destroy(musicObject);
      SceneManager.LoadScene(gameData.lastLevel);
    }

    public void ButtonSettings(){
      SceneManager.LoadScene("Settings");
    }

    public void ButtonExit(){
      Application.Quit();
    }
}
