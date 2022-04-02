using EGamePlay.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 实体死亡事件
    /// </summary>
    public class EntityDeadEvent { public CombatEntity DeadEntity; }

    /// <summary>
    /// 战斗实体
    /// </summary>
    public sealed class CombatEntity : Entity, IPosition
    {
        /// <summary>
        /// 模型物体
        /// </summary>
        public GameObject ModelObject { get; set; }
        /// <summary>
        /// 属性对象
        /// </summary>
        public UnitPropertyEntity UnitPropertyEntity { get; private set; }
        public UnitStatusComponent UnitStatusComponent { get; private set; }
        /// <summary>
        /// 效果赋给行动能力
        /// </summary>
        public EffectAssignAbility EffectAssignAbility { get; private set; }
        /// <summary>
        /// 施法技能行动能力
        /// </summary>
        public SpellActionAbility SpellAbility { get; private set; }
        /// <summary>
        /// 移动行动能力
        /// </summary>
        public MotionActionAbility MotionAbility { get; private set; }
        /// <summary>
        /// 伤害行动能力
        /// </summary>
        public DamageActionAbility DamageAbility { get; private set; }
        /// <summary>
        /// 治疗行动能力
        /// </summary>
        public CureActionAbility CureAbility { get; private set; }
        /// <summary>
        /// 施加状态行动能力
        /// </summary>
        public AddStatusActionAbility AddStatusAbility { get; private set; }
        /// <summary>
        /// 施法普攻行动能力
        /// </summary>
        public AttackActionAbility SpellAttackAbility { get; private set; }
        /// <summary>
        /// 回合行动能力
        /// </summary>
        public RoundActionAbility RoundAbility { get; private set; }
        /// <summary>
        /// 起跳行动能力
        /// </summary>
        public JumpToActionAbility JumpToAbility { get; private set; }

        /// <summary>
        /// 普攻能力
        /// </summary>
        public AttackAbility AttackAbility { get; set; }
        /// <summary>
        /// 当前施法技能
        /// </summary>
        public SkillExecution CurrentSkillExecution { get; set; }
        /// <summary>
        /// <name,技能>
        /// </summary>
        public Dictionary<string, SkillAbility> NameSkills { get; set; } = new Dictionary<string, SkillAbility>();
        /// <summary>
        /// <KeyCode,技能>
        /// </summary>
        public Dictionary<KeyCode, SkillAbility> InputSkills { get; set; } = new Dictionary<KeyCode, SkillAbility>();
        /// <summary>
        /// <name,状态列表>
        /// </summary>
        public Dictionary<string, List<StatusAbility>> TypeIdStatuses { get; set; } = new Dictionary<string, List<StatusAbility>>();
        /// <summary>
        /// <Type,状态列表>
        /// </summary>
        public Dictionary<Type, List<StatusAbility>> TypeStatuses { get; set; } = new Dictionary<Type, List<StatusAbility>>();
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position { get; set; }
        /// <summary>
        /// 方位
        /// </summary>
        public float Direction { get; set; }
        /// <summary>
        /// 行为控制
        /// </summary>
        public ActionControlType ActionControlType { get; set; }


        public override void Awake()
        {
            //添加战斗属性数值组件
            AddComponent<AttributeComponent>();
            //添加行为组件
            AddComponent<ActionComponent>();
            //添加行动点管理器
            AddComponent<ActionPointComponent>();
            //添加条件管理组件
            AddComponent<ConditionComponent>();
            //添加状态管理组件
            AddComponent<StatusComponent>();
            //添加技能管理组件
            AddComponent<SkillComponent>();
            //添加技能施法组件
            AddComponent<SpellComponent>();

            SpellAbility = AttachAction<SpellActionAbility>();
            MotionAbility = AttachAction<MotionActionAbility>();
            DamageAbility = AttachAction<DamageActionAbility>();
            CureAbility = AttachAction<CureActionAbility>();
            AddStatusAbility = AttachAction<AddStatusActionAbility>();
            SpellAttackAbility = AttachAction<AttackActionAbility>();
            EffectAssignAbility = AttachAction<EffectAssignAbility>();
            RoundAbility = AttachAction<RoundActionAbility>();
            JumpToAbility = AttachAction<JumpToActionAbility>();

            AttackAbility = AttachAbility<AttackAbility>(null);
            UnitPropertyEntity = AddChild<UnitPropertyEntity>();//new UnitPropertyEntity();
            UnitStatusComponent = AddChild<UnitStatusComponent>();//new UnitStatusComponent();
            InitProperty(Application.dataPath + "/test.json");
        }

        public void InitProperty(string config)
        {
            UnitPropertyEntity.InitData(config);
        }

        /// <summary>
        /// 发起行动
        /// </summary>
        public T MakeAction<T>() where T : ActionExecution
        {
            var action = Parent.GetComponent<CombatActionManageComponent>().CreateAction<T>(this);
            return action;
        }

        #region 行动点事件
        /// <summary>
        /// 添加行动监听事件
        /// </summary>
        /// <param name="actionPointType">行动点类型</param>
        /// <param name="action">事件</param>
        public void ListenActionPoint(ActionPointType actionPointType, Action<ActionExecution> action)
        {
            GetComponent<ActionPointComponent>().AddListener(actionPointType, action);
        }
        /// <summary>
        /// 移除行动监听事件
        /// </summary>
        /// <param name="actionPointType">行动点类型</param>
        /// <param name="action">事件</param>
        public void UnListenActionPoint(ActionPointType actionPointType, Action<ActionExecution> action)
        {
            GetComponent<ActionPointComponent>().RemoveListener(actionPointType, action);
        }
        /// <summary>
        /// 触发监听事件
        /// </summary>
        /// <param name="actionPointType">需触发的行动点类型</param>
        /// <param name="action">战斗行动</param>
        public void TriggerActionPoint(ActionPointType actionPointType, ActionExecution action)
        {
            GetComponent<ActionPointComponent>().TriggerActionPoint(actionPointType, action);
        }
        #endregion

        #region 条件事件
        /// <summary>
        /// 添加条件行动监听事件
        /// </summary>
        /// <param name="conditionType">条件类型</param>
        /// <param name="action">事件</param>
        /// <param name="paramObj">参数</param>
        public void ListenerCondition(ConditionType conditionType, Action action, object paramObj = null)
        {
            GetComponent<ConditionComponent>().AddListener(conditionType, action, paramObj);
        }
        /// <summary>
        /// 移除条件行动监听事件
        /// </summary>
        /// <param name="conditionType">条件类型</param>
        /// <param name="action">事件</param>
        public void UnListenCondition(ConditionType conditionType, Action action)
        {
            GetComponent<ConditionComponent>().RemoveListener(conditionType, action);
        }
        #endregion

        /// <summary>
        /// 受到伤害事件->减少血量
        /// </summary>
        /// <param name="combatAction">DamageAction</param>
        public void ReceiveDamage(ActionExecution combatAction)
        {
            var damageAction = combatAction as DamageAction;
            UnitPropertyEntity.HP.Minus(damageAction.DamageValue);
        }
        /// <summary>
        /// 回复血量事件
        /// </summary>
        /// <param name="combatAction">CureAction</param>
        public void ReceiveCure(ActionExecution combatAction)
        {
            var cureAction = combatAction as CureAction;
            UnitPropertyEntity.HP.Minus(cureAction.CureValue);

        }
        /// <summary>
        /// 检测是否死亡,即当前血量是否小于0
        /// </summary>
        /// <returns></returns>
        public bool CheckDead()
        {
            return UnitPropertyEntity.HP.Value <= 0;
        }

        /// <summary>
        /// 挂载能力，技能、被动、buff等都通过这个接口挂载
        /// </summary>
        /// <param name="configObject">AbilityEntity->需要挂载的能力</param>
        /// <returns>挂载的对象</returns>
        private T AttachAbility<T>(object configObject) where T : AbilityEntity
        {
            var ability = this.AddChild<T>(configObject);
            return ability;
        }
        /// <summary>
        /// 挂载行动Action
        /// </summary>
        /// <typeparam name="T">ActionAbility->需要挂载的行动Action</typeparam>
        /// <returns>挂载的对象</returns>
        public T AttachAction<T>() where T : ActionAbility
        {
            var action = AttachAbility<T>(null);
            return action;
        }
        /// <summary>
        /// 获取对应的ActionAbility
        /// </summary>
        /// <typeparam name="T">ActionAbility</typeparam>
        public T GetAction<T>() where T : ActionAbility
        {
            return GetChild<T>();
        }
        /// <summary>
        /// 挂载技能,并加入NameSkills字典
        /// </summary>
        /// <typeparam name="T">SkillAbility</typeparam>
        /// <param name="configObject">需要挂载的对象</param>
        /// <returns>挂载的对象</returns>
        public T AttachSkill<T>(object configObject) where T : SkillAbility
        {
            var skill = AttachAbility<T>(configObject);
            NameSkills.Add(skill.SkillConfig.Name, skill);
            return skill;
        }
        /// <summary>
        /// 挂载状态,并加入TypeIdStatuses字典
        /// </summary>
        /// <typeparam name="T">StatusAbility</typeparam>
        /// <param name="configObject">需要挂载的对象</param>
        /// <returns>挂载的对象</returns>
        public T AttachStatus<T>(object configObject) where T : StatusAbility
        {
            var status = AttachAbility<T>(configObject);
            if (!TypeIdStatuses.ContainsKey(status.StatusConfig.ID))
            {
                TypeIdStatuses.Add(status.StatusConfig.ID, new List<StatusAbility>());
            }
            TypeIdStatuses[status.StatusConfig.ID].Add(status);
            return status;
        }
        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="statusAbility">需要移除的状态</param>
        public void OnStatusRemove(StatusAbility statusAbility)
        {
            TypeIdStatuses[statusAbility.StatusConfig.ID].Remove(statusAbility);
            if (TypeIdStatuses[statusAbility.StatusConfig.ID].Count == 0)
            {
                TypeIdStatuses.Remove(statusAbility.StatusConfig.ID);
            }
            this.Publish(new RemoveStatusEvent() { CombatEntity = this, Status = statusAbility, StatusId = statusAbility.Id });
        }
        /// <summary>
        /// 绑定技能输入,并加入InputSkills字典
        /// </summary>
        /// <param name="abilityEntity">需要绑定的技能</param>
        /// <param name="keyCode">绑定的按键</param>
        public void BindSkillInput(SkillAbility abilityEntity, KeyCode keyCode)
        {
            InputSkills.Add(keyCode, abilityEntity);
            abilityEntity.TryActivateAbility();
        }
        /// <summary>
        /// 是否有对应状态类型
        /// </summary>
        /// <typeparam name="T">StatusAbility</typeparam>
        /// <param name="statusType">状态类型</param>
        /// <returns></returns>
        public bool HasStatus<T>(T statusType) where T : StatusAbility
        {
            return TypeStatuses.ContainsKey(statusType.GetType());
        }
        /// <summary>
        /// 是否有对应状态类型ID
        /// </summary>
        /// <param name="statusTypeId">状态类型ID</param>
        /// <returns></returns>
        public bool HasStatus(string statusTypeId)
        {
            return TypeIdStatuses.ContainsKey(statusTypeId);
        }
        /// <summary>
        /// 获取对应状态类型ID的状态能力
        /// </summary>
        /// <param name="statusTypeId">状态类型ID</param>
        /// <returns>StatusAbility</returns>
        public StatusAbility GetStatus(string statusTypeId)
        {
            return TypeIdStatuses[statusTypeId][0];
        }

        public int GetStatusNum(string statusTypeId)
        {
            return TypeIdStatuses[statusTypeId].Count;
        }

        #region 回合制战斗
        /// <summary>
        /// 位置
        /// </summary>
        public int SeatNumber { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public int JumpToTime { get; set; }
        /// <summary>
        /// 是否是玩家
        /// </summary>
        public bool IsHero { get; set; }
        /// <summary>
        /// 是否是敌人
        /// </summary>
        public bool IsMonster => IsHero == false;
        /// <summary>
        /// 获取敌人的位置
        /// </summary>
        /// <param name="seat">位置</param>
        /// <returns></returns>
        public CombatEntity GetEnemy(int seat)
        {
            if (IsHero)
            {
                return GetParent<CombatContext>().GetMonster(seat);
            }
            else
            {
                return GetParent<CombatContext>().GetHero(seat);
            }
        }
        /// <summary>
        /// 获取友军的位置
        /// </summary>
        /// <param name="seat">位置</param>
        /// <returns></returns>
        public CombatEntity GetTeammate(int seat)
        {
            if (IsHero)
            {
                return GetParent<CombatContext>().GetHero(seat);
            }
            else
            {
                return GetParent<CombatContext>().GetMonster(seat);
            }
        }
        #endregion
    }

    /// <summary>
    /// 移除状态事件
    /// </summary>
    public class RemoveStatusEvent
    {
        /// <summary>
        /// 战斗实体
        /// </summary>
        public CombatEntity CombatEntity { get; set; }
        /// <summary>
        /// 状态能力
        /// </summary>
        public StatusAbility Status { get; set; }
        /// <summary>
        /// 状态ID
        /// </summary>
        public long StatusId { get; set; }
    }
}