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

    /// <summary>
    /// 判断角色是否死亡，若死亡则执行对应事件
    /// </summary>
    public void CheckDeath()
    {
        if (combatEntity.CheckDead())
        {
            DeathAction();
        }
    }

    private void DeathAction()
    {
        var state = unitAnimatorComponent.Play(unitAnimatorComponent.animationClipsDict["EnemyDeath"]);
        enemy.currentState = state;
        enemy.AnimState = AnimState.ForcePost;
        enemy.PlayerAction = EnemyAction.Death;
        state.Events.OnEnd = () =>
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
            GameObject.Destroy(combatEntity.ModelObject, 0.5f);
        };
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
