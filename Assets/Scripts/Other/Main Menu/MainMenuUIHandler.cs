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

    protected void Start(){
      gameData = transform.GetComponentInChildren<MetaGameData>();

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
      }
    }

    public void ButtonPlay(){
      SceneManager.LoadScene(gameData.lastLevel);
    }

    public void ButtonSettings(){

    }

    public void ButtonExit(){
      Application.Quit();
    }
}
