using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionTrigger : MonoBehaviour
{
    public string textToSpeech;

    void OnTriggerEnter2D(Collider2D col){
      if (col.gameObject.tag == "Player") {
        col.transform.parent.GetComponentInChildren<Player>().StartCoroutine("ShowText", this.textToSpeech);
        Destroy(this.gameObject);
      }
    }
}
