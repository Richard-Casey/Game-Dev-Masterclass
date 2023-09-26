using UnityEngine;


// This script needs to be attached to each playable character in the game
// The 'abilities' array will be populated automatically when the game starts through the Start() method.
public class AbilityManager : MonoBehaviour
{
    public Abilities.Ability[] abilities;

    private void Start()
    {
        // Initialise abilities
        abilities = new Abilities.Ability[8]; // This will need to be modified as abilities are added/removed!
        abilities[0] = new Abilities.Melee();
        abilities[1] = new Abilities.Block();
        abilities[2] = new Abilities.Dash();
        abilities[3] = new Abilities.StrongMeleeAttack();
        abilities[4] = new Abilities.ThreeArrowAttack();
        abilities[5] = new Abilities.Shield();
        abilities[6] = new Abilities.Teleport();
        abilities[7] = new Abilities.ExplodingBolt();

        // Conformation in console that array is being populated
        foreach (var ability in abilities)
        {
            Debug.Log("Ability: " + ability.abilityName + " added to array");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ActivateAbility(0, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ActivateAbility(1, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ActivateAbility(2, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateAbility(3, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            ActivateAbility(4, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            ActivateAbility(5, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateAbility(6, this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ActivateAbility(7, this.gameObject);
        }
    }

    public void ActivateAbility(int index, GameObject user)
    {
        if (abilities[index] != null)
        {
            abilities[index].Activate(user);
        }
    }
}
