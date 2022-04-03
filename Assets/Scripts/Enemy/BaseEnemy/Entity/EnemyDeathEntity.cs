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
    /// �жϽ�ɫ�Ƿ���������������ִ�ж�Ӧ�¼�
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
