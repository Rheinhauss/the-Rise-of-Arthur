using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetEntity : Entity
{
    private Player Player = Player.Instance;
    private Transform m_transform => Player.transform;

    private CombatEntity combatEntity => GetParent<CombatEntity>();

    private StatusObject StatusObject;

    private Transform ResurrectionPoint;
    private UnitAnimatorComponent unitAnimatorComponent => Player.unitAnimatorComponent;
    public CountDownTimer timer = new CountDownTimer(1, false, false);

    public void Init()
    {
        StatusObject = new StatusObject();
        StatusObject.Init(StatusType.AddStatus, "Status/BaseStatus/Status_5_ResetHP", AddSkillEffetTargetType.Self);
        Player.PlayerDeathEntity.AddAction(() =>
        {
            timer.Start();
        });
        ResurrectionPoint = GameObject.Find("PlayerDefaultResurrectionPoint").transform;

        timer.EndActions.Add(DeathReset);
    }

    private void DeathReset()
    {
        ResetHP();
        ResetToDeafultPoint();
    }

    public void ResetToDeafultPoint()
    {
        ResetTransform(ResurrectionPoint);
        AfterResetEvent();
    }

    private void AfterResetEvent()
    {
        var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        Player.currentState = state;
        Player.AnimState = AnimState.None;
        Player.PlayerAction = PlayerAction.Idle;
    }

    public void SetResurrectionPoint(Transform transform)
    {
        if(transform == null)
        {
            ResurrectionPoint = null;
            return;
        }
        ResurrectionPoint = transform;
    }

    public void ResetTransform(Vector3 point)
    {
        m_transform.position = point;
        m_transform.rotation = Quaternion.identity;
        AfterResetEvent();
    }

    public void ResetTransform(Transform point)
    {
        m_transform.position = point.position;
        m_transform.rotation = Quaternion.identity;
        AfterResetEvent();
    }

    public void ResetHP()
    {
        combatEntity.unitSpellStatusToSelfComponent.SpellToSelf(StatusObject);
        if(Player.PlayerAction == PlayerAction.Death)
        {
            AfterResetEvent();
        }
    }
}
