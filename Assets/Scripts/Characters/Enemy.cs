using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter{

    public Animator anim;
    public float attackInterval = 2f;
    public GameObject missile;

    void Start(){
        InvokeRepeating("Attack", attackInterval, attackInterval);
    }

    void Attack(){
        if(missile){
            Instantiate(missile, transform.position - new Vector3(1f, 0f), transform.rotation);
        }
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
}
