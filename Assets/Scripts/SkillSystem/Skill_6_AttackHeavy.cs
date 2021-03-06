using EGamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EGamePlay.Combat;

public class Skill_6_AttackHeavy : Entity
{
    public SkillObject skill_6_AttackHeavy;
    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    private PlayerMoveEntity PlayerMoveEntity => combatEntity.GetChild<PlayerMoveEntity>();
    private UnitAnimatorComponent unitAnimatorComponent => PlayerMoveEntity.PlayerTransform.GetComponent<Player>().unitAnimatorComponent;
    private UnitSkillControllerComponent skillControllerComponent => PlayerMoveEntity.PlayerTransform.GetComponent<Player>().skillControllerComponent;
    private Player Player => Player.Instance;

    private CountDownTimer countDownTimer;

    private InputEntity inputEntity;


    public void Init()
    {
        skill_6_AttackHeavy = new SkillObject();
        Skill_AttackHeavy();
        inputEntity = new InputEntity(KeyCode.Mouse1, KeyCodeType.DOWN, false);
        inputEntity.name = "Skill_6_AttackHeavy";
        inputEntity.BindInputAction(Execute);
    }

    private void Execute()
    {
        if (Player.AnimState == AnimState.ForcePost || Player.CanAttack == false || Player.PlayerAction == PlayerAction.AttackHeavy)
        {
            return;
        }
        skill_6_AttackHeavy.action.Invoke();
    }

    private void Skill_AttackHeavy()
    {
        skill_6_AttackHeavy.InitSkillObject("Skills/SkillConfigs/Skill_6_AttackHeavy", combatEntity, () => {
            SpellComponent.SpellWithDirect(skill_6_AttackHeavy.SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles, PlayerMoveEntity.ModelTransform.position);
            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttackHeavyCast"]);
            Player.PlayerAction = PlayerAction.AttackHeavy;
            Player.currentState = state;
            Player.AnimState = AnimState.Pre;
            state.Events.OnEnd = () =>
            {
                var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttackHeavyEnd"]);
                Player.currentState = state1;
                Player.AnimState = AnimState.Post;
                state1.Events.OnEnd = () =>
                {
                    var state2 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
                    Player.currentState = state2;
                    Player.PlayerAction = PlayerAction.Idle;
                    Player.AnimState = AnimState.None;

                };
            };
        });


    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        inputEntity.UnBindInputAction(Execute);
    }
}
