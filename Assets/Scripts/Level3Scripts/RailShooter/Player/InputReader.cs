using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField, Self] private PlayerInput playerInput;
    InputAction moveAction;

    public Vector2 GetMove => moveAction.ReadValue<Vector2>();
    // For reference, the above is equivalent to this:

    //public Vector2 GetMove
    //{
    //    get
    //    {
    //        return moveAction.ReadValue<Vector2>();
    //    }
    //}


    // Validates references, attempts to assign missing refs, and logs errors for us.
    private void OnValidate()
    {
        this.ValidateRefs();
    }

    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }
}
