using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter{

    public Animator anim;
    //public float attackInterval = 2f;
    //public GameObject missile;
    private bool isActivated = false;
    private BoxCollider activationTrigger;
    public string text = "";
    [SerializeField]private string printedText = "";
    private int lastIdx = 0;
    public int subInterval = 2;
    public Camera cam;
    //TODO: make relying to screen space size
    public float guiW = 250f;
    public float guiH = 250f;
    private bool textDelay = false;
    [SerializeField]private GUISkin skin;

    void Start(){
        activationTrigger = this.GetComponents<BoxCollider>().ToList<BoxCollider>().Where( x => x.isTrigger ).ToList()[0];
        if (cam == null){
          cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    void LateUpdate(){
        if(isActivated){
            // StartCoroutine("Attack");
            StartCoroutine("PrintText");
        }
    }

    void IncrementText(){
      if( lastIdx + subInterval >= text.Length ){
        printedText = text;
      }
      else{
        printedText = text.Substring(0, lastIdx + subInterval);
        lastIdx += subInterval;
      }
    }

    void OnGUI(){
      GUI.skin = skin;
      if (isActivated){
        Vector3 screenPosition = cam.WorldToScreenPoint(this.transform.position);
        GUI.Box(new Rect(screenPosition.x - guiW/2f, Screen.height - (screenPosition.y*1.5f + guiH), guiW, guiH), this.printedText);
      }
    }

    IEnumerator PrintText(){
      if(textDelay || !isActivated){
        yield break;
      }

      if(printedText.Length == text.Length){
        //isActivated = true;
        yield break;
      }

      textDelay = true;
      this.IncrementText();
      yield return new WaitForSeconds(0.1f);
      textDelay = false;
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
