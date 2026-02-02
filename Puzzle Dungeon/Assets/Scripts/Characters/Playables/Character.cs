using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterClass characterClass;
    private Spell[] spells;
    void Start()
    {
        spells = FindObjectsByType<Spell>(FindObjectsSortMode.None)         // Access all Spell instances in the scene
            .Where(spell => spell.casterClass == characterClass).ToArray(); // Filter spells by the character's class
    }
}
