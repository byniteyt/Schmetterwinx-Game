using TMPro;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    public static EnemyWarning instance;
    public GameObject text;
    [SerializeField] private Vector2 offset;

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
        ShowEnemiesHP();
    }
    public void ShowEnemiesIntentions()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<EnemyBasic>().GetUIAction() != null)
            {
                // If enemy already has a UI element for the intention, update its text without creating another one
                enemy.GetComponent<EnemyBasic>().GetUIAction().GetComponent<TextMeshProUGUI>().text =
                (enemy.GetComponent<EnemyBasic>().intention[0] == 0 ? "Attack" : "Block") + ": " +
                enemy.GetComponent<EnemyBasic>().power[0];
                return;
            }
            var textObject = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            Vector3 screenPos = new Vector3(
                Camera.main.WorldToScreenPoint(enemy.GetComponent<BoxCollider2D>().bounds.center).x, // center of the object
                Camera.main.WorldToScreenPoint(enemy.GetComponent<BoxCollider2D>().bounds.min).y, 0); // top 
            textObject.transform.position = screenPos + (Vector3)offset;
            textObject.GetComponent<TextMeshProUGUI>().text =
                (enemy.GetComponent<EnemyBasic>().intention[0] == 0 ? "Attack" : "Block") + ": " +
                enemy.GetComponent<EnemyBasic>().power[0];
            enemy.GetComponent<EnemyBasic>().AddUIAction(textObject);   // register the UI element in the EnemyBasic script for future updates
        }

    }
    public void ShowEnemiesHP() {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<EnemyBasic>().GetUILife() != null)
            {
                // If enemy already has a UI element for the intention, update its text without creating another one
                enemy.GetComponent<EnemyBasic>().GetUILife().GetComponent<TextMeshProUGUI>().text =
                ($"HP: { enemy.GetComponent<EnemyBasic>().hp}");
                return;
            }
            var textObject = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            Vector3 screenPos = new Vector3( 
                Camera.main.WorldToScreenPoint(enemy.GetComponent<BoxCollider2D>().bounds.center).x, // center of the object
                Camera.main.WorldToScreenPoint(enemy.GetComponent<BoxCollider2D>().bounds.max).y,0); // top 
            textObject.transform.position = screenPos;
            textObject.GetComponent<TextMeshProUGUI>().text =
                ("HP: "+enemy.GetComponent<EnemyBasic>().hp);
            enemy.GetComponent<EnemyBasic>().AddUILife(textObject);
        }
    }
}
