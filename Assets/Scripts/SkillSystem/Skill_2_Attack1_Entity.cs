using EGamePlay;
using EGamePlay.Combat;
using GameUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2_Attack1_Entity : Entity
{

    public SkillObject[] Skill_2_Attack1;
    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    private PlayerMoveEntity PlayerMoveEntity => combatEntity.GetChild<PlayerMoveEntity>();
    private UnitAnimatorComponent unitAnimatorComponent => PlayerMoveEntity.PlayerTransform.GetComponent<Player>().unitAnimatorComponent;
    private UnitSkillControllerComponent skillControllerComponent => PlayerMoveEntity.PlayerTransform.GetComponent<Player>().skillControllerComponent;
    private Player Player => Player.Instance;

    private CountDownTimer countDownTimer;

    private PlayerEvadeEntity PlayerEvadeEntity => combatEntity.GetChild<PlayerEvadeEntity>();

    /// <summary>
    /// 0执行1段，以此类推
    /// </summary>
    private int curAttackState = 0;

    public StatusObject Status_ElementPassive_Jin1 = new StatusObject();

    public void Init()
    {
        AddComponent<UpdateComponent>();
        Skill_2_Attack1 = new SkillObject[4];
        Skill_2_Attack1[0] = new SkillObject();
        Skill_2_Attack1[1] = new SkillObject();
        Skill_2_Attack1[2] = new SkillObject();
        Skill_2_Attack1[3] = new SkillObject();
        countDownTimer = new CountDownTimer(1);

        Skill_2_Attack1_1();
        Skill_2_Attack1_2();
        Skill_2_Attack1_3();
        Skill_2_Attack1_4();

        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.Mouse0, () => {
            if (Player.PlayerAction == PlayerAction.Evade)
            {
                Skill_2_Attack1[3].action.Invoke();
                //PlayerEvadeEntity.AddPostAction(Skill_2_Attack1[3].action);
            }
            if (Player.AnimState == AnimState.ForcePost || (Player.PlayerAction == PlayerAction.Attack1 && Player.AnimState == AnimState.Pre))
            {
                return;
            }
            Skill_2_Attack1[curAttackState].action.Invoke();
            ++curAttackState;
            if(curAttackState == 3)
            {
                countDownTimer.Reset(1,true);
                curAttackState = 0;
            }
            else
            {
                countDownTimer.Start();
            }
        }, KeyCodeType.DOWN);
    }

    public override void Update()
    {
        if (countDownTimer.IsTimeUp)
        {
            curAttackState = 0;
        }
    }
    private void Skill_2_Attack1_1()
    {
        Skill_2_Attack1[0].InitSkillObject("Skills/SkillConfigs/Skill_2_Attack1_1", combatEntity, () => {
            Quaternion rot = PlayerMoveEntity.ModelTransform.rotation;
            PlayerMoveEntity.ModelTransform.Rotate(new Vector3(-90, 180, 0));
            SpellComponent.SpellWithDirect(Skill_2_Attack1[0].SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles,PlayerMoveEntity.ModelTransform.position);
            PlayerMoveEntity.ModelTransform.rotation = rot;
            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttack1"]);

            Player.PlayerAction = PlayerAction.Attack1;
            Player.currentState = state;
            Player.AnimState = AnimState.Pre;

            state.Events.OnEnd = () =>
            {
                var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttack1End"]);

                Player.currentState = state1;
                Player.AnimState = AnimState.Post;

                state1.Events.OnEnd = () =>
                {
                    Player.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);

                    Player.PlayerAction = PlayerAction.Idle;
                    Player.AnimState = AnimState.None;

                };
            };
        });
        // 测试自定义状态效果
        //Status_ElementPassive_Jin1.Init(StatusType.AddStatus, "Status/StatusConfigs/WuXingStrengthen/Status_22001_Jin1",AddSkillEffetTargetType.SkillTarget,10000);
        //Skill_2_Attack1[0].AddStatus(Status_ElementPassive_Jin1);
        skillControllerComponent.AddSkillObject(Skill_2_Attack1[0]);
    }
    private void Skill_2_Attack1_2()
    {
        Skill_2_Attack1[1].InitSkillObject("Skills/SkillConfigs/Skill_3_Attack1_2", combatEntity, () => {
            Quaternion rot = PlayerMoveEntity.ModelTransform.rotation;
            PlayerMoveEntity.ModelTransform.Rotate(new Vector3(-90, 180, 0));
            SpellComponent.SpellWithDirect(Skill_2_Attack1[1].SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles, PlayerMoveEntity.ModelTransform.position);
            PlayerMoveEntity.ModelTransform.rotation = rot;

            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttack2"]);

            Player.PlayerAction = PlayerAction.Attack1;
            Player.currentState = state;
            Player.AnimState = AnimState.Pre;

            state.Events.OnEnd = () =>
            {
                var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttack2End"]);

                Player.currentState = state1;
                Player.AnimState = AnimState.Post;

                state1.Events.OnEnd = () =>
                {
                    unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);

                    Player.PlayerAction = PlayerAction.Idle;
                    Player.AnimState = AnimState.None;

                };
            };
        });
        skillControllerComponent.AddSkillObject(Skill_2_Attack1[1]);
    }
    private void Skill_2_Attack1_3()
    {
        Skill_2_Attack1[2].InitSkillObject("Skills/SkillConfigs/Skill_4_Attack1_3", combatEntity, () => {
            Quaternion rot = PlayerMoveEntity.ModelTransform.rotation;
            PlayerMoveEntity.ModelTransform.Rotate(new Vector3(-90, 180, 0));
            SpellComponent.SpellWithDirect(Skill_2_Attack1[2].SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles, PlayerMoveEntity.ModelTransform.position);
            PlayerMoveEntity.ModelTransform.rotation = rot;

            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttack3"]);

            Player.PlayerAction = PlayerAction.Attack1;
            Player.currentState = state;
            Player.AnimState = AnimState.Pre;

            state.Events.OnEnd = () =>
            {
                var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttack3End"]);

                Player.currentState = state1;
                Player.AnimState = AnimState.Post;

                state1.Events.OnEnd = () =>
                {
                    unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);

                    Player.PlayerAction = PlayerAction.Idle;
                    Player.AnimState = AnimState.None;

                };
            };
        });
        skillControllerComponent.AddSkillObject(Skill_2_Attack1[2]);
    }
    /// <summary>
    /// 冲刺攻击
    /// </summary>
    private void Skill_2_Attack1_4()
    {
        Skill_2_Attack1[3].InitSkillObject("Skills/SkillConfigs/Skill_7_AttackEvade", combatEntity, () => {
            PlayerEvadeEntity.EndEvade();
            Quaternion rot = PlayerMoveEntity.ModelTransform.rotation;
            PlayerMoveEntity.ModelTransform.Rotate(new Vector3(-90, 180, 0));
            SpellComponent.SpellWithDirect(Skill_2_Attack1[3].SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles, PlayerMoveEntity.ModelTransform.position);
            PlayerMoveEntity.ModelTransform.rotation = rot;

            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttackEvade"]);

            Player.PlayerAction = PlayerAction.Attack1;
            Player.currentState = state;
            Player.AnimState = AnimState.Pre;

            state.Events.OnEnd = () =>
            {
                var state1 = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanAttackEvadeEnd"]);

                Player.currentState = state1;
                Player.AnimState = AnimState.Post;

                state1.Events.OnEnd = () =>
                {
                    Player.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);

                    Player.PlayerAction = PlayerAction.Idle;
                    Player.AnimState = AnimState.None;

                };
            };
            countDownTimer.Reset(1, true);
            curAttackState = 0;
        });
        skillControllerComponent.AddSkillObject(Skill_2_Attack1[3]);
    }


}
