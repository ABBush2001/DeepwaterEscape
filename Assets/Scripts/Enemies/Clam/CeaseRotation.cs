using UnityEngine;


// This script just simply sets the non-horizontal rotation to 0


public class CeaseRotation : MonoBehaviour
{
    Quaternion objRot;
    public bool lockX = false;
    public bool lockY = true;
    public bool lockZ = true;

    private float xRot = 0f;
    private float yRot = 0f;
    private float zRot = 0f;

    void Awake()
    {
        objRot = transform.rotation;
        Debug.Log("I am sorry, but this script is not going to do anything, it is dumb.", gameObject);
    }

    void Update()
    {
        if (!lockX) {
            xRot = objRot.x;
        }
        if (!lockY) { 
            yRot = objRot.y; 
        }
        if (!lockZ) {
            zRot = objRot.z; 
        }

        objRot.Set(xRot, yRot, zRot, objRot.w);
    }
}
