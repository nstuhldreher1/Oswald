using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance { get; private set; }
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera battleCamera;
    private bool isBattleCameraActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleBattleCamera()
    {
        if (!isBattleCameraActive)
        {
            Debug.Log("Switching to battle camera.");
            battleCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            isBattleCameraActive = true;
        }
        else
        {
            Debug.Log("Switching back to main camera.");
            battleCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            isBattleCameraActive = false;
        }
    }
}