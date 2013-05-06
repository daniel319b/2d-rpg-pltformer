using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace RPGPlatformerEngine
{
    static class InputActions
    {
        public static void Update()
        {
            OnKeyPress(Keys.Escape, () => { }/*ScreenSystem.Show(Screens.Pause)*/);
            OnKeyPress(Keys.E, () => UI.WindowManager.ToggleShow(UI.Windows.Inventory));     
            OnKeyPress(Keys.U, () => UI.WindowManager.ToggleShow(UI.Windows.Upgrades));
            OnKeyPress(Keys.I, () => UI.WindowManager.ToggleShow(UI.Windows.Skills));
        }

        private static void OnKeyPress(Keys k, Action a)
        {
            if (Input.KeyPressed(k))
                a(); 
        }
    }

    
}
