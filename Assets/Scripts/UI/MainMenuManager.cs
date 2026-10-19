using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public void OnClickStartGame()
    {
       Debug.Log("Start Game");
        SceneManager.instance.LoadScene("SampleScene");
    }

    public void OnClickQuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}