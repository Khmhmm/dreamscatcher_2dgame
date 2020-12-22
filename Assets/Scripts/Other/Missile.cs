using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // contains particlesFx prefab
    public GameObject particlesFx;

    void Start(){
        StartCoroutine("SelfDestroy");
        if(particlesFx != null && particlesFx.GetComponent<DestroyFX>() != null){
            particlesFx = Instantiate(particlesFx, transform.position, transform.rotation);
            particlesFx.transform.parent = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(-0.05f, 0f));
        if(particlesFx!=null){
            particlesFx.transform.Translate(new Vector3(-0.05f, 0f));
        }
    }

    IEnumerator SelfDestroy(){
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col){
        if( col.gameObject.GetComponent<Player>() != null ){
            Destroy(col.gameObject);
        }
    }
}
