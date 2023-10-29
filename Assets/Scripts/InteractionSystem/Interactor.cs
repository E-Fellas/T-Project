using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

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
    }

    //Draws the interactable area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
