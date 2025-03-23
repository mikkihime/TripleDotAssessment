using BottomBar;
using Header;
using PopUp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class HomeView : MonoBehaviour
    {
        [field: SerializeField] private BottomBarView BottomBar { get; set; }
        [field: SerializeField] private SettingsPopUp SettingsPopUp { get; set; }
        [field: SerializeField] private HeaderView Header { get; set; }
        
        [field: SerializeField] private Button SettingsButton { get; set; }
        [field: SerializeField] private Button LevelCompletedButton { get; set; }

        private void Start()
        {
            BottomBar.ShowBottomBar();
            Header.ShowHeader();
            
            SettingsSetup();
            
            LevelCompletedButton.onClick.AddListener((() =>
            {
                SceneManager.LoadScene("LevelCompleted");
            }));
        }

        private void SettingsSetup()
        {
            SettingsPopUp.InitToggles();
            SettingsPopUp.Setup();
            SettingsPopUp.gameObject.SetActive(false);
            
            SettingsButton.onClick.AddListener(() =>
            {
                SettingsPopUp.gameObject.SetActive(true);
                SettingsPopUp.AnimatePopUp(true);
            });
            
            SettingsPopUp.CloseButton.onClick.AddListener(() =>
            {
                SettingsPopUp.AnimatePopUp(false, () => SettingsPopUp.gameObject.SetActive(false));
            });
        }
    }
}
