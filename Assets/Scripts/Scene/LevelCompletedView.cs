using DG.Tweening;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class LevelCompletedView : MonoBehaviour
    {
        [field: Header("Entry Animation Setup")]
        [field: SerializeField] private Animator TitleAnimator { get; set; }
        
        [field: Header("Buttons Setup")]
        [field: SerializeField] private Button HomeButton { get; set; }
        [field: SerializeField] private Button AdButton { get; set; }
        
        [field: Header("Prizes Setup")]
        [field: SerializeField] private PrizeItemView StarsPrize { get; set; }
        [field: SerializeField] private int StarsInitialAmount { get; set; }
        [field: SerializeField] private int StarsFinalAmount { get; set; }
        
        [field: SerializeField] private PrizeItemView CoinsPrize { get; set; }
        [field: SerializeField] private int CoinsInitialAmount { get; set; }
        [field: SerializeField] private int CoinsFinalAmount { get; set; }
        
        [field: SerializeField] private PrizeItemView CrownsPrize { get; set; }
        [field: SerializeField] private int CrownsInitialAmount { get; set; }
        [field: SerializeField] private int CrownsFinalAmount { get; set; }

        private Sequence ShowContentSequence { get; set; }

        private void Start()
        {
            SceneSetup();
            ShowContent();

            ShowContentSequence?.Play();
        }

        private void SceneSetup()
        {
            HomeButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Home");
            });
            
            HomeButton.transform.localScale = Vector3.zero;
            AdButton.transform.localScale = Vector3.zero;
            
            StarsPrize.SetupPrize(StarsInitialAmount);
            CoinsPrize.SetupPrize(CoinsInitialAmount);
            CrownsPrize.SetupPrize(CrownsInitialAmount);
        }
        
        private void ShowContent()
        {
            ShowContentSequence?.Kill();

            ShowContentSequence = DOTween.Sequence();
            
            ShowContentSequence.InsertCallback(0.3f, () =>
            {
                TitleAnimator.SetTrigger("Show");
            });
            ShowContentSequence.InsertCallback(1, () =>
            {
                StarsPrize.StartAnimations(StarsInitialAmount, StarsFinalAmount, false);
            });
            ShowContentSequence.InsertCallback(1.5f, () =>
            {
                CoinsPrize.StartAnimations(CoinsInitialAmount, CoinsFinalAmount);
            });
            ShowContentSequence.InsertCallback(1.7f, () =>
            {
                CrownsPrize.StartAnimations(CrownsInitialAmount, CrownsFinalAmount);
            });
            ShowContentSequence.Insert(2.5f, AdButton.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce));
            ShowContentSequence.Insert(3, HomeButton.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce));
        }
    }
}
