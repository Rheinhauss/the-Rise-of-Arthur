using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageEntity : Entity
{
    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private Player Player => Player.Instance;
    private PlayerUIController PlayerUIController => Player.PlayerUIController;
    private PlayerDeathEntity PlayerDeathEntity => Player.PlayerDeathEntity;
    private UnitAnimatorComponent unitAnimatorComponent => Player.unitAnimatorComponent;

    public void Init()
    {
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
    }
    public void OnReceiveDamage(ActionExecution combatAction)
    {
        var damageAction = combatAction as DamageAction;
        PlayerUIController._HP.OnReceiveDamage(combatEntity.UnitPropertyEntity.HP.Percent(), damageAction.DamageValue);
        if (!PlayerDeathEntity.CheckDeath())
        {
            if(damageAction.DamageValue <= 20)
            {
                var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanHit"]);
                Player.currentState = state;
                Player.AnimState = AnimState.ForcePost;
                Player.PlayerAction = PlayerAction.ReceiveDamage;
                state.Events.OnEnd = () =>
                {
                    var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
                    Player.currentState = state;
                    Player.AnimState = AnimState.None;
                    Player.PlayerAction = PlayerAction.Idle;
                };
            }
            else
            {
                var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanHit"]);
                Player.currentState = state;
                Player.AnimState = AnimState.ForcePost;
                Player.PlayerAction = PlayerAction.ReceiveDamage;
                state.Events.OnEnd = () =>
                {
                    var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanHitLarge"]);
                    Player.currentState = state;
                    Player.AnimState = AnimState.None;
                    Player.PlayerAction = PlayerAction.Idle;
                };
            }
        }
    }
}
