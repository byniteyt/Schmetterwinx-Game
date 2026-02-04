using TMPro;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    public static EnemyWarning instance;
    public GameObject text;
    [SerializeField] private Vector2 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ShowEnemiesIntentions();
    }
    public void ShowEnemiesIntentions()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            var textObject = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            textObject.transform.position = screenPos + (Vector3)offset;
            textObject.GetComponent<TextMeshProUGUI>().text =
                (enemy.GetComponent<EnemyBasic>().intention[0] == 0 ? "Attack" : "Block") + ": " +
                enemy.GetComponent<EnemyBasic>().power[0];
        }

    }
}
