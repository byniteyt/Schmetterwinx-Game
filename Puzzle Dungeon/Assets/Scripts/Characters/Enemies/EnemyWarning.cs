using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWarning : MonoBehaviour
{
    GameObject text;
    GameObject lifeText;
    GameObject actionText;
    private int turn;
    [SerializeField] private Vector2 offset;

    void Start()
    {
        GameObject gm = GetComponent<EnemyInfinite>().gm;
        turn = gm.GetComponent<GameManager>().turn;
        text = Resources.Load<GameObject>("UI/Text");
        ShowEnemyIntention();
        ShowEnemyHP();
    }
    public void ShowEnemyIntention()
    {
        if (SceneManager.GetActiveScene().name.Equals("InfiniteLevel"))
        {

            if (actionText != null)
            {
                // If enemy already has a UI element for the intention, update its text without creating another one
                actionText.GetComponent<TextMeshProUGUI>().text = (GetComponent<EnemyInfinite>().intention[turn] == 0 ? "Attack" : "Block") + ": " +
                GetComponent<EnemyInfinite>().power[turn];
                return;
            }
            actionText = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            actionText.transform.position = MarginForText.GetBottomPosition(gameObject, new Vector3(0, 30, 0));
            actionText.GetComponent<TextMeshProUGUI>().text =
                (GetComponent<EnemyInfinite>().intention[turn] == 0 ? "Attack" : "Block") + ": " +
                GetComponent<EnemyInfinite>().power[turn];

            return;
        }
        else
        {
            if (actionText != null)
            {
                // If enemy already has a UI element for the intention, update its text without creating another one
                actionText.GetComponent<TextMeshProUGUI>().text = (GetComponent<EnemyBasic>().intention[0] == 0 ? "Attack" : "Block") + ": " + GetComponent<EnemyBasic>().power[turn];
                return;
            }
            actionText = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            actionText.transform.position = MarginForText.GetBottomPosition(gameObject, new Vector3(0, 30, 0));
            actionText.GetComponent<TextMeshProUGUI>().text =
                (GetComponent<EnemyBasic>().intention[turn] == 0 ? "Attack" : "Block") + ": " +
                GetComponent<EnemyBasic>().power[turn];
        }
    }
    public void ShowEnemyHP()
    {
        if (SceneManager.GetActiveScene().name.Equals("InfiniteLevel"))
        {

            if (lifeText != null)
            {
                // If enemy already has a UI element for the intention, update its text without creating another one
                lifeText.GetComponent<TextMeshProUGUI>().text = ($"HP: {GetComponent<EnemyInfinite>().hp}");
                return;
            }
            lifeText = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            lifeText.transform.position = MarginForText.GetTopPosition(gameObject, new Vector3(0, 30, 0));
            lifeText.GetComponent<TextMeshProUGUI>().text =
                ("HP: " + GetComponent<EnemyInfinite>().hp);
            return;
        }
        else
        {
            if (lifeText != null)
            {
                // If enemy already has a UI element for the intention, update its text without creating another one
                lifeText.GetComponent<TextMeshProUGUI>().text = ($"HP: {GetComponent<EnemyBasic>().hp}");
                return;
            }
            lifeText = Instantiate(text, FindAnyObjectByType<Canvas>().transform);
            lifeText.transform.position = MarginForText.GetTopPosition(gameObject, new Vector3(0, 30, 0));
            lifeText.GetComponent<TextMeshProUGUI>().text =
                ("HP: " + GetComponent<EnemyBasic>().hp);
        }
    }

    public void UpdateHealth()
    {
        if (SceneManager.GetActiveScene().name.Equals("InfiniteLevel")){
            lifeText.GetComponent<TextMeshProUGUI>().text = ($"HP: {GetComponent<EnemyInfinite>().hp}");
        }
        else
        {
            lifeText.GetComponent<TextMeshProUGUI>().text = ($"HP: {GetComponent<EnemyBasic>().hp}");
        }
    }
    public void UpdateIntention(int index)
    {
        if (SceneManager.GetActiveScene().name.Equals("InfiniteLevel")){
            actionText.GetComponent<TextMeshProUGUI>().text =
               (GetComponent<EnemyInfinite>().intention[index] == 0 ? "Attack" : "Block") + ": " +
               GetComponent<EnemyInfinite>().power[index];

        }
        else
        {
            actionText.GetComponent<TextMeshProUGUI>().text =
                (GetComponent<EnemyBasic>().intention[index] == 0 ? "Attack" : "Block") + ": " +
                GetComponent<EnemyBasic>().power[index];
        }
    }

    private void OnDestroy()
    {
        Destroy(actionText.gameObject);
        Destroy(lifeText.gameObject);
    }
}
