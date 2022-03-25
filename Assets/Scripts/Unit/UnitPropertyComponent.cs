using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPropertyComponent
{
    #region ��������

    /// <summary>
    /// ���ĺ���������ԣ�ԭ�������޸�
    /// </summary>
    //���HPֵ ��ӵ�е����HPֵ
    public float MaxHP = 100;
    //���MPֵ ��ӵ�е����MPֵ
    public float MaxMP = 100;
    //��󻤶�ֵ ��ӵ�е���󻤶�ֵ�����HP�İٷֱȣ�
    public float MaxShield = 0.5f;
    //����ֵ��ɢ����  ÿ�뻤����ɢ��������Ӿ�����󻤶�
    public float ShieldDissipateRate = 0.25f;
    //�ƶ��ٶ�  ��ɫÿ�����ƶ���������
    public float MoveSpeed = 5;
    //������   ��ɫ�Ļ���������
    public float AttackPower = 20;
    //�����ٶ�  ��ɫ���๥�������Ĳ����ٶ�
    public float AttackSpeed = 1;
    //�˺�����(ȫ�ܼӳ�)    �˺���ʽ��ȫ�ܼӳ���
    public float Versality = 0;
    //Ԫ���˺��ӳ�
    public float ElementalDmgModifier = 0;
    // ��Ԫ���˺��ӳ�
    public float JinDmgModifier = 0;
    // ľԪ���˺��ӳ�
    public float MuDmgModifier = 0;
    // ˮԪ���˺��ӳ�
    public float ShuiDmgModifier = 0;
    // ��Ԫ���˺��ӳ�
    public float HuoDmgModifier = 0;
    // ��Ԫ���˺��ӳ�
    public float TuDmgModifier = 0;
    // ��������
    public float CriticalChance = 0;
    //�����˺�  ������Ϊ�õ�λ���˺��ı����˺�
    public float CriticalDamage = 1.5f;
    //HPֵ   ��ǰ������ֵ��Ϊ0ʱ��ɫ����
    public float CurrentHP = 100;
    //MPֵ   ��ǰ��MPֵ
    public float CurrentMP = 0;
    //����ֵ   ��ǰ��ɫӵ�еĻ���ֵ�������ܴ���0ʱ���ܵ��������������Ļ���ֵ�����ܻ������У�
    public float CurrentShield = 0;
    /// <summary>
    /// ���ܼ�������������������ۣ�			
    /// </summary>
    //���˰ٷֱ� ���˰ٷֱ�����
    public float TakenDmgModifier = 0;
    //Ԫ�ؿ��������ٷֱ� 5+1(������)���Կ��Էֱ����
    public float TakenElementalDmgModifier = 0;
    //���˿���  �ܵ�����Ч��ʱ�������˵ľ������ʱ�ļ��ٰٷֱ�'
    public float KnockBackResistance = 0;
    //�ܵ�ӲֱЧ��ʱ��Ӳֱ�ĳ���ʱ�����ʱ�ļ��ٰٷֱ�
    public float RigidResistance = 0;
    //�Ƿ����ѷ���λ
    public bool isOwn = false;
    //�Ƿ��Ƿ��е�λ   ������ײ��
    public bool isFly = false;

    #endregion
    #region �������ݵ��ļ���д
    /// <summary>
    /// д�뵱ǰ���������
    /// </summary>
    /// <param name="fileName">�ļ���</param>
    /// <param name="path">�ļ�·��</param>
    /// <returns>�Ƿ�д��ɹ�</returns>
    public bool Save(string fileName, string path)
    {
        return UnityJSon.Saves(this, fileName, path);
    }
    /// <summary>
    /// �����ļ����ݵ���ǰ����
    /// </summary>
    /// <param name="fileName">�ļ���������·��</param>
    public void ReadToSelf(string fileName)
    {
        UnitPropertyComponent unit = UnityJSon.Read(typeof(UnitPropertyComponent), fileName) as UnitPropertyComponent;
        this.MaxHP = unit.MaxHP;
        this.MaxMP = unit.MaxMP;
        this.MaxShield = unit.MaxShield;
        this.ShieldDissipateRate = unit.ShieldDissipateRate;
        this.MoveSpeed = unit.MoveSpeed;
        this.AttackPower = unit.AttackPower;
        this.AttackSpeed = unit.AttackSpeed;
        this.Versality = unit.Versality;
        this.ElementalDmgModifier = unit.ElementalDmgModifier;
        this.JinDmgModifier = unit.JinDmgModifier;
        this.MuDmgModifier = unit.MuDmgModifier;
        this.HuoDmgModifier = unit.HuoDmgModifier;
        this.ShuiDmgModifier = unit.ShuiDmgModifier;
        this.TuDmgModifier = unit.TuDmgModifier;
        this.CriticalChance = unit.CriticalChance;
        this.CriticalDamage = unit.CriticalDamage;
        this.CurrentHP = unit.CurrentHP;
        this.CurrentMP = unit.CurrentMP;
        this.CurrentShield = unit.CurrentShield;
        this.TakenDmgModifier = unit.TakenDmgModifier;
        this.TakenElementalDmgModifier = unit.TakenElementalDmgModifier;
        this.KnockBackResistance = unit.KnockBackResistance;
        this.RigidResistance = unit.RigidResistance;
        this.isOwn = unit.isOwn;
        this.isFly = unit.isFly;
    }
    /// <summary>
    /// �����ļ����ݲ����ض����Ķ���
    /// </summary>
    /// <param name="fileName">�ļ��У�����·��</param>
    /// <returns></returns>
    public UnitPropertyComponent Read(string fileName)
    {
        return UnityJSon.Read(typeof(UnitPropertyComponent), fileName) as UnitPropertyComponent;
    }
    #endregion
}
