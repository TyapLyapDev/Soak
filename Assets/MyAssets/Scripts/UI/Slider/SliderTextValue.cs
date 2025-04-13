using TMPro;
using UnityEngine;

public class SliderTextValue : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private string _format = string.Empty;

    public void Init(float maxValue)
    {
        _text = GetComponent<TextMeshProUGUI>();

        switch (maxValue)
        {
            case > 30:
                _format = "F0";
                break;

            case > 5:
                _format = "F1";
                break;

            default:
                _format = "F2";
                break;
        }
    }

    public void SetValue(float value) =>
        _text.text = value.ToString(_format);
}