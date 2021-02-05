using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour, IDestroyable
{
  private float signX = 1f, signY = 1f;
    void Start()
    {
      StartCoroutine("RecursivelyMoveAndSwitchDirection");
    }

    IEnumerator RecursivelyMoveAndSwitchDirection(){
      for(int _i=0; _i < 10; _i++){
        yield return new WaitForSeconds(0.1f);
        transform.Translate(new Vector3(signX * 0.025f, signY * 0.0125f));
      }
      signX = -signX;
      for(int _i=0; _i < 10; _i++){
        yield return new WaitForSeconds(0.1f);
        transform.Translate(new Vector3(signX * 0.0125f, signY * 0.025f));
      }
      signY = -signY;
      StartCoroutine("RecursivelyMoveAndSwitchDirection");
    }
}
