using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private UI_InteractPrompt _interactionPromptUI;
    public InputHandler inputHandler;
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;
    //Outputs the number of objects overlapping on the interactable range.
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc
        (
            _interactionPoint.position,
            _interactionPointRadius,
            _colliders,
            _interactableMask
        );

        if (_numFound >0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null)
            {
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                if (inputHandler.inputInteragir)
                {
                    _interactionPromptUI.Result(_interactable.Interact(this));
                }
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }
    }

    //Draws the interactable area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
//Old interactor script
//
//interface IInteragir
//{
//    public void Interagir();
//}
//public class Interacoes : MonoBehaviour
//{
//    public Transform InteractorSource;
//    public float InteractRange = 6f;
//    public float SphereRadius = 0f; // You can adjust this value to change the size of the sphere
//
//    void Update()
//    {
//        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
//
//        // Draw the ray in green for debugging purposes
//        Debug.DrawRay(r.origin, r.direction * InteractRange, Color.green);
//
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            //Verifica se existe algum objeto na frente do player, baseado no InteractRange
//            if (Physics.SphereCast(r, SphereRadius, out RaycastHit hitInfo, InteractRange))
//            {
//                //Verifica se o objeto possui a Interface IInteragir em seu escopo
//                if (hitInfo.collider.gameObject.TryGetComponent(out IInteragir interactObj))
//                {
//                    interactObj.Interagir();
//                }
//            }
//
//            Debug.DrawRay(r.origin, r.direction * InteractRange, Color.red);
//        }
//    }
//
//}