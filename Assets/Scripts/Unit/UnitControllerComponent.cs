using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControllerComponent : MonoBehaviour
{
    public static InputComponent inputComponent;
    public UnitSkillControllerComponent skillControllerComponent = new UnitSkillControllerComponent();
    public UnitStatusControllerComponent statusControllerComponent = new UnitStatusControllerComponent();
    public UnitAnimatorComponent unitAnimatorComponent;
    public CombatEntity combatEntity;

    public void AddSkill(SkillObject skillObject)
    {
        skillControllerComponent.AddSkillObject(skillObject);
    }
    public void RemoveSkill(SkillObject skillObject)
    {
        skillControllerComponent.RemoveSkillObject(skillObject);
    }
    /*
     * StatusController再次封装add和remove
     */
    public void Init(string config)
    {
        combatEntity.InitProperty(config);
    }
    public virtual void OnReceiveDamage(ActionExecution combatAction)
    {

    }
    public virtual void OnReceiveCure(ActionExecution combatAction)
    {

    }

    public virtual void OnReceiveStatus(ActionExecution combatAction)
    {
        
    }

    public virtual void OnRemoveStatus(RemoveStatusEvent eventData)
    {

    }
}
