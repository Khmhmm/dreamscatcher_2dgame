using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter{

    public Animator anim;
    public float attackInterval = 2f;
    public GameObject missile;
    private bool isActivated = false;
    private BoxCollider activationTrigger;

    void Start(){
        activationTrigger = this.GetComponents<BoxCollider>().ToList<BoxCollider>().Where( x => x.isTrigger ).ToList()[0];
    }

    void LateUpdate(){
        if(isActivated){
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack(){
        if(!isActivated){
            yield break;
        }
        isActivated = false;
        if(missile){
            Instantiate(missile, transform.position - new Vector3(1f, 0f), transform.rotation);
        }
        yield return new WaitForSeconds(attackInterval);
        isActivated = true;
    }

    public void Move(Vector3 movement){
        transform.Translate(movement);
    }

    public void SetAnimator(string fieldName, bool flag){
        anim?.SetBool(fieldName, flag);
    }

    public float GetSpeed(){
        return 1f;
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player") {
            isActivated = true;
            Destroy(this.activationTrigger);
        }
    }
}
