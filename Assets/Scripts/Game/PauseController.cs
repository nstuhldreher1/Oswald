using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController instance{ get; private set; }
    private bool isGamePaused = false;

    private void Awake()
    {
        instance = this;
    }
    public bool getIsGamePaused()
    {
        return isGamePaused;
    }

    public void setIsGamePaused(bool value)
    {
        isGamePaused = value;
    }
    
}
