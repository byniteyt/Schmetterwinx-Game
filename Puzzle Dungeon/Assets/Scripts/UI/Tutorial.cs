using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;
    int index;
    public string scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
        StartTutorial();
    }
    void StartTutorial()
    {
        tutorial.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void NextTutorial()
    {
        if (index < tutorial.transform.childCount-1)
        {
            tutorial.transform.GetChild(index).gameObject.SetActive(false);
            index++;
            tutorial.transform.GetChild(index).gameObject.SetActive(true);
            return;
        }
        SceneManager.LoadScene(scene);
    }
}
