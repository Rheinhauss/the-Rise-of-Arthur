using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCureEntity : Entity
{
    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private Player Player => Player.Instance;
    private PlayerUIController PlayerUIController => Player.PlayerUIController;
    public void Init()
    {
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);
    }
    public void OnReceiveCure(ActionExecution combatAction)
    {
        var cureAction = combatAction as CureAction;
        PlayerUIController._HP.OnReceiveCure(combatEntity.UnitPropertyEntity.HP.Percent(), cureAction.CureValue);
    }
}
