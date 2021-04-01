using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifts : MonoBehaviour{
    private List<LiftNode> nodes;
    private bool clickDelay = false;

    void Start(){
      nodes = new List<LiftNode>();
      foreach(Transform child in this.transform){
        var gobj = child.gameObject;
        gobj.AddComponent<LiftNode>();
        nodes.Add(gobj.GetComponent<LiftNode>());
      }
    }

    void LateUpdate(){
      for(int i=0;i<nodes.Count;i++) {
          var node = nodes[i];
          if (node.IsHere()) {
            if (clickDelay) { return; }

            if (Input.GetKeyDown(KeyCode.W)) {
              var nextNode = nodes[i+1];
              if (nextNode!=null) {
                  node.GetPlayerPtr().transform.position = new Vector3( nextNode.transform.position.x, nextNode.transform.position.y, node.GetPlayerPtr().transform.position.z );
              }
              clickDelay = true;
              StartCoroutine("LockClick");
            } else if (Input.GetKeyDown(KeyCode.S)) {
              var nextNode = nodes[i-1];
              if (nextNode!=null) {
                  node.GetPlayerPtr().transform.position = new Vector3( nextNode.transform.position.x, nextNode.transform.position.y, node.GetPlayerPtr().transform.position.z );
              }
              clickDelay = true;
              StartCoroutine("LockClick");
            }
          }
      }
    }

    IEnumerator LockClick() {
      yield return new WaitForSeconds(0.25f);
      clickDelay = false;
    }
}

public class LiftNode: MonoBehaviour {
  private bool isHere = false;
  private GameObject playerPtr;

  void OnTriggerEnter2D(Collider2D col) {
    if (col.tag == "Player") {
        isHere = true;
        playerPtr = col.transform.parent.gameObject;
    }
  }

  void OnTriggerExit2D(Collider2D col) {
    if (col.tag == "Player") {
        isHere = false;
        playerPtr = null;
    }
  }

  public bool IsHere() {
    return this.isHere;
  }

  public GameObject GetPlayerPtr() {
    return this.playerPtr;
  }
}
