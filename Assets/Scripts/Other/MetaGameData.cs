using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class MetaGameData : MonoBehaviour, IDeserializable
{
  public string lang = "en";
  public int lastLevel = 1;
  public float volume = 1f;
  private string path = "." + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "savegame.json";

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
    return "save";
  }

  void IDeserializable.SetProperty(string property){
    var inbounds = property.Split('[')[1].Split(']')[0];
    var data = inbounds.Split(',');
    this.lang = data[0];
    this.lastLevel = int.Parse(data[1]);
    this.volume = float.Parse(data[2]);
  }

  public void SaveGame(Deser deser){
    string json = "[" +
      lang.ToString() +
      "," + lastLevel.ToString() +
      "," + volume.ToString() +
    "]";

    var map = new DeserMap();
    map.Add( ((IDeserializable)(this)).Hash(), json );

    deser.Save(map, "savegame.json");
  }
}
