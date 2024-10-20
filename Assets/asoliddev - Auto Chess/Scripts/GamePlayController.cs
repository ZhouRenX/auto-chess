﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Reflection;
using General;
using ExcelConfig;

public enum GameStage { Preparation, Combat, Loss };

/// <summary>
/// Controlls most of the game logic and player interactions
/// </summary>
public class GamePlayController : CreateSingleton<GamePlayController>
{
    public GameStage currentGameStage;
    public GameStage lastStage;
    public float timer = 0;
    private bool isReady = false;

    public EventCenter eventCenter = new EventCenter();
    public ChampionManager ownChampionManager;
    public ChampionManager oponentChampionManager;
    public MainCameraController mainCameraController;
    public GOToUICameraController _GOToUICameraController;
    public ChampionController pickedChampion = null;


    Dictionary<string, CallBack> gameStageActions = new Dictionary<string, CallBack>();

    public void OnMapReady()
    {
        isReady = true;
        StageChange(GameStage.Preparation);
        //Time.timeScale = .2f;
    }

    protected override void InitSingleton()
    {
        isReady = false;
        currentGameStage = GameStage.Preparation;
        lastStage = GameStage.Preparation;
    }

    void Start()
    {
        InitStageDic();
        StageStateAddListener(gameStageActions);
        StageStateAddListener(oponentChampionManager.gameStageActions);
        StageStateAddListener(ownChampionManager.gameStageActions);
        StageStateAddListener(UIController.Instance.gameStageActions);
    }

    /// Update is called once per frame
    void Update()
    {
        if (isReady)
            eventCenter.Broadcast("OnUpdate" + currentGameStage.ToString());
    }

    /// <summary>
    /// Returns the number of gold we should recieve
    /// </summary>
    /// <returns></returns>
    private int CalculateIncome()
    {
        int income = 0;

        //banked gold
        int bank = (int)(GameData.Instance.currentGold / 10);

        income += GameConfig.Instance.baseGoldIncome;
        income += bank;

        return income;
    }

    /// <summary>
    /// Called when Game was lost
    /// </summary>
    public void RestartGame()
    {
        ownChampionManager.Reset();
        oponentChampionManager.Reset();
        GameData.Instance.currentHP = 100;
        GameData.Instance.currentGold = 0;
        StageChange(GameStage.Preparation);

        UIController.Instance.ShowGameScreen();
    }


    /// <summary>
    /// Ends the round
    /// </summary>
    public void EndRound()
    {
        timer = GameConfig.Instance.combatStageDuration - 3; //reduce timer so game ends fast
    }


    public List<ConstructorBonusType> GetAllChampionTypes(ConstructorBaseData constructorData)
    {
        List<ConstructorBonusType> types = new List<ConstructorBonusType>();
        types.Add(GameExcelConfig.Instance._eeDataManager.Get<ConstructorBonusType>(constructorData.property1));
        types.Add(GameExcelConfig.Instance._eeDataManager.Get<ConstructorBonusType>(constructorData.property2));
        types.Add(GameExcelConfig.Instance._eeDataManager.Get<ConstructorBonusType>(constructorData.property3));
        return types;
    }

    public void InitStageDic()
    {
        gameStageActions.Add("OnEnterPreparation", OnEnterPreparation);
        gameStageActions.Add("OnEnterCombat", OnEnterCombat);
        gameStageActions.Add("OnEnterLoss", OnEnterLoss);
        gameStageActions.Add("OnUpdatePreparation", OnUpdatePreparation);
        gameStageActions.Add("OnUpdateCombat", OnUpdateCombat);
        gameStageActions.Add("OnUpdateLoss", OnUpdateLoss);
        gameStageActions.Add("OnLeavePreparation", OnLeavePreparation);
        gameStageActions.Add("OnLeaveCombat", OnLeaveCombat);
        gameStageActions.Add("OnLeaveLoss", OnLeaveLoss);
    }

    //不同状态stage命令模式绑定事件
    public void StageStateAddListener(Dictionary<string, CallBack> actions)
    {
        foreach (string stage in Enum.GetNames(typeof(GameStage)))
        {
            eventCenter.AddListener("OnEnter" + stage, actions["OnEnter" + stage]);
            eventCenter.AddListener("OnUpdate" + stage, actions["OnUpdate" + stage]);
            eventCenter.AddListener("OnLeave" + stage, actions["OnLeave" + stage]);
        }
    }

    public void StageStateRemoveListener(Dictionary<string, CallBack> actions)
    {
        foreach (string stage in Enum.GetNames(typeof(GameStage)))
        {
            eventCenter.RemoveListener("OnEnter" + stage, actions["OnEnter" + stage]);
            eventCenter.RemoveListener("OnUpdate" + stage, actions["OnUpdate" + stage]);
            eventCenter.RemoveListener("OnLeave" + stage, actions["OnLeave" + stage]);
        }
    }

    public void StageChange(GameStage nextStage)
    {
        eventCenter.Broadcast("OnLeave" + currentGameStage.ToString());
        lastStage = currentGameStage;
        currentGameStage = nextStage;
        eventCenter.Broadcast("OnEnter" + currentGameStage.ToString());
    }

    public void SetPickedChampion(ChampionController championCtrl)
    {
        if (pickedChampion != null)
        {
            foreach (Transform tran in pickedChampion.GetComponentsInChildren<Transform>())
            {
                tran.gameObject.layer = 0;
            }
        }
        pickedChampion = championCtrl;
        if (pickedChampion != null)
        {
            foreach (Transform tran in pickedChampion.GetComponentsInChildren<Transform>())
            {
                tran.gameObject.layer = 9;
            }
            _GOToUICameraController.ResetCam(pickedChampion.transform);
        }
    }

    public void PickChampion()
    {
        if (InputController.Instance.ui == null)
        {
            UIController.Instance.isSlotUIDragged = false;
            //get trigger info
            GridInfo gridInfo = InputController.Instance.gridInfo;
            //if mouse cursor on trigger
            if (gridInfo != null)
            {
                ChampionController championCtrl = gridInfo.occupyChampion;
                if (championCtrl != null)
                {
                    SetPickedChampion(championCtrl);
                    UIController.Instance.championInfoController.UpdateUI();
                    UIController.Instance.constructorAssembleController.UpdateUI();
                    return;
                }
            }
            SetPickedChampion(null);
            UIController.Instance.championInfoController.UpdateUI();
            UIController.Instance.constructorAssembleController.UpdateUI();
        }
        else
        {
            UIController.Instance.isSlotUIDragged = true;
        }
    }

    public string GetConstructorIconPath(ConstructorBaseData constructorBaseData)
    {
        string iconPath = constructorBaseData.prefab.Substring(0, constructorBaseData.prefab.IndexOf(constructorBaseData.type));
        iconPath = "Prefab/Constructor/" + iconPath + constructorBaseData.type + "/Icon/";
        string namePath = constructorBaseData.prefab.Substring(constructorBaseData.prefab.IndexOf(constructorBaseData.type) + constructorBaseData.type.Length + 1);
        return iconPath + namePath;
    }

    #region StageFuncs
    public void OnEnterPreparation()
    {
        GameData.Instance.currentGold += CalculateIncome();
        //ChampionShop.Instance.RefreshShop(true);

        if (GameData.Instance.currentHP <= 0)
        {
            currentGameStage = GameStage.Loss;
            StageChange(GameStage.Loss);
        }

    }
    public void OnUpdatePreparation()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PickChampion();
            GamePlayController.Instance.ownChampionManager.StartDrag();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GamePlayController.Instance.ownChampionManager.StopDrag();
        }
    }
    public void OnLeavePreparation()
    {
        timer = 0;
    }

    public void OnEnterCombat()
    {
        //Map.Instance.HideIndicators();
        if (ownChampionManager.IsAllChampionDead())
            EndRound();

    }
    public void OnUpdateCombat()
    {
        timer += Time.deltaTime;
        UIController.Instance.levelInfo.UpdateCombatTimer((int)(GameConfig.Instance.combatStageDuration - timer));

        if (timer > GameConfig.Instance.combatStageDuration)
        {
            StageChange(GameStage.Preparation);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PickChampion();
        }

    }
    public void OnLeaveCombat()
    {
        timer = 0;
        GameData.Instance.mapLevel++;
    }

    public void OnEnterLoss()
    {
        UIController.Instance.ShowLossScreen();

    }
    public void OnUpdateLoss()
    {

    }
    public void OnLeaveLoss()
    {

    }
    #endregion












}
