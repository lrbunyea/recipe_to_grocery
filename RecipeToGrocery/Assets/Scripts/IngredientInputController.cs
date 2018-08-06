using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInputController : MonoBehaviour {

    #region Variables
    //CONSTANTS
    const float INPUT_FIELD_SPACING = 30f;
    const int MAX_NUM_OF_INPUT_FIELDS = 10;

    //OBJECTS TO LINK IN EDITOR
    [SerializeField] InputField ingredientInputField;
    [SerializeField] Button addIngredientButton;

    //PRIVATE
    private RectTransform buttonRectTransform;
    private float xPos;
    private float yPos;
    private int currentNumInputFields = 0;
    #endregion

    #region Button Press Functions
    /// <summary>
    /// Instantiates an input field at the initial location of the add ingredient button, then shifts the button down so long as the input limit has not been reached.
    /// Hides the input ingredient button if input field limit is reached.
    /// </summary>
    public void InputAnotherIngredient()
    {
        GetButtonInformation();
        //Instantiate input field at the location of the of button
        InputField field;
        field = Instantiate(ingredientInputField, buttonRectTransform.anchoredPosition, Quaternion.identity);
        field.transform.SetParent(transform, false);
        //Shift button down
        buttonRectTransform.anchoredPosition = new Vector2(xPos, yPos);
        currentNumInputFields++;

        //Check after index is increased
        if(currentNumInputFields == MAX_NUM_OF_INPUT_FIELDS)
        {
            addIngredientButton.gameObject.SetActive(false);
        }
    }

    public void SaveIngredients()
    {
        //This will find all the ingredient fields, iterate through them, and save them to a new recipe class which will be saved in a file.
    }
    #endregion

    #region Helper Functions
    /// <summary>
    /// Gets the rect transform of the "add ingredient" button and then calculates the x and y values to move the button to.
    /// </summary>
    private void GetButtonInformation()
    {
        //To use in instantiating input fields
        buttonRectTransform = addIngredientButton.GetComponent<RectTransform>();
        //New x and y values to shift the button to
        xPos = buttonRectTransform.anchoredPosition.x;
        yPos = buttonRectTransform.anchoredPosition.y - INPUT_FIELD_SPACING;
    }
    #endregion
}
