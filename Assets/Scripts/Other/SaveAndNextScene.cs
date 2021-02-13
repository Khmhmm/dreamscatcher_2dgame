using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndNextScene : MonoBehaviour{

  public int nextSceneIdx;

  public void Start(){
    var mgd = GameObject.Find("GameData").GetComponent<MetaGameData>();
    mgd.lastLevel = nextSceneIdx;
    var deser = transform.parent.GetComponent<Deser>();
    mgd.SaveGame(deser);
    SceneManager.LoadScene(nextSceneIdx);
  }
}
