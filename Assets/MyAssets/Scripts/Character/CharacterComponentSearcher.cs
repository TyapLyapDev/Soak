using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterComponentSearcher
{
    private readonly Transform _character;
    private Transform _physicalModel;
    private readonly bool _isPlayer;

    public CharacterComponentSearcher(Character character)
    {
        _character = character.transform;
        _isPlayer = character is Player;
        InitModel();
    }

    public Transform GetCenterModel()
    {
        Transform centerModel = _physicalModel.GetComponentInChildren<CenterModel>(true).transform;

        if (centerModel == null)
            throw new NullReferenceException($"Не найден компонент CenterModel в иерархии {_physicalModel.name}");

        return centerModel;
    }

    public CharacterView GetView()
    {
        CharacterView view = _physicalModel.GetComponentInChildren<CharacterView>(true);

        if (view == null)
            throw new NullReferenceException($"Не найден компонент CharacterView в иерархии {_physicalModel.name}");

        return view;
    }

    public HashSet<Collider> GetColliders()
    {
        HashSet<Collider> colliders = new(_physicalModel.GetComponentsInChildren<Collider>(true).ToList());

        if (colliders.Count == 0)
            throw new NullReferenceException($"Не найдены компоненты Collider в иерархии {_physicalModel.name}");

        return colliders;
    }

    public WaterJet GetWaterJet()
    {
        if (_isPlayer == false)
            return _physicalModel.GetComponentInChildren<WaterJet>(true);

        Transform camera = Camera.main.transform;
        return camera.GetComponentInChildren<WaterJet>(true);
    }

    private void InitModel()
    {
        PhysicalModel model = _character.GetComponentInChildren<PhysicalModel>(true);        

        if (model == null)
            throw new NullReferenceException($"Не найден компонент PhysicalModel в иерархии {_character.name}");

        _physicalModel = model.transform;
    }
}