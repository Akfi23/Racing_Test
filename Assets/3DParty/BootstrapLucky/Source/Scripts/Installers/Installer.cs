using UnityEngine;


public abstract class Installer : MonoBehaviour, IInstaller
{
    public abstract int Order { get; }

    public abstract void Process();
}