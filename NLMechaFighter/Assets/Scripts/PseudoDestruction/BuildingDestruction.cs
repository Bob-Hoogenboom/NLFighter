using UnityEngine;

public class BuildingDestruction : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject[] destructionPhase;
    [SerializeField]private int destructionPhaseIndex = 0;
    private BoxCollider _boxCol;

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider>();
    }

    public void DestructionUpdate()
    {
        if (!(destructionPhaseIndex == destructionPhase.Length -1))
        {
            //#linked list?
            destructionPhase[destructionPhaseIndex].SetActive(false);
            destructionPhaseIndex ++;
            destructionPhase[destructionPhaseIndex].SetActive(true);

        }

        if (destructionPhaseIndex >= destructionPhase.Length -1)
        {
            _boxCol.enabled = false;
            this.enabled = false; //failsave, disable entire script for security
        }
    }
}
