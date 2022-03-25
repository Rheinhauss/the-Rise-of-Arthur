﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Sirenix.OdinInspector;

namespace EGamePlay.Combat
{
    public enum ReplaceType
    {
        [LabelText("直接替换之前的状态")]
        DirectReplace,
        [LabelText("选择状态中剩余时间最长的状态")]
        SelectMaxTime
    }
    public enum AddStatusType
    {
        [LabelText("时间叠加")]
        TimeStack,
        [LabelText("状态替换")]
        ReplaceStatus,
        [LabelText("层数叠加")]
        CanStack
    }

    [CreateAssetMenu(fileName = "状态配置", menuName = "技能|状态/状态配置")]
    //[LabelText("状态配置")]
    public class StatusConfigObject
#if !NOT_UNITY
        : SerializedScriptableObject
#endif
    {
        [LabelText(StatusIdLabel), DelayedProperty]
        public string ID = "1";
        [LabelText(StatusNameLabel), DelayedProperty]
        public string Name = "状态1";
        [LabelText(StatusTypeLabel)]
        public StatusType StatusType;
        [HideInInspector]
        public uint Duration;
        [LabelText("是否在状态栏显示"), UnityEngine.Serialization.FormerlySerializedAs("ShowInStatusIconList")]
        public bool ShowInStatusSlots;

        [LabelText("是否永续")]
        public bool IsTemp;

        [LabelText("新加状态处理")]
        public AddStatusType addStatusType;
        

        [LabelText("能否叠加持续时间"), ShowIf("addStatusType", AddStatusType.TimeStack)]
        public bool TimeStack;

        [LabelText("替换状态"), ShowIf("addStatusType", AddStatusType.ReplaceStatus)]
        public bool ReplaceStatus;
        [LabelText("替换方式"), ShowIf("TimeStack"), ShowIf("addStatusType", AddStatusType.ReplaceStatus)]
        public ReplaceType ReplaceType;

        [LabelText("能否叠加层数"), ShowIf("addStatusType", AddStatusType.CanStack)]
        public bool CanStack;
        [LabelText("状态之间是否独立"), ShowIf("CanStack"), ShowIf("addStatusType", AddStatusType.CanStack)]
        public bool IsIndep;
        [LabelText("最高叠加层数"), ShowIf("CanStack"), ShowIf("addStatusType", AddStatusType.CanStack), Range(0, 99)]
        public int MaxStack = 0;



        [LabelText("子状态效果")]
        public bool EnableChildrenStatuses;
        [OnInspectorGUI("DrawSpace", append: true)]
        [HideReferenceObjectPicker]
        [LabelText("子状态效果列表"), ShowIf("EnableChildrenStatuses"), ListDrawerSettings(DraggableItems = false, ShowItemCount = false, CustomAddFunction = "AddChildStatus")]
        public List<ChildStatus> ChildrenStatuses = new List<ChildStatus>();

        private void AddChildStatus()
        {
            ChildrenStatuses.Add(new ChildStatus());
        }

        [ToggleGroup("EnabledStateModify", "行为禁制")]
        public bool EnabledStateModify;
        [ToggleGroup("EnabledStateModify")]
        public ActionControlType ActionControlType;

        [ToggleGroup("EnabledAttributeModify", "属性修饰")]
        public bool EnabledAttributeModify;
        [ToggleGroup("EnabledAttributeModify")]
        public PropertyType PropertyType;

        [ToggleGroup("EnabledAttributeModify"), LabelText("数值参数")]
        public string NumericValue;
        public string NumericValueProperty { get; set; }
        [ToggleGroup("EnabledAttributeModify")]
        public ModifyType ModifyType;
        //[ToggleGroup("EnabledAttributeModify"), LabelText("属性修饰")]
        //[DictionaryDrawerSettings(KeyLabel =)]
        //public Dictionary<NumericType, string> AttributeChanges = new Dictionary<NumericType, string>();

        [ToggleGroup("EnabledLogicTrigger", "逻辑触发")]
        public bool EnabledLogicTrigger;

        [ToggleGroup("EnabledLogicTrigger")]
        [LabelText("效果列表")/*, Space(30)*/]
        [ListDrawerSettings(Expanded = true, DraggableItems = true, ShowItemCount = false, HideAddButton = true)]
        [HideReferenceObjectPicker]
        public List<Effect> Effects = new List<Effect>();
        [HorizontalGroup("EnabledLogicTrigger/Hor2", PaddingLeft = 40, PaddingRight = 40)]
        [HideLabel]
        [OnValueChanged("AddEffect")]
        [ValueDropdown("EffectTypeSelect")]
        public string EffectTypeName = "(添加效果)";

        public IEnumerable<string> EffectTypeSelect()
        {
            var types = typeof(Effect).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(Effect).IsAssignableFrom(x))
                //.Where(x => x != typeof(AttributeNumericModifyEffect))
                .Where(x => x.GetCustomAttribute<EffectAttribute>() != null)
                .OrderBy(x => x.GetCustomAttribute<EffectAttribute>().Order)
                .Select(x => x.GetCustomAttribute<EffectAttribute>().EffectType);

            //var status = AssetDatabase.FindAssets("t:StatusConfigObject", new string[] { "Assets" })
            //    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            //    .Select(path => AssetDatabase.LoadAssetAtPath<StatusConfigObject>(path).Name)
            //    .Select(name => $"施加状态效果 [{name}]");

            var results = types.ToList();
            //results.AddRange(status);
            results.Insert(0, "(添加效果)");
            return results;
        }

        private void AddEffect()
        {
            if (EffectTypeName != "(添加效果)")
            {
                //if (EffectTypeName.Contains("施加状态效果 ["))
                //{
                //    var effect = Activator.CreateInstance<AddStatusEffect>() as Effect;
                //    effect.Enabled = true;
                //    if (effect is AddStatusEffect addStatusEffect)
                //    {
                //        var status = AssetDatabase.FindAssets("t:StatusConfigObject", new string[] { "Assets" })
                //            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                //            .Select(path => AssetDatabase.LoadAssetAtPath<StatusConfigObject>(path).Name)
                //            .Select(name => $"施加状态效果 [{name}]")
                //            .Where(name => name == $"施加状态效果 [{name}]");
                //        //addStatusEffect.AddStatus = AssetDatabase.load
                //    }
                //    Effects.Add(effect);
                //}
                //else
                {
                    var effectType = typeof(Effect).Assembly.GetTypes()
                        .Where(x => !x.IsAbstract)
                        .Where(x => typeof(Effect).IsAssignableFrom(x))
                        .Where(x => x.GetCustomAttribute<EffectAttribute>() != null)
                        .Where(x => x.GetCustomAttribute<EffectAttribute>().EffectType == EffectTypeName)
                        .First();
                    var effect = Activator.CreateInstance(effectType) as Effect;
                    effect.Enabled = true;
                    Effects.Add(effect);
                }

                EffectTypeName = "(添加效果)";
            }
            //SkillHelper.AddEffect(Effects, EffectType);
        }

#if !NOT_UNITY
        [LabelText("状态特效")]
        [OnInspectorGUI("BeginBox", append:false)]
        public GameObject ParticleEffect;

        public GameObject GetParticleEffect() => ParticleEffect;

        [LabelText("状态音效")]
        [OnInspectorGUI("EndBox", append:true)]
        public AudioClip Audio;

        [TextArea, LabelText("状态描述")]
        public string StatusDescription;
#endif
        
#if UNITY_EDITOR
        [SerializeField, LabelText("自动重命名")]
        public bool AutoRename { get { return true; } set { AutoRenameStatic = value; } }
        public static bool AutoRenameStatic = true;

        private void OnEnable()
        {
            AutoRenameStatic = UnityEditor.EditorPrefs.GetBool("AutoRename", true);
        }

        private void OnDisable()
        {
            UnityEditor.EditorPrefs.SetBool("AutoRename", AutoRenameStatic);
        }

        private void DrawSpace()
        {
            GUILayout.Space(20);
        }

        private void BeginBox()
        {
            GUILayout.Space(30);
            Sirenix.Utilities.Editor.SirenixEditorGUI.DrawThickHorizontalSeparator();
            GUILayout.Space(10);
            Sirenix.Utilities.Editor.SirenixEditorGUI.BeginBox("状态表现");
        }

        private void EndBox()
        {
            Sirenix.Utilities.Editor.SirenixEditorGUI.EndBox();
            GUILayout.Space(30);
            Sirenix.Utilities.Editor.SirenixEditorGUI.DrawThickHorizontalSeparator();
            GUILayout.Space(10);
        }

        //private bool NeedClearLog;
        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            //if (NeedClearLog)
            //{
            //    var assembly = Assembly.GetAssembly(typeof(UnityEditor.SceneView));
            //    var type = assembly.GetType("UnityEditor.LogEntries");
            //    var method = type.GetMethod("Clear");
            //    method.Invoke(new object(), null);
            //    NeedClearLog = false;
            //}
            //if (EffectType != SkillEffectType.None)
            //{
            //    if (EffectType == SkillEffectType.AddStatus) MyToggleObjects.Add(new StateToggleGroup());
            //    if (EffectType == SkillEffectType.NumericModify) MyToggleObjects.Add(new DurationToggleGroup());
            //    EffectType = SkillEffectType.None;
            //    NeedClearLog = true;
            //}

            if (!AutoRename)
            {
                return;
            }

            RenameFile();
        }

        [Button("重命名配置文件"), HideIf("AutoRename")]
        private void RenameFile()
        {
            string[] guids = UnityEditor.Selection.assetGUIDs;
            int i = guids.Length;
            if (i == 1)
            {
                string guid = guids[0];
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                var so = UnityEditor.AssetDatabase.LoadAssetAtPath<StatusConfigObject>(assetPath);
                if (so != this)
                {
                    return;
                }
                var fileName = Path.GetFileNameWithoutExtension(assetPath);
                var newName = $"Status_{this.ID}_{this.Name}";
                if (fileName != newName)
                {
                    //Debug.Log(assetPath);
                    UnityEditor.AssetDatabase.RenameAsset(assetPath, newName);
                }
            }
        }
#endif


#if EGamePlay_EN
        private const string StatusIdLabel = "StatusID";
        private const string StatusNameLabel = "Name";
        private const string StatusTypeLabel = "Type";
#else
        private const string StatusIdLabel = "状态ID";
        private const string StatusNameLabel = "状态名称";
        private const string StatusTypeLabel = "状态类型";
#endif
    }

    public class ChildStatus
    {
        [LabelText("状态效果")]
        public StatusConfigObject StatusConfigObject;

        public ET.StatusConfig StatusConfig { get; set; }

        [LabelText("参数列表"), HideReferenceObjectPicker]
        public Dictionary<string, string> Params = new Dictionary<string, string>();
    }

    public enum StatusType
    {
        [LabelText("Buff(增益)")]
        Buff,
        [LabelText("Debuff(减益)")]
        Debuff,
        [LabelText("其他")]
        Other,
    }

    public enum EffectTriggerType
    {
        [LabelText("（空）")]
        None = 0,
        [LabelText("立即触发")]
        Instant = 1,
        [LabelText("条件触发")]
        Condition = 2,
        [LabelText("行动点触发")]
        Action = 3,
        [LabelText("间隔触发")]
        Interval = 4,
        [LabelText("在行动点且满足条件")]
        ActionCondition = 5,
    }

    public enum ConditionType
    {
        [LabelText("自定义条件")]
        CustomCondition = 0,
        [LabelText("当生命值低于x")]
        WhenHPLower = 1,
        [LabelText("当生命值低于百分比x")]
        WhenHPPctLower = 2,
        [LabelText("当x秒内没有受伤")]
        WhenInTimeNoDamage = 3,

    }
}