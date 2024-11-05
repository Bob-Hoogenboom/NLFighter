using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingDestruction : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject[] destructionPhase;
    private BoxCollider _boxCol;

    private LinkedList<GameObject> _destructionPhases = new LinkedList<GameObject>();
    private LinkedListNode<GameObject> _currentPhase;

    public UnityEvent OnNextPhase = new UnityEvent();
    public UnityEvent OnFinalPhase = new UnityEvent();

    [SerializeField]
    private int _scoreValue = 5;
    public int scoreValue => _scoreValue;

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider>();

        foreach (GameObject phase in destructionPhase)
        {
            _destructionPhases.AddLast(phase);
        }
        _currentPhase = _destructionPhases.First;
    }

    public void DestructionUpdate()
    {
        if (_currentPhase != _destructionPhases.Last)
        {
            _currentPhase.Value.SetActive(false);
            _currentPhase = _currentPhase.Next;
            _currentPhase.Value.SetActive(true);
            OnNextPhase.Invoke();
        }

        if (_currentPhase == _destructionPhases.Last)
        {
            OnFinalPhase.Invoke();
            _boxCol.enabled = false;
            this.enabled = false; //failsave, disable entire script for security
        }
    }
}
