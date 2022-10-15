using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Trains
{
    [RequireComponent(typeof(Train))]
    public class TrainInput : MonoBehaviour
    {
        private Train _train;

        private PlayerInput _playerInput;

        private InputAction _showTrainOrderNumbersAction;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            _showTrainOrderNumbersAction = _playerInput.actions.FindAction("Show Train Order Numbers");
        }

        public void Initialize()
        {
            _train = GetComponent<Train>();

            _showTrainOrderNumbersAction.started += ShowOrderNumbers;
            _showTrainOrderNumbersAction.canceled += HideOrderNumbers;
        }

        public void Dispose()
        {
            _showTrainOrderNumbersAction.started -= ShowOrderNumbers;
            _showTrainOrderNumbersAction.canceled -= HideOrderNumbers;
        }

        private void ShowOrderNumbers(InputAction.CallbackContext _)
        {
            _train.ShowOrderNumbers();
        }

        private void HideOrderNumbers(InputAction.CallbackContext _)
        {
            _train.HideOrderNumbers();
        }
    }
}
