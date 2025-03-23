using UnityEngine;

public class SetSafeArea : MonoBehaviour
    {
        RectTransform SafeArea;
        [SerializeField] bool AdjustX = true;
        [SerializeField] bool AdjustY = true;
        void Awake ()
        {
            SafeArea = GetComponent<RectTransform> ();

            if (SafeArea == null)
            {
                Debug.LogError ("Cannot apply safe area - no RectTransform found on " + name);
                Destroy (gameObject);
            }

            Rect safeArea = Screen.safeArea;
            ApplySafeArea (safeArea);
        }
        
        void ApplySafeArea (Rect r)
        {
            if (!AdjustX)
            {
                r.x = 0;
                r.width = Screen.width;
            }
            
            if (!AdjustY)
            {
                r.y = 0;
                r.height = Screen.height;
            }

            
            if (Screen.width > 0 && Screen.height > 0)
            {
                Vector2 anchorMin = r.position;
                Vector2 anchorMax = r.position + r.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;

                if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
                {
                    SafeArea.anchorMin = anchorMin;
                    SafeArea.anchorMax = anchorMax;
                }
            }
        }
    }