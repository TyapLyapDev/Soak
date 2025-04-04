using GC;
using UnityEngine;

namespace GameConsoleFiXiK
{
    public class TestLogs : MonoBehaviour
    {
        [Header ("������� ���������")]
        [TextArea][SerializeField] private string _textLog;
        [SerializeField] private KeyCode _keyLog;

        [Header("��������� - ��������������")]
        [TextArea][SerializeField] private string _textLogWarning;
        [SerializeField] private KeyCode _keyWarning;

        [Header("��������� �� ������")]
        [TextArea][SerializeField] private string _textError;
        [SerializeField] private KeyCode _keyError;

        [Header("��������� �� ������")]
        [TextArea][SerializeField] private string _textCorrect;
        [SerializeField] private KeyCode _keyCorrect;

        [Header("��������� ���������")]
        [TextArea][SerializeField] private string _textExclusive;
        [SerializeField] private KeyCode _keyExclusive;

        void Update()
        {
            if (Input.GetKeyDown(_keyLog))
                GameConsole.Log(_textLog);

            if (Input.GetKeyDown(_keyWarning))
                GameConsole.LogWarning(_textLogWarning);

            if (Input.GetKeyDown(_keyError))
                GameConsole.LogError(_textError);

            if (Input.GetKeyDown(_keyCorrect))
                GameConsole.LogCorrect(_textCorrect);

            if (Input.GetKeyDown(_keyExclusive))
                GameConsole.LogExclusive(_textExclusive);
        }
    }
}