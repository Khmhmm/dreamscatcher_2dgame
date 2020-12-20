using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour{
    // contains prefab of the effect which will be spawned when destroying something
    public GameObject fx;
    public GameObject lastfx;
    public bool continuingBlock=false;
    [SerializeField]private bool isBlockAffects = true;

    void Awake(){
        isBlockAffects = true;
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
        if(gObj.tag != "Player" && isBlockAffects) {
            Destroy(gObj);
        }

        // cheks if this effect will be deleted from scene
        if( fx != null && fx.GetComponent<DestroyFX>() != null){
            if(lastfx != null){
                if(continuingBlock){
                    lastfx.GetComponent<DestroyFX>().prohibitDestroy = true;
                }
                else{
                    DestroyLastFx();
                }
            }
            lastfx = Instantiate(fx, transform.position + new Vector3(1f, 1f, 0f), Quaternion.Euler(0f,0f,0f));
            lastfx.transform.parent = this.transform;
        }
    }

    public void DestroyLastFx(){
        Destroy(lastfx);
    }

    public bool GetIsBlockAffects(){
        return isBlockAffects;
    }
}
