using TMPro;
using UnityEngine;

public class InputFieldView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _field;

    public string Text => _field.text;

    public void SetText(string text) =>
        _field.text = text;
}