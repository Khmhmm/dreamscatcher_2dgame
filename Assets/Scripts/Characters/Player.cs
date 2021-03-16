using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private Quaternion NullRotation = Quaternion.Euler(0f,0f,0f);
    private float NullZSize = 2f;
    private Quaternion BackRotation = Quaternion.Euler(0f,180f,0f);
    private bool isLeft = false;

    private Vector3 startPosition;
    const float NormalizeSpeed = 10f;
    [SerializeField]private float[] speed = {0f, 0.06f, 0.1f};
    public Animator animator;
    public float speedMultiplier=1f;
    [SerializeField]private bool isStopped=true;
    private bool dialogue = false;

    private float starterAnimatorSpeed;
    private GameObject phoneLight;

    [SerializeField]private bool blockDelay = false;

    private GameObject destroyer;
    [SerializeField]private float blockTime = 0f;

    [SerializeField]private bool insideDream = true;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        starterAnimatorSpeed = this.animator.speed;
        // takes `NeuroForce object`
        this.destroyer = this.transform.GetChild(0).gameObject;
        this.phoneLight = this.transform.GetChild(1).gameObject;
        this.destroyer.SetActive(false);
        startPosition = transform.parent.position;
        base.Start();
    }


    void FixedUpdate()
    {
        if(dialogue){
          return;
        }
        // NOTE: debug feature
        // if (Input.GetMouseButton(0) || true){
        if (Input.GetMouseButton(0) && insideDream){
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
            if(!isLeft) {
              transform.rotation = BackRotation;
              transform.localScale = new Vector3(transform.localScale[0], transform.localScale[1], -this.NullZSize);
              isLeft = true;
            }
        }
        else if (Input.GetKey(KeyCode.D)){
            this.Move(Vector3.right);
            if(isLeft) {
              transform.rotation = NullRotation;
              transform.localScale = new Vector3(transform.localScale[0], transform.localScale[1], this.NullZSize);
              isLeft = false;
            }
        }
        else{
            this.isStopped = true;
        }
    }

    void LateUpdate(){
        this.SetAnimator("stay", isStopped);
        this.SetAnimator("block", blockDelay);
        phoneLight.SetActive(this.GetComponent<Animator>().GetBool("phone"));
        if (transform.position.y < -200f){
            transform.parent.position = startPosition;
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(Input.GetKey(KeyCode.Escape)){
          SceneManager.LoadScene(0);
        }
    }

    public override void Move(Vector3 movement){
        Vector3 norm_d = Vector3.Normalize(movement);
        norm_d.x *= GetSpeed(); norm_d.y *= GetSpeed(); norm_d.z = 0;
        transform.parent.Translate(norm_d);
        this.isStopped = false;
    }

    public override void SetAnimator(string fieldName, bool flag){
        this.animator.speed =  (isStopped)? starterAnimatorSpeed : GetSpeed()*NormalizeSpeed*starterAnimatorSpeed;
        this.animator.SetBool(fieldName, flag);
    }

    // TODO: extend
    public override float GetSpeed(){
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

    public void LockDialogue(){
      dialogue = true;
      isStopped = true;
    }

    public void ReleaseDialogue(){
      dialogue = false;
    }
}
