using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CombinedSpell : MonoBehaviour
{
    Spell spellA, spellB;
    public GameObject targetC;
    int power;
    public EffectType effect2;
    private Vector2 originalPosition = Vector2.zero;
    private GameObject textInfoC;

    public  void Fuuuuusion(Spell a, Spell b)
    { 
        this.spellA = a;
        this.spellB = b;
        power = (int)((a.power + b.power) * 1.3);
        //spellA.power = (int)(spellA.power * 1.3f);
        //spellB.power = (int)(spellB.power * 1.3f);
        originalPosition = b.getPosition();
            transform.position = originalPosition;
    }

    private void Start()
    {
    }
    public void CastSpell(GameObject target)
    {
        targetC = target;
        ApplyEffect();
    }
    private void ApplyEffect()
    {
        // Apply the spell effect to the target
        switch (spellA.effect)
        {
            case EffectType.Damage:
                if (targetC.gameObject.CompareTag("Enemy"))
                {
                    GameManager.instance.ApplyEffect(spellA.casterClass);
                    targetC.GetComponent<EnemyBasic>().TakeDamage(power);
                    break;
                }
                Debug.LogWarning("Cannot cast damage spell on non-enemy target.");
                return;
            case EffectType.Protection:
                // Create a shield or increase damage resistance
                GameManager.instance.block += power;
                GameManager.instance.ApplyEffect(spellA.casterClass);
                break;
            case EffectType.Boost:
                float a = (GameObject.FindGameObjectsWithTag("Card").Length - 1);
                int i = 0;
                while (a > 0)
                {
                    GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Spell>().power += power;
                    i++;
                    a--;
                }
                GameManager.instance.ApplyEffect(spellA.casterClass);
                break;
            default:
                Debug.LogWarning("Effect type not implemented yet.");
                return;
        }
        switch (spellB.effect)
        {
            case EffectType.Damage:
                if (targetC.gameObject.CompareTag("Enemy"))
                {
                    GameManager.instance.ApplyEffect(spellB.casterClass);
                    targetC.GetComponent<EnemyBasic>().TakeDamage(power);
                    break;
                }
                Debug.LogWarning("Cannot cast damage spell on non-enemy target.");
                return;
            case EffectType.Protection:
                // Create a shield or increase damage resistance
                GameManager.instance.block += power;
                GameManager.instance.ApplyEffect(spellB.casterClass);
                break;
            case EffectType.Boost:
                float a = (GameObject.FindGameObjectsWithTag("Card").Length - 1);
                int i = 0;
                while (a > 0)
                {
                    GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Spell>().power += power;
                    i++;
                    a--;
                }
                GameManager.instance.ApplyEffect(spellB.casterClass);
                break;
            default:
                Debug.LogWarning("Effect type not implemented yet.");
                return;
        }
        Destroy(gameObject);
    }
    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        originalPosition = transform.position;
    }

    void OnMouseDrag()
    {
        textInfoC.SetActive(false);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        transform.position = mousePos;
    }

    private void OnMouseUp()
    {
        int i = 0;
        // Return the card to its original position
        transform.position = originalPosition;
        originalPosition = Vector2.zero;
        Collider2D[] hit = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        while (hit.Contains(this.gameObject.GetComponent<Collider2D>()))
        {
            if (hit[i].Equals(gameObject.GetComponent<Collider2D>()))
            {
                hit[i] = null;
            }
            i++;

        }
        if (hit.Length != 0)
        {
            foreach (Collider2D h in hit)
            {
                if (h != null)
                {
                    if (h.gameObject.CompareTag("Enemy") || h.gameObject.CompareTag("Player") || h.gameObject.CompareTag("Card"))
                    {
                        //CastSpell(h.gameObject);
                        targetC = h.gameObject;
                        ApplyEffect();
                        return;
                    }
                }
            }
            Debug.Log("No valid target selected.");
        }
    }

    private void OnMouseEnter()
    {
        if (textInfoC == null)
        {
            textInfoC = Instantiate(Resources.Load<GameObject>("UI/Text"), FindAnyObjectByType<Canvas>().transform);
            textInfoC.transform.position = MarginForText.GetTopPosition(gameObject, new Vector3(0, 30, 0));
            textInfoC.GetComponent<TextMeshProUGUI>().text =   "this card has more than the combined might of its parts";
        }
        else
        {
            textInfoC.SetActive(true);
        }

        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnMouseExit()
    {
        textInfoC.SetActive(false);
        transform.localScale = new Vector3(1, 1, 1);
    }
}