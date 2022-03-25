using EGamePlay;
using EGamePlay.Combat;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[LabelText("��������")]
public enum PropertyType 
{
    [LabelText("��")]
    None,
    [LabelText("����ֵ����")]
    MaxHP,
    [LabelText("��ǰ����ֵ")]
    CurrentHP,
    [LabelText("MP����")]
    MaxMP,
    [LabelText("��ǰMP")]
    CurrentMP,
    [LabelText("����ֵ����")]
    MaxShield,
    [LabelText("��ǰ����ֵ")]
    CurrentShield,
    [LabelText("����ֵ��ɢ����")]
    ShieldDissipateRate,
    [LabelText("�ƶ��ٶ�")]
    MoveSpeed,
    [LabelText("������")]
    AttackPower,
    [LabelText("�����ٶ�")]
    AttackSpeed,
    [LabelText("�˺�����(ȫ�ܼӳ�)")]
    Versality,
    [LabelText("Ԫ���˺��ӳ�")]
    ElementalDmgModifier,
    [LabelText("��Ԫ���˺��ӳ�")]
    JinDmgModifier,
    [LabelText("ľԪ���˺��ӳ�")]
    MuDmgModifier,
    [LabelText("ˮԪ���˺��ӳ�")]
    ShuiDmgModifier,
    [LabelText("��Ԫ���˺��ӳ�")]
    HuoDmgModifier,
    [LabelText("��Ԫ���˺��ӳ�")]
    TuDmgModifier,
    [LabelText("������")]
    CriticalChance,
    [LabelText("�����˺�")]
    CriticalDamage,
    [LabelText("���ܼ�������")]
    TakenDmgModifier,
    [LabelText("Ԫ�ؿ�������")]
    TakenElementalDmgModifier,
    [LabelText("���˿���")]
    KnockBackResistance,
    [LabelText("Ӳֱ����")]
    RigidResistance,
    [LabelText("����")]
    isOwn,
    [LabelText("�з�")]
    isFly,
}

public class UnitPropertyEntity : Entity
{
    /// <summary>
    /// ս��������ֵ->(name,FloatNumeric)
    /// </summary>
    private readonly Dictionary<string, HealthPoint> propertyNameNumericsDict = new Dictionary<string, HealthPoint>();


    public UnitPropertyComponent UnitPropertyComponent = new UnitPropertyComponent();
    /// <summary>
    /// HP��MaxHP
    /// </summary>
    public HealthPoint HP { get { return GetPropertyDictHealthPoint(PropertyType.CurrentHP); } }
    /// <summary>
    /// MP��MaxMP
    /// </summary>
    public HealthPoint MP { get { return GetPropertyDictHealthPoint(PropertyType.CurrentMP); } }
    /// <summary>
    /// Shield��MaxShield
    /// </summary>
    public HealthPoint Shield { get{ return GetPropertyDictHealthPoint(PropertyType.CurrentShield); } }
    /// <summary>
    /// ����ֵ��ɢ����  ÿ�뻤����ɢ��������Ӿ�����󻤶�
    /// </summary>
    public HealthPoint ShieldDissipateRate { get{ return GetPropertyDictHealthPoint(PropertyType.ShieldDissipateRate); } }
    /// <summary>
    /// �ƶ��ٶ�  ��ɫÿ�����ƶ���������
    /// </summary>
    public HealthPoint MoveSpeed { get{ return GetPropertyDictHealthPoint(PropertyType.MoveSpeed); } }
    /// <summary>
    /// ������   ��ɫ�Ļ���������
    /// </summary>
    public HealthPoint AttackPower { get{ return GetPropertyDictHealthPoint(PropertyType.AttackPower); } }
    /// <summary>
    /// �����ٶ�  ��ɫ���๥�������Ĳ����ٶ�
    /// </summary>
    public HealthPoint AttackSpeed { get{ return GetPropertyDictHealthPoint(PropertyType.AttackSpeed); } }
    /// <summary>
    /// �˺�����(ȫ�ܼӳ�)    �˺���ʽ��ȫ�ܼӳ���
    /// </summary>
    public HealthPoint Versality { get{ return GetPropertyDictHealthPoint(PropertyType.Versality); } }
    /// <summary>
    /// Ԫ���˺��ӳ�    
    /// </summary>
    public HealthPoint ElementalDmgModifier { get{ return GetPropertyDictHealthPoint(PropertyType.ElementalDmgModifier); } }
    /// <summary>
    /// ��Ԫ���˺��ӳ�
    /// </summary>
    public HealthPoint JinDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.JinDmgModifier); } }
    /// <summary>
    /// ľԪ���˺��ӳ�
    /// </summary>
    public HealthPoint MuDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.MuDmgModifier); } }
    /// <summary>
    /// ˮԪ���˺��ӳ�
    /// </summary>
    public HealthPoint ShuiDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.ShuiDmgModifier); } }
    /// <summary>
    /// ��Ԫ���˺��ӳ�
    /// </summary>
    public HealthPoint HuoDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.HuoDmgModifier); } }
    /// <summary>
    /// ��Ԫ���˺��ӳ�
    /// </summary>
    public HealthPoint TuDmgModifier { get { return GetPropertyDictHealthPoint(PropertyType.TuDmgModifier); } }
    /// <summary>
    /// ������  ������Ϊ�õ�λ���˺��ı�������
    /// </summary>
    public HealthPoint CriticalChance { get { return GetPropertyDictHealthPoint(PropertyType.CriticalChance); } }
    /// <summary>
    /// �����˺�  ������Ϊ�õ�λ���˺��ı����˺�
    /// </summary>
    public HealthPoint CriticalDamage { get{ return GetPropertyDictHealthPoint(PropertyType.CriticalDamage); } }
    /// <summary>
    /// ���ܼ�������������������ۣ�			
    /// </summary>
    //���˰ٷֱ� ���˰ٷֱ�����
    public HealthPoint TakenDmgModifier { get{ return GetPropertyDictHealthPoint(PropertyType.TakenDmgModifier); } }
    /// <summary>
    /// Ԫ�ؿ��������ٷֱ� 5+1(������)���Կ��Էֱ����
    /// </summary>
    public HealthPoint TakenElementalDmgModifier { get{ return GetPropertyDictHealthPoint(PropertyType.TakenElementalDmgModifier); } }
    /// <summary>
    /// ���˿���  �ܵ�����Ч��ʱ�������˵ľ������ʱ�ļ��ٰٷֱ�
    /// </summary>
    public HealthPoint KnockBackResistance { get{ return GetPropertyDictHealthPoint(PropertyType.KnockBackResistance); } }
    /// <summary>
    /// �ܵ�ӲֱЧ��ʱ��Ӳֱ�ĳ���ʱ�����ʱ�ļ��ٰٷֱ�
    /// </summary>
    public HealthPoint RigidResistance { get{ return GetPropertyDictHealthPoint(PropertyType.RigidResistance); } }

    /// <summary>
    /// ��ȡ�ֵ��е�HelthPoint��ֵ
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
    /// ��ʼ������
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
