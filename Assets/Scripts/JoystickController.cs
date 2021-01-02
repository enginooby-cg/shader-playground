using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] VariableJoystick joystickMove;
    [SerializeField] VariableJoystick joystickRotate;

    [SerializeField] float rotateSensitiy = 0.5f;
    private FirstPersonAIO fpsController;
    // Start is called before the first frame update
    void Start()
    {
        fpsController = GetComponent<FirstPersonAIO>();

        if (!Application.isMobilePlatform)
        {
            fpsController.joystickEnabled = false;
            joystickMove.transform.parent.gameObject.SetActive(false);
            joystickRotate.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            fpsController.joystickEnabled = true;
            joystickMove.transform.parent.gameObject.SetActive(true);
            joystickRotate.transform.parent.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        fpsController.Rotate(joystickRotate.Direction * rotateSensitiy);
    }
    void FixedUpdate()
    {
        fpsController.HandleMove(joystickMove.Direction);
    }
}
