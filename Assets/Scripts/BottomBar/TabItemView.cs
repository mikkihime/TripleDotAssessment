using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BottomBar
{
    public class TabItemView : MonoBehaviour
    {
        [field: SerializeField] private Button ItemButton { get; set; }
        
        [field: SerializeField] public bool Locked { get; set; }
        [field: SerializeField] public bool Selected { get; set; }
        
        [field: SerializeField] public RectTransform TabRect { get; set; }
        [field: SerializeField] private RectTransform IconRect { get; set; }
        [field: SerializeField] private RectTransform LockedIconRect { get; set; }
        [field: SerializeField] private TextMeshProUGUI TabTitle{ get; set; }
    
        private Action ContentActivated { get; set; }
        private Action BackgroundActivated { get; set; }
        private Sequence AnimationSequence { get; set; }

        public void Setup(Action contentActivated)
        {
            LockedIconRect.gameObject.SetActive(Locked);
            IconRect.gameObject.SetActive(!Locked);
            TabTitle.alpha = 0;
            
            Selected = false;
            ContentActivated = contentActivated;
        
            ItemButton.onClick.AddListener(() =>
            {
                if (!Selected)
                    ContentActivated?.Invoke();
            
                Selected = !Selected;
            });
        }

        public void CallAnimation()
        {
            BackgroundActivated?.Invoke();
        }

        public void Activate(Action animateBackground)
        {
            if (AnimationSequence != null)
                AnimationSequence.Kill();
            
            AnimationSequence = DOTween.Sequence();
            
            if(!Locked)
            {
                AnimationSequence.Insert(0, TabRect.DOSizeDelta(new Vector2(250, 150), 0.2f));
                AnimationSequence.Insert(0, IconRect.DOAnchorPos(new Vector2(0, -25), 0.2f));
                AnimationSequence.Insert(0.1f, TabTitle.DOFade(1, 0.2f));
                AnimationSequence.InsertCallback(0.2f, animateBackground.Invoke);
            }
            else
            {
                AnimationSequence.Insert(0, LockedIconRect.DOShakePosition(0.3f, 10, 20));
            }
            
            AnimationSequence.Play();
            
            Selected = true;
        }

        public void Deactivate(Action animateBackground)
        {
            if (AnimationSequence != null)
                AnimationSequence.Kill();
            
            AnimationSequence = DOTween.Sequence();
            
            AnimationSequence.Insert(0,TabRect.DOSizeDelta(new Vector2(120, 150), 0.1f));
            AnimationSequence.Insert(0, IconRect.DOAnchorPos(new Vector2(0, -60), 0.2f));
            AnimationSequence.Insert(0, TabTitle.DOFade(0, 0.2f));
            AnimationSequence.InsertCallback(0, animateBackground.Invoke);
            
            AnimationSequence.Play();
            
            Selected = false;
        }
    }
}
