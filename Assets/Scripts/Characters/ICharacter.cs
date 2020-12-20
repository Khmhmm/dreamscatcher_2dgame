using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter{
    void Move(Vector3 movement);
    void SetAnimator(string fieldName, bool flag);
    float GetSpeed();
}
