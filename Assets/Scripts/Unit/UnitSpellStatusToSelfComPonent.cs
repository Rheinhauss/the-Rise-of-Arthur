using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpellStatusToSelfComponent : EGamePlay.Component
{
    private CombatEntity combatEntity => GetEntity<CombatEntity>();
    private SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    public override void Awake()
    {
        base.Awake();
    }
    public AbilityEffect SpellToSelf(StatusObject StatusObject)
    {
        return SpellComponent.SpellStatusToTarget(StatusObject, combatEntity);
    }
}
