using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionTrigger : MonoBehaviour, IDeserializable, IDestroyAndThen
{
    public string textToSpeech;
    public GameObject[] activateOnDestroy;

    void OnTriggerEnter2D(Collider2D col){
      if (col.gameObject.tag == "Player") {
        col.transform.parent.GetComponentInChildren<Player>().StartCoroutine("ShowText", this.textToSpeech);
        this.GetComponent<IDestroyAndThen>().DestroyAndThen();
      }
    }

    // impl of IDeserializable
    string IDeserializable.Hash(){
      return this.gameObject.name + "ax152dsa231L";
    }

    void IDeserializable.SetProperty(string property){
      this.textToSpeech = property;
    }

     void IDestroyAndThen.DestroyAndThen(){
         foreach(var obj in this.activateOnDestroy) {
           obj.SetActive(true);
         }

         Destroy(this.gameObject);
     }
}
