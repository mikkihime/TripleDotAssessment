using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BottomBar
{
    public enum Tabs
    {
        FirstLock = 0,
        Shop = 1,
        Home = 2,
        Map = 3,
        SecondLock = 4,
        None = 5
    }
    public class BottomBarView : MonoBehaviour
    {
        
        [field: SerializeField] private List<TabItemView> TabItems { get; set;}
        [field: SerializeField] private Animator BottomBarAnimator {get; set;}
        [field: SerializeField] private RectTransform SelectedBackground { get; set; }
        
        private Sequence SelectedSequence {get; set;}
        private int ActiveTabIndex = 2;
        public bool Closed;
        private void Start()
        {
            for (int i = 0; i < TabItems.Count; i++)
            {
                var i1 = i;
                TabItems[i].Setup(() =>
                {
                    SetActive(i1);
                });
            }
            
            var homeTab =  TabItems[(int)Tabs.Home];
            homeTab.Activate(() => ShowSelectedBackground(homeTab.TabRect.anchoredPosition).Play());
            ActiveTabIndex = (int)Tabs.Home;
        }

        private void SetActive(int index)
        {
            if (index > TabItems.Count - 1)
                return;
            
            var newTab = TabItems[index];
            var isSameTab = index == ActiveTabIndex;
                
            if (!newTab.Locked)
            {
                if (!Closed)
                {
                    TabItems[ActiveTabIndex].Deactivate(() =>
                    {
                        if (isSameTab)
                            HideSelectedBackground().Play();
                    });
                }
                
                ActiveTabIndex = isSameTab? (int)Tabs.None : index;
            }
            
            if (ActiveTabIndex != (int)Tabs.None || newTab.Locked)
            {
                if (Closed)
                {
                    SetBackgroundPosition(newTab.TabRect.anchoredPosition);
                    newTab.Activate(() => ShowSelectedBackground(newTab.TabRect.anchoredPosition).Play());
                }
                else
                {
                    newTab.Activate(() => MoveToSelected(newTab.TabRect.anchoredPosition).Play());
                }
                
            }
            
            Closed = ActiveTabIndex == (int)Tabs.None;
        } 

        public void ShowBottomBar()
        {
            BottomBarAnimator.SetBool("Show", true);
        }
    
        public void HideBottomBar()
        {
            BottomBarAnimator.SetBool("Show", false);
        }

        private void SetBackgroundPosition(Vector3 targetPosition)
        {
            Debug.Log($"SetBackgroundPosition: {targetPosition}");
            
            SelectedBackground.anchoredPosition = new Vector3(targetPosition.x, -700);
        }

        private void KillCurrentSequence()
        {
            if (SelectedSequence != null)
                SelectedSequence.Kill();
        }

        private Sequence ShowSelectedBackground(Vector3 targetPosition)
        {
            KillCurrentSequence();
            
            SelectedSequence = DOTween.Sequence();
            SelectedSequence.Insert(0, SelectedBackground.DOAnchorPos(new Vector3(targetPosition.x, -375), 0.2f).SetEase(Ease.OutCirc));

            return SelectedSequence;
        }
        
        private Sequence HideSelectedBackground()
        {
            KillCurrentSequence();

            SelectedSequence = DOTween.Sequence();
            SelectedSequence.Insert(0, SelectedBackground.DOAnchorPos(new Vector3(SelectedBackground.anchoredPosition.x, -700), 0.2f).SetEase(Ease.OutCirc));

            return SelectedSequence;
        }

        private Sequence MoveToSelected(Vector3 targetPosition)
        {
            KillCurrentSequence();

            SelectedSequence = DOTween.Sequence();
            SelectedSequence.Insert(0,SelectedBackground.DOAnchorPos(new Vector3(targetPosition.x, -375), 0.2f).SetEase(Ease.OutCirc));

            return SelectedSequence;
        }
    }
}
