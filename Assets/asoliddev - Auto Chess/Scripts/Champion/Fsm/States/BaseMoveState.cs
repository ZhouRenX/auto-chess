using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseMoveState : State
{
    public override void Init()
    {
        base.Init();
        championController.eventCenter.AddListener<GridInfo>("OnEnterGrid", OnEnterGrid);
        championController.eventCenter.AddListener<GridInfo>("OnMoveFailed", OnMoveFailed);
        championController.eventCenter.AddListener<GridInfo>("OnGetTarget", OnGetTarget);
    }
    public override void OnEnter()
    {
        //championController.FindPath();
    }
    public override void OnUpdate()
    {
        if (championController.target == null || championController.CheckState("immovable") || championController.isDead)
        {

            fsm.SwitchState("Idle");
            return;
        }
        if (championController.target.isDead == true) //target champion is alive
        {
            championController.target = null;
            fsm.SwitchState("Idle");
            return;
        }
        else
        {
            if (championController.path == null)
            {
                fsm.SwitchState("Idle");
                return;
            }
            else
            {
                championController.MoveToTarget();
            }

        }
    }
    public override void OnLeave()
    {

    }

    void OnEnterGrid(GridInfo grid)
    {
        var c = championController.FindTarget((int)championController.attributesController.attackRange.GetTrueLinearValue(), FindTargetMode.AnyInRange);
        if (c != null)
        {
            championController.target = c;
            if (championController.IsLegalAttackIntervel())
            {
                fsm.SwitchState("Attack");
            }

            return;
        }
        if (!championController.FindPath())
        {
            fsm.SwitchState("Idle");
            return;
        }
    }

    void OnMoveFailed(GridInfo grid)
    {
        fsm.SwitchState("Idle");
        return;
    }

    void OnGetTarget(GridInfo grid)
    {
        fsm.SwitchState("Idle");
        return;
    }
}