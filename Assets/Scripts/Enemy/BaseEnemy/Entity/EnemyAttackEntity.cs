using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay.Combat;
using DG.Tweening;

public class EnemyAttackEntity : Entity
{
    public SkillObject AttackSkill;
    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    private UnitAnimatorComponent unitAnimatorComponent => Enemy.unitAnimatorComponent;
    private UnitSkillControllerComponent skillControllerComponent => Enemy.skillControllerComponent;
    private Enemy Enemy => combatEntity.ModelObject.GetComponent<Enemy>();
    private Transform CombatTarget => Enemy.CombatTarget;
    /// <summary>
    /// 是否正在攻击，是否结束
    /// </summary>
    public bool IsAttacking = false;
    public CountDownTimer CountDownTimer = new CountDownTimer(3);


    public void Init()
    {
        AttackSkill = new SkillObject();
        InitAttackSkill();
        CountDownTimer.Start();
    }

    public bool Attack()
    {
        if (CountDownTimer.IsTimeUp)
        {
            AttackSkill.action.Invoke();
            CountDownTimer.Start();
            return true;
        }
        return false;
    }
    
    private void InitAttackSkill()
    {
        AttackSkill.InitSkillObject("Skills/SkillConfigs/Skill_8_EnemyAttack", combatEntity, () => {
            IsAttacking = true;
            Quaternion origin = Enemy.transform.rotation;
            Enemy.transform.LookAt(CombatTarget.position);
            Quaternion quaternion = Enemy.transform.rotation;
            Enemy.transform.rotation = origin;
            Enemy.transform.DORotateQuaternion(quaternion, 0.15f);

            SpellComponent.SpellWithDirect(AttackSkill.SkillAbility, new Vector3(0,0,0), Enemy.transform.position + Enemy.transform.forward*0.5f + Enemy.transform.up*0.5f);

            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["EnemyAttack"]);
            Enemy.currentState = state;
            Enemy.PlayerAction = EnemyAction.Attack1;
            Enemy.AnimState = AnimState.Pre;
            state.Events.OnEnd = () =>
            {
                IsAttacking = false;
                var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["EnemyIdle"]);
                Enemy.currentState = state1;
                Enemy.PlayerAction = EnemyAction.Idle;
                Enemy.AnimState = AnimState.None;
            };
        });
    }
}
