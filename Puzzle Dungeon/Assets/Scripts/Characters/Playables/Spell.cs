using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    GameObject textInfo;
    public EffectType effect;           // Type of effect the spell has
    public int power;                   // Magnitude of the effect
    private GameObject target;          // Target of the spell. Protection spells will target the caster's team
    public CharacterClass casterClass;  // Class of the character casting the spell
    private Vector2 originalPosition = Vector2.zero;

    /*public Spell(GameObject a) { 
        this.transform.position = a.transform.position;
        this.power = a.GetComponent<Spell>().power;
        this.effect = a.GetComponent<Spell>().effect;
        this.casterClass = a.GetComponent<Spell>().casterClass;
    }*/
    public Spell() { }
    private void Start()
    {
    }
    /*public void CastSpell(GameObject target)
    {
        this.target = target;
        if (!target.CompareTag("Card")) {
        ApplyEffect();
    }
        Spell objetivo =  new Spell (target);
        Combine(objetivo);
    }*/

    private void ApplyEffect()
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
        GetComponent<SpriteRenderer>().enabled = false;
        //Destroy(gameObject);
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
        // Return the card to its original position
        transform.position = originalPosition;
        originalPosition = Vector2.zero;
        Collider2D[] hit = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (hit.Length != 0)
        {
            foreach (Collider2D h in hit)
            {
                if (h.gameObject.CompareTag("Enemy")|| h.gameObject.CompareTag("Player"))
                {
                    //CastSpell(h.gameObject);
                    target = h.gameObject;
                    ApplyEffect();
                    return;
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
            textInfo.SetActive(true);
        }

            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnMouseExit()
    {
        textInfo.SetActive(false);
        transform.localScale = new Vector3(1, 1, 1);
    }
    private void Combine(Spell carta) {
        Spell Combinacion = Instantiate(carta, carta.transform.position, Quaternion.identity);
        Combinacion.power = ((int)((carta.power + this.power)*1.2));

        // new Spell() { effect = this.effect, power = this.power+carta.power, transform.position = carta.transform.position,};

    }
}
