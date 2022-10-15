using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        public void Show()
        {
            _uiDocument.enabled = true;
        }

        public void Hide()
        {
            _uiDocument.enabled = false;
        }
    }
}
