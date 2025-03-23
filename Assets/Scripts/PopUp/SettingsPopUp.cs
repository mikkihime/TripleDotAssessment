using System;
using Buttons;
using DG.Tweening;
using UnityEngine;

namespace PopUp
{
    public class SettingsPopUp : BasePopUpView
    {
        [field: SerializeField] private CanvasGroup SupportButton {get; set; }
        
        [field: Header("Settings Toggles")]
        [field: SerializeField] private ToggleButton SoundToggle { get; set; }
        [field: SerializeField] private ToggleButton MusicToggle { get; set; }
        [field: SerializeField] private ToggleButton VibrationToggle { get; set; }
        [field: SerializeField] private ToggleButton NotificationsToggle { get; set; }
        
        [field: Header("Debug Values")]
        [field: SerializeField] private bool SoundOn { get; set; }
        [field: SerializeField] private bool MusicOn { get; set; }
        [field: SerializeField] private bool VibrationOn { get; set; }
        [field: SerializeField] private bool NotificationsOn { get; set; }

        public void InitToggles()
        {
            SoundToggle.SetInitialState(SoundOn);
            MusicToggle.SetInitialState(MusicOn);
            VibrationToggle.SetInitialState(VibrationOn);
            NotificationsToggle.SetInitialState(NotificationsOn);
        }

        public override void AnimatePopUp(bool show, Action onComplete = null)
        {
            if (show)
            {
                SupportButton.alpha = 0;
                ShowPopUpSequence(onComplete);
                Seq.Insert(ShowAndHideDuration, SupportButton.DOFade(1, 0.2f));
            }
                
            else
            {
                HidePopUpSequence(onComplete);
                Seq.Insert(0, SupportButton.DOFade(0, 0.2f));
            }
            
            Seq.Play();
        }
    }
}