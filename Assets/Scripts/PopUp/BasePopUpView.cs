using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    public class BasePopUpView : MonoBehaviour
    {
        [field: SerializeField] public Button CloseButton { get; set; }

        [field: SerializeField] private CanvasGroup DarkenOverlay { get; set; }
        [field: SerializeField] private CanvasGroup PopUpCanvasGroup { get; set; }

        [field: Header("Animation Setup")]
        [field: SerializeField] private float PopUpHeight { get; set; } = 980;
        [field: SerializeField] private float PopUpInitialHeight { get; set; } = 200;
        [field: SerializeField] protected float ShowAndHideDuration { get; set; }
        
        private RectTransform PopUpRect { get; set; }

        protected Sequence Seq {get; set; }


        public void Setup()
        {
            PopUpRect = PopUpCanvasGroup.gameObject.GetComponent<RectTransform>();
            PopUpCanvasGroup.alpha = 0;
            DarkenOverlay.alpha = 0;
            PopUpRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, PopUpInitialHeight);
        }

        protected void ShowPopUpSequence(Action onComplete = null)
        {
            if (Seq != null)
                Seq.Kill();
            
            Seq = DOTween.Sequence();
            
            Seq.Insert(0, DarkenOverlay.DOFade(1f, 0.8f));
            Seq.Insert(0.2f, PopUpCanvasGroup.DOFade(1f, 0.6f));
            Seq.Insert(0.4f, PopUpRect.DOSizeDelta(new Vector2(PopUpRect.rect.width, PopUpHeight), ShowAndHideDuration));
            Seq.AppendCallback(() => onComplete?.Invoke());
            
        }

        protected void HidePopUpSequence(Action onComplete = null)
        {
            if (Seq != null)
                Seq.Kill();
            
            Seq = DOTween.Sequence();
            Seq.Insert(0, PopUpRect.DOSizeDelta(new Vector2(PopUpRect.rect.width, PopUpInitialHeight), ShowAndHideDuration));
            Seq.Insert(0.2f, PopUpCanvasGroup.DOFade(0f, 0.8f));
            Seq.Insert(ShowAndHideDuration, DarkenOverlay.DOFade(0f, 0.2f));
            Seq.AppendCallback(() =>
            {
                Setup();
                onComplete?.Invoke();
            });
        }
        
        public virtual void AnimatePopUp(bool show, Action onComplete = null)
        {
            if (show)
                ShowPopUpSequence(onComplete);
            else
                HidePopUpSequence(onComplete);
            
            Seq.Play();
        }
    }
}
