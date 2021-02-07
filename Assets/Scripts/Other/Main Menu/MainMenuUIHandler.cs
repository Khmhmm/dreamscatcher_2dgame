using System.Collections;
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
      var gameData = transform.GetComponentInChildren<MetaGameDataBehaviour>();

      try{
        var fileData = Deser.ReadGeneric<MetaGameData>(gameData.inner.path);
        if(fileData != null){
          gameData.inner.Mutate(fileData);
        }
      } catch(SerializationException _e) {}
      finally{
        //gameData.inner.SaveGame();
        //gameData.transform.parent = null;
        //DontDestroyOnLoad(gameData.gameObject);
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
