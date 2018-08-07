using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInputFieldController : MonoBehaviour {
    
    #region Variables
    [SerializeField] Text ingredient;
    #endregion

    #region Helper Functions
    /// <summary>
    /// Fetches the ingredient entered into the field by the user.
    /// </summary>
    /// <returns>The ingredient indicated by the user.</returns>
    public string FetchIngredient()
    {
        return ingredient.text;
    }
    #endregion
}
