using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonPanel : MonoBehaviour 
{
    private Button _button;

    public event Action <ButtonPanel> Clicked;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(OnClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnClick);

    private void OnClick() =>
        Clicked?.Invoke(this);
}