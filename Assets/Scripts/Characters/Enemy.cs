using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Enemy : Character, IDeserializable, IDestroyable{

    public Animator anim;
    //public float attackInterval = 2f;
    //public GameObject missile;
    private bool isActivated = false;
    private BoxCollider activationTrigger;
    public string text = "";

    void Start(){
        try{
          activationTrigger = this.GetComponents<BoxCollider>().ToList<BoxCollider>().Where( x => x.isTrigger ).ToList()[0];
        } catch(Exception _){}
        base.Start();
    }

    void LateUpdate(){
        if(isActivated){
            InvokeShowText(this.text);
            isActivated = false;
        }
    }

    // IEnumerator Attack(){
    //     if(!isActivated){
    //         yield break;
    //     }
    //     isActivated = false;
    //     if(missile){
    //         Instantiate(missile, transform.position - new Vector3(1f, 0f), transform.rotation);
    //     }
    //     yield return new WaitForSeconds(attackInterval);
    //     isActivated = true;
    // }

    public override void Move(Vector3 movement){
        transform.Translate(movement);
    }

    public override void SetAnimator(string fieldName, bool flag){
        anim?.SetBool(fieldName, flag);
    }

    public override float GetSpeed(){
        return 1f;
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player") {
            isActivated = true;
            Destroy(this.activationTrigger);
        }
    }

    // impl of IDeserializable
    string IDeserializable.Hash(){
      return this.gameObject.name + "41289334e89scdsL";
    }

    void IDeserializable.SetProperty(string property){
      this.text = property;
    }
}
