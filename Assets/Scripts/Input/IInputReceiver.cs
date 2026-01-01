using UnityEngine;

namespace Input
{
    public interface IInputReceiver
    {
        void OnPointerClicked(Vector3 position);
    }
}