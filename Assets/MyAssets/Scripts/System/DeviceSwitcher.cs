using UnityEngine;

public class DeviceSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _mobileController;
    [SerializeField] private JoystickInputReader _joystickInputReader;
    [SerializeField] private KeyBoardInputReader _keyBoardInputReader;

    private void Awake()
    {
        bool _isMobile = Application.isMobilePlatform;

        _mobileController.SetActive(_isMobile);
        _joystickInputReader.gameObject.SetActive(_isMobile);
        _keyBoardInputReader.gameObject.SetActive(!_isMobile);
    }
}
