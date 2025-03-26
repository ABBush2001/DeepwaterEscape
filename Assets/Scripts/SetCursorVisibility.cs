using UnityEngine;

public class SetCursorVisibility : MonoBehaviour
{
    [Tooltip("Is the cursor visible?")]
    public bool isVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        // Confined = visible and confined to the game window, locked = invisible and locked to the center of the screen. 
        // Locked is obviously not usable for menus
        switch (isVisible)
        {
            case true:
                UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                break;
            case false:
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
}
