using UnityEngine;

public class Sneacker
{
    private const float SneackingMultiplier = 0.5f;
    private const float ControllerResizeSpeed = 4f;
    private const float ModelRepositionSpeed = 4f;

    private readonly Rutine _rutine;
    private readonly CharacterController _controller;
    private readonly Transform _model;

    private readonly float _valueRisingController;
    private readonly float _valueSneackingController;
    private readonly float _sneackingPositionModel;
    private readonly float _risingPositionModel;

    private float _controllerTarget;
    private float _modelTarget;
    private bool _iSneacking;

    public bool IsSneacking => _iSneacking;

    public Sneacker(CharacterController controller, CharacterModel model)
    {
        _rutine = new(controller.GetComponent<MonoBehaviour>(), UpdateSize);
        _controller = controller;
        _model = model.transform;

        _valueRisingController = _controller.height;
        _valueSneackingController = _valueRisingController * SneackingMultiplier;
        _risingPositionModel = 0;
        _sneackingPositionModel = _risingPositionModel + _valueSneackingController * 0.5f;
    }

    public void Sneack() =>
        StartRutine(true);

    public void Rise() =>
        StartRutine(false);

    private void StartRutine(bool isSneacking)
    {
        _iSneacking = isSneacking;
        _controllerTarget = isSneacking ? _valueSneackingController : _valueRisingController;
        _modelTarget = isSneacking ? _sneackingPositionModel : _risingPositionModel;

        _rutine.Start();
    }

    private void UpdateSize()
    {
        if (_controller.height == _controllerTarget && _model.localPosition.y == _modelTarget)
        {
            _rutine.Stop();

            return;
        }

        _controller.height = Mathf.MoveTowards(_controller.height, _controllerTarget, ControllerResizeSpeed * Time.deltaTime);

        Vector3 tempPosition = _model.localPosition;
        tempPosition.y = Mathf.MoveTowards(tempPosition.y, _modelTarget, ModelRepositionSpeed * Time.deltaTime);
        _model.localPosition = tempPosition;
    }
}