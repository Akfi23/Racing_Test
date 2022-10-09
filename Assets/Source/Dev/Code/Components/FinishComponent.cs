using System.Collections;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class FinishComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Signals.Get<OnCollide>().Dispatch(other.transform);
    }
}
