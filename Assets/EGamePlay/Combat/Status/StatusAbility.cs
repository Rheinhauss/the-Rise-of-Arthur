using EGamePlay.Combat;
using System.Collections.Generic;
using ET;
using UnityEngine;

#if !EGAMEPLAY_EXCEL
namespace EGamePlay.Combat
{
    public partial class StatusAbility : AbilityEntity
    {
        //投放者、施术者
        //public override CombatEntity OwnerEntity { get; set; }
        public StatusConfigObject StatusConfig { get; set; }
        public FloatModifier NumericModifier { get; set; }
        public bool IsChildStatus { get; set; }
        public int Duration { get; set; }
        public ChildStatus ChildStatusData { get; set; }
        private List<StatusAbility> ChildrenStatuses { get; set; } = new List<StatusAbility>();


        public override void Awake(object initData)
        {
            base.Awake(initData);
            var statusConfig = StatusConfig = initData as StatusConfigObject;
            Name = StatusConfig.Name;
            //子状态效果
            if (StatusConfig.EnableChildrenStatuses)
            {

            }
            //行为禁制
            if (StatusConfig.EnabledStateModify)
            {

            }
            //属性修饰
            if (StatusConfig.EnabledAttributeModify)
            {
                AddComponent<StatusAttributeModifyComponent>();
            }
            //逻辑触发
            if (StatusConfig.EnabledLogicTrigger)
            {
                AddComponent<AbilityEffectComponent>(StatusConfig.Effects);
            }
        }

        public void ProccessInputKVParams(Dictionary<string, string> Params)
        {
            for (int i = 0; i < StatusConfig.Effects.Count; i++)
            {
                var abilityEffect = AbilityEffectComponent.GetEffect(i);
                var logicEffect = abilityEffect.EffectConfig;

                if (logicEffect.EffectTriggerType == EffectTriggerType.Interval)
                {
                    if (!string.IsNullOrEmpty(logicEffect.Interval))
                    {
                        var intervalComponent = abilityEffect.GetComponent<EffectIntervalTriggerComponent>();
                        intervalComponent.IntervalValue = logicEffect.Interval;
                        foreach (var aInputKVItem in Params)
                        {
                            intervalComponent.IntervalValue = intervalComponent.IntervalValue.Replace(aInputKVItem.Key, aInputKVItem.Value);
                        }
                    }
                }
                if (logicEffect.EffectTriggerType == EffectTriggerType.Condition)
                {
                    if (!string.IsNullOrEmpty(logicEffect.ConditionParam))
                    {
                        var conditionComponent = abilityEffect.GetComponent<EffectConditionTriggerComponent>();
                        conditionComponent.ConditionParamValue = logicEffect.ConditionParam;
                        foreach (var aInputKVItem in Params)
                        {
                            conditionComponent.ConditionParamValue = conditionComponent.ConditionParamValue.Replace(aInputKVItem.Key, aInputKVItem.Value);
                        }
                    }
                }

                if (logicEffect is DamageEffect damage)
                {
                    var damageComponent = abilityEffect.GetComponent<EffectDamageComponent>();
                    damageComponent.DamageValueProperty = damage.DamageValueFormula;
                    foreach (var aInputKVItem in Params)
                    {
                        if (!string.IsNullOrEmpty(damageComponent.DamageValueProperty))
                        {
                            damageComponent.DamageValueProperty = damageComponent.DamageValueProperty.Replace(aInputKVItem.Key, aInputKVItem.Value);
                        }
                    }
                }
                if (logicEffect is CureEffect cure)
                {
                    var cureComponent = abilityEffect.GetComponent<EffectCureComponent>();
                    cureComponent.CureValueProperty = cure.CureValueFormula;
                    foreach (var aInputKVItem in Params)
                    {
                        if (!string.IsNullOrEmpty(cureComponent.CureValueProperty))
                        {
                            cureComponent.CureValueProperty = cureComponent.CureValueProperty.Replace(aInputKVItem.Key, aInputKVItem.Value);
                        }
                    }
                }
            }
        }

        //激活
        public override void ActivateAbility()
        {
            base.ActivateAbility();

            //子状态效果
            if (StatusConfig.EnableChildrenStatuses)
            {
                foreach (var childStatusData in StatusConfig.ChildrenStatuses)
                {
                    var status = ParentEntity.AttachStatus<StatusAbility>(childStatusData.StatusConfigObject);
                    status.OwnerEntity = OwnerEntity;
                    status.IsChildStatus = true;
                    status.ChildStatusData = childStatusData;
                    status.ProccessInputKVParams(childStatusData.Params);
                    status.TryActivateAbility();
                    ChildrenStatuses.Add(status);
                }
            }
            //行为禁制
            if (StatusConfig.EnabledStateModify)
            {
                ParentEntity.ActionControlType = ParentEntity.ActionControlType | StatusConfig.ActionControlType;
                //Log.Debug($"{OwnerEntity.ActionControlType}");
                if (ParentEntity.ActionControlType.HasFlag(ActionControlType.MoveForbid))
                {
                    MoveCtrl(false);
                }
                if (ParentEntity.ActionControlType.HasFlag(ActionControlType.AttackForbid))
                {
                    AttackCtrl(false);
                }
            }
            //属性修饰
            if (StatusConfig.EnabledAttributeModify)
            {
                if (StatusConfig.PropertyType != PropertyType.None && StatusConfig.NumericValue != "")
                {
                    var numericValue = StatusConfig.NumericValue;
                    if (IsChildStatus)
                    {
                        foreach (var paramItem in ChildStatusData.Params)
                        {
                            numericValue = numericValue.Replace(paramItem.Key, paramItem.Value);
                        }
                    }
                    numericValue = numericValue.Replace("%", "");
                    var expression = ExpressionHelper.ExpressionParser.EvaluateExpression(numericValue);
                    var value = (float)expression.Value;
                    NumericModifier = new FloatModifier() { Value = value };

                    var PropertyType = StatusConfig.PropertyType.ToString();
                    if (StatusConfig.ModifyType == ModifyType.Add)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).AddAddModifier(NumericModifier);
                    }
                    else if (StatusConfig.ModifyType == ModifyType.PercentAdd)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).AddPctAddModifier(NumericModifier);
                    }
                    else if (StatusConfig.ModifyType == ModifyType.FinalAdd)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).AddFinalAddModifier(NumericModifier);
                    }
                    else if (StatusConfig.ModifyType == ModifyType.FinalPercentAdd)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).AddFinalPctAddModifier(NumericModifier);
                    }
                }
            }
            //逻辑触发
            if (StatusConfig.EnabledLogicTrigger)
            {
                AbilityEffectComponent.Enable = true;
            }
        }

        //结束
        public override void EndAbility()
        {
            //子状态效果
            if (StatusConfig.EnableChildrenStatuses)
            {
                foreach (var item in ChildrenStatuses)
                {
                    item.EndAbility();
                }
                ChildrenStatuses.Clear();
            }
            //行为禁制
            if (StatusConfig.EnabledStateModify)
            {
                ParentEntity.ActionControlType = ParentEntity.ActionControlType & (~StatusConfig.ActionControlType);
                //Log.Debug($"{OwnerEntity.ActionControlType}");
                if (ParentEntity.ActionControlType.HasFlag(ActionControlType.MoveForbid) == false)
                {
                    MoveCtrl(true);
                }
                if (ParentEntity.ActionControlType.HasFlag(ActionControlType.AttackForbid) == false)
                {
                    AttackCtrl(true);
                }
            }
            //属性修饰
            if (StatusConfig.EnabledAttributeModify)
            {
                if (StatusConfig.PropertyType != PropertyType.None && StatusConfig.NumericValue != "")
                {
                    var PropertyType = StatusConfig.PropertyType.ToString();
                    if (StatusConfig.ModifyType == ModifyType.Add)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).RemoveAddModifier(NumericModifier);
                    }
                    else if (StatusConfig.ModifyType == ModifyType.PercentAdd)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).RemovePctAddModifier(NumericModifier);
                    }
                    else if (StatusConfig.ModifyType == ModifyType.FinalAdd)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).RemoveFinalAddModifier(NumericModifier);
                    }
                    else if (StatusConfig.ModifyType == ModifyType.FinalPercentAdd)
                    {
                        ParentEntity.GetChild<UnitPropertyEntity>().GetPropertyDictNum(PropertyType).RemoveFinalPctAddModifier(NumericModifier);
                    }
                }
            }
            //逻辑触发
            if (StatusConfig.EnabledLogicTrigger)
            {

            }

            NumericModifier = null;
            ParentEntity.OnStatusRemove(this);
            base.EndAbility();
        }
        //public override AbilityExecution CreateExecution()
        //{
        //    var execution = OwnerEntity.AddChild<StatusExecution>(this);
        //    execution.AddComponent<UpdateComponent>();
        //    return execution;
        //}
        public int GetDuration()
        {
            return Duration;
        }

        public void MoveCtrl(bool b)
        {
            try
            {
                ParentEntity.ModelObject.GetComponent<Player>().CanMove = b;
            }
            catch
            {
                //通过CombatEntity获得敌人的移动脚本，将CanMove = false；这样敌人不可移动
                ParentEntity.ModelObject.GetComponent<Enemy>().CanMove = b;
            }
        }

        public void AttackCtrl(bool b)
        {
            try
            {
                ParentEntity.ModelObject.GetComponent<Player>().CanAttack = b;
            }
            catch
            {
                //通过CombatEntity获得敌人的移动脚本，将CanMove = false；这样敌人不可移动
                ParentEntity.ModelObject.GetComponent<Enemy>().CanAttack = b;
            }
        }

    }
}
#endif