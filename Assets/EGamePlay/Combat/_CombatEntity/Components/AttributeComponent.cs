using System.Collections.Generic;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 战斗属性数值更新事件
    /// </summary>
    public class AttributeUpdateEvent { public FloatNumeric Numeric; }

    /// <summary>
    /// 战斗属性数值组件，在这里管理角色所有战斗属性数值的存储、变更、刷新等
    /// </summary>
    public class AttributeComponent : Component
	{
        /// <summary>
        /// 战斗属性数值->(name,FloatNumeric)
        /// </summary>
        private readonly Dictionary<string, FloatNumeric> attributeNameNumerics = new Dictionary<string, FloatNumeric>();
        //private readonly Dictionary<AttributeType, FloatNumeric> attributeTypeNumerics = new Dictionary<AttributeType, FloatNumeric>();
        /// <summary>
        /// 战斗属性数值更新事件
        /// </summary>
        private readonly AttributeUpdateEvent attributeUpdateEvent = new AttributeUpdateEvent();
        /// <summary>
        /// 移动速度
        /// </summary>
        public FloatNumeric MoveSpeed { get { return attributeNameNumerics[nameof(AttributeType.MoveSpeed)]; } }
        /// <summary>
        /// 当前生命值
        /// </summary>
        public FloatNumeric HealthPoint { get { return attributeNameNumerics[nameof(AttributeType.HealthPoint)]; } }
        /// <summary>
        /// 生命值上限
        /// </summary>
        public FloatNumeric HealthPointMax { get { return attributeNameNumerics[nameof(AttributeType.HealthPointMax)]; } }
        /// <summary>
        /// 攻击力
        /// </summary>
        public FloatNumeric Attack { get { return attributeNameNumerics[nameof(AttributeType.Attack)]; } }
        /// <summary>
        /// 防御力(护甲)
        /// </summary>
        public FloatNumeric Defense { get { return attributeNameNumerics[nameof(AttributeType.Defense)]; } }
        /// <summary>
        /// 法术强度
        /// </summary>
        public FloatNumeric AbilityPower { get { return attributeNameNumerics[nameof(AttributeType.AbilityPower)]; } }
        /// <summary>
        /// 魔法抗性
        /// </summary>
        public FloatNumeric SpellResistance { get { return attributeNameNumerics[nameof(AttributeType.SpellResistance)]; } }
        /// <summary>
        /// 暴击概率
        /// </summary>
        public FloatNumeric CriticalProbability { get { return attributeNameNumerics[nameof(AttributeType.CriticalProbability)]; } }
        /// <summary>
        /// 造成伤害
        /// </summary>
        public FloatNumeric CauseDamage { get { return attributeNameNumerics[nameof(AttributeType.CauseDamage)]; } }


        public override void Setup()
        {
            Initialize();
        }
        /// <summary>
        /// 初始化战斗属性数值->生命值,最大生命值,移动速度,攻击力,护甲,暴击概率,造成伤害
        /// </summary>
        public void Initialize()
        {
            AddNumeric(AttributeType.HealthPointMax, 99_999);
            AddNumeric(AttributeType.HealthPoint, 99_999);
            AddNumeric(AttributeType.MoveSpeed, 1);
            AddNumeric(AttributeType.Attack, 1000);
            AddNumeric(AttributeType.Defense, 300);
            AddNumeric(AttributeType.CriticalProbability, 0.5f);
            AddNumeric(AttributeType.CauseDamage, 1);
        }
        /// <summary>
        /// 添加战斗属性数值
        /// </summary>
        /// <param name="attributeType">战斗属性数值的类型</param>
        /// <param name="baseValue">战斗属性数值的基础值</param>
        /// <returns>返回生成的战斗属性数值对象</returns>
        public FloatNumeric AddNumeric(AttributeType attributeType, float baseValue)
        {
            var numeric = Entity.AddChild<FloatNumeric>();
            numeric.Name = attributeType.ToString();
            numeric.SetBase(baseValue);
            attributeNameNumerics.Add(attributeType.ToString(), numeric);
            return numeric;
        }
        /// <summary>
        /// 获取战斗属性数值对象
        /// </summary>
        /// <param name="attributeName">需要获取的对象的名称</param>
        /// <returns></returns>
        public FloatNumeric GetNumeric(string attributeName)
        {
            return attributeNameNumerics[attributeName];
        }
        /// <summary>
        /// 更新战斗属性更新数值
        /// </summary>
        /// <param name="numeric"></param>
        public void OnNumericUpdate(FloatNumeric numeric)
        {
            attributeUpdateEvent.Numeric = numeric;
            Entity.Publish(attributeUpdateEvent);
        }
	}
}
