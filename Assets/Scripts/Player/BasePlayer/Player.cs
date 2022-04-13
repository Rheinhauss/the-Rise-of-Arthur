using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EGamePlay;
using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枚举：人物会执行的技能类型
/// 站立、移动、轻击
/// </summary>
public enum PlayerAction
{
    Idle,
    Move,
    Evade,
    Attack1,
    AttackHeavy,
    ReceiveCure,
    ReceiveDamage,
    Death,
}
/// <summary>
/// 枚举：Action类型的阶段
/// 前摇、强制后摇、后摇
/// </summary>
public enum AnimState
{
    None,
    Pre,
    ForcePost,
    Post
}

/// <summary>
/// 玩家基类；后续女侠、老外等角色脚本会继承Player
/// 继承：UnityControllerComponent
/// </summary>
public class Player : UnitControllerComponent, MoveCtrlInterface, AttackCtrlInterface
{
    /// <summary>
    /// 初始化Player
    /// </summary>
    public static Player Instance;
    #region 状态
    /// <summary>
    /// Player当前Action类型
    /// </summary>
    public PlayerAction PlayerAction = PlayerAction.Idle;
    /// <summary>
    /// Player的Action类型的阶段
    /// </summary>
    public AnimState AnimState = AnimState.None;
    /// <summary>
    /// Player当前播放的Action动画的阶段
    /// </summary>
    public Animancer.AnimancerState currentState;
    #endregion

    #region 能力
    /// <summary>
    /// 技能调用组件
    /// </summary>
    public SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    /// <summary>
    /// Player移动Skill实体
    /// </summary>
    public PlayerMoveEntity PlayerMoveEntity;
    /// <summary>
    /// Player旋转实体
    /// </summary>
    public PlayerRotateEntity PlayerRotateEntity;
    /// <summary>
    /// Player所有UI的控制
    /// </summary>
    public PlayerUIController PlayerUIController;
    /// <summary>
    /// Player的死亡检测+控制+死亡后事件
    /// </summary>
    public PlayerDeathEntity PlayerDeathEntity;
    /// <summary>
    /// Player鼠标控制
    /// </summary>
    public PlayerCursorEntity PlayerCursorEntity;
    /// <summary>
    /// 闪避Skill实体
    /// </summary>
    public PlayerEvadeEntity PlayerEvadeEntity;
    /// <summary>
    /// 轻击Skill实体
    /// </summary>
    public Skill_2_Attack1_Entity Skill_2_Attack1_Entity;
    /// <summary>
    /// 重击Skill实体
    /// </summary>
    public Skill_6_AttackHeavy Skill_6_AttackHeavy;
    /// <summary>
    /// 背包
    /// </summary>
    public PlayerInventoryEntity PlayerInventoryEntity;
    /// <summary>
    /// 装备实体
    /// </summary>
    public PlayerEquipEntity PlayerEquipEntity;
    /// <summary>
    /// 货币实体
    /// </summary>
    public PlayerMoneyEntity PlayerMoneyEntity;
    /// <summary>
    /// Player接收治疗实体
    /// </summary>
    public PlayerCureEntity PlayerCureEntity;
    /// <summary>
    /// Player受到伤害实体
    /// </summary>
    public PlayerDamageEntity PlayerDamageEntity;
    #endregion

    #region 控制
    /// <summary>
    /// 移动禁制
    /// </summary>
    public bool CanMove { get; set; }
    /// <summary>
    /// 攻击禁制
    /// </summary>
    public bool CanAttack { get; set; }
    /// <summary>
    /// 摄像机旋转禁止
    /// </summary>
    public bool CanCameraRot { get { return PlayerRotateEntity.RotEnable; } set { PlayerRotateEntity.RotEnable = value; } }
    /// <summary>
    /// 使用道具禁止
    /// </summary>
    public bool CanUseItem = true;
    /// <summary>
    /// 是否正在购物
    /// </summary>
    public bool IsShopping = false;
    /// <summary>
    /// 是否打开背包
    /// </summary>
    public bool IsOpenInventory = false;
    #endregion

    private void Awake()
    {
        if(Instance != null)
        {
            Instance.transform.position = this.transform.position;
            Instance.transform.rotation = this.transform.rotation;
            DestroyImmediate(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        // 初始化CombatEntity
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;
        currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        combatEntity.InitProperty(Application.persistentDataPath + "/PlayerProperty.json");

        // 挂载移动Skill实体
        PlayerMoveEntity = combatEntity.AddChild<PlayerMoveEntity>();
        PlayerMoveEntity.Init(this.transform, this.transform.GetChild(0));

        //挂载旋转实体
        PlayerRotateEntity = combatEntity.AddChild<PlayerRotateEntity>();
        PlayerRotateEntity.Init();

        // 挂载Death实体
        PlayerDeathEntity = combatEntity.AddChild<PlayerDeathEntity>();

        //鼠标指针实体
        PlayerCursorEntity = combatEntity.AddChild<PlayerCursorEntity>();
        PlayerCursorEntity.Init();

        // 挂载轻击Skill实体，已经挂载CanAttack
        Skill_2_Attack1_Entity = combatEntity.AddChild<Skill_2_Attack1_Entity>();
        Skill_2_Attack1_Entity.Init();

        // 挂载重击Skill实体，已经挂载CanAttack
        Skill_6_AttackHeavy = combatEntity.AddChild<Skill_6_AttackHeavy>();
        Skill_6_AttackHeavy.Init();

        // 挂载闪避Skill实体
        PlayerEvadeEntity = combatEntity.AddChild<PlayerEvadeEntity>();
        PlayerEvadeEntity.Init();

        //挂载装备实体
        PlayerEquipEntity = combatEntity.AddChild<PlayerEquipEntity>();
        PlayerEquipEntity.Init();

        //背包实体
        PlayerInventoryEntity = combatEntity.AddChild<PlayerInventoryEntity>();
        PlayerInventoryEntity.Init();

        //货币实体
        PlayerMoneyEntity = combatEntity.AddChild<PlayerMoneyEntity>();
        PlayerMoneyEntity.Init();

        //接收治疗实体
        PlayerCureEntity = combatEntity.AddChild<PlayerCureEntity>();
        PlayerCureEntity.Init();

        //受到伤害实体
        PlayerDamageEntity = combatEntity.AddChild<PlayerDamageEntity>();
        PlayerDamageEntity.Init();

        StartController();
    }

    public void CursorLock()
    {
        PlayerCursorEntity.CursorLock();
        PlayerCursorEntity.isEnabled = true;
    }
    public void CursorUnLock()
    {
        PlayerCursorEntity.CursorUnLock();
        PlayerCursorEntity.isEnabled = false;
    }


    private void OnTriggerStay(Collider other)
    {
        PlayerInventoryEntity.OperateItem(other);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventoryEntity.OperateItemExit(other);
    }

    public static void StopController()
    {
        PlayerCursorEntity.CursorUnLock();
        PlayerCursorEntity.isEnabled = false;
        Player.Instance.CanAttack = false;
        Player.Instance.CanMove = false;
        Player.Instance.CanCameraRot = false;
        Player.Instance.CanUseItem = false;
    }

    public static void StartController()
    {
        PlayerCursorEntity.CursorLock();
        PlayerCursorEntity.isEnabled = true;
        Player.Instance.CanAttack = true;
        Player.Instance.CanMove = true;
        Player.Instance.CanCameraRot = true;
        Player.Instance.CanUseItem = true;
    }

}
