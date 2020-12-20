using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFX : MonoBehaviour
{
    public float duration = 2f;
    private Light lght;
    public float extendLightSpeed = 0.05f;
    public bool prohibitDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Terminate");
        lght = this.transform.GetChild(0).GetComponent<Light>();
        StartCoroutine("Extend");
    }

    IEnumerator Terminate(){
        yield return new WaitForSeconds(duration);
        if(prohibitDestroy){
            StartCoroutine("Terminate");
        }
        Destroy(this.gameObject);
    }

    IEnumerator Extend(){
        while(!prohibitDestroy && lght != null){
            yield return new WaitForFixedUpdate();
            lght.range += extendLightSpeed;
            lght.intensity += extendLightSpeed;
        }
    }
}
