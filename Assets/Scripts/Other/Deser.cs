using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using UnityEngine;


public class Deser : MonoBehaviour
{
    public string searchDir = "";
    public string filename = "";
    private string searchPath = "";
    public string langPrefix = "";
    static readonly char sep = Path.DirectorySeparatorChar;
    public bool isSimplePath = false;


    protected void Start()
    {
      IDeserializable[] children = this.transform.GetComponentsInChildren<IDeserializable>();
      DeserMap map = new DeserMap();
      this.BuildPath();


      try{
        using(Stream sr = File.OpenRead(this.searchPath)){
          var serde = new DataContractJsonSerializer(map.GetType());
          map = (DeserMap) serde.ReadObject(sr);
        }
      } catch (ArgumentException e){
        Debug.LogError(e);
      } catch (IOException _e) {
        File.Create(this.searchPath);
      }

      foreach(var entry in map.GetInner()){
        for(int i=0; i < children.Length; i++) {
          if (children[i].Hash() == entry.Key) {
            children[i].SetProperty(entry.Value);
            break;
          }
        }
      }

    }


    public static T ReadGeneric<T>(string path){
      T ret = default(T);
      using(Stream sr = File.OpenRead(path)){
        var serde = new DataContractJsonSerializer(typeof(T));
        ret = (T)serde.ReadObject(sr);
      }
      return ret;
    }


    public void BuildPath(){
      try{
        var gData = GameObject.Find("GameData").GetComponent<MetaGameData>();
        this.langPrefix = gData.lang;
      } catch (NullReferenceException _e) {
        // Do nothing
      }
      if (isSimplePath){
        this.searchPath = "." + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + filename;
      }
      else{
        this.searchDir = langPrefix + sep + searchDir;
        this.searchDir = "." + sep + "Data" + sep + this.searchDir;
        this.searchPath = this.searchDir + sep + this.filename;
      }


      if (!File.Exists(this.searchPath)){
        try{
          if(!isSimplePath){
            Directory.CreateDirectory(this.langPrefix);
            Directory.CreateDirectory(this.langPrefix + sep + this.searchDir);
          }
          var f = File.Create(this.searchPath);
          f.Close();
        } catch (IOException _e){
          Debug.Log(_e);
        }
      }
    }

    public void Save(DeserMap map, string path){
      var writePath = "." + sep + "Data" + sep + path;

      try{
        using(Stream sw = File.OpenWrite(writePath)){
          var serde = new DataContractJsonSerializer(map.GetType());
          serde.WriteObject(sw, map);
        }
      } catch (ArgumentException e){
        Debug.LogError(e);
      } catch (IOException _e) {
        File.Create(this.searchPath);
      }
    }


}

public class DeserMap{
  public Dictionary<string, string> map;

  public DeserMap(){
    this.map = new Dictionary<string, string>();
  }

  public Dictionary<string, string> GetInner(){
    return this.map;
  }

  public void Add(string hash, string content){
    this.map.Add(hash, content);
  }

  public int Length(){
    return this.map.Count;
  }
}
