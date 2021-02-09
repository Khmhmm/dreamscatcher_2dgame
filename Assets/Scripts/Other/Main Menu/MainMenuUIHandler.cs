using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class MainMenuUIHandler : Deser
{
    protected void Start(){
      var gameData = transform.GetComponentInChildren<MetaGameData>();

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

    }

    public void ButtonSettings(){

    }

    public void ButtonExit(){
      Application.Quit();
    }
}
