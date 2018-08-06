using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static UIManager Instance;

    public float buttonDown;
    public GameObject ingredientInputField;
    public GameObject canvas;
    #endregion

    #region Unity Functions
    void Awake()
    {
        //Singletone pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start () {
		
	}
	
	
	void Update () {
		
	}
    #endregion

    #region Button Functions
    //Function that spawns another ingredient input field
    public void InputAnotherIngredient()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y - buttonDown;
        GameObject field;

        Debug.Log("Button transform: " + transform.position);
        Debug.Log("Button local transform: " + transform.localPosition);

        //field = Instantiate(ingredientInputField, transform.localPosition, Quaternion.identity);
        //field.transform.SetParent(canvas.transform, false);
        //field.transform.localPosition = transform.localPosition;
        
    }
    #endregion
}
