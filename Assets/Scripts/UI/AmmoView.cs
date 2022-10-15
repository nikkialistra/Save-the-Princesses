using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class AmmoView : MonoBehaviour
    {
        private VisualElement _charge1;
        private VisualElement _charge2;
        private VisualElement _charge3;
        private VisualElement _charge4;

        public void Initialize()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _charge1 = root.Q<VisualElement>("charge1");
            _charge2 = root.Q<VisualElement>("charge2");
            _charge3 = root.Q<VisualElement>("charge3");
            _charge4 = root.Q<VisualElement>("charge4");
        }

        [Button]
        public void UpdateQuantity(int quantity)
        {
            UpdateCharge1(quantity);
            UpdateCharge2(quantity);
            UpdateCharge3(quantity);
            UpdateCharge4(quantity);
        }

        private void UpdateCharge1(int quantity)
        {
            if (quantity >= 1)
                ShowAsFilled(_charge1);
            else
                ShowAsEmpty(_charge1);
        }

        private void UpdateCharge2(int quantity)
        {
            if (quantity >= 2)
                ShowAsFilled(_charge2);
            else
                ShowAsEmpty(_charge2);
        }

        private void UpdateCharge3(int quantity)
        {
            if (quantity >= 3)
                ShowAsFilled(_charge3);
            else
                ShowAsEmpty(_charge3);
        }

        private void UpdateCharge4(int quantity)
        {
            if (quantity >= 4)
                ShowAsFilled(_charge4);
            else
                ShowAsEmpty(_charge4);
        }

        private static void ShowAsFilled(VisualElement element)
        {
            element.AddToClassList("charge-filled");
            element.RemoveFromClassList("charge-empty");
        }

        private static void ShowAsEmpty(VisualElement element)
        {
            element.AddToClassList("charge-empty");
            element.RemoveFromClassList("charge-filled");
        }
    }
}
