using UnityEngine;

public class Shooter
{
    private const float _raycastOffset = 0.3f;

    private readonly Transform _aim;
    private readonly ParticleSystem _waterJet;
    private readonly Rutine _rutine;
    private LayerMask _ignoreLayers;

    private Vector3 _initDirection;

    public Shooter(Transform aim, ParticleSystem waterJet, LayerMask ignoreLayers)
    {
        _rutine = new(aim.GetComponent<MonoBehaviour>(), Update);
        _aim = aim;
        _waterJet = waterJet;
        _ignoreLayers = ignoreLayers;

        _initDirection = _waterJet.transform.localEulerAngles;
        _waterJet.Stop();
    }

    public void Start()
    {
        _rutine.Start();
        _waterJet.Play();
    }

    public void Stop()
    {
        _rutine.Stop();
        _waterJet.Stop();
    }

    private void Update()
    {
        Vector3 rayStart = _aim.position + _aim.forward * _raycastOffset;
        Ray ray = new(rayStart, _aim.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, ~_ignoreLayers))
            _waterJet.transform.LookAt(hit.point);
        else
            _waterJet.transform.localEulerAngles = _initDirection;
    }
}