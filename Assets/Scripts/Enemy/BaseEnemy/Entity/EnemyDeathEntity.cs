using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEntity : Entity
{
    private CombatEntity combatEntity => GetParent<CombatEntity>();

    private List<Action> actions = new List<Action>();

    private Enemy enemy => combatEntity.ModelObject.GetComponent<Enemy>();
    private UnitSkillControllerComponent skillControllerComponent => enemy.skillControllerComponent;
    private UnitAnimatorComponent unitAnimatorComponent => enemy.unitAnimatorComponent;

    private bool IsExecuteDeathAction = false;
    private bool IsPlayOverDeathAnim = false;

    /// <summary>
    /// 判断角色是否死亡，若死亡则执行对应事件
    /// </summary>
    public bool CheckDeath()
    {
        if (combatEntity.CheckDead())
        {
            DeathAction();
            return true;
        }
        IsExecuteDeathAction = false;
        IsPlayOverDeathAnim = false;
        return false;
    }

    private void DeathAction()
    {
        if (IsExecuteDeathAction)
        {
            return;
        }
        var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["EnemyDeath"]);
        enemy.currentState = state;
        enemy.AnimState = AnimState.ForcePost;
        enemy.PlayerAction = EnemyAction.Death;
        state.Events.OnEnd = () =>
        {
            if (IsPlayOverDeathAnim)
            {
                return;
            }
            foreach (var action in actions)
            {
                action.Invoke();
            }
            IsPlayOverDeathAnim = true;
            combatEntity.Dispose();
            GameObject.Destroy(combatEntity.ModelObject, 0.5f);
        };
        IsExecuteDeathAction = true;
    }

    /// <summary>
    /// 添加死亡后执行事件
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(Action action)
    {
        actions.Add(action);
    }
    /// <summary>
    /// 移除死亡后执行的事件
    /// </summary>
    /// <param name="action"></param>
    public void Remove(Action action)
    {
        actions.Remove(action);
    }
}
