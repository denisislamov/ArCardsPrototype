using UnityEngine;
using System.Collections;

public abstract class InteractableTarget
{
    public abstract void Interact();
    public abstract void StopInteract();
    public abstract bool CanInteract { get; set; }
}
