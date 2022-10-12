using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCarBehaviourSystem : GameSystem
{
    private Vector3 _finishPos;


    public override void OnStateEnter()
    {
        _finishPos = FindObjectOfType<FinishComponent>().transform.position;
    }

    public override void OnUpdate()
    {
        foreach (var opponentCar in game.OpponentsCars)
        {
            CalculateDirection(opponentCar);
        }
    }

    public override void OnFixedUpdate()
    {
        foreach (var opponentCar in game.OpponentsCars)
        {
            //CalculateDirection(opponentCar);
            MoveToTarget(opponentCar);  
        }
    }

    private void CalculateDirection(OpponentCarComponent car)
    {
        Vector3 target = new Vector3(_finishPos.x, car.transform.position.y, _finishPos.z);
        Vector3 vectorToTarget = car.transform.InverseTransformPoint(target);

        float distance = vectorToTarget.magnitude;


        car.Forward = Mathf.Lerp(car.Forward,1,config.Acceleration);
        car.Turn = vectorToTarget.x / distance;

        CheckEnvironment(car);
    }

    private void MoveToTarget(OpponentCarComponent car)
    {
        for (int i = 0; i < car.WheelColliders.Length - 2; i++)
        {
            ref var wheel = ref car.WheelColliders[i];
            wheel.motorTorque = car.Forward * config.OpponentForce;
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, car.Turn * config.MaxSteerAngle, 0.5f);

            UpdateWheelPos(wheel, car.WheelTransforms[i]);
        }
    }

    private void CheckEnvironment(OpponentCarComponent car)
    {
        if (Physics.Raycast(car.GetSensorStart(0f), car.GetSensorDir(0f), car.LenghtSensor)) car.Forward *= -1;

        if (Physics.Raycast(car.GetSensorStart(-1f), car.GetSensorDir(-1f), car.LenghtSensor)) car.Turn = 1;
        if (Physics.Raycast(car.GetSensorStart(-1f), car.GetSensorDir(0f), car.LenghtSensor)) car.Turn = 1;
        if (Physics.Raycast(car.GetSensorStart(1f), car.GetSensorDir(0f), car.LenghtSensor)) car.Turn = -1;
        if (Physics.Raycast(car.GetSensorStart(1f), car.GetSensorDir(1f), car.LenghtSensor)) car.Turn = -1;
    }

    private void UpdateWheelPos(WheelCollider wheel,Transform transform)
    {
        wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
