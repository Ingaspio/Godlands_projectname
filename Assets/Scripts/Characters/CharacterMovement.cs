using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//samyam Mouse Click Movement in Isometric Tilemap - Unity Tutorial https://www.youtube.com/watch?v=b0AQg5ZTpac&list=PLkpNHFgVZ-g0JuLmMKwfyGOC2zatSolkr&index=153&t=198s&ab_channel=samyam
public class CharacterMovement : MonoBehaviour
{
    MouseInput mouseInput;
    public Tilemap tilemap;
    private Vector3 destination;
    [SerializeField]
    private float movementSpeed;
    private void Awake()
    {
        mouseInput = new MouseInput(); 
    }
    private void OnEnable()
    {
        mouseInput.Enable();
    }
    private void OnDisable()
    {
        mouseInput.Disable();
    }
    void Start()
    {
        mouseInput.Mouse.MouseLeftClick.performed += _ => MouseClick();
    }

    private void MouseClick()
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);
        if (tilemap.HasTile(gridPosition)) { destination = mousePosition; }
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, destination) > 0.1f)
        transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        transform.position = tilemap.GetCellCenterLocal(tilemap.WorldToCell(destination));
    }
}
