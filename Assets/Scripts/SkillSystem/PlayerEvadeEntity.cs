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

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore;

    private Collider PlayerCol => Player.GetComponent<Collider>();
    private PlayerTerrainCol PlayerTerrainCol => Player.transform.GetComponentInChildren<PlayerTerrainCol>();
    public void Init()
    {
        Skill_5_Evade = new SkillObject();
        Skill_Evade();
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.Space, () =>
        {
            if (Player.AnimState == AnimState.ForcePost || Player.CanMove == false)
            {
                return;
            }
            Skill_5_Evade.action.Invoke();
        }, KeyCodeType.DOWN);
        PlayerTerrainCol.actions.Add(() =>
        {
            EndEvade();
        });
    }
    private void Skill_Evade()
    {
        Skill_5_Evade.InitSkillObject("Skills/SkillConfigs/Skill_5_Evade", combatEntity, () =>
        {
            //SpellComponent.SpellWithDirect(Skill_5_Evade.SkillAbility, PlayerMoveEntity.ModelTransform.rotation.eulerAngles, PlayerMoveEntity.ModelTransform.position);
            var state = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanEvade"]);
            //wudi
            PlayerCol.enabled = false;
            Player.PlayerAction = PlayerAction.Evade;
            Player.currentState = state;
            Player.AnimState = AnimState.ForcePost;
            tweenerCore = Player.transform.DOMove(PlayerMoveEntity.ModelTransform.forward * 3.26f + PlayerMoveEntity.ModelTransform.position, 0.3f);
            tweenerCore.OnComplete(() =>
            {
                PlayerCol.enabled = true;
                Player.AnimState = AnimState.Post;
                foreach(Action action in actions)
                {
                    action?.Invoke();
                }
            });
            state.Events.OnEnd = () =>
            {
                Player.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);

                Player.PlayerAction = PlayerAction.Idle;
                Player.AnimState = AnimState.None;
            };
        });
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
        tweenerCore.Pause();
        PlayerCol.enabled = true;
    }

}
