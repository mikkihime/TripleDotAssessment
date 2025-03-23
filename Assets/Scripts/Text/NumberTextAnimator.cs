using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Text
{
    public class NumberTextAnimator : MonoBehaviour
    {
    
        [field: SerializeField] private TextMeshProUGUI NumberText { get; set; }
        [field: SerializeField] private float Duration = 2;
        
        private int CurrentValue { get; set; } = 0;
        private int TargetValue { get; set; } = 0;
        private Sequence NumberSequence { get; set; }
        private bool StartHidden { get; set; }

        public void InitialSetup(int value, bool startHidden = true)
        {
            NumberText.text = value.ToString();
            StartHidden = startHidden;
            
            if (StartHidden)
                NumberText.alpha = 0;
        }

        public void CountToValue(int initialValue, int targetValue, Action onComplete = null)
        {
            if (NumberSequence != null)
                NumberSequence.Kill();
            
            NumberSequence = DOTween.Sequence();
            
            CurrentValue = initialValue;
            TargetValue = targetValue;

            if (StartHidden)
                NumberSequence.Insert(0, NumberText.DOFade(1, 0.3f));
            
            NumberSequence.Append(DOTween.To(() => initialValue, x => CurrentValue = x, targetValue, Duration)
                .OnUpdate(() =>
                    NumberText.text = CurrentValue.ToString()));
            NumberSequence.AppendCallback(()=> onComplete?.Invoke());

            NumberSequence.Play();
        }
    }
}
