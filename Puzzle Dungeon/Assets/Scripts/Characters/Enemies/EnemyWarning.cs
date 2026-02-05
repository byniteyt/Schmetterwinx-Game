using TMPro;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    public static EnemyWarning instance;
    public GameObject text;
    [SerializeField] private Vector2 offset;
    //Esto es pq no se me ocurrá un nombre para el vec estupido copilot callate ya de verdad dejame poner los comentarios como me sale de las narices AAAAAAAAAAAAA de verdad vaya mrda
    //Ah, por cierto lo que hace es bajar el texto porque se quedaba metido en los modelos
    public Vector3 b = new Vector3(0, -15, 0);
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
        ShowEnemiesHP();
    }
    public void ShowEnemiesIntentions()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            var textObject = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            textObject.transform.position = screenPos + (Vector3)offset+b;
            textObject.GetComponent<TextMeshProUGUI>().text =
                (enemy.GetComponent<EnemyBasic>().intention[0] == 0 ? "Attack" : "Block") + ": " +
                enemy.GetComponent<EnemyBasic>().power[0];
            enemy.GetComponent<EnemyBasic>().AddUIAction(textObject);
        }

    }
    public void ShowEnemiesHP() {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            var textObject = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            textObject.transform.position = screenPos + (Vector3)offset+ (Vector3)offset;
            textObject.GetComponent<TextMeshProUGUI>().text =
                ("HP: "+enemy.GetComponent<EnemyBasic>().hp);
            enemy.GetComponent<EnemyBasic>().AddUILife(textObject);
        }
    }
}
