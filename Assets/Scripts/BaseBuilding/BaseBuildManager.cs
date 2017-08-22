using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildManager : MonoBehaviour {
    public Camera maincam;

    //Building GameObject
    private float HeightOffset;
    private GameObject tempbuildingGO;
    public GameObject WallGO;
    public GameObject StoneWallGO;
    public GameObject WoodFloorGO;
    public GameObject DoorGO;
    public GameObject TowerGO;
    public GameObject GUIBUILDWINDOW;
    public GameObject CraftingTableGO;
    private Material tempmat;
    private int rebuild = 1;

    //construction cost
    private int woodcost;
    private int stonecost;
    

    // BOOL VARIABLES
    public bool PlacingBuildingBool;

    // mouse variable
    private Quaternion buildingPlaceRotation;

    // building costs
    public int Building1WoodCost;
    public int building1StoneCost;

    //testing
    private Vector2 mousePos;
    private bool CollidingwithSomthing;

	// Use this for initialization
	void Start () {
        GUIBUILDWINDOW.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        //faster building with b key (builds last building you wanted
        rebuildfunction();
        // follow mouse when placeing building
        if (PlacingBuildingBool == true)
        {

            if (tempbuildingGO != null)
            {
                //snap to grid
                mousePos = maincam.ScreenToWorldPoint(Input.mousePosition);
                tempbuildingGO.transform.position = new Vector3(Mathf.Round(mousePos.x * 5) *0.2f, Mathf.Round(mousePos.y * 5) * 0.2f);
                
                //old movement
                //tempbuildingGO.transform.position = maincam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, HeightOffset);
                if (tempbuildingGO.GetComponent<Collider2D>() != null)
                tempbuildingGO.GetComponent<Collider2D>().isTrigger = true;


                //collision detection for placement
                
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                tempbuildingGO.transform.Rotate(Vector3.forward * 45);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                tempbuildingGO.transform.Rotate(Vector3.forward * -45);
            }

            //left click to place building and finish construction
            if (Input.GetMouseButtonDown(0))
            {
                if (tempbuildingGO.GetComponent<buildingInheritance>().CanBePlaced == true)
                {
                    if (tempbuildingGO.GetComponent<Collider2D>() != null)
                        tempbuildingGO.GetComponent<Collider2D>().isTrigger = false;
                    tempbuildingGO.GetComponent<buildingInheritance>().placed = true;
                    
                    PlacingBuildingBool = false;

                    tempbuildingGO.GetComponent<Renderer>().material.color = Color.white;
                }
                else
                {
                    Debug.Log("Cant Place This Here");
                }
            }

            //press escape to cancel construction 
            if (Input.GetMouseButton(1))
            {
                Destroy(tempbuildingGO);
                tempbuildingGO = null;
                PlacingBuildingBool = false;
                //refund resources using building cost variable
            }
        }
	}

    public void rebuildfunction()
    {
        if (PlacingBuildingBool == false)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                BuildBuilding(rebuild);
            }
        }
    }

    public void BuildBuilding(int BuildingChoice)
    {
        rebuild = BuildingChoice;
        switch (BuildingChoice)
        {   
            case 1:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources
                //set construction cost incease of escape

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(WallGO, this.transform.position, this.transform.rotation);
                HeightOffset = 10.4f;
                tempbuildingGO.GetComponent<Renderer>().material.color = Color.green;
                //else - //FALSE = display message

                break;
            case 2:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources
                //set construction cost incease of escape

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(StoneWallGO, this.transform.position, this.transform.rotation);
                HeightOffset = 10f;
                tempbuildingGO.GetComponent<Renderer>().material.color = Color.green;
                //else - //FALSE = display message
                break;
            case 3:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources
                //set construction cost incease of escape

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(WoodFloorGO, this.transform.position, this.transform.rotation);
                HeightOffset = 10.5f;
                tempbuildingGO.GetComponent<Renderer>().material.color = Color.green;
                //else - //FALSE = display message
                break;
            case 4:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources
                //set construction cost incease of escape

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(DoorGO, this.transform.position, this.transform.rotation);
                HeightOffset = 10.3f;
                tempbuildingGO.GetComponent<Renderer>().material.color = Color.green;
                //else - //FALSE = display message
                break;
            case 5:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources
                //set construction cost incease of escape

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(TowerGO, this.transform.position, this.transform.rotation);
                HeightOffset = 10f;
                tempbuildingGO.GetComponent<Renderer>().material.color = Color.green;
                //else - //FALSE = display message
                break;
            case 6:
                //check if requred resources are met
                //if (Building1WoodCost <= currentwood && building1StoneCost <= currentstone)

                //close menu
                GUIBUILDWINDOW.SetActive(false);
                //remove resources
                //set construction cost incease of escape

                // placebuilding variable is true
                PlacingBuildingBool = true;

                // instantiated building follows mouse untill clicked
                tempbuildingGO = Instantiate(CraftingTableGO, this.transform.position, this.transform.rotation);
                HeightOffset = 10f;
                tempbuildingGO.GetComponent<Renderer>().material.color = Color.green;
                //else - //FALSE = display message
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
