using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum ButtonInputState
{
    None,
    Pressed,
    Holded,
    Released
}
public static class ButtonControlExtension
{
    public static ButtonInputState IsCurrentState(this ButtonControl buttonControl, ref bool pressed)
    {
        if (!pressed && buttonControl.isPressed)
        {
            pressed = true;
            return ButtonInputState.Pressed;
        }
        else if (pressed && !buttonControl.isPressed)
        {
            pressed = false;
            return ButtonInputState.Released;
        }
        else if (pressed)
        {
            return ButtonInputState.Holded;
        }
        else
        {
            return ButtonInputState.None;
        }
    }
}