using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInputCanvasController : MonoBehaviour {

    #region Variables
    //CONSTANTS
    const float INPUT_FIELD_SPACING = 30f;
    const int MAX_NUM_OF_INPUT_FIELDS = 10;

    //OBJECTS TO LINK IN EDITOR
    [SerializeField] InputField ingredientInputField;
    [SerializeField] InputField recipeNameInputField;
    [SerializeField] Button addIngredientButton;

    //PRIVATE
    private RectTransform buttonRectTransform;
    Vector2 newPosition;
    Vector2 originalPosition;
    private int currentNumInputFields = 0;
    #endregion

    #region Unity API Functions
    void Awake()
    {
        GetInitalButtonInformation();
    }
    #endregion

    #region Button Press Functions
    /// <summary>
    /// Instantiates an input field at the initial location of the add ingredient button, then shifts the button down so long as the input limit has not been reached.
    /// Hides the input ingredient button if input field limit is reached.
    /// </summary>
    public void InputAnotherIngredient()
    {
        UpdateButtonInfo();
        //Instantiate input field at the location of the of button
        InputField field;
        field = Instantiate(ingredientInputField, buttonRectTransform.anchoredPosition, Quaternion.identity);
        field.transform.SetParent(transform, false);
        //Shift button down
        buttonRectTransform.anchoredPosition = newPosition;
        currentNumInputFields++;

        //Check after index is increased
        if(currentNumInputFields == MAX_NUM_OF_INPUT_FIELDS)
        {
            addIngredientButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Fetches the recipe name and ingredients from the input fields then writes them to the file.
    /// After this, it refreshes the ingredient input screen.
    /// </summary>
    public void SaveIngredients()
    {
        string recipe = recipeNameInputField.text;
        FileIOManager.Instance.SaveRecipe(recipe, FetchIngredientInput());
        ResetInputScreen();
    }
    #endregion

    #region Helper Functions
    /// <summary>
    /// Gets the rect transform of the "add ingredient" button then stores its original anchored position.
    /// </summary>
    private void GetInitalButtonInformation()
    {
        buttonRectTransform = addIngredientButton.GetComponent<RectTransform>();
        originalPosition = buttonRectTransform.anchoredPosition;
    }

    /// <summary>
    /// Calculates, based on the ingredients current position, the new position to shift the button to.
    /// </summary>
    private void UpdateButtonInfo()
    {
        float newXPos = buttonRectTransform.anchoredPosition.x;
        float newYPos = buttonRectTransform.anchoredPosition.y - INPUT_FIELD_SPACING;
        newPosition = new Vector2(newXPos, newYPos);
    }

    /// <summary>
    /// Resets the field number counter, resets the add ingredient back to its original position and activates it.
    /// Also resets the recipe name input field.
    /// </summary>
    private void ResetInputScreen()
    {
        currentNumInputFields = 0;
        buttonRectTransform.anchoredPosition = originalPosition;
        addIngredientButton.gameObject.SetActive(true);
        recipeNameInputField.text = "";
    }

    /// <summary>
    /// Finds all of the input fields on the ingredient input screen.
    /// Deletes the actual gameobjects as it saves their data to an array, then DESTROYS the associated input field.
    /// </summary>
    /// <returns>The ingredients as an array of strings.</returns>
    private string[] FetchIngredientInput()
    {
        //Fetch all input fields
        IngredientInputFieldController[] data = FindObjectsOfType<IngredientInputFieldController>();

        //Rip ingredients from all input fields, then delete the input fields themselves
        string[] ingredientData = new string[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            ingredientData[i] = data[i].FetchIngredient();
            Destroy(data[i].gameObject);
        }
        return ingredientData;
    }
    #endregion
}
