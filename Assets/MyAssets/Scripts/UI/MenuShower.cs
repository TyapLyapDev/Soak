using System;
using UnityEngine;

public class MenuShower : MonoBehaviour
{
    [SerializeField] private InputInformer _informer;
    [SerializeField] private WindowSwitcher _windowSwitcher;
    [SerializeField] private GameObject _canvasMobile;

    private bool _isShowing;

    public bool IsShowing => _isShowing;

    public event Action ShowingStateChanged;

    private void Start() =>
        OnShowingChanged(_isShowing);

    private void OnEnable()
    {
        _informer.MenuPressed += OnMenuPressed;
        _windowSwitcher.ReturnToGamePressed += OnMenuPressed;
    }

    private void OnDisable()
    {
        _informer.MenuPressed -= OnMenuPressed;
        _windowSwitcher.ReturnToGamePressed -= OnMenuPressed;
    }

    private void OnMenuPressed()
    {
        _isShowing = !_isShowing;
        OnShowingChanged(_isShowing);

        if (_isShowing == false && Application.isMobilePlatform)
            _canvasMobile.SetActive(true);

        if (_isShowing && Application.isMobilePlatform)
            _canvasMobile.SetActive(false);
    }

    private void OnShowingChanged(bool isShowing)
    {
        _windowSwitcher.gameObject.SetActive(isShowing);
        ShowingStateChanged?.Invoke();
    }
}