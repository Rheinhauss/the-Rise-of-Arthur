using EGamePlay;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMoveEntity : Entity
{
    public Transform PlayerTransform;
    public Transform ModelTransform;

    public Skill_1_Move skill_1_move_left;
    public Skill_1_Move skill_1_move_right;
    public Skill_1_Move skill_1_move_forward;
    public Skill_1_Move skill_1_move_backward;

    public SkillObject moveEnd_left;
    public SkillObject moveEnd_right;
    public SkillObject moveEnd_forward;
    public SkillObject moveEnd_backward;
    /// <summary>
    /// 关联三个参数
    /// </summary>
    private UnitSkillControllerComponent skillControllerComponent => PlayerTransform.GetComponent<Player>().skillControllerComponent;
    private UnitAnimatorComponent unitAnimatorComponent => PlayerTransform.GetComponent<Player>().unitAnimatorComponent;
    private CombatEntity combatEntity => PlayerTransform.GetComponent<Player>().combatEntity;
    private Player Player => Player.Instance;

    private Transform CameraTransform => Camera.main.transform;

    private Rigidbody rigidbody => PlayerTransform.GetComponent<Rigidbody>();

    /// <summary>
    /// 初始化函数
    /// 里面进行了移动的逻辑计算，但是不涉及具体移动
    /// </summary>
    public void Init(Transform playerTrans,Transform modelTrans)
    {
        PlayerTransform = playerTrans;
        ModelTransform = modelTrans;


        AddComponent<UpdateComponent>();
        Player.CanMove = true;
        #region 按键抬起
        //moveEnd_left
        moveEnd_left = new SkillObject();
        
        moveEnd_left.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition & ~CurrentPosition.Left;
            if (Skill_1_Move.CurrentPosition == CurrentPosition.None)
                Skill_1_Move.CurrentPosition = CurrentPosition.Cross;
        });

        this.skillControllerComponent.AddSkillObject(moveEnd_left);
        //moveEnd_right
        moveEnd_right = new SkillObject();
        moveEnd_right.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition & ~CurrentPosition.Right;
            if (Skill_1_Move.CurrentPosition == CurrentPosition.None)
                Skill_1_Move.CurrentPosition = CurrentPosition.Cross;
        });
        this.skillControllerComponent.AddSkillObject(moveEnd_right);
        //moveEnd_Back
        moveEnd_backward = new SkillObject();
        moveEnd_backward.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition & ~CurrentPosition.Backward;
            if (Skill_1_Move.CurrentPosition == CurrentPosition.None)
                Skill_1_Move.CurrentPosition = CurrentPosition.Cross;
        });
        this.skillControllerComponent.AddSkillObject(moveEnd_backward);
        //moveEnd_forward
        moveEnd_forward = new SkillObject();
        moveEnd_forward.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition & ~CurrentPosition.Forward;
            if (Skill_1_Move.CurrentPosition == CurrentPosition.None)
                Skill_1_Move.CurrentPosition = CurrentPosition.Cross;
        });
        this.skillControllerComponent.AddSkillObject(moveEnd_forward);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.W, moveEnd_forward.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.A, moveEnd_left.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.S, moveEnd_backward.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.D, moveEnd_right.action, KeyCodeType.UP);
        #endregion

        #region 按键
        //left
        skill_1_move_left = new Skill_1_Move();
        skill_1_move_left.position = new Vector3(-1, 0, 0);
        skill_1_move_left.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition | CurrentPosition.Left;
            skill_1_move_left.position = CameraTransform.right;
            skill_1_move_left.position.y = 0;
            Skill_1_Move.movePosition -= skill_1_move_left.position.normalized;
        });
        this.skillControllerComponent.AddSkillObject(skill_1_move_left);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.A, skill_1_move_left.action, KeyCodeType.ING);
        //backward
        skill_1_move_backward = new Skill_1_Move();
        skill_1_move_backward.position = new Vector3(0, 0, -1);
        skill_1_move_backward.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition | CurrentPosition.Backward;
            skill_1_move_backward.position = CameraTransform.forward;
            skill_1_move_backward.position.y = 0;
            Skill_1_Move.movePosition -= skill_1_move_backward.position.normalized;
        });
        this.skillControllerComponent.AddSkillObject(skill_1_move_backward);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.S, skill_1_move_backward.action, KeyCodeType.ING);
        //right
        skill_1_move_right = new Skill_1_Move();
        skill_1_move_right.position = new Vector3(1, 0, 0);
        skill_1_move_right.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition | CurrentPosition.Right;
            skill_1_move_right.position = CameraTransform.right;
            skill_1_move_right.position.y = 0;
            Skill_1_Move.movePosition += skill_1_move_right.position.normalized;
        });
        this.skillControllerComponent.AddSkillObject(skill_1_move_right);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.D, skill_1_move_right.action, KeyCodeType.ING);
        //forward
        skill_1_move_forward = new Skill_1_Move();
        skill_1_move_forward.SetAction(() =>
        {
            Skill_1_Move.CurrentPosition = Skill_1_Move.CurrentPosition | CurrentPosition.Forward;
            skill_1_move_forward.position = CameraTransform.forward;
            skill_1_move_forward.position.y = 0;
            Skill_1_Move.movePosition += skill_1_move_forward.position.normalized;
        });
        this.skillControllerComponent.AddSkillObject(skill_1_move_forward);
        UnitControllerComponent.inputComponent.BindInputAction(KeyCode.W, skill_1_move_forward.action, KeyCodeType.ING);
        #endregion
    }

    public void Move()
    {
        if (Player.CanMove == false)
            return;
        Skill_1_Move.movePosition.Normalize();
        rigidbody.velocity = (Skill_1_Move.movePosition * combatEntity.UnitPropertyEntity.MoveSpeed.Value + new Vector3(0, rigidbody.velocity.y, 0));
        if (Skill_1_Move.CurrentPosition != CurrentPosition.None && Skill_1_Move.CurrentPosition != CurrentPosition.Cross)
        {
            Quaternion origin = ModelTransform.rotation;
            ModelTransform.LookAt(Skill_1_Move.movePosition + PlayerTransform.position);
            Quaternion quaternion = ModelTransform.rotation;
            ModelTransform.rotation = origin;
            ModelTransform.DORotateQuaternion(quaternion, 0.15f);

            Player.currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanMove"]);
            Player.PlayerAction = PlayerAction.Move;
            Player.AnimState = AnimState.None;
        }
        if (Skill_1_Move.CurrentPosition == CurrentPosition.Cross)
        {
            unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
            Player.PlayerAction = PlayerAction.Idle;
            Player.AnimState = AnimState.None;
        }
    }

    public void ClearMoveData()
    {
        Skill_1_Move.movePosition = Vector3.zero;
        Skill_1_Move.CurrentPosition = CurrentPosition.None;
    }

    public override void Update()
    {
        // Player移动函数
        if ((int)Player.PlayerAction <= (int)PlayerAction.Move)
        {
            Move();
        }
        else if(Player.PlayerAction == PlayerAction.Evade)
        {
            Skill_1_Move.movePosition.Normalize();
            rigidbody.velocity = (Skill_1_Move.movePosition * combatEntity.UnitPropertyEntity.MoveSpeed.Value + new Vector3(0, rigidbody.velocity.y, 0));
        }
        else if(Player.PlayerAction != PlayerAction.Evade)
        {
            rigidbody.velocity = Vector3.zero;
        }
        ClearMoveData();
        //rigidbody.AddForce(new Vector3(0, -9.81f, 0));
    }

    public override void OnDestroy()
    {
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.W, moveEnd_forward.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.A, moveEnd_left.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.S, moveEnd_backward.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.D, moveEnd_right.action, KeyCodeType.UP);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.W, skill_1_move_forward.action, KeyCodeType.ING);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.D, skill_1_move_right.action, KeyCodeType.ING);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.S, skill_1_move_backward.action, KeyCodeType.ING);
        UnitControllerComponent.inputComponent.UnBindInputAction(KeyCode.A, skill_1_move_left.action, KeyCodeType.ING);
    }

}

