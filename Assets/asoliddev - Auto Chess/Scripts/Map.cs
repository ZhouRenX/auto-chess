﻿using UnityEngine;
/// <summary>
/// Creates map grids where the player can move champions on
/// </summary>
public class Map : CreateSingleton<Map>
{
    //declare grid types
    public static int GRIDTYPE_OWN_INVENTORY = 0;
    public static int GRIDTYPE_OPONENT_INVENTORY = 1;
    public static int GRIDTYPE_HEXA_MAP = 2;

    public static int hexMapSizeX = 7;
    public static int hexMapSizeZ = 8;
    public static int inventorySize = 9;


    //地图格子坐标
    [HideInInspector]
    public Vector3[] ownInventoryGridPositions;
    [HideInInspector]
    public Vector3[] oponentInventoryGridPositions;
    [HideInInspector]
    public Vector3[,] mapGridPositions;

    //地图格子显示物体
    [HideInInspector]
    public GameObject[] ownIndicatorArray;
    [HideInInspector]
    public GameObject[] oponentIndicatorArray;
    [HideInInspector]
    public GameObject[,] mapIndicatorArray;

    //地图格子触发器
    [HideInInspector]
    public TriggerInfo[] ownTriggerArray;
    [HideInInspector]
    public TriggerInfo[] oponentTriggerArray;
    [HideInInspector]
    public TriggerInfo[,] mapGridTriggerArray;

    public GameObject ownIndicatorContainer;
    public GameObject ownMapContainer;
    public GameObject oponentIndicatorContainer;
    public GameObject oponentMapContainer;


    public Plane m_Plane;

    //start positions
    public Transform ownInventoryStartPosition;
    public Transform oponentInventoryStartPosition;
    public Transform mapStartPosition;


    //indicators that show where we place champions
    public GameObject squareIndicator;
    public GameObject hexaIndicator;


    public Color indicatorDefaultColor;
    public Color indicatorActiveColor;

    protected override void InitSingleton()
    {

    }

    private void Start()
    {
        CreateGridPosition();
        CreateIndicators();
        HideIndicators();

        m_Plane = new Plane(Vector3.up, Vector3.zero);

        GamePlayController.Instance.OnMapReady();
    }

    /// Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// 初始化坐标
    /// </summary>
    private void CreateGridPosition()
    {
        //initialize position arrays
        ownInventoryGridPositions = new Vector3[inventorySize];
        oponentInventoryGridPositions = new Vector3[inventorySize];
        mapGridPositions = new Vector3[hexMapSizeX, hexMapSizeZ];


        //create own inventory position
        for (int i = 0; i < inventorySize; i++)
        {
            //calculate position x offset for this slot
            float offsetX = i * -2.5f;

            //calculate and store the position
            Vector3 position = GetMapHitPoint(ownInventoryStartPosition.position + new Vector3(offsetX, 0, 0));

            //add position variable to array
            ownInventoryGridPositions[i] = position;
        }

        //create oponent inventory  position
        for (int i = 0; i < inventorySize; i++)
        {
            //calculate position x offset for this slot
            float offsetX = i * -2.5f;

            //calculate and store the position
            Vector3 position = GetMapHitPoint(oponentInventoryStartPosition.position + new Vector3(offsetX, 0, 0));

            //add position variable to array
            oponentInventoryGridPositions[i] = position;
        }

        //create map position
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                //calculate even or add row
                int rowOffset = z % 2;

                //calculate position x and z
                float offsetX = x * -3f + rowOffset * 1.5f;
                float offsetZ = z * -2.5f;

                //calculate and store the position
                Vector3 position = GetMapHitPoint(mapStartPosition.position + new Vector3(offsetX, 0, offsetZ));

                //add position variable to array
                mapGridPositions[x, z] = position;
            }

        }

    }



    /// <summary>
    /// 初始化显示物体
    /// </summary>
    private void CreateIndicators()
    {
        //create a container for triggers
        GameObject ownTriggerContainer = new GameObject();
        ownTriggerContainer.name = "OwnTriggerContainer";

        GameObject oponentTriggerContainer = new GameObject();
        oponentTriggerContainer.name = "OponentTriggerContainer";


        //initialise arrays to store indicators
        ownIndicatorArray = new GameObject[inventorySize];
        oponentIndicatorArray = new GameObject[inventorySize];
        mapIndicatorArray = new GameObject[hexMapSizeX, hexMapSizeZ];

        ownTriggerArray = new TriggerInfo[inventorySize];
        oponentTriggerArray = new TriggerInfo[inventorySize];
        mapGridTriggerArray = new TriggerInfo[hexMapSizeX, hexMapSizeZ];


        //iterate own grid position
        for (int i = 0; i < inventorySize; i++)
        {
            //create indicator gameobject
            GameObject indicatorGO = Instantiate(squareIndicator);

            //set indicator gameobject position
            indicatorGO.transform.position = ownInventoryGridPositions[i];

            //set indicator parent
            indicatorGO.transform.parent = ownIndicatorContainer.transform;

            //store indicator gameobject in array
            ownIndicatorArray[i] = indicatorGO;

            //create trigger gameobject
            GameObject trigger = CreateBoxTrigger(GRIDTYPE_OWN_INVENTORY, i);

            //set trigger parent
            trigger.transform.parent = ownTriggerContainer.transform;

            //set trigger gameobject position
            trigger.transform.position = ownInventoryGridPositions[i];

            //store triggerinfo
            ownTriggerArray[i] = trigger.GetComponent<TriggerInfo>();
        }


        //iterate oponent grid position
        for (int i = 0; i < inventorySize; i++)
        {
            //create indicator gameobject
            GameObject indicatorGO = Instantiate(squareIndicator);

            //set indicator gameobject position
            indicatorGO.transform.position = oponentInventoryGridPositions[i];

            //set indicator parent
            indicatorGO.transform.parent = oponentIndicatorContainer.transform;

            //store indicator gameobject in array
            oponentIndicatorArray[i] = indicatorGO;

            //create trigger gameobject
            GameObject trigger = CreateBoxTrigger(GRIDTYPE_OPONENT_INVENTORY, i);

            //set trigger parent
            trigger.transform.parent = oponentTriggerContainer.transform;

            //set trigger gameobject position
            trigger.transform.position = oponentInventoryGridPositions[i];

            //store triggerinfo
            oponentTriggerArray[i] = trigger.GetComponent<TriggerInfo>();


        }

        //iterate map grid position
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                //create indicator gameobject
                GameObject indicatorGO = Instantiate(hexaIndicator);

                //set indicator gameobject position
                indicatorGO.transform.position = mapGridPositions[x, z];

                if (z < hexMapSizeZ / 2)
                    indicatorGO.transform.parent = ownMapContainer.transform;
                else
                    indicatorGO.transform.parent = oponentMapContainer.transform;

                //store indicator gameobject in array
                mapIndicatorArray[x, z] = indicatorGO;

                //create trigger gameobject
                GameObject trigger = CreateSphereTrigger(GRIDTYPE_HEXA_MAP, x, z);
                if (z < hexMapSizeZ / 2)
                    trigger.transform.parent = ownTriggerContainer.transform;
                else
                    trigger.transform.parent = oponentTriggerContainer.transform;

                //set trigger gameobject position
                trigger.transform.position = mapGridPositions[x, z];

                //store triggerinfo
                mapGridTriggerArray[x, z] = trigger.GetComponent<TriggerInfo>();

            }
        }

    }

    /// <summary>
    /// Get a point with accurate y axis
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMapHitPoint(Vector3 p)
    {
        Vector3 newPos = p;

        RaycastHit hit;

        if (Physics.Raycast(newPos + new Vector3(0, 10, 0), Vector3.down, out hit, 15))
        {
            newPos = hit.point;
        }

        return newPos;
    }

    /// <summary>
    /// Creates a trigger collider gameobject and returns it
    /// </summary>
    /// <returns></returns>
    private GameObject CreateBoxTrigger(int type, int x)
    {
        //create primitive gameobject
        GameObject trigger = new GameObject();

        //add collider component
        BoxCollider collider = trigger.AddComponent<BoxCollider>();

        //set collider size
        collider.size = new Vector3(2, 0.5f, 2);

        //set collider to trigger 
        collider.isTrigger = true;

        //add and store trigger info
        TriggerInfo trigerInfo = trigger.AddComponent<TriggerInfo>();
        trigerInfo.gridType = type;
        trigerInfo.gridX = x;

        trigger.layer = LayerMask.NameToLayer("Triggers");

        return trigger;
    }

    /// <summary>
    /// Creates a trigger collider gameobject and returns it
    /// </summary>
    /// <returns></returns>
    private GameObject CreateSphereTrigger(int type, int x, int z)
    {
        //create primitive gameobject
        GameObject trigger = new GameObject();

        //add collider component
        SphereCollider collider = trigger.AddComponent<SphereCollider>();

        //set collider size
        collider.radius = 1.4f;

        //set collider to trigger 
        collider.isTrigger = true;

        //add and store trigger info
        TriggerInfo trigerInfo = trigger.AddComponent<TriggerInfo>();
        trigerInfo.gridType = type;
        trigerInfo.gridX = x;
        trigerInfo.gridZ = z;

        trigger.layer = LayerMask.NameToLayer("Triggers");

        return trigger;
    }


    /// <summary>
    /// Returns grid indicator from triggerinfo
    /// </summary>
    /// <param name="triggerinfo"></param>
    /// <returns></returns>
    public GameObject GetIndicatorFromTriggerInfo(TriggerInfo triggerinfo)
    {
        GameObject triggerGo = null;

        if (triggerinfo.gridType == GRIDTYPE_OWN_INVENTORY)
        {
            triggerGo = ownIndicatorArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == GRIDTYPE_OPONENT_INVENTORY)
        {
            triggerGo = oponentIndicatorArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == GRIDTYPE_HEXA_MAP)
        {
            triggerGo = mapIndicatorArray[triggerinfo.gridX, triggerinfo.gridZ];
        }


        return triggerGo;
    }

    /// <summary>
    /// Resets all indicator colors to default
    /// </summary>
    public void resetIndicators()
    {
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ / 2; z++)
            {
                mapIndicatorArray[x, z].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
            }
        }


        for (int x = 0; x < 9; x++)
        {
            ownIndicatorArray[x].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
            // oponentIndicatorArray[x].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
        }

    }

    /// <summary>
    /// Make all map indicators visible
    /// </summary>
    public void ShowIndicators(ChampionTeam teamType)
    {
        if (teamType == ChampionTeam.Player)
        {
            ownIndicatorContainer.SetActive(true);
            ownMapContainer.SetActive(true);
        }
        else if (teamType == ChampionTeam.Oponent)
        {
            oponentIndicatorContainer.SetActive(true);
            oponentMapContainer.SetActive(true);
        }

    }

    /// <summary>
    /// Make all map indicators invisible
    /// </summary>
    public void HideIndicators()
    {
        ownIndicatorContainer.SetActive(false);
        ownMapContainer.SetActive(false);
        oponentIndicatorContainer.SetActive(false);
        oponentMapContainer.SetActive(false);
    }
}















/*! \mainpage Auto Chess documentation
 * 
* <b>Thank you for purchasing Auto Chess.</b><br>
* <br>For any question don't hesitate to contact me at : asoliddev@gmail.com
* <br>AssetStore Profile : https://assetstore.unity.com/publishers/38620 
* <br>ArtStation Profile : https://asoliddev.artstation.com/
* 
*  \subsection Basics
* Auto Chess complete and fully functional game, <br>
* it has been created with simplicity in mind. <br>
* Great as a starting point to create your own Auto Chess game. <br>
* All the scripts are attached to the Script Gameobject In the Hierarchy window. <br>
* Basic game rules and Champions can be changed by adjusting public variables. <br>
* Core game changes can be done by changing the source code. <br>
* Source code uses MVC design and can be easly expanded on.
*  
* 
*  \subsection Champions
* Existing Champions and ChampionTypes are located in the <b>Champions</b> and <b>ChampionTypes</b> folder. <br>
* To Create new Champion or ChampionType go to Assets Menu -> Create -> Auto Chess -> Champion or ChampionType. <br>
* Champions can be customised with this two classes from the editor. <br>
* For detailed options check out : Champion and ChampionType documentation. <br>
* All Champions and ChampionTypes need to be assigned to the GameData script to be recognised by the ChampionShop.
* 
* 
* \subsection Packages
* Auto chess uses Post Processing package for better visuals. <br>
* To install it go to Window Menu -> Package Manager and install Post Processing
* 
*/
