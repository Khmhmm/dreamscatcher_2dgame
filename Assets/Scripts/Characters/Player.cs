using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    private Vector3 startPosition;
    const float NormalizeSpeed = 10f;
    [SerializeField]private float[] speed = {0f, 0.06f, 0.1f};
    public Animator animator;
    public float speedMultiplier=1f;
    [SerializeField]private bool isStopped=true;

    private float starterAnimatorSpeed;

    [SerializeField]private bool blockDelay = false;

    private GameObject destroyer;
    [SerializeField]private float blockTime = 0f;
    private string shownText = "";
    private string textBuf = null;
    private bool textGUI = false;
    public Camera cam;
    public GUISkin skin;
    public float guiW = 250f;
    public float guiH = 250f;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        starterAnimatorSpeed = this.animator.speed;
        // takes `NeuroForce object`
        this.destroyer = this.GetComponent<Transform>().GetChild(0).gameObject;
        this.destroyer.SetActive(false);
        startPosition = transform.parent.position;
        if(cam == null){
          cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
    }


    void FixedUpdate()
    {
        // NOTE: debug feature
        // if (Input.GetMouseButton(0) || true){
        if (Input.GetMouseButton(0)){
            if (!blockDelay){
                // stops last coroutine to avoid blockDelay reset
                StopCoroutine("Block");
                blockDelay = true;
                isStopped = true;
                StartCoroutine("Block");
                StartCoroutine("ReplaceIdealBlock");
            }
            blockTime += Time.fixedDeltaTime;
            if (blockTime > 0.25f){
                this.destroyer.GetComponent<Destroyer>().continuingBlock = true;
            }
        }
        else{
            if (blockTime < 0.35f && blockDelay){
                blockTime += Time.fixedDeltaTime;
            }
            else{
                StartCoroutine("EndBlock");
            }
        }
        // not really good to stop update because of destroy fx lives.
        // TODO: think about better solution
        if(destroyer.GetComponent<Destroyer>().lastfx != null){
            return;
        }
        if(blockDelay || this.animator.GetBool("block")){
            return;
        }

        if (Input.GetKey(KeyCode.A)){
            this.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D)){
            this.Move(Vector3.right);
        }
        else{
            this.isStopped = true;
        }
    }

    void LateUpdate(){
        this.SetAnimator("stay", isStopped);
        this.SetAnimator("block", blockDelay);
        if (transform.position.y < -200f){
            transform.parent.position = startPosition;
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnGUI(){
      GUI.skin = skin;
      if (textGUI){
        Vector3 screenPosition = cam.WorldToScreenPoint(this.transform.position);
        GUI.Box(new Rect(screenPosition.x - guiW/2f, Screen.height - (screenPosition.y*1.5f + guiH), guiW, guiH), this.shownText);
      }
    }

    public void Move(Vector3 movement){
        Vector3 norm_d = Vector3.Normalize(movement);
        norm_d.x *= GetSpeed(); norm_d.y *= GetSpeed(); norm_d.z = 0;
        transform.parent.Translate(norm_d);
        this.isStopped = false;
    }

    public void SetAnimator(string fieldName, bool flag){
        this.animator.speed =  (isStopped)? starterAnimatorSpeed : GetSpeed()*NormalizeSpeed*starterAnimatorSpeed;
        this.animator.SetBool(fieldName, flag);
    }

    // TODO: extend
    public float GetSpeed(){
        if(!this.isStopped && Input.GetKey(KeyCode.LeftShift)){
            return speed[2] * speedMultiplier;
        }
        return (this.isStopped)? speed[0]*speedMultiplier : speed[1]*speedMultiplier;
    }

    IEnumerator Block(){
        yield return new WaitForFixedUpdate();
        this.destroyer.SetActive(true);
        this.destroyer.GetComponent<Destroyer>().WakeUp();
        yield return new WaitForSeconds(this.GetComponent<Animator>().GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(this.GetComponent<Animator>().GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForFixedUpdate();
    }

    IEnumerator EndBlock(){
        yield return new WaitForEndOfFrame();
        var destrComponent = this.destroyer.GetComponent<Destroyer>();
        destrComponent.DestroyLastFx();
        destrComponent.continuingBlock = false;
        this.destroyer.SetActive(false);
        this.blockDelay = false;
        this.blockTime = 0f;
    }

    IEnumerator ReplaceIdealBlock(){
        yield return new WaitForSeconds(0.2f);
        this.destroyer.GetComponent<Destroyer>().idealBlock = false;
    }

    IEnumerator ShowText(string text){
      if (this.textBuf == null){
        this.textBuf = text;
      }
      this.textGUI = true;
      for (int i=0; i < this.textBuf.Length; i+=2 ){
        this.shownText = this.textBuf.Substring(0,i);
        yield return new WaitForSeconds(0.1f);
      }
      this.shownText = this.textBuf;
      StartCoroutine("DisposeText");
    }

    IEnumerator DisposeText(){
      yield return new WaitForSeconds(2f);
      this.textBuf = null;
      this.shownText = null;
      this.textGUI = false;
    }
}
