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
    public float InteractRange = 6f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

            //Verifica se existe algum objeto na frente do player, baseado no InteractRange
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                //Verifica se o objeto possui a Interface IInteragir em seu escopo
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteragir interactObj))
                {
                    interactObj.Interagir();
                }
            }
        }
    }
}