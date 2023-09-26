using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{


    //public Texture2D CrosshairTexture;

    public Vector2 MouseInputDelta { private set; get; }
    public Vector2 MousePositionOnScreen { private set; get; }
    public Vector2 MoveInput { private set; get; }
    public bool ConfineMouseInput { private set; get; }
    public bool IsSprining { private set; get; }
    public bool IsJumping { private set; get; }
    public bool IsKneel { private set; get; }
    public bool IsCrouch { private set; get; }
    public float RotateInput { private set; get; }
    public float InOutInput { private set; get; }

    #region Focus
    private void OnApplicationFocus(bool hasFocus) => SetCursorState(false);
    private void OnApplicationPause() => SetCursorState(true);

    private void SetCursorState(bool newState)
    {
        ConfineMouseInput = newState;
        Cursor.lockState = newState ? CursorLockMode.None : CursorLockMode.Confined;
        //Cursor.SetCursor(CrosshairTexture, new Vector2(CrosshairTexture.width/2f, CrosshairTexture.height/2f),CursorMode.Auto);
    }

    #endregion

    #region Move
    void OnMove(InputValue value) => SetNewMoveInput(value.Get<Vector2>());
    private void SetNewMoveInput(Vector2 newMoveInput)
    {
        MoveInput = newMoveInput;
    }

    void OnSprint(InputValue value) => SetNewSprintInput(value.isPressed);

    private void SetNewSprintInput(bool newSprintInput)
    {
        IsSprining = newSprintInput;
    }

    void OnJump(InputValue value) => SetNewJumpInput(value.isPressed);

    private void SetNewJumpInput(bool newJumpInput)
    {
        IsJumping = newJumpInput;
    }

    void OnKneel(InputValue value) => SetNewKneelInput(value.isPressed);

    private void SetNewKneelInput(bool newKneelInput)
    {
        if (newKneelInput) IsKneel = !IsKneel;
    }

    void OnCrouch(InputValue value) => SetNewCrouchInput(value.isPressed);

    private void SetNewCrouchInput(bool newCrouchInput)
    {
        if (newCrouchInput) IsCrouch = !IsCrouch;
    }
    #endregion

    #region Look
    void OnLook(InputValue value) => SetNewMouseDelta(value.Get<Vector2>());
    private void SetNewMouseDelta(Vector2 newDelta)
    {
        MouseInputDelta = newDelta;
        MousePositionOnScreen = Mouse.current.position.value;
    }

    #region Rotate

    void OnRotate(InputValue value) => SetNewLockState(value.Get<float>());
    private void SetNewLockState(float newInput)
    {
        RotateInput = newInput;
    }

    #endregion

    #region InOut

    void OnInOut(InputValue value) => SetNewInOut(value.Get<float>());
    private void SetNewInOut(float newIo)
    {
        InOutInput = newIo;
    }

    #endregion
    #endregion
}
