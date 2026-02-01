using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameWorld");
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {
        /* This function is a function I created some time ago
         * We can load a new scene or instantiate a prefab with the settings menu with:
         * Instantiate(Resources.Load<GameObject>("location of the UI"), this.transform);
         */
        SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadScene(SceneAsset scene)
    {
        // this function is in case of we want to load a scene by its asset reference instead of create a new one
        SceneManager.LoadScene(scene.name);
    }
}
