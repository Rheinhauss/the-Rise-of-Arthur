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
    /// �жϽ�ɫ�Ƿ���������������ִ�ж�Ӧ�¼�
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
    /// ���������ִ���¼�
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(Action action)
    {
        actions.Add(action);
    }
    /// <summary>
    /// �Ƴ�������ִ�е��¼�
    /// </summary>
    /// <param name="action"></param>
    public void Remove(Action action)
    {
        actions.Remove(action);
    }
}
