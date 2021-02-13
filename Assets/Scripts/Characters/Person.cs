using UnityEngine;

public class Person: Character{
  public override void Move(Vector3 v){
    this.transform.position += v;
  }

  public override float GetSpeed(){
    return 0f;
  }

  public override void SetAnimator(string s, bool b){
    
  }
}
