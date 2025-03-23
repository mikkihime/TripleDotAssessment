using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Text
{
    [CreateAssetMenu(fileName = "TextSettings", menuName = "Scriptable Objects/TextSettings")]
    public class TextSettings : ScriptableObject
    {
        [field: SerializeField] public List<TextSetup> TextStyleSettings { get; set;}

        public TextSetup GetTextStyle(TextSetup.Styles style)
        {
            return TextStyleSettings.FirstOrDefault(p => p.StyleName == style);
        }
    }

    [System.Serializable]
    public class TextSetup 
    {
        public enum Styles
        {
            Header,
            Body,
            Number,
            Small,
            Button,
            ButtonLarge,
            SceneTitle,
            PrizeAmount,
        }
        [field: SerializeField] public Styles StyleName { get; set; }
    
        [field: SerializeField] public TMP_FontAsset FontAsset { get; set; }
    
        [field: Header("Font Size")]
        [field: SerializeField] public bool Autosize { get; private set; }

        [field: SerializeField, Tooltip("Applied with AutoSize")] public float FontMinSize { get; private set; }
        [field: SerializeField, Tooltip("Applied with AutoSize")] public float FontMaxSize { get; private set; }
        [field: SerializeField, Tooltip("Applied without AutoSize")] public float FontSize { get; private set; }
    
    }
}