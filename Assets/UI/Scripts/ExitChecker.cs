using UnityEngine;

public class ExitChecker : MonoBehaviour
{
    public ExitHandler progressionCheck;
    // Start is called before the first frame update

    void Start()
    {
        PlayerPrefs.SetInt("HasJellied", 1);
        progressionCheck.hasJellied = true;
    }
}
