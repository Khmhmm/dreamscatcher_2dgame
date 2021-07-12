using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour{
    // contains prefab of the effects
    public GameObject fx;
    public GameObject lastfx;
    public bool continuingBlock=false;
    [SerializeField]private bool isBlockAffects = true;
    public bool idealBlock = true;

    public void WakeUp(){
        isBlockAffects = true;
        idealBlock = true;

        // cheks if this effect's prefab was assigned and will be deleted from scene
        if( fx != null && fx.GetComponent<DestroyFX>() != null){
            if(lastfx != null){
                if(continuingBlock){
                    lastfx.GetComponent<DestroyFX>().prohibitDestroy = true;
                }
                else{
                    DestroyLastFx();
                }
            }
            lastfx = Instantiate(fx, transform.position + new Vector3(0.1f, 0.1f, 0f), Quaternion.Euler(0f,0f,0f));
            lastfx.transform.parent = this.transform;
        }
    }

    void LateUpdate(){
        if(lastfx==null){
            isBlockAffects = false;
        }
        else{
            isBlockAffects = true;
        }
    }

    //Destroys everything what collides except of player
    void OnTriggerEnter(Collider col){
        var gObj = col.gameObject;
        Debug.Log(gObj.name);
        if(gObj.GetComponent<IDestroyable>() != null && isBlockAffects) {
            var then = gObj.GetComponent<IDestroyAndThen>();
            if (then != null) {
              then.DestroyAndThen();
            }
            else{
              Destroy(gObj);
            }
        }
    }

    void OnTriggerStay(Collider col){
        var gObj = col.gameObject;
        if(gObj.tag != "Player") {
            if(!idealBlock){
                transform.parent.GetComponent<Player>().Move(Vector3.left);
            }
        }
    }

    public void DestroyLastFx(){
        Destroy(lastfx);
    }

    public bool GetIsBlockAffects(){
        return isBlockAffects;
    }
}
