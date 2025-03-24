using UnityEngine;

public class OnRailsPlayerFollow : MonoBehaviour
{
    private Transform playerModel;

    public Camera mainCam;
    public float xySpeed = 18;
    public float lookSpeed = 340;
    public float leanLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerModel = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        LocalMove(h, v, xySpeed);
        ClampPosition();
        HorizontalLean(playerModel, h, leanLimit, .1f);
    }

    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += speed * Time.deltaTime * new Vector3(x, y, 0);
    }

    void ClampPosition()
    {
        Vector3 pos = mainCam.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = mainCam.ViewportToWorldPoint(pos);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));
    }

}
