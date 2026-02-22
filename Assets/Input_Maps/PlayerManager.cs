using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Player_Action_Map _input;

    private void Start()
    {
        InitializeInputs();
    }

    private void Update()
    {
        var move = _input.Player.Movement.ReadValue<Vector2>();
        _player.Movement(move);
    }

    void InitializeInputs()
    {
        _input = new Player_Action_Map();
        _input.Player.Enable();
    }
}
