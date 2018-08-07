using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeNameInputFieldController : MonoBehaviour {

    #region Variables
    [SerializeField] Text recipe;
    #endregion

    #region Helper Functions
    /// <summary>
    /// Fetches the recipe name entered into the field by the user.
    /// </summary>
    /// <returns>The recipe indicated by the user.</returns>
    public string FetchRecipeName()
    {
        return recipe.text;
    }
    #endregion
}
