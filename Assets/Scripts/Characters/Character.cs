using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: MonoBehaviour{
    public abstract void Move(Vector3 movement);
    public abstract void SetAnimator(string fieldName, bool flag);
    public abstract float GetSpeed();

    public void ViewText(Vector3 screenPosition, float guiW, float guiH, string shownText){
      GUI.Box(new Rect(screenPosition.x + guiW/2f, Screen.height - (screenPosition.y*1.5f + guiH), guiW + shownText.Length * 7.5f, guiH), shownText);
    }
}
