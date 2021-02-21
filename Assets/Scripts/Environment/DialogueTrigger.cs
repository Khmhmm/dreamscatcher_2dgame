using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IDeserializable, IDestroyAndThen
{
  const int UNACTIVATEDTRIGGER = -15;
  [SerializeField]public Dialogue dialogue;
  private bool done = false;
  private int idx = UNACTIVATEDTRIGGER;
  private bool clickDelay = false;
  private Player player;
  public GameObject spawnOnDestroy;
  public bool takePhone = false;


    void OnTriggerEnter2D(Collider2D col){
      var _player = col.transform.parent.GetComponentInChildren<Player>();
      if(_player != null){
        this.player = _player;
        player.LockDialogue();
        if(takePhone){
          player.GetComponent<Character>().SetAnimator("phone", true);
        }
        // we will increment this one by mouse button
        idx = -1;
        Debug.Log("Started dialogue");
      }
    }

    void LateUpdate(){
      if(idx == UNACTIVATEDTRIGGER){
        return;
      }

      if(Input.GetMouseButton(0) && !clickDelay){
        clickDelay = true;
        if (idx + 1 < dialogue.replicsList.Count) {
          idx += 1;
          var iter = dialogue.replicsList[idx];
          float timeToShow = iter.speaker.InvokeShowText(iter.text);
          var unclickAsync = Unclick(timeToShow, iter.speaker);
          StartCoroutine(unclickAsync);
        }
        else{
          player.ReleaseDialogue();
          this.GetComponent<IDestroyAndThen>().DestroyAndThen();
        }
      }
    }


  IEnumerator Unclick(float delay, Character checkQuickUnclick){
    for(int i=0; i<4;i++){
      yield return new WaitForSeconds(delay/4f);
      if(!checkQuickUnclick.IsTextGUI()){
        break;
      }
    }
    clickDelay = false;
  }

  [Serializable]
  public struct Dialogue{
    [SerializeField]public List<Replics> replicsList;

    [Serializable]
    public struct Replics{
      public Character speaker;
      public string text;

      public Replics(GameObject speaker, string text){
        this.speaker = speaker.GetComponent<Character>();
        this.text = text;
      }
    }
  }


  // impl of IDeserializable
  string IDeserializable.Hash(){
    return this.gameObject.name + "fasdjfjs1Lw12";
  }

  void IDeserializable.SetProperty(string property){
    string[] collection = property.Split(';');
    foreach(var entry in collection){
      string[] speakerAndText = entry.Split(':');
      Dialogue.Replics replics = new Dialogue.Replics(
        GameObject.Find(speakerAndText[0]),
        speakerAndText[1]
      );
      this.dialogue.replicsList.Add(replics);
    }
  }

  void IDestroyAndThen.DestroyAndThen(){
    player.GetComponent<Character>().SetAnimator("phone", false);
    if(this.spawnOnDestroy != null){
      Instantiate(this.spawnOnDestroy, this.transform.parent);
    }
    Destroy(this.gameObject);
  }
}
