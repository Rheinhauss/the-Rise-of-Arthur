using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEntity : Entity
{
    private CombatEntity combatEntity => GetParent<CombatEntity>();

    private List<Action> actions;

    private Player player => Player.Instance;
    private UnitSkillControllerComponent skillControllerComponent => player.skillControllerComponent;
    private UnitAnimatorComponent unitAnimatorComponent => player.unitAnimatorComponent;

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
        var state = unitAnimatorComponent.Play(unitAnimatorComponent.animationClipsDict["SwordsmanDeath"]);
        player.currentState = state;
        player.AnimState = AnimState.ForcePost;
        player.PlayerAction = PlayerAction.Death;
        state.Events.OnEnd = () =>
        {
            foreach (var action in actions)
            {
                action.Invoke();
            }
        };
    }

    /// <summary>
    /// ����������ִ���¼�
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