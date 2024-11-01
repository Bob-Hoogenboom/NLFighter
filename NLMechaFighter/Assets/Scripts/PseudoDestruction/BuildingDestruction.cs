using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingDestruction : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject[] destructionPhase;
    private BoxCollider _boxCol;

    private LinkedList<GameObject> destructionPhases = new LinkedList<GameObject>();
    private LinkedListNode<GameObject> currentPhase;

    public UnityEvent OnNextPhase = new UnityEvent();

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider>();

        foreach (GameObject phase in destructionPhase)
        {
            destructionPhases.AddLast(phase);
        }
        currentPhase = destructionPhases.First;
    }

    public void DestructionUpdate()
    {
        if (currentPhase != destructionPhases.Last)
        {
            currentPhase.Value.SetActive(false);
            currentPhase = currentPhase.Next;
            currentPhase.Value.SetActive(true);
            OnNextPhase.Invoke();
        }

        if (currentPhase == destructionPhases.Last)
        {
            _boxCol.enabled = false;
            this.enabled = false; //failsave, disable entire script for security
        }
    }
}
