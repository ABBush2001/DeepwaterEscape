using UnityEngine;

// literally all this does is just disable the gameobject on load

public class DisableOnLoad : MonoBehaviour
{
    void Start() => gameObject.SetActive(false);
}
