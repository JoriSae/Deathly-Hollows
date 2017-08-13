using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildManager : MonoBehaviour {
    public Camera maincam;

    //Building GameObject
    private GameObject tempbuildingGO;
    public GameObject WallGO;
    public GameObject GUIBUILDWINDOW;
    

    // BOOL VARIABLES
    public bool PlacingBuildingBool;

    // mouse variable
    private Quaternion buildingPlaceRotation;

    // building costs
    public int Building1WoodCost;
    public int building1StoneCost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // follow mouse when placeing building
        if (PlacingBuildingBool == true)
        {
            
            if (tempbuildingGO != null)
            {
                tempbuildingGO.transform.position = maincam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0, 9.5f);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                tempbuildingGO.transform.Rotate(Vector3.forward * 5);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                tempbuildingGO.transform.Rotate(Vector3.forward * -5);
            }

            //left click to place building and finish construction
            if (Input.GetMouseButtonDown(0))
            {
                PlacingBuildingBool = false;
            }

            //press escape to cancel construction 
        }
	}
    

    public void BuildBuilding(int BuildingChoice)
    {
        switch (BuildingChoice)
        {
            case 1:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(WallGO, this.transform.position, this.transform.rotation);
                

                //else - //FALSE = display message

                break;
            case 2:

                break;
        }

        
        //when clicked instantiate building on mouse click
        //place building variable is false
    }

    //open close menu
    public void openclosemenu()
    {
        if (GUIBUILDWINDOW.active == true)
        {
            GUIBUILDWINDOW.SetActive(false);
        }
        else
        {
            GUIBUILDWINDOW.SetActive(true);
        }
    }
}
