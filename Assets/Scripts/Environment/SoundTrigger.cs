using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {
  private bool delay = false;
  public AudioSource sound;

  void OnTriggerEnter2D(Collider2D col) {
    if (col.transform.parent.GetComponentInChildren<Player>() != null && !delay) {
      StartCoroutine("PlaySound", 1f);
    }
  }

  IEnumerator PlaySound(float secondsDelay) {
    delay = true;
    if(sound != null) { sound.Play(); }
    yield return new WaitForSeconds(secondsDelay);
    delay = false;
  }
}
