using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarContainerComponent : MonoBehaviour
{
    [Header("Wheels Stuff")]
    [SerializeField] private Transform[] _wheelTransforms;
    [SerializeField] private WheelCollider[] _wheelColliders;

    [Header("Car Body")]
    [SerializeField] private MeshRenderer bodyRenderer;
    [SerializeField] private Rigidbody _rb;

    private CarOwner _owner;

    public Transform[] WheelTransforms => _wheelTransforms;
    public WheelCollider[] WheelColliders => _wheelColliders;
    public MeshRenderer BodyRenderer => bodyRenderer;
    public Rigidbody RB => _rb;

    public void SetOwner(CarOwner owner)
    {
        _owner = owner;
    }

    public CarOwner GetOwner()
    {
        return _owner;
    }
}
