using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using OpenCover.Framework.Model;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using Random = UnityEngine.Random;
using File = System.IO.File;
public class GameManager : MonoBehaviour
{

    //////////////////////////////////////////////////////////
    [System.Serializable]
    public class PersistantData
    {
        [SerializeField] public int currency = 0;
        [SerializeField] public string test = "";
    }


    [SerializeField] public PersistantData SaveableData = new();
    String DefaultSaveName = "SavableData";
    //////////////////////////////////////////////////////////


    
    public static GameManager Instance;


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
        SaveableData = DeserializeFromSaveFile<PersistantData>("SavableData");

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


            ///////////////////////////////////////////
            SceneManager.sceneLoaded += OnSceneUnloaded;
            ///////////////////////////////////////////
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
        SaveableData.currency += amount;
        //////////////////////////
        UpdateCurrencyUI();
        //////////////////////////
    }

    public void SubtractCurrency(int amount)
    {
        if (SaveableData.currency - amount <= 0) SaveableData.currency = 0;
        else SaveableData.currency -= amount;
        //////////////////////////
        UpdateCurrencyUI();
        //////////////////////////
    }

    public void UpdateCurrencyUI()
    {
        currencyValue.text = SaveableData.currency.ToString();
    }

    ////////////////////////////////////////////////////

    //Create a wrapper for the serialize function that uses a default path
    private static void SerializeToDefaultFile<T>(T DataToSave)
    {
        SerializeToSaveFile("SavableData", DataToSave);
    }

    //Static function which saves Data type T to Filename
    public static void SerializeToSaveFile<T>(string Filename , T DataToSerialize)
    {
        string jsonOutput = JsonUtility.ToJson(DataToSerialize);
        string FullFilePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + Filename + ".json";
        File.WriteAllText(FullFilePath, jsonOutput);
    }

    //Static function which Loads Data type T from Filename and returns it
    public static T DeserializeFromSaveFile<T>(string Filename)
    {
        string FullFilePath = Application.persistentDataPath +"/"+ Filename + ".json";
        T DataToReturn = default;
        //If The File Exists Read It
        if (File.Exists(FullFilePath))
        {
            string FileOutput = File.ReadAllText(FullFilePath);
            DataToReturn = JsonUtility.FromJson<T>(FileOutput);
        }

        return DataToReturn;
    }

    public void OnDestroy()
    {
        SerializeToDefaultFile(SaveableData);
    }


    void OnSceneUnloaded(Scene scene, LoadSceneMode mode)
    {
        
    }
    ////////////////////////////////////////////////////

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