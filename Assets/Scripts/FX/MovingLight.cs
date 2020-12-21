using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    [SerializeField]private float interval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StepBack");
    }

    IEnumerator StepBack(){
        yield return new WaitForSeconds(interval);
        transform.localPosition += new Vector3(0.05f, 0f, 0f);
        StartCoroutine("StepBack");
    }
}
