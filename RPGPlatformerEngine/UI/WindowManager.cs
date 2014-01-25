using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    /// <summary>
    /// The types of windows in the game.
    /// </summary>
    public enum Windows
    {
        Inventory,
        Upgrades,
        //SaveMap,
        // LoadMap,
        Skills,
        PlayerInfo,
    }

    /// <summary>
    /// A class that handles all the updating/drawing/showing/hiding of windows.
    /// </summary>
    public static class WindowManager
    {
        static Dictionary<string, Window> windows = new Dictionary<string,Window>();

        public static void InitializeWindows()
        {
            foreach (string name in Enum.GetNames(typeof(Windows)))
            {
                string windowName = "RPGPlatformerEngine.UI." + name + "Window";
                Type t = Type.GetType(windowName);
                object obj = Activator.CreateInstance(t);
                Window window = obj as Window;
                windows[name] = window;
            }
 
        }

        /// <summary>
        /// Toggles show/hide of a window.
        /// </summary>
        /// <param name="window"></param>
        public static void ToggleShow(Windows window)
        {
            //Show Window Logic here.
            string windowName = Enum.GetName(typeof(Windows), window);
            Window win = windows[windowName];
            win.Enabled = win.Enabled ? false : true;
        }

        /// <summary>
        /// Gets the window object by the type;
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static Window GetWindow(Windows window)
        {
            string windowName = Enum.GetName(typeof(Windows), window);
            return windows[windowName];
        }

        /// <summary>
        /// Updates all the windows.
        /// </summary>
        public static void Update()
        {
            foreach (Window w in windows.Values)
                if(w.Enabled)
                    w.Update();
        }

        /// <summary>
        /// Draws all the windows.
        /// </summary>
        /// <param name="spriteBatch">A Spritebatch object.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Window w in windows.Values)
                if (w.Enabled)
                    w.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
