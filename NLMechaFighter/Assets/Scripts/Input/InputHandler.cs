using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Tutorial Source: https://www.youtube.com/watch?v=2YhGK-PXz7g
/// A Input System that makes it possible to have more player pass input to their respective character
/// This Input System uses Unity Events to parse Input data to this script and then its parsed to the mechFighter
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

    public void OnMove(CallbackContext context)
    {
        _mechController.SetMoveVector(context.ReadValue<Vector2>());
    }

    public void OnPunch(CallbackContext context)
    {
        _mechController.SetPunchBool(context.performed);
    }
}