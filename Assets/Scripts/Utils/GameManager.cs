using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currency = 0;
    public TextMeshProUGUI currencyValue;

    public PlayerAssignment.PlayerClass playerClass;
    public PlayerAssignment.PlayerRole playerRole;

    public bool isClassAndRoleAssigned = false;

    // Add this Dictionary to map PlayerClass to a pair of AbilityType
    public Dictionary<PlayerAssignment.PlayerClass, (AbilityType, AbilityType)> classAbilities = new Dictionary<PlayerAssignment.PlayerClass, (AbilityType, AbilityType)>
    {
        { PlayerAssignment.PlayerClass.Melee, (AbilityType.Melee, AbilityType.Block) },
        { PlayerAssignment.PlayerClass.Range, (AbilityType.FireWeapon, AbilityType.Dash) },
        { PlayerAssignment.PlayerClass.AOE, (AbilityType.ThrowProjectile, AbilityType.Push) }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!isClassAndRoleAssigned)
            {
                playerClass = (PlayerAssignment.PlayerClass)Random.Range(0, System.Enum.GetValues(typeof(PlayerAssignment.PlayerClass)).Length);
                playerRole = (PlayerAssignment.PlayerRole)Random.Range(0, System.Enum.GetValues(typeof(PlayerAssignment.PlayerRole)).Length);
                isClassAndRoleAssigned = true;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    public static GameManager GetInstance()
    {
        return Instance;
    }

    public void AssignClassAndRole(PlayerAssignment.PlayerClass pClass, PlayerAssignment.PlayerRole pRole)
    {
        playerClass = pClass;
        playerRole = pRole;
        isClassAndRoleAssigned = true;
    }

    // Add this method to get the abilities for a given class
    public (AbilityType, AbilityType) GetAbilitiesForClass(PlayerAssignment.PlayerClass pClass)
    {
        return classAbilities[pClass];
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public void SubtractCurrency(int amount)
    {
        currency -= amount;
        currency = Mathf.Max(0, currency); //  To make sure that it doesnt go below zero
    }

    public void UpdateCurrencyUI()
    {
        currencyValue.text = currency.ToString();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);  // Debugging line

        // Checking to see if the current scene holds the currency value GameObject
        if (scene.name == "Main Scene")  // Make sure this name matches your scene name exactly
        {
            GameObject currencyValueObject = GameObject.Find("CurrencyValue");
            if (currencyValueObject != null)
            {
                Debug.Log("CurrencyValue Object found");  // Debugging line
                currencyValue = currencyValueObject.GetComponent<TextMeshProUGUI>();
                UpdateCurrencyUI();
            }
            else
            {
                Debug.Log("CurrencyValue Object not found in scene");
            }
        }
    }

}