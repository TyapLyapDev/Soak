using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GC;

namespace GameConsoleFiXiK
{
    public enum TypeMessage
    {
        TypeLog,
        TypeWarning,
        TypeError,
        TypeCorrect,
        TypeExclusive
    }

    public class PanelGameConsole : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _textPrefab;

        [Header("Максимум сообщений:")]
        [SerializeField] private int _logLimit;

        [Header("Автоскроллинг при запуске:")]
        [SerializeField] private bool _isStartAutoscrollOn;

        [Header("Скорость автопрокрутки:")]
        [SerializeField] private float _speedAutoscroll;

        [Header("Выводить соответствующие логи в консоль Unity")]
        [SerializeField] private bool _isUnityLogs;

        [Header("Горячие клавиши")]
        [SerializeField] private KeyCode _keyShowHidePanel;
        [SerializeField] private KeyCode _keyClearMessage;

        [Header("Цвета текстов по типам сообщений:")]
        [SerializeField] private Color _log;
        [SerializeField] private Color _logWarning;
        [SerializeField] private Color _logError;
        [SerializeField] private Color _logCorrected;
        [SerializeField] private Color _logExclusive;

        [Header("Цвета заднего фона нечётных и чётных сообщений")]
        [SerializeField] private Color _oddMessagePanel;
        [SerializeField] private Color _evenMessagePanel;
        [SerializeField] private Color _exclusiveMessagePanel;

        public event Action ActionNumberMessagesChanged;
        public event Action ActionPanelShowChanged;
        public event Action ActionAutoscrollChanged;

        private List<MessageBox> _messageBoxes = new();
        private bool _isColorOne;

        public KeyCode GetKeyShowHidePanel => _keyShowHidePanel;

        public KeyCode GetKeyClearMessage => _keyClearMessage;

        public float GetSpeedScrolling => _speedAutoscroll;

        public int NumberMessages => _messageBoxes.Count;

        public bool IsShowing { get; private set; }

        public bool IsAutoscrollOn { get; private set; }

        private string GetCurrentTime => DateTime.Now.ToString("HH:mm:ss");

        private void Awake()
        {
            SetAutoscroll(_isStartAutoscrollOn);
            GameConsole.IsUnityLogs = _isUnityLogs;
        }

        private void OnEnable()
        {
            GameConsole.ActionMessage += OnMessageReceived;
        }

        private void OnDisable()
        {
            GameConsole.ActionMessage -= OnMessageReceived;
        }

        public void SetAutoscroll(bool isScroll)
        {
            if (IsAutoscrollOn == isScroll)
                return;

            IsAutoscrollOn = isScroll;
            ActionAutoscrollChanged?.Invoke();
        }

        public void SwitchShow()
        {
            IsShowing = !IsShowing;
            ActionPanelShowChanged?.Invoke();
        }

        public void Clear()
        {
            foreach (MessageBox messageBox in _messageBoxes)
            {
                Destroy(messageBox.GameObject);
            }

            _messageBoxes = new();
            ActionNumberMessagesChanged?.Invoke();
        }

        private void OnMessageReceived(string message, TypeMessage type)
        {
            MessageBox newNewMessageBox = CreateNewMessageBox(message, type);
            _messageBoxes.Add(newNewMessageBox);

            ApplyStyle(newNewMessageBox);
            RemoveOverLimit();

            ActionNumberMessagesChanged?.Invoke();
        }

        private MessageBox CreateNewMessageBox(string message, TypeMessage type)
        {
            return new(Instantiate(_textPrefab, _content.transform), message, type);
        }

        private void ApplyStyle(MessageBox messageBox)
        {
            if (messageBox.Type == TypeMessage.TypeExclusive)
            {
                messageBox.Image.color = _exclusiveMessagePanel;
                messageBox.Text.alignment = TextAlignmentOptions.Center;
                messageBox.Text.fontStyle = FontStyles.Bold;
                messageBox.Text.text = $"{messageBox.Message}";
            }
            else
            {
                messageBox.Image.color = _isColorOne ? _evenMessagePanel : _oddMessagePanel;
                messageBox.Text.text = $"[{GetCurrentTime}] {messageBox.Message}";
            }

            messageBox.Text.color = GetColorMessage(messageBox.Type);
            _isColorOne = !_isColorOne;
        }

        private Color GetColorMessage(TypeMessage type)
        {
            return type switch
            {
                TypeMessage.TypeLog => _log,
                TypeMessage.TypeWarning => _logWarning,
                TypeMessage.TypeError => _logError,
                TypeMessage.TypeCorrect => _logCorrected,
                _ => _logExclusive
            };
        }

        private void RemoveOverLimit()
        {
            while (NumberMessages > _logLimit)
            {
                Destroy(_messageBoxes[0].GameObject);
                _messageBoxes.RemoveAt(0);
            }
        }
    }
}