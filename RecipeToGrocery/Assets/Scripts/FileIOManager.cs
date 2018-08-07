using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// Stores a dictionary that matches lists of ingredients to recipe names.
/// </summary>
[System.Serializable]
public class UserData
{
    public IDictionary<string, Recipe> recipeBook = new Dictionary<string, Recipe>();
}

[System.Serializable]
public struct Recipe
{
    public string[] ingredients;
}

public class FileIOManager : MonoBehaviour {
    #region Variables
    const string USER_SAVE_FILE_NAME = "/userRecipeBook.dat";

    //Singleton pattern
    public static FileIOManager Instance;

    private UserData currentUserData;
    private IDictionary<string, Recipe> recipeBook;
    private Recipe newRecipe;
    #endregion

    #region Unity API Functions
    void Start () {
        //Singletone pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        //Load the user's recipe book
        LoadUserData();

        //Initialize other variables
        currentUserData = new UserData();
        recipeBook = new Dictionary<string, Recipe>();
        newRecipe = new Recipe();
    }
    #endregion

    #region Saving and Loading Functions
    /// <summary>
    /// Pulls the user's current recipe book from their data path, updates the variables in this class.
    /// </summary>
    public void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + USER_SAVE_FILE_NAME))
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + USER_SAVE_FILE_NAME, FileMode.Open);
            UserData userData = (UserData)formatter.Deserialize(fs);
            currentUserData = userData;
            recipeBook = currentUserData.recipeBook;
            fs.Close();
        }
    }

    /// <summary>
    /// Saves a new recipe to the user's data file.
    /// </summary>
    /// <param name="recipeName">Name of the recipe to be added.</param>
    /// <param name="ingredients">List of all the ingredients the recipe contains.</param>
    public void SaveRecipe(string recipeName, string[] ingredients)
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + USER_SAVE_FILE_NAME);
        newRecipe.ingredients = ingredients;
        recipeBook.Add(recipeName, newRecipe);
        currentUserData.recipeBook = recipeBook;
        formatter.Serialize(fs, currentUserData);
        fs.Close();
        Debug.Log("Save successful!");
    }
    #endregion
}
