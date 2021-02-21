using UnityEngine;

public class SwitchAnimOnActor : MonoBehaviour, IDestroyAndThen{
  public GameObject actor;
  public string actorName;
  public string boolName;
  public bool valueToSet;
  public GameObject spawnOnDestroy;

  void Start() {
    if (actor == null){
      actor = GameObject.Find(actorName);
    }
    actor.GetComponent<Animator>().SetBool(boolName, valueToSet);
    this.GetComponent<IDestroyAndThen>().DestroyAndThen();
  }



  void IDestroyAndThen.DestroyAndThen(){
    if(this.spawnOnDestroy != null){
      Instantiate(this.spawnOnDestroy, this.transform.parent);
    }
    Destroy(this.gameObject);
  }
}
