﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelConfig;
using System;
[Serializable]
public class InventoryConstructor
{
    public ConstructorBaseData constructorBaseData;
    public bool isNew;
    public InventoryConstructor(ConstructorBaseData _constructorBaseData, bool _isNew)
    {
        constructorBaseData = _constructorBaseData;
        isNew = _isNew;
    }
}

public class GameData : CreateSingleton<GameData>
{
    
    public int[] testList;
    public int currentHP = 100;
    public int currentGold = 0;
    public List<InventoryConstructor> allInventoryConstructors;
    public int constructsOnSaleLimit = 3;

    [Header("战斗等级")]
    public int combatLevel = 1;
    [Header("交易等级")]
    public int tradeLevel = 1;
    [Header("指挥等级")]
    public int commandLevel = 1;
    [Header("后勤等级")]
    public int logisticsLevel = 1;

    [Header("关卡")]
    public int mapLevel = 1;


    public List<ConstructorBaseData> constructsOnSale;
    protected override void InitSingleton()
    { }
    public void RemoveInventoryConstructor(ConstructorBaseData constructorBaseData)
    {
        InventoryConstructor i = allInventoryConstructors.Find(c => c.constructorBaseData == constructorBaseData);
        allInventoryConstructors.Remove(i);
    }
}


