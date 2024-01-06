using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
   
    public string startSceneName;

    
    public void StartGame()
    {
        SceneManager.LoadScene(startSceneName);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void ClearAllDataGame() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
