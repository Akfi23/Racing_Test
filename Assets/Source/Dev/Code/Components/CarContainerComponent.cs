using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarContainerComponent : MonoBehaviour
{
    [Header("Wheels Stuff")]
    [SerializeField] protected Transform[] _wheelTransforms;
    [SerializeField] protected WheelCollider[] _wheelColliders;

    [Header("Car Body")]
    [SerializeField] protected MeshRenderer bodyRenderer;
    [SerializeField] protected Rigidbody _rb;

    protected CarOwner _owner;

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
