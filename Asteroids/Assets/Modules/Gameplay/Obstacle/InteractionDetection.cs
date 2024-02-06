using System;
using Modules.Gameplay;
using UnityEngine;

public class InteractionDetection : MonoBehaviour, IInteractable
{
    public event Action<BulletView> OnInteract;
    public void Interact(BulletView playerView) => OnInteract?.Invoke(playerView);
}