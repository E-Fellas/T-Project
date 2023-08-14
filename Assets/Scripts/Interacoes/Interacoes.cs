using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteragir
{
    public void Interagir();
}
public class Interacoes : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Apertou E");
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log("If 1");
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteragir interactObj))
                {
                    Debug.Log("If 2");
                    interactObj.Interagir();
                }
            }
        }
    }
}