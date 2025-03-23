using System;
using Text;
using TMPro;
using UnityEngine;

public class BaseText : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }
    [field: SerializeField] private TextSettings TextSettings { get; set; }
    
    [field: SerializeField] private TextSetup.Styles TextStyle { get; set; }
    
    
    private TextSetup Style { get; set; }
    private void OnValidate()
    {
        Style = TextSettings.GetTextStyle(TextStyle);
        
        Text.font = Style.FontAsset;
        Text.enableAutoSizing = Style.Autosize;

        if (Style.Autosize)
        {
            Text.fontSizeMin = Style.FontMinSize;
            Text.fontSizeMax = Style.FontMaxSize;
        }

        else
        {
            Text.fontSize = Style.FontSize;
        }
    }

    public void SetText(string text)
    {
        Text.text = text;
    }
}