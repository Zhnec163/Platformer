using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private event Action<float> Moving;
    private event Action Jumped;
    private event Action BaseAttacked;
    private event Action VampirismAttacked;

    public void Init(Action<float> moving, Action jumped, Action baseAttacked, Action vampirismAttacked)
    {
        Moving = moving;
        Jumped = jumped;
        BaseAttacked = baseAttacked;
        VampirismAttacked = vampirismAttacked;
    }

    private void Update()
    {
        ProcessInput();
    }

    private void OnDestroy()
    {
        Moving = null;
        Jumped = null;
        BaseAttacked = null;
        VampirismAttacked = null;
    }

    private void ProcessInput()
    {
        float horizontalInput = Input.GetAxis(Constant.HorizontalAxis);
        
        if (Mathf.Approximately(0, horizontalInput) == false)
            Moving?.Invoke(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
            Jumped?.Invoke();

        if (Input.GetKeyDown(KeyCode.N))
            BaseAttacked?.Invoke();
        else if (Input.GetKeyDown(KeyCode.M))
            VampirismAttacked?.Invoke();
    }
}