using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CombinedSpell : Spell
{
    Spell spellA, spellB;
    public GameObject targetC;
    public EffectType effect2;
    public CharacterClass casterClass2;
    private Vector2 originalPosition = Vector2.zero;
    private GameObject textInfoC;

    public  void Fuuuuusion(Spell a, Spell b)
    { 
        this.spellA = a;
        this.spellB = b;
        this.effect = a.effect;
        this.power = a.power + b.power; // Combine the power of both spells
        this.casterClass = a.casterClass;
        this.effect2 = b.effect;
        this.casterClass2 = b.casterClass;
        this.originalPosition = b.transform.position;
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
        switch (effect)
        {
            case EffectType.Damage:
                if (targetC.gameObject.CompareTag("Enemy"))
                {
                    GameManager.instance.ApplyEffect(casterClass);
                    targetC.GetComponent<EnemyBasic>().TakeDamage(power);
                    break;
                }
                Debug.LogWarning("Cannot cast damage spell on non-enemy target.");
                return;
            case EffectType.Protection:
                // Create a shield or increase damage resistance
                GameManager.instance.block += power;
                GameManager.instance.ApplyEffect(casterClass);
                break;
            default:
                Debug.LogWarning("Effect type not implemented yet.");
                return;
        }
        switch (effect2)
        {
            case EffectType.Damage:
                if (targetC.gameObject.CompareTag("Enemy"))
                {
                    GameManager.instance.ApplyEffect(casterClass2);
                    targetC.GetComponent<EnemyBasic>().TakeDamage(power);
                    break;
                }
                Debug.LogWarning("Cannot cast damage spell on non-enemy target.");
                return;
            case EffectType.Protection:
                // Create a shield or increase damage resistance
                GameManager.instance.block += power;
                GameManager.instance.ApplyEffect(casterClass2);
                break;
            default:
                Debug.LogWarning("Effect type not implemented yet.");
                return;
        }
        GetComponent<SpriteRenderer>().enabled = false;
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
        // Return the card to its original position
        transform.position = originalPosition;
        originalPosition = Vector2.zero;
        Collider2D[] hit = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (hit.Length != 0)
        {
            foreach (Collider2D h in hit)
            {
                if (h.gameObject.CompareTag("Enemy") || h.gameObject.CompareTag("Player"))
                {
                    CastSpell(h.gameObject);
                    return;
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