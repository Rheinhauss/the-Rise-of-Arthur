using EGamePlay;
using EGamePlay.Combat;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[LabelText("属性类型")]
public enum PropertyType 
{
    [LabelText("空")]
    None,
    [LabelText("生命值上限")]
    MaxHP,
    [LabelText("当前生命值")]
    CurrentHP,
    [LabelText("MP上限")]
    MaxMP,
    [LabelText("当前MP")]
    CurrentMP,
    [LabelText("护盾值上限")]
    MaxShield,
    [LabelText("当前护盾值")]
    CurrentShield,
    [LabelText("护盾值消散比例")]
    ShieldDissipateRate,
    [LabelText("移动速度")]
    MoveSpeed,
    [LabelText("攻击力")]
    AttackPower,
    [LabelText("攻击速度")]
    AttackSpeed,
    [LabelText("伤害修正(全能加成)")]
    Versality,
    [LabelText("元素伤害加成")]
    ElementalDmgModifier,
    [LabelText("金元素伤害加成")]
    JinDmgModifier,
    [LabelText("木元素伤害加成")]
    MuDmgModifier,
    [LabelText("水元素伤害加成")]
    ShuiDmgModifier,
    [LabelText("火元素伤害加成")]
    HuoDmgModifier,
    [LabelText("土元素伤害加成")]
    TuDmgModifier,
    [LabelText("暴击率")]
    CriticalChance,
    [LabelText("暴击伤害")]
    CriticalDamage,
    [LabelText("护盾减伤修正")]
    TakenDmgModifier,
    [LabelText("元素抗性修正")]
    TakenElementalDmgModifier,
    [LabelText("击退抗性")]
    KnockBackResistance,
    [LabelText("硬直抗性")]
    RigidResistance,
    [LabelText("己方")]
    isOwn,
    [LabelText("敌方")]
    isFly,
}

public class UnitPropertyEntity : Entity
{
    /// <summary>
    /// 战斗属性数值->(name,FloatNumeric)
    /// </summary>
    private readonly Dictionary<string, HealthPoint> propertyNameNumericsDict = new Dictionary<string, HealthPoint>();


    public UnitPropertyComponent UnitPropertyComponent = new UnitPropertyComponent();
    /// <summary>
    /// HP和MaxHP
    /// </summary>
    public HealthPoint HP { get { return GetPropertyDictHealthPoint(PropertyType.CurrentHP); } }
    /// <summary>
    /// MP和MaxMP
    /// </summary>
    public HealthPoint MP { get { return GetPropertyDictHealthPoint(PropertyType.CurrentMP); } }
    /// <summary>
    /// Shield和MaxShield
    /// </summary>
    public HealthPoint Shield { get{ return GetPropertyDictHealthPoint(PropertyType.CurrentShield); } }
    /// <summary>
    /// 护盾值消散比例  每秒护盾消散比例，间接决定最大护盾
    /// </summary>
    public HealthPoint ShieldDissipateRate { get{ return GetPropertyDictHealthPoint(PropertyType.ShieldDissipateRate); } }
    /// <summary>
    /// 移动速度  角色每秒能移动的最大距离
    /// </summary>
    public HealthPoint MoveSpeed { get{ return GetPropertyDictHealthPoint(PropertyType.MoveSpeed); } }
    /// <summary>
    /// 攻击力   角色的基本攻击力
    /// </summary>
    public HealthPoint AttackPower { get{ return GetPropertyDictHealthPoint(PropertyType.AttackPower); } }
    /// <summary>
    /// 攻击速度  角色各类攻击动画的播放速度
    /// </summary>
    public HealthPoint AttackSpeed { get{ return GetPropertyDictHealthPoint(PropertyType.AttackSpeed); } }
    /// <summary>
    /// 伤害修正(全能加成)    伤害公式中全能加成项
    /// </summary>
    public HealthPoint Versality { get{ return GetPropertyDictHealthPoint(PropertyType.Versality); } }
    /// <summary>
    /// 元素伤害加成    
    /// </summary>
    public HealthPoint ElementalDmgModifier { get{ return GetPropertyDictHealthPoint(PropertyType.ElementalDmgModifier); } }
    /// <summary>
    /// 金元素伤害加成
    /// </summary>
    public HealthPoint JinDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.JinDmgModifier); } }
    /// <summary>
    /// 木元素伤害加成
    /// </summary>
    public HealthPoint MuDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.MuDmgModifier); } }
    /// <summary>
    /// 水元素伤害加成
    /// </summary>
    public HealthPoint ShuiDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.ShuiDmgModifier); } }
    /// <summary>
    /// 火元素伤害加成
    /// </summary>
    public HealthPoint HuoDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.HuoDmgModifier); } }
    /// <summary>
    /// 土元素伤害加成
    /// </summary>
    public HealthPoint TuDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.TuDmgModifier); } }
    /// <summary>
    /// 暴击率  发起者为该单位的伤害的暴击概率
    /// </summary>
    public HealthPoint CriticalChance { get { return GetPropertyDictHealthPoint(PropertyType.CriticalChance); } }
    /// <summary>
    /// 暴击伤害  发起者为该单位的伤害的暴击伤害
    /// </summary>
    public HealthPoint CriticalDamage { get{ return GetPropertyDictHealthPoint(PropertyType.CriticalDamage); } }
    /// <summary>
    /// 护盾减伤修正（需与程序讨论）			
    /// </summary>
    //减伤百分比 受伤百分比修正
    public HealthPoint TakenDmgModifier { get{ return GetPropertyDictHealthPoint(PropertyType.TakenDmgModifier); } }
    /// <summary>
    /// 元素抗性修正百分比 5+1(无属性)属性抗性分别计算
    /// </summary>
    public HealthPoint TakenElementalDmgModifier { get{ return GetPropertyDictHealthPoint(PropertyType.TakenElementalDmgModifier); } }
    /// <summary>
    /// 击退抗性  受到击退效果时，被击退的距离计算时的减少百分比
    /// </summary>
    public HealthPoint KnockBackResistance { get{ return GetPropertyDictHealthPoint(PropertyType.KnockBackResistance); } }
    /// <summary>
    /// 受到硬直效果时，硬直的持续时间计算时的减少百分比
    /// </summary>
    public HealthPoint RigidResistance { get{ return GetPropertyDictHealthPoint(PropertyType.RigidResistance); } }

    /// <summary>
    /// 获取字典中的HelthPoint的值
    /// </summary>
    /// <param name="propertyStr"></param>
    /// <returns></returns>
    public FloatNumeric GetPropertyDictNum(string propertyStr)
    {
        if (propertyStr.Contains("Max"))
        {
            propertyStr = propertyStr.Replace("Max", "Current");
            return propertyNameNumericsDict[propertyStr].HealthPointMaxNumeric;
        }
        else
        {
            return propertyNameNumericsDict[propertyStr].HealthPointNumeric;
        }
    }
    public FloatNumeric GetPropertyDictNum(PropertyType propertyType)
    {
        return GetPropertyDictNum(propertyType.ToString());
    }
    public HealthPoint GetPropertyDictHealthPoint(string propertyStr)
    {
        return propertyNameNumericsDict[propertyStr];
    }
    public HealthPoint GetPropertyDictHealthPoint(PropertyType propertyType)
    {
        return propertyNameNumericsDict[propertyType.ToString()];
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="configpath"></param>
    public void InitData(string configpath)
    {
        HealthPoint tmp;
        UnitPropertyComponent.ReadToSelf(configpath);
        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(UnitPropertyComponent.MaxHP);
        tmp.SetBaseValue(UnitPropertyComponent.CurrentHP);
        propertyNameNumericsDict.Add(PropertyType.CurrentHP.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(UnitPropertyComponent.MaxMP);
        tmp.SetBaseValue(UnitPropertyComponent.CurrentMP);
        propertyNameNumericsDict.Add(PropertyType.CurrentMP.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(UnitPropertyComponent.MaxShield);
        tmp.SetBaseValue(UnitPropertyComponent.CurrentShield);
        propertyNameNumericsDict.Add(PropertyType.CurrentShield.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(1.0f);
        tmp.SetBaseValue(UnitPropertyComponent.ShieldDissipateRate);
        propertyNameNumericsDict.Add(PropertyType.ShieldDissipateRate.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.MoveSpeed);
        propertyNameNumericsDict.Add(PropertyType.MoveSpeed.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.AttackPower);
        propertyNameNumericsDict.Add(PropertyType.AttackPower.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.AttackSpeed);
        propertyNameNumericsDict.Add(PropertyType.AttackSpeed.ToString(), tmp);


        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.Versality);
        propertyNameNumericsDict.Add(PropertyType.Versality.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.ElementalDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.ElementalDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.JinDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.JinDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.MuDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.MuDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.ShuiDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.ShuiDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.HuoDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.HuoDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.TuDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.TuDmgModifier.ToString(), tmp);


        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.CriticalChance);
        propertyNameNumericsDict.Add(PropertyType.CriticalChance.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.CriticalDamage);
        propertyNameNumericsDict.Add(PropertyType.CriticalDamage.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(1);
        tmp.SetBaseValue(UnitPropertyComponent.TakenDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.TakenDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(10000);
        tmp.SetBaseValue(UnitPropertyComponent.TakenElementalDmgModifier);
        propertyNameNumericsDict.Add(PropertyType.TakenElementalDmgModifier.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(1);
        tmp.SetBaseValue(UnitPropertyComponent.KnockBackResistance);
        propertyNameNumericsDict.Add(PropertyType.KnockBackResistance.ToString(), tmp);

        tmp = AddChild<HealthPoint>();
        tmp.SetBaseMaxValue(1);
        tmp.SetBaseValue(UnitPropertyComponent.RigidResistance);
        propertyNameNumericsDict.Add(PropertyType.RigidResistance.ToString(), tmp);

    }
    private void UpdateData()
    {

    }
}
