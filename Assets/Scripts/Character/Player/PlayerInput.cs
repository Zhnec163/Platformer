using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private event Action<float> OnMoving;
    private event Action OnJumped;
    private event Action OnBaseAttacked;
    private event Action OnVampirismAttacked;

    public void Init(Action<float> onMoving, Action onJumped, Action onBaseAttacked, Action onVampirismAttacked)
    {
        OnMoving = onMoving;
        OnJumped = onJumped;
        OnBaseAttacked = onBaseAttacked;
        OnVampirismAttacked = onVampirismAttacked;
    }

    private void Update()
    {
        ProcessInput();
    }

    private void OnDestroy()
    {
        OnMoving = null;
        OnJumped = null;
        OnBaseAttacked = null;
        OnVampirismAttacked = null;
    }

    private void ProcessInput()
    {
        float horizontalInput = Input.GetAxis(Constant.HorizontalAxis);
        
        if (Mathf.Approximately(0, horizontalInput) == false)
            OnMoving?.Invoke(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
            OnJumped?.Invoke();

        if (Input.GetKeyDown(KeyCode.N))
            OnBaseAttacked?.Invoke();
        else if (Input.GetKeyDown(KeyCode.M))
            OnVampirismAttacked?.Invoke();
    }
}