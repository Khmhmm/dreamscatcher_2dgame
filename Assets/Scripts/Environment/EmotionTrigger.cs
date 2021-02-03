using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionTrigger : MonoBehaviour, IDeserializable
{
    public string textToSpeech;

    void OnTriggerEnter2D(Collider2D col){
      if (col.gameObject.tag == "Player") {
        col.transform.parent.GetComponentInChildren<Player>().StartCoroutine("ShowText", this.textToSpeech);
        Destroy(this.gameObject);
      }
    }

    // impl of IDeserializable
    string IDeserializable.Hash(){
      return this.gameObject.name + "ax152dsa231L";
    }

    void IDeserializable.SetProperty(string property){
      this.textToSpeech = property;
    }
}
