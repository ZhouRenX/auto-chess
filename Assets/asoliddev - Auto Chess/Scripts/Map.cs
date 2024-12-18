﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// Creates map grids where the player can move champions on
/// </summary>

public enum GridType { Inventory, HexaMap }
public class Map : CreateSingleton<Map>
{
    public static int hexMapSizeX = 7;
    public static int hexMapSizeZ = 8;
    public static int inventorySize = 28;

    public static float hexEdgeLength = 2.4f;

    //地图格子坐标
    [HideInInspector]
    public Vector3[] ownInventoryGridPositions;
    [HideInInspector]
    public Vector3[] oponentInventoryGridPositions;
    [HideInInspector]
    public Vector3[,] mapGridPositions;

    [HideInInspector]
    public GridInfo[] ownInventoryGridArray;
    [HideInInspector]
    public GridInfo[] oponentInventoryGridArray;
    [HideInInspector]
    public GridInfo[,] mapGridArray;

    public GameObject ownInventoryContainer;
    public GameObject ownMapContainer;
    public GameObject oponentInventoryContainer;
    public GameObject oponentMapContainer;


    public Plane m_Plane;

    //start positions
    public Transform ownInventoryStartPosition;
    public Transform oponentInventoryStartPosition;
    public Transform mapStartPosition;


    //indicators that show where we place champions
    public GameObject squareGrid;
    public GameObject hexaGrid;


    public Color indicatorDefaultColor;
    public Color indicatorActiveColor;
    public Color indicatorDisactiveColor;

    protected override void InitSingleton()
    {

    }

    private void Start()
    {
        CreateGridPosition();
        CreateIndicators();
        //HideIndicators();

        m_Plane = new Plane(Vector3.up, Vector3.zero);

        Invoke("Ready", 1f);
        //GamePlayController.Instance.OnMapReady();

    }

    void Ready()
    {
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
            float offsetX = (i % 7) * -4f;
            float offsetY = Mathf.Floor(i / 7) * 4f;
            //calculate and store the position
            //Vector3 position = GetMapHitPoint(ownInventoryStartPosition.position + new Vector3(offsetX, 0, offsetY));

            //add position variable to array
            ownInventoryGridPositions[i] = ownInventoryStartPosition.position + new Vector3(offsetX, 0, offsetY);
        }

        //create oponent inventory  position
        for (int i = 0; i < inventorySize; i++)
        {
            //calculate position x offset for this slot
            float offsetX = (i % 7) * -4f;
            float offsetY = Mathf.Floor(i / 7) * -4f;

            //calculate and store the position
            //Vector3 position = GetMapHitPoint(oponentInventoryStartPosition.position + new Vector3(offsetX, 0, offsetY));

            //add position variable to array
            oponentInventoryGridPositions[i] = oponentInventoryStartPosition.position + new Vector3(offsetX, 0, offsetY);
        }

        //create map position
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                //calculate even or add row
                int rowOffset = z % 2;

                float weightX = hexEdgeLength * Mathf.Sqrt(3);
                float heightZ = hexEdgeLength * 1.5f;
                //calculate position x and z
                float offsetX = x * -weightX + rowOffset * -weightX / 2;
                float offsetZ = z * -heightZ;

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
        ownInventoryGridArray = new GridInfo[inventorySize];
        oponentInventoryGridArray = new GridInfo[inventorySize];
        mapGridArray = new GridInfo[hexMapSizeX, hexMapSizeZ];


        //生成玩家仓库网格
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject gridOBJ = Instantiate(squareGrid);
            gridOBJ.transform.position = ownInventoryGridPositions[i];
            gridOBJ.transform.parent = ownInventoryContainer.transform;

            ownInventoryGridArray[i] = gridOBJ.GetComponent<GridInfo>();
            ownInventoryGridArray[i].Init(new GridIndex(i, -1), new Vector3(i, -1, -1), GridType.Inventory);
        }


        //生成敌人仓库网格
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject gridOBJ = Instantiate(squareGrid);
            gridOBJ.transform.position = oponentInventoryGridPositions[i];
            gridOBJ.transform.parent = oponentInventoryContainer.transform;

            oponentInventoryGridArray[i] = gridOBJ.GetComponent<GridInfo>();
            oponentInventoryGridArray[i].Init(new GridIndex(i, -1), new Vector3(i, -1, -1), GridType.Inventory);
        }

        //生成地图网格
        for (int z = 0; z < hexMapSizeZ; z++)
        {
            for (int x = 0; x < hexMapSizeX; x++)
            {
                //create indicator gameobject
                GameObject gridOBJ = Instantiate(hexaGrid);
                gridOBJ.transform.position = mapGridPositions[x, z];

                if (z < hexMapSizeZ / 2)
                    gridOBJ.transform.parent = ownMapContainer.transform;
                else
                    gridOBJ.transform.parent = oponentMapContainer.transform;

                //store indicator gameobject in array
                mapGridArray[x, z] = gridOBJ.GetComponent<GridInfo>();


                int xOffset = z >> 1;
                mapGridArray[x, z].Init(
                    new GridIndex(x, z),
                    new Vector3(x - xOffset, z, 0 - (x - xOffset + z)),
                    GridType.HexaMap);
            }
        }
        foreach (var t in mapGridArray)
        {
            t.CacheNeighbors();
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
    /// Resets all indicator colors to default
    /// </summary>
    public void resetIndicators()
    {
        foreach (var grid in mapGridArray)
            grid.SetColor(indicatorDefaultColor);
        foreach (var grid in ownInventoryGridArray)
            grid.SetColor(indicatorDefaultColor);
        foreach (var grid in oponentInventoryGridArray)
            grid.SetColor(indicatorDefaultColor);
    }

    /// <summary>
    /// Make all map indicators visible
    /// </summary>
    public void ShowIndicators(ChampionTeam teamType)
    {
        if (teamType == ChampionTeam.Player)
        {
            ownInventoryContainer.SetActive(true);
            ownMapContainer.SetActive(true);
        }
        else if (teamType == ChampionTeam.Oponent)
        {
            oponentInventoryContainer.SetActive(true);
            oponentMapContainer.SetActive(true);
        }
    }

    /// <summary>
    /// Make all map indicators invisible
    /// </summary>
    public void HideIndicators()
    {
        ownInventoryContainer.SetActive(false);
        ownMapContainer.SetActive(false);
        oponentInventoryContainer.SetActive(false);
        oponentMapContainer.SetActive(false);
    }

    //获取某个区域的所有Grid
    public List<GridInfo> GetGridArea(GridInfo center, int range)
    {
        List<GridInfo> grids = new List<GridInfo>();
        foreach (GridInfo g in mapGridArray)
        {
            if (center.GetDistance(g) <= range)
            {
                grids.Add(g);
            }
        }
        return grids;
    }

    public GridInfo GetHexaMapSlot(Vector3 _coor)
    {
        foreach (var grid in mapGridArray)
        {
            if (grid.coor == _coor)
                return grid;
        }
        return null;
    }

    public GridInfo GetEmptySlot(ChampionTeam team, GridType type)
    {
        if (type == GridType.HexaMap)
        {
            foreach (var grid in mapGridArray)
            {
                if (grid.occupyChampion == null && grid.bookChampion == null)
                {
                    if (team == ChampionTeam.Player)
                    {
                        if (grid.index.y < hexMapSizeZ / 2)
                            return grid;
                    }
                    else if (team == ChampionTeam.Oponent)
                    {
                        if (grid.index.y >= hexMapSizeZ / 2)
                            return grid;
                    }

                }
            }
            return null;
        }
        else if (team == ChampionTeam.Player)
        {
            foreach (var grid in ownInventoryGridArray)
            {
                if (grid.occupyChampion == null && grid.bookChampion == null)
                {
                    return grid;
                }
            }
            return null;
        }
        else if (team == ChampionTeam.Oponent)
        {
            foreach (var grid in oponentInventoryGridArray)
            {
                if (grid.occupyChampion == null && grid.bookChampion == null)
                {
                    return grid;
                }
            }
            return null;
        }
        return null;
    }

    public GridInfo GetEmptySlot(GridInfo grid, int range)
    {
        foreach (var g in mapGridArray)
        {
            if (g.occupyChampion == null && g.bookChampion == null)
            {
                if (grid.GetDistance(g) <= range)
                    return g;
            }
        }
        return null;
    }

    public List<GridInfo> FindPath(GridInfo startNode, GridInfo targetNode, ChampionController champion)
    {
        var toSearch = new List<GridInfo>() { startNode };
        var processed = new List<GridInfo>();

        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
            {
                if (t.f < current.f || t.f == current.f && t.h < current.h)
                {
                    current = t;
                }
            }
            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetNode)
            {
                GridInfo currentPathTile = targetNode;
                var path = new List<GridInfo>();
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.connection;
                }
                path.Reverse();
                return path;
            }

            foreach (var neighbor in current.neighbors.
            Where(t => t == targetNode ||
            (t.walkable && !processed.Contains(t) && !t.IsBookedOrOccupied(champion))))
            {
                var inSearch = toSearch.Contains(neighbor);
                var costToNeighbor = current.g + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.g)
                {
                    neighbor.g = costToNeighbor;
                    neighbor.connection = current;

                    if (!inSearch)
                    {
                        neighbor.h = neighbor.GetDistance(targetNode);
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    public void ResetAllGridInfo()
    {
        foreach (GridInfo grid in mapGridArray)
        {
            grid.walkable = true;
        }
    }
}
