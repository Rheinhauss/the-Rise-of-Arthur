using DG.Tweening;
using DG.Tweening.Plugins.Options;
using EGamePlay;
using EGamePlay.Combat;
using PixelCrushers.DialogueSystem.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ö�٣������ִ�еļ�������
/// վ�����ƶ������
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
/// ö�٣�Action���͵Ľ׶�
/// ǰҡ��ǿ�ƺ�ҡ����ҡ
/// </summary>
public enum AnimState
{
    None,
    Pre,
    ForcePost,
    Post
}

/// <summary>
/// ��һ��ࣻ����Ů��������Ƚ�ɫ�ű���̳�Player
/// �̳У�UnityControllerComponent
/// </summary>
public class Player : UnitControllerComponent, MoveCtrlInterface, AttackCtrlInterface
{
    /// <summary>
    /// ��ʼ��Player
    /// </summary>
    public static Player Instance;
    #region ״̬
    /// <summary>
    /// Player��ǰAction����
    /// </summary>
    public PlayerAction PlayerAction = PlayerAction.Idle;
    /// <summary>
    /// Player��Action���͵Ľ׶�
    /// </summary>
    public AnimState AnimState = AnimState.None;
    /// <summary>
    /// Player��ǰ���ŵ�Action�����Ľ׶�
    /// </summary>
    public Animancer.AnimancerState currentState;
    #endregion

    #region ����
    /// <summary>
    /// ���ܵ������
    /// </summary>
    public SpellComponent SpellComponent => combatEntity.GetComponent<SpellComponent>();
    /// <summary>
    /// Player�ƶ�Skillʵ��
    /// </summary>
    public PlayerMoveEntity PlayerMoveEntity;
    /// <summary>
    /// Player��תʵ��
    /// </summary>
    public PlayerRotateEntity PlayerRotateEntity;
    /// <summary>
    /// Player����UI�Ŀ���
    /// </summary>
    public PlayerUIController PlayerUIController;
    /// <summary>
    /// Player���������+����+�������¼�
    /// </summary>
    public PlayerDeathEntity PlayerDeathEntity;
    /// <summary>
    /// Player������
    /// </summary>
    public PlayerCursorEntity PlayerCursorEntity;
    /// <summary>
    /// ����Skillʵ��
    /// </summary>
    public PlayerEvadeEntity PlayerEvadeEntity;
    /// <summary>
    /// ���Skillʵ��
    /// </summary>
    public Skill_2_Attack1_Entity Skill_2_Attack1_Entity;
    /// <summary>
    /// �ػ�Skillʵ��
    /// </summary>
    public Skill_6_AttackHeavy Skill_6_AttackHeavy;
    /// <summary>
    /// ����
    /// </summary>
    public PlayerInventoryEntity PlayerInventoryEntity;
    /// <summary>
    /// װ��ʵ��
    /// </summary>
    public PlayerEquipEntity PlayerEquipEntity;
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public PlayerMoneyEntity PlayerMoneyEntity;
    /// <summary>
    /// Player��������ʵ��
    /// </summary>
    public PlayerCureEntity PlayerCureEntity;
    /// <summary>
    /// Player�ܵ��˺�ʵ��
    /// </summary>
    public PlayerDamageEntity PlayerDamageEntity;
    /// <summary>
    /// Player��λʵ��
    /// </summary>
    public PlayerResetEntity PlayerResetEntity;
    #endregion

    #region ����
    /// <summary>
    /// �ƶ�����
    /// </summary>
    public bool CanMove { get; set; }
    /// <summary>
    /// ��������
    /// </summary>
    public bool CanAttack { get; set; }
    /// <summary>
    /// �������ת��ֹ
    /// </summary>
    public bool CanCameraRot { get { return PlayerRotateEntity.RotEnable; } set { PlayerRotateEntity.RotEnable = value; } }
    /// <summary>
    /// ʹ�õ��߽�ֹ
    /// </summary>
    public bool CanUseItem = true;
    /// <summary>
    /// �Ƿ����ڹ���
    /// </summary>
    public bool IsShopping = false;
    /// <summary>
    /// �Ƿ�򿪱���
    /// </summary>
    public bool IsOpenInventory = false;
    /// <summary>
    /// �Ƿ��ܹ��򿪱���
    /// </summary>
    public bool CanOpenInventory { get; set; }
    #endregion

    private void Awake()
    {
        if(Instance != null)
        {
            Instance.transform.position = this.transform.position;
            Instance.transform.rotation = this.transform.rotation;
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        // ��ʼ��CombatEntity
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.ModelObject = this.gameObject;
        currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        combatEntity.InitProperty(Application.streamingAssetsPath + "/PlayerProperty.json");

        // �����ƶ�Skillʵ��
        PlayerMoveEntity = combatEntity.AddChild<PlayerMoveEntity>();
        PlayerMoveEntity.Init(this.transform, this.transform.GetChild(0));

        //������תʵ��
        PlayerRotateEntity = combatEntity.AddChild<PlayerRotateEntity>();
        PlayerRotateEntity.Init();

        // ����Deathʵ��
        PlayerDeathEntity = combatEntity.AddChild<PlayerDeathEntity>();

        //���ָ��ʵ��
        PlayerCursorEntity = combatEntity.AddChild<PlayerCursorEntity>();
        PlayerCursorEntity.Init();

        // �������Skillʵ�壬�Ѿ�����CanAttack
        Skill_2_Attack1_Entity = combatEntity.AddChild<Skill_2_Attack1_Entity>();
        Skill_2_Attack1_Entity.Init();

        // �����ػ�Skillʵ�壬�Ѿ�����CanAttack
        Skill_6_AttackHeavy = combatEntity.AddChild<Skill_6_AttackHeavy>();
        Skill_6_AttackHeavy.Init();

        // ��������Skillʵ��
        PlayerEvadeEntity = combatEntity.AddChild<PlayerEvadeEntity>();
        PlayerEvadeEntity.Init();

        //����װ��ʵ��
        PlayerEquipEntity = combatEntity.AddChild<PlayerEquipEntity>();
        PlayerEquipEntity.Init();

        //����ʵ��
        PlayerInventoryEntity = combatEntity.AddChild<PlayerInventoryEntity>();
        PlayerInventoryEntity.Init();

        //����ʵ��
        PlayerMoneyEntity = combatEntity.AddChild<PlayerMoneyEntity>();
        PlayerMoneyEntity.Init();

        //��������ʵ��
        PlayerCureEntity = combatEntity.AddChild<PlayerCureEntity>();
        PlayerCureEntity.Init();

        //�ܵ��˺�ʵ��
        PlayerDamageEntity = combatEntity.AddChild<PlayerDamageEntity>();
        PlayerDamageEntity.Init();

        //��λʵ��
        PlayerResetEntity = combatEntity.AddChild<PlayerResetEntity>();
        PlayerResetEntity.Init();

        StartController();
        CanOpenInventory = true;
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

    public void SwitchScene()
    {
        if(currentState != null)
        {
            currentState.Stop();
        }
        currentState = unitAnimatorComponent.PlayFade(unitAnimatorComponent.animationClipsDict["SwordsmanIdle"]);
        AnimState = AnimState.None;
        Skill_1_Move.CurrentPosition = CurrentPosition.None;
        Skill_1_Move.movePosition = Vector3.zero;
        PlayerAction = PlayerAction.Idle;
        PlayerResetEntity.SetResurrectionPoint(GameObject.Find("PlayerDefaultResurrectionPoint").transform);
    }

    private void OnDestroy()
    {
        if(combatEntity != null)
            Entity.Destroy(combatEntity);
    }

}
