using Sirenix.OdinInspector;

namespace EGamePlay.Combat
{
    [LabelText("元素伤害类型")]
    public enum ElementDmgType
    {
        [LabelText("金")]
        JIN,
        [LabelText("木")]
        MU,
        [LabelText("水")]
        SHUI,
        [LabelText("火")]
        HUO,
        [LabelText("土")]
        TU,
        [LabelText("无")]
        WU
    }

    [Effect("造成伤害", 10)]
    public class DamageEffect : Effect
    {
        public override string Label => "造成伤害";

        [ToggleGroup("Enabled")]
        public DamageType DamageType;

        [ToggleGroup("Enabled")]
        public ElementDmgType elementDmgType;

        [ToggleGroup("Enabled"), LabelText("伤害取值")]
        public string DamageValueFormula;

        [ToggleGroup("Enabled"), LabelText("能否暴击")]
        public bool CanCrit;
     
        public string DamageValueProperty { get; set; }
    }
}