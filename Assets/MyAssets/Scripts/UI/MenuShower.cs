using System;
using UnityEngine;

public class MenuShower : MonoBehaviour
{
    [SerializeField] private WindowSwitcher _windowSwitcher;
    [SerializeField] private InputInformer _inputInformer;
    [SerializeField] private GameObject _canvasMobile;

    public bool IsShowing { get; private set; }

    public event Action<bool> ShowingStateChanged;

    private void Start() =>
        OnShowingChanged(IsShowing);

    private void OnEnable()
    {
        _inputInformer.MenuPressed += OnMenuPressed;
        _windowSwitcher.ReturnToGamePressed += OnMenuPressed;
    }

    private void OnDisable()
    {
        _inputInformer.MenuPressed -= OnMenuPressed;
        _windowSwitcher.ReturnToGamePressed += OnMenuPressed;
    }

    private void OnMenuPressed()
    {
        IsShowing = !IsShowing;
        OnShowingChanged(IsShowing);

        if (IsShowing == false && Application.isMobilePlatform)
            _canvasMobile.SetActive(true);

        if (IsShowing && Application.isMobilePlatform)
            _canvasMobile.SetActive(false);
    }

    private void OnShowingChanged(bool isShowing)
    {
        _windowSwitcher.gameObject.SetActive(isShowing);
        ShowingStateChanged?.Invoke(isShowing);
    }
}