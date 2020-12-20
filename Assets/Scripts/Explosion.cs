using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float power=5f;
    public List<Collider2D> affectsOn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Blow");
    }

    void OnTriggerEnter2D(Collider2D col){
        if (!affectsOn.Contains(col)){
            GameObject obj = col.gameObject;
            if (obj.GetComponent<Rigidbody2D>() != null && obj.tag != "Player"){
                affectsOn.Add(col);
            }
        }
    }

    IEnumerator Blow(){
        yield return new WaitForEndOfFrame();

        foreach (var col in affectsOn){
            if (col.gameObject.tag == "Player" || col.gameObject == this){
                continue;
            }
            Vector2 movement = ( (Vector2)(col.gameObject.transform.position - this.transform.position) );
            movement.Normalize();
            movement.x *= power;
            movement.y *= power;
            GameObject obj = col.gameObject;
            obj.GetComponent<Rigidbody2D>().AddForce(movement);
            obj.GetComponent<Rigidbody2D>().mass *= 2f;
            obj.transform.localScale = new Vector3(obj.transform.localScale.x / 2f, obj.transform.localScale.y / 2f, obj.transform.localScale.z);
        }

        Destroy(this.gameObject);
    }
}
