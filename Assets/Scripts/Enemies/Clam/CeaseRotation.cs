using UnityEngine;


// This script just simply sets the non-horizontal rotation to 0


public class CeaseRotation : MonoBehaviour
{
    Quaternion objRot;

    void Awake() => objRot = transform.rotation;
    void Update() => objRot.Set(objRot.x, 0f, 0f, objRot.w);
}
