using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPicture : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D col) {
    if (col.tag == "Player") {
      this.GetComponent<MeshRenderer>().enabled = false;
    }
  }

  void OnTriggerExit2D(Collider2D col) {
    if (col.tag == "Player") {
      this.GetComponent<MeshRenderer>().enabled = true;
    }
  }
}
