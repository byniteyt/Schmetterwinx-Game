using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spell : MonoBehaviour
{
    GameObject textInfo;
    public EffectType effect;           // Type of effect the spell has
    public int power;                   // Magnitude of the effect
    private GameObject target;          // Target of the spell. Protection spells will target the caster's team
    public CharacterClass casterClass;  // Class of the character casting the spell
    private Vector2 originalPosition = Vector2.zero;

    private void Start()
    {
        originalPosition = transform.position;

    }
    public Vector2 getPosition() { 
    return originalPosition;
    }

    private void ApplyEffect()
    {
        if (target.gameObject.CompareTag("Enemy") || target.gameObject.CompareTag("Player") || target.gameObject.CompareTag("Untagged"))
        {
            // Apply the spell effect to the target
            switch (effect)
            {
                case EffectType.Damage:
                    if (target.gameObject.CompareTag("Enemy"))
                    {
                        GameManager.instance.ApplyEffect(casterClass);
                        target.GetComponent<EnemyBasic>().TakeDamage(power);
                        break;
                    }
                    else
                    {
                        Debug.LogWarning("No puedes usar hechizos si no es un enemigo.");
                        return;
                    }
                case EffectType.Protection:
                    // Create a shield or increase damage resistance
                    GameManager.instance.block += power;
                    GameManager.instance.ApplyEffect(casterClass);
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
                    GameManager.instance.ApplyEffect(casterClass);
                    break;
                default:
                    Debug.LogWarning("Effect type not implemented yet.");
                    return;
            }
        }
        else
        {
            Spell laggan = (Spell)target.GetComponent<Spell>();
            if (!laggan.casterClass.Equals(this.casterClass))
            {
                GameObject GurrenLaggan = new GameObject();
                GurrenLaggan.AddComponent<SpriteRenderer>();
                GurrenLaggan.AddComponent<CombinedSpell>().Fuuuuusion(this, laggan);
                GurrenLaggan.AddComponent<BoxCollider2D>();
                GurrenLaggan.GetComponent<BoxCollider2D>().size = new Vector2(1.586667f, 2.443333f);

                Debug.LogWarning("GURREN LAGGAN");
                switch (casterClass)
                {
                    case CharacterClass.Warrior:
                        switch (laggan.casterClass)
                        {
                            case CharacterClass.Mage:
                                GurrenLaggan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cards/cardsfirsttry3_0");
                                break;
                            case CharacterClass.Cleric:
                                GurrenLaggan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cards/cardsfirsttry3_2");
                                break;
                        }
                        break;
                    case CharacterClass.Mage:
                        switch (laggan.casterClass)
                        {
                            case CharacterClass.Warrior:
                                GurrenLaggan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cards/cardsfirsttry3_0");
                                break;
                            case CharacterClass.Cleric:
                                GurrenLaggan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cards/cardsfirsttry3_1");
                                break;
                        }
                        break;
                    case CharacterClass.Cleric:
                        switch (laggan.casterClass)
                        {
                            case CharacterClass.Warrior:
                                GurrenLaggan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cards/cardsfirsttry3_2");
                                break;
                            case CharacterClass.Mage:
                                GurrenLaggan.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("cards/cardsfirsttry3_1");
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }
            Destroy(laggan);
            Destroy(target);
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
            textInfo.SetActive(false);
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
        while(hit.Contains(this.gameObject.GetComponent<Collider2D>())) {
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
                        target = h.gameObject;
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
        if (textInfo == null)
        {
            textInfo = Instantiate(Resources.Load<GameObject>("UI/Text"), FindAnyObjectByType<Canvas>().transform);
            textInfo.transform.position = MarginForText.GetTopPosition(gameObject, new Vector3(0, 30, 0));
            textInfo.GetComponent<TextMeshProUGUI>().text =
                effect.ToString() + "\nPower: " + power.ToString();
        }
        else
        {
            textInfo.GetComponent<TextMeshProUGUI>().text =
                effect.ToString() + "\nPower: " + power.ToString();
            textInfo.SetActive(true);
        }

            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnMouseExit()
    {
        textInfo.SetActive(false);
        transform.localScale = new Vector3(1, 1, 1);
    }
    
}
