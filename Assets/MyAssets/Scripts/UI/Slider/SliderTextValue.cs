using TMPro;
using UnityEngine;

public class SliderTextValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private string _format = string.Empty;

    public void Init(float maxValue)
    {
        _format = maxValue switch
        {
            > 30 => "F0",
            > 5 => "F1",
            _ => "F2",
        };
    }

    public void SetValue(float value) =>
        _text.text = value.ToString(_format);
}