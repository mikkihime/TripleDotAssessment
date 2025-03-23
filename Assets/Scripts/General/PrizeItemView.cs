using Text;
using UnityEngine;

namespace General
{
    public class PrizeItemView : MonoBehaviour
    {
        [field: SerializeField] private NumberTextAnimator PrizeAmountText { get; set; }
        [field: SerializeField] private Animator IconAnimator { get; set; }
        
        private bool AnimateIcon { get; set; }
        public void SetupPrize(int initialAmount)
        {
            PrizeAmountText.InitialSetup(initialAmount);
        }

        public void ShowIcon()
        {
            IconAnimator.SetTrigger("Show");
        }
        
        public void StartAnimations(int initialAmount, int updatedAmount, bool animateIcon = true)
        {
            AnimateIcon = animateIcon;
            
            if (AnimateIcon)
                StartIconAnimation();
            
            PrizeAmountText.CountToValue(initialAmount, updatedAmount, StopIconAnimation);
        }

        private void StartIconAnimation()
        {
            IconAnimator.SetTrigger("StartCounting");
        }   

        private void StopIconAnimation()
        {
            if (AnimateIcon)
                IconAnimator.SetTrigger("FinishedCounting");
        }
    }
}
