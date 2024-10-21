using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Tutorial Source: https://www.youtube.com/watch?v=2YhGK-PXz7g
/// </summary>
public class InputHandler : MonoBehaviour
{
    private IFighter _mechController;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        var fighters = FindObjectsOfType<MonoBehaviour>().OfType<IFighter>(); //WorkAround, but it works!(around*)
        var index = _playerInput.playerIndex;
        _mechController = fighters.FirstOrDefault(m => m.fighterIndex == index);
    }
}