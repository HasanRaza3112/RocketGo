using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
   
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("are you want to quit the game");
            Application.Quit();
        }
    }
}
