using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerCarMoevementSystem : GameSystem
{
    [InputAxis] [SerializeField] string horizontalAxis;
    [InputAxis] [SerializeField] string verticalAxis;
    
    private float _horizontalValue;
    private float _verticalValue;
    private float _currentForce;
    private bool _isHandBrake;
    private float _currentBrakeForce;
    private float _steerAngle;

    public override void OnInit()
    {
        _currentForce = config.BaseForce;
    }

    public override void OnFixedUpdate()
    {
        TryGetInput();

        SetForce();
        SetSteering();

        UpdateWheelsTransform();

        TryIncreaseGearNumber();
        TryDecreaseGearNumber();
    }

    private void TryGetInput()
    {
        _horizontalValue = Input.GetAxis("Horizontal");
        _verticalValue = Input.GetAxis("Vertical");

        _isHandBrake = Input.GetKey(KeyCode.Space);
    }

    private void SetForce()
    {
        for (int i = 0; i < game.Player.WheelColliders.Length-2; i++)
        {
            ref var collider = ref game.Player.WheelColliders[i];
            collider.motorTorque = _verticalValue * _currentForce;
        }

        _currentBrakeForce = _isHandBrake ? config.BrakeForce : 0;

        //if (!_isHandBrake) return;

        TrySetHandBrake();
    }

    private void TrySetHandBrake()
    {
        if(_isHandBrake)
            DecreaseGearNumberWhenBrake();

        for (int i = 0; i < game.Player.WheelColliders.Length; i++)
        {
            ref var collider = ref game.Player.WheelColliders[i];
            collider.brakeTorque = _currentBrakeForce;
        }
    }

    private void SetSteering()
    {
        _steerAngle = config.MaxSteerAngle * _horizontalValue;

        for (int i = 0; i < game.Player.WheelColliders.Length - 2; i++)
        {
            ref var collider = ref game.Player.WheelColliders[i];
            collider.steerAngle = _steerAngle;
        }
    }

    private void UpdateWheelsTransform()
    {
        for (int i = 0; i < game.Player.WheelColliders.Length; i++)
        {
            ref var transform = ref game.Player.WheelTransforms[i];
            ref var collider = ref game.Player.WheelColliders[i];

            Quaternion rotation;
            Vector3 position;

            collider.GetWorldPose(out position, out rotation);

            transform.position = position;
            transform.rotation = rotation;
        }
    }

    private void TryIncreaseGearNumber()
    {
        if (!Input.GetKeyDown(KeyCode.LeftShift)) return;
        if (game.GearShiftNumber > 4) return;

        game.GearShiftNumber++;
        _currentForce = config.BaseForce*game.GearShiftNumber;
    }

    private void TryDecreaseGearNumber()
    {
        if (!Input.GetKeyDown(KeyCode.LeftControl)) return;
        if (game.GearShiftNumber < 2) return;

        game.GearShiftNumber--;
        _currentForce = config.BaseForce * game.GearShiftNumber;

        TrySetHandBrake(); // should speed reduce here?
    }

    private void DecreaseGearNumberWhenBrake()
    {
        game.GearShiftNumber = 1;
        _currentForce = config.BaseForce;
    }
}
    