using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;
    private CursorControls controls;

    private static CursorController instance;  // Singleton

    private void Awake()
    {
        // Implementaci�n del Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Hace que este objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject);  // Destruye el objeto si ya existe una instancia
            return;
        }

        // Inicializaci�n del cursor
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        // Suscripci�n a eventos de clic
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => PerformedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }

    private void PerformedClick()
    {
        ChangeCursor(cursor);
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }
}
