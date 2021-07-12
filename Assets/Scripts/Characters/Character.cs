using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: MonoBehaviour{
    public GUISkin skin;
    string textBuf, shownText;
    bool textGUI = false;
    Camera cam;
    public float guiW = 250f;
    public float guiH = 250f;
    private bool mouseBreak = false;

    public abstract void Move(Vector3 movement);
    public abstract void SetAnimator(string fieldName, bool flag);
    public abstract float GetSpeed();

    protected void Start(){
      if (cam == null){
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
      }
    }

    void ViewText(Vector3 screenPosition, float guiW, float guiH, string shownText){
      GUI.Box(new Rect(screenPosition.x + guiW/2f, Screen.height - (screenPosition.y*1.5f + guiH), guiW + shownText.Length * 7.5f + 20f, guiH), shownText);
    }

    // Returns time (in seconds) which needed to print all the text
    // and dispose it (and also 0.25 seconds to avoid some problems)
    public float InvokeShowText(string text){
      StartCoroutine("MouseBreak");
      StartCoroutine("ShowText", text);
      return (float)((text.Length / 5) + 1) * 0.1f + 2.25f;
    }

    IEnumerator ShowText(string text){
      if (this.textBuf == null){
        this.textBuf = text;
      }
      this.textGUI = true;
      for (int i=0; i + 5 < this.textBuf.Length; i+=5 ){
        if(mouseBreak) { break; }
        this.shownText = this.textBuf.Substring(0,i);
        yield return new WaitForSeconds(0.1f);
      }
      this.shownText = this.textBuf;
      mouseBreak = false;
      StartCoroutine("MouseBreak");
      StartCoroutine("DisposeText");
    }

    IEnumerator MouseBreak() {
      yield return new WaitForSeconds(0.25f);
      if (mouseBreak){
        mouseBreak = false;
        yield return new WaitForSeconds(0.25f);
      }
      for(int i=0;i<2500;i++){
        if(Input.GetMouseButtonDown(0)){
          mouseBreak = true;
          Debug.Log("Clicked");
          break;
        }
        yield return new WaitForSeconds(0.001f);
      }
      Debug.Log("Exit");
    }

    IEnumerator DisposeText(){
      for(int i=0; i< 250; i++){
        yield return new WaitForSeconds(0.01f);
        if(mouseBreak) { break; }
      }
      this.textBuf = null;
      this.shownText = null;
      this.textGUI = false;
      mouseBreak = false;
    }

    string GetTextToShow(){
      return this.shownText;
    }

    public bool IsTextGUI(){
      return this.textGUI;
    }

    void OnGUI(){
      GUI.skin = skin;
      if (this.IsTextGUI()){
        Vector3 screenPosition = cam.WorldToScreenPoint(this.transform.position);
        this.ViewText(screenPosition, guiW, guiH, this.GetTextToShow());
      }
    }
}
