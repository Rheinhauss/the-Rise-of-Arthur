using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeHitEntity : Entity
{
    private CombatEntity combatEntity => GetParent<CombatEntity>();

    private List<Action> actions = new List<Action>();

    private Enemy enemy => combatEntity.ModelObject.GetComponent<Enemy>();
    private UnitSkillControllerComponent skillControllerComponent => enemy.skillControllerComponent;
    private UnitAnimatorComponent unitAnimatorComponent => enemy.unitAnimatorComponent;

    public bool IsBeHit = false;
    public Transform Creator;

    /// <summary>
    /// 判断角色是否受伤，若死亡则执行对应事件
    /// </summary>
    public bool CheckBehit()
    {
        //如果在攻击，则不会执行
        if (enemy.PlayerAction > EnemyAction.BeHit)
        {
            IsBeHit = false;
        }
        if (IsBeHit)
        {
            BeHitAction();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 设置伤害来源
    /// </summary>
    /// <param name="Creator"></param>
    public void SetCreator(Transform Creator)
    {
        this.Creator = Creator;
        IsBeHit = true;
    }

    private void BeHitAction()
    {
        var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["EnemyBeHit"]);
        enemy.currentState = state;
        enemy.AnimState = AnimState.None;
        enemy.PlayerAction = EnemyAction.BeHit;
        state.Events.OnEnd = () =>
        {
            var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["EnemyIdle"]);
            enemy.currentState = state1;
            enemy.AnimState = AnimState.None;
            enemy.PlayerAction = EnemyAction.Idle;
            IsBeHit = false;
            foreach (var action in actions)
            {
                action.Invoke();
            }
        };
    }

    /// <summary>
    /// 添加受伤后执行事件
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(Action action)
    {
        actions.Add(action);
    }
    /// <summary>
    /// 移除受伤后执行的事件
    /// </summary>
    /// <param name="action"></param>
    public void Remove(Action action)
    {
        actions.Remove(action);
    }
}
