using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeEntity : Entity
{
    public SkillObject Skill_5_Evade;
    private CombatEntity combatEntity => GetParent<CombatEntity>();
    private SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    private PlayerMoveEntity PlayerMoveEntity => combatEntity.GetChild<PlayerMoveEntity>();
    private UnitAnimatorComponent unitAnimatorComponent => PlayerMoveEntity.PlayerTransform.GetComponent<Player>().unitAnimatorComponent;
    private UnitSkillControllerComponent skillControllerComponent => PlayerMoveEntity.PlayerTransform.GetComponent<Player>().skillControllerComponent;
    private Player Player => Player.Instance;

    private CountDownTimer countDownTimer;

    private List<Action> actions = new List<Action>();

    private Collider PlayerCol => Player.GetComponent<Collider>();
    //private PlayerTerrainCol PlayerTerrainCol => Player.transform.GetComponentInChildren<PlayerTerrainCol>();

    CountDownTimer timer = new CountDownTimer(0.3f, false, false);

    private Rigidbody Rigidbody => Player.GetComponent<Rigidbody>();
    private InputEntity inputEntity;

    public void Init()
    {
        Skill_5_Evade = new SkillObject();
        Skill_Evade();
        inputEntity = new InputEntity(KeyCode.Space, KeyCodeType.DOWN);
        inputEntity.name = "PlayerEvadeEntity";
        inputEntity.BindInputAction(Execute);

        timer.UpdateAction.Add(OnUpdate);

        timer.EndActions.Add(OnEnd);
    }
    private void Execute()
    {
        // || (Player.PlayerAction == PlayerAction.Evade && Player.AnimState <= AnimState.ForcePost)
        if (Player.AnimState == AnimState.ForcePost || Player.CanMove == false || Player.PlayerAction >= PlayerAction.Evade)
        {
            return;
        }
        Skill_5_Evade.action.Invoke();
    }

    private int startFrame;

    private void Skill_Evade()
    {
        Skill_5_Evade.InitSkillObject("Skills/SkillConfigs/Skill_5_Evade", combatEntity, () =>
        {
            //SpellComponent.SpellWithDirect(Skill_5_Evade.SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles, PlayerMoveEntity.ModelTransform.position);
            int preIndex = Player.currentState.Index;
            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanEvade"]);
            startFrame = Time.frameCount;
            //нч╣п
            combatEntity.IsInvincibel = true;
            Player.PlayerAction = PlayerAction.Evade;
            Player.currentState = state;
            Player.AnimState = AnimState.Pre;
            timer.Start();
            PlayerRotateEntity.RotEnable = false;
            state.Events.OnEnd = () =>
            {
                if((Time.frameCount - startFrame) <= 2)
                {
                    EndEvade();
                    return;
                }
                Player.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
                PlayerRotateEntity.RotEnable = true;
                Player.PlayerAction = PlayerAction.Idle;
                Player.AnimState = AnimState.None;
            };
        });
    }
    private void OnUpdate(float currentTime)
    {
        Player.AnimState = AnimState.ForcePost;
        Rigidbody.velocity = PlayerMoveEntity.ModelTransform.forward * currentTime * 70;
    }

    public void OnEnd()
    {
        combatEntity.IsInvincibel = false;
        Player.AnimState = AnimState.Post;
        foreach (Action action in actions)
        {
            action?.Invoke();
        }
    }

    public void AddPostAction(Action action)
    {
        actions.Add(action);
    }
    public void RemovePostAction(Action action)
    {
        actions.Remove(action);
    }

    public void EndEvade()
    {
        timer.End();
        PlayerRotateEntity.RotEnable = true;
    }

    public override void OnDestroy()
    {
        inputEntity.UnBindInputAction(Execute);
    }


}
