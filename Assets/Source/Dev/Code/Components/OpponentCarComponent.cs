using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCarComponent : CarContainerComponent
{
    public float Forward;
    public float Turn;

    public float FrontOffsetSensor;
    public float SideOffsetSensor;
    public float EndShiftSensor;
    public float LenghtSensor;

    public Vector3 GetSensorStart(float dir)
    {
        return transform.position +Vector3.up*0.5f  + transform.forward * FrontOffsetSensor + transform.right * SideOffsetSensor * dir;
    }

    public Vector3 GetSensorDir(float shift)
    {
        return transform.forward * LenghtSensor + transform.right * shift * EndShiftSensor;   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(GetSensorStart(0f), GetSensorDir(0f));
        Gizmos.DrawRay(GetSensorStart(-1f), GetSensorDir(-1f));
        Gizmos.DrawRay(GetSensorStart(-1f), GetSensorDir(0f));
        Gizmos.DrawRay(GetSensorStart(1f), GetSensorDir(1f));
        Gizmos.DrawRay(GetSensorStart(1f), GetSensorDir(0f));
    }
}
