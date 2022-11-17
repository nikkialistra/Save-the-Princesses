using UnityEngine.InputSystem;

namespace Trains
{
    public class TrainInput
    {
        private readonly Train _train;

        private readonly InputAction _showTrainOrderNumbersAction;

        public TrainInput(Train train, PlayerInput playerInput)
        {
            _train = train;

            _showTrainOrderNumbersAction = playerInput.actions.FindAction("Show Train Order Numbers");

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
