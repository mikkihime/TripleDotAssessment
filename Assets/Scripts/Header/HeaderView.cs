using UnityEngine;

namespace Header
{
    public class HeaderView : MonoBehaviour
    {
        [field: SerializeField] private Animator HeaderAnimator { get; set; }


        public void ShowHeader()
        {
            HeaderAnimator.SetTrigger("Show");
        }

        public void HideHeader()
        {
            HeaderAnimator.SetTrigger("Hide");
        }
    }
}
