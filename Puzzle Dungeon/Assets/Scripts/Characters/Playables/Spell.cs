using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public EffectType effect;           // Type of effect the spell has
    public int power;                   // Magnitude of the effect
    private GameObject target;          // Target of the spell. Protection spells will target the caster's team
    public CharacterClass casterClass;  // Class of the character casting the spell
    private Vector2 originalPosition = Vector2.zero;
    void Start()
    {
    }

    public void CastSpell(GameObject target)
    {
        this.target = target;
        ApplyEffect();
    }

    private void ApplyEffect()
    {
        // Apply the spell effect to the target
        switch (effect)
        {
            case EffectType.Damage:
                // Reduce the target's health by power
                break;
            case EffectType.Protection:
                // Create a shield or increase damage resistance
                break;
            default:
                Debug.LogWarning("Effect type not implemented yet.");
                break;
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("Spell selected: " + gameObject.name);
    }
    private void OnMouseEnter()
    {
        // Optionally, highlight the spell card or show additional info
        Debug.Log("Hovering over spell: " + gameObject.name);
    }
    private void OnMouseDrag()
    {
        if (originalPosition == Vector2.zero)
            originalPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    private void OnMouseUp()
    {
        // Return the card to its original position
        transform.position = originalPosition;
        originalPosition = Vector2.zero;
            Collider2D[] hit = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition),
    LayerMask.GetMask("Battle"));
            if (hit.Length != 0)
            {
                foreach (Collider2D h in hit)
                {
                    if (h.gameObject.CompareTag("Enemy"))
                    {
                        CastSpell(h.gameObject);
                        Debug.Log($"Spell cast on {h.gameObject.name}");
                        return;
                    }
                }
                Debug.Log("No valid target selected.");
            }
    }

}
