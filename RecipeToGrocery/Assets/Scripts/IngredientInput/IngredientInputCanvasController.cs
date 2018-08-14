using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInputCanvasController : MonoBehaviour {

    #region Variables
    //CONSTANTS
    const float INPUT_FIELD_SPACING = 30f;
    const float DELETE_BUTTON_SPACING = 30f;
    const int MAX_NUM_OF_INPUT_FIELDS = 10;
    

    //OBJECTS TO LINK IN EDITOR
    [SerializeField] InputField ingredientInputFieldPrefab;
    [SerializeField] InputField recipeNameInputField;
    [SerializeField] Button addIngredientButton;
    [SerializeField] Button deleteIngredientButton;

    //PRIVATE
    RectTransform ingredientButtonRectTransform;
    RectTransform deleteButtonRectTransform;
    Vector2 newIngredientButtonPosition;
    Vector2 newDeleteButtonPosition;
    Vector2 originalIngredientPosition;
    Vector2 originalDeletePosition;
    int currentNumInputFields;
    InputField[] currentIngredients;
    #endregion

    #region Unity API Functions
    void Awake()
    {
        currentNumInputFields = 0;
        currentIngredients = new InputField[MAX_NUM_OF_INPUT_FIELDS];
        GetInitalButtonInformation();
    }
    #endregion

    #region Button Press Functions
    /// <summary>
    /// Instantiates an input field at the initial location of the add ingredient button, then shifts the buttons down so long as the input limit has not been reached.
    /// Hides the input ingredient button if input field limit is reached.
    /// </summary>
    public void InputAnotherIngredient()
    {
        //Set delete button to interactable after first button press
        if (currentNumInputFields == 0)
        {
            deleteIngredientButton.interactable = true;
        }
        //Instantiate input field at the location of the of button
        InputField field;
        field = Instantiate(ingredientInputFieldPrefab, ingredientButtonRectTransform.anchoredPosition, Quaternion.identity);
        field.transform.SetParent(transform, false);
        //Save ingredient field game object for later reference
        currentIngredients[currentNumInputFields] = field;
        //Shift all buttons down
        ShiftIngredientButtonDown();
        ShiftDeleteButtonDown();
        currentNumInputFields++;

        //Check after index is increased
        if(currentNumInputFields == MAX_NUM_OF_INPUT_FIELDS)
        {
            addIngredientButton.gameObject.SetActive(false);
            deleteButtonRectTransform.anchoredPosition = ingredientButtonRectTransform.anchoredPosition;
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

    /// <summary>
    /// Deletes the most recently created input field.
    /// Also sets delete button to not interactable if there will be no more input fields to delete.
    /// </summary>
    public void DeleteIngredient()
    {
        //If there will be no more field to delete, do not let the user be able to press the delete button
        if (currentNumInputFields == 1)
        {
            deleteIngredientButton.interactable = false;
        }
        //Check to see if the ingredient button needs to reappear
        if (currentNumInputFields == MAX_NUM_OF_INPUT_FIELDS)
        {
            addIngredientButton.gameObject.SetActive(true);
            ShiftIngredientButtonUp();
        } else
        {
            ShiftIngredientButtonUp();
            ShiftDeleteButtonUp();
        }
        Destroy(currentIngredients[currentNumInputFields - 1].gameObject);
        currentIngredients[currentNumInputFields - 1] = null;
        currentNumInputFields--;
    }
    #endregion

    #region Helper Functions
    /// <summary>
    /// Gets the rect transform of the "add ingredient" button then stores its original anchored position.
    /// </summary>
    private void GetInitalButtonInformation()
    {
        ingredientButtonRectTransform = addIngredientButton.GetComponent<RectTransform>();
        deleteButtonRectTransform = deleteIngredientButton.GetComponent<RectTransform>();
        originalIngredientPosition = ingredientButtonRectTransform.anchoredPosition;
        originalDeletePosition = deleteButtonRectTransform.anchoredPosition;
        
    }

    /// <summary>
    /// Shift the ingredent button down according to the pre-defined constant.
    /// </summary>
    private void ShiftIngredientButtonDown()
    {
        float newXPos = ingredientButtonRectTransform.anchoredPosition.x;
        float newYPos = ingredientButtonRectTransform.anchoredPosition.y - INPUT_FIELD_SPACING;
        newIngredientButtonPosition = new Vector2(newXPos, newYPos);
        ingredientButtonRectTransform.anchoredPosition = newIngredientButtonPosition;
    }

    /// <summary>
    /// Shift the ingredent button up according to the pre-defined constant.
    /// </summary>
    private void ShiftIngredientButtonUp()
    {
        float newXPos = ingredientButtonRectTransform.anchoredPosition.x;
        float newYPos = ingredientButtonRectTransform.anchoredPosition.y + INPUT_FIELD_SPACING;
        newIngredientButtonPosition = new Vector2(newXPos, newYPos);
        ingredientButtonRectTransform.anchoredPosition = newIngredientButtonPosition;
    }

    /// <summary>
    /// Shift the delete button down according to the pre-defined constant.
    /// </summary>
    private void ShiftDeleteButtonDown()
    {
        float newXPos = deleteButtonRectTransform.anchoredPosition.x;
        float newYPos = deleteButtonRectTransform.anchoredPosition.y - DELETE_BUTTON_SPACING;
        newDeleteButtonPosition = new Vector2(newXPos, newYPos);
        deleteButtonRectTransform.anchoredPosition = newDeleteButtonPosition;
    }

    /// <summary>
    /// Shift the delete button up according to the pre-defined constant.
    /// </summary>
    private void ShiftDeleteButtonUp()
    {
        float newXPos = deleteButtonRectTransform.anchoredPosition.x;
        float newYPos = deleteButtonRectTransform.anchoredPosition.y + DELETE_BUTTON_SPACING;
        newDeleteButtonPosition = new Vector2(newXPos, newYPos);
        deleteButtonRectTransform.anchoredPosition = newDeleteButtonPosition;
    }

    /// <summary>
    /// Resets the field number counter, resets the add ingredient and delete buttons back to their original position/states.
    /// Also resets the recipe name input field.
    /// </summary>
    private void ResetInputScreen()
    {
        //delete all ingredient fields
        for (int i = 0; i < currentNumInputFields; i++)
        {
            Destroy(currentIngredients[i].gameObject);
            currentIngredients[i] = null;
        }
        //Reset button positions/states
        addIngredientButton.gameObject.SetActive(true);
        deleteIngredientButton.interactable = false;
        ingredientButtonRectTransform.anchoredPosition = originalIngredientPosition;
        deleteButtonRectTransform.anchoredPosition = originalDeletePosition;
        recipeNameInputField.text = "";
        currentNumInputFields = 0;
    }

    /// <summary>
    /// Saves information located in the input fields to an array.
    /// </summary>
    /// <returns>The ingredients as an array of strings.</returns>
    private string[] FetchIngredientInput()
    {
        //Rip ingredients from all input fields, then delete the input fields themselves
        string[] ingredientData = new string[currentNumInputFields];
        for (int i = 0; i < currentNumInputFields; i++)
        {
            if (currentIngredients[i] != null)
            {
                ingredientData[i] = currentIngredients[i].GetComponent<IngredientInputFieldController>().FetchIngredient();
            }
        }
        return ingredientData;
    }
    #endregion
}
