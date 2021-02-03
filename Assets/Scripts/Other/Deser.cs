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
    [SerializeField]private DeserMap map = new DeserMap();

    void Start()
    {
      this.searchDir = "." + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + this.searchDir;
      this.searchPath = this.searchDir + Path.DirectorySeparatorChar + this.filename;
      IDeserializable[] children = this.transform.GetComponentsInChildren<IDeserializable>();

      if (!File.Exists(this.searchPath)){
        try{
          Directory.CreateDirectory(this.searchDir);
          Debug.Log("Not found directory. Bad news bruh.");
          var f = File.Create(this.searchPath);
          f.Close();
        } catch (IOException _e){
          Debug.Log(_e);
        }
      }
      using(Stream sr = File.OpenRead(this.searchPath)){
        var serde = new DataContractJsonSerializer(this.map.GetType());
        this.map = (DeserMap) serde.ReadObject(sr);
      }


      foreach(var entry in this.map.GetInner()){
        for(int i=0; i < children.Length; i++) {
          if (children[i].Hash() == entry.Key) {
            children[i].SetProperty(entry.Value);
            break;
          }
        }
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
