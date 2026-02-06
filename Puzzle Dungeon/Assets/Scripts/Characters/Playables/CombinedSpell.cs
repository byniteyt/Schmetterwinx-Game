using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CombinedSpell : Spell
{
    public GameObject targetC;
    public EffectType effect2;
    public CharacterClass casterClass2;
    private Vector2 originalPositionC = Vector2.zero;
    private GameObject textInfoC;
    private void Start()
    {
        info = EnemyWarning.instance.text;
    }
    new public void CastSpell(GameObject target)
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
                    //Hay que poner aquí que el enemigo pierda vida, en vaez de ser destruido, y que si llega a 0, entonces sea destruido(creo que esto último sería mejor como función dentro de los enemigos algo como if hp=0 Destroy()this))
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
                    //Hay que poner aquí que el enemigo pierda vida, en vaez de ser destruido, y que si llega a 0, entonces sea destruido(creo que esto último sería mejor como función dentro de los enemigos algo como if hp=0 Destroy()this))
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
        //Destroy(gameObject);
    }
    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        originalPositionC = transform.position;
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
        transform.position = originalPositionC;
        originalPositionC = Vector2.zero;
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
            textInfoC = Instantiate(info, FindAnyObjectByType<Canvas>().transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 position = new Vector3(0, 150, 0);
            textInfoC.transform.position = screenPos + position;
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