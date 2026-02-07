/* 
 * This class is just a placeholder for enums that we may need in the future
 * Here we will define the enums that we may need in the future and
 * have it all in one place instead of having them scattered around the project
*/

// Each possible effect type for the cards or enemy attacks
public enum EffectType
{
    Damage,         // Direct damage to health
    Protection,     // Temporary shield or damage reduction
    AOEDamage,
    // The next ones are for future implementations if it's possible
    Boost,          // Temporary increase the damage or shield generated or received
    //Reduction       // Temporary decrease the damage generated or received
}

// Each possible class for the characters
public enum CharacterClass
{
    Warrior,
    Mage,
    Cleric
}