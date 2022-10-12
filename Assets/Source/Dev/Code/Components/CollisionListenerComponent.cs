using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;

public class CollisionListenerComponent : GameSystem
{
    public void OnCollisionEnter(Collision collision)
    {
        Signals.Get<OnObstacleCollide>().Dispatch(collision.relativeVelocity.sqrMagnitude);
    }
}
