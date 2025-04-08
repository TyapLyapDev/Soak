using UnityEngine;

public class DeviceSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _mobileController;
    [SerializeField] private TouchInputReader _joystick;
    [SerializeField] private KeyboardInputReader _keyBoard;

    private void Awake()
    {
        bool _isMobile = Application.isMobilePlatform;

        _mobileController.SetActive(_isMobile);
        _joystick.gameObject.SetActive(_isMobile);
        _keyBoard.gameObject.SetActive(!_isMobile);
    }
}
