using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Buttons
{
    public class ToggleButton : MonoBehaviour
    {
        [field: SerializeField] public Toggle Toggle { get; set; }
        [field: SerializeField] private Image Background { get; set; }
        [field: SerializeField] private RectTransform Circle { get; set; }
    
        [field: Header("Animation Setup")]
        [field: SerializeField] private Color ActiveColor { get; set; }
        [field: SerializeField] private Color InactiveColor { get; set; }
        [field: SerializeField] private RectTransform OnTargetPosition { get; set; }
        [field: SerializeField] private RectTransform OffTargetPosition { get; set; }
        [field: SerializeField] private float Duration { get; set; }
    
        private Sequence Seq { get; set; }

        public void SetInitialState(bool active)
        {
            Toggle.isOn = active;
        
            Background.color = active ? ActiveColor : InactiveColor;
            Circle.position = active? OnTargetPosition.position : OffTargetPosition.position;
            
            Toggle.onValueChanged.AddListener(SetToggle);
        }

        private void SetToggle(bool isOn)
        {
            if (Seq != null)
                Seq.Kill();
        
            Seq = DOTween.Sequence();

            if (!isOn)
            {
                Seq.Insert(0,Background.DOColor(InactiveColor, Duration));
                Seq.Insert(0, Circle.DOMove(OffTargetPosition.position,Duration));
            }

            else
            {
                Seq.Insert(0,Background.DOColor(ActiveColor, Duration));
                Seq.Insert(0, Circle.DOMove(OnTargetPosition.position,Duration));
            }
            

            Seq.Play();
        }
    }
}
