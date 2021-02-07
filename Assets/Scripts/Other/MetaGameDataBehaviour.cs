using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class MetaGameDataBehaviour : MonoBehaviour{
  [SerializeField]public MetaGameData inner;
}

[Serializable]
public class MetaGameData : IDeserializable, IDeserializableGeneric
{
  public string lang = "en";
  public int lastLevel = 1;
  public string path = "." + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "savegame.json";

  public string GetLanguage(){
    return this.lang;
  }

  public int GetLastLevel(){
    return this.lastLevel;
  }

  public string GetPath(){
    return this.path;
  }

  string IDeserializable.Hash(){
    return "skip";
  }

  void IDeserializable.SetProperty(string property){
    //this.lastLevel = int.Parse(property);
  }

  void IDeserializableGeneric.Set(object ob){
    try{
      var obj = (MetaGameData) ob;
      this.lang = obj.GetLanguage();
      this.lastLevel = obj.GetLastLevel();
    } catch(InvalidCastException _e){
      Debug.LogError(_e);
    }
  }

  public void Mutate(MetaGameData mgd){
    this.lang = mgd.GetLanguage();
    this.lastLevel = mgd.GetLastLevel();
  }

  public void SaveGame(){
    using(Stream sw = File.OpenWrite(this.path)){
      var serde = new DataContractJsonSerializer(this.GetType());
      serde.WriteObject(sw, this);
    }
  }
}
