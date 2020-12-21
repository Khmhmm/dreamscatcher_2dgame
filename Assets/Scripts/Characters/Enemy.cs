using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter{

    public Animator anim;

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
