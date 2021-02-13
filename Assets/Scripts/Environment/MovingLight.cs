using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
  private Vector3 startPosition;
  private IEnumerator moveTask;
  public float xLimit;
  public float speed;
  public float interval;

  void Start(){
    startPosition = this.transform.position;
    InvokeRepeating("Move", this.interval, this.interval);
  }

  void FixedUpdate()
  {
    if (transform.position.x <  xLimit){
      this.transform.position = startPosition;
    }
  }

  void Move(){
    this.transform.position -= new Vector3(speed, 0f, 0f);
  }
}
