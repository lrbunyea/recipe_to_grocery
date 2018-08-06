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
}
