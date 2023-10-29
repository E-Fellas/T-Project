using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour, IInteractable
{
    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Colheu Maça!!");
        return true;
    }
}
