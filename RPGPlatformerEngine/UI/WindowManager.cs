﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    public enum Windows
    {
        Inventory,
        Upgrades,
        //SaveMap,
       // LoadMap,
        Skills,
    }

    public static class WindowManager
    {
        static Dictionary<string, Window> windows = new Dictionary<string,Window>();

        public static void InitializeWindows()
        {
            
            foreach (string name in Enum.GetNames(typeof(Windows)))
            {
                string windowName = "RPGPlatformerEngine.UI." + name + "Window";
                Type t = Type.GetType(windowName);
                ConstructorInfo c = t.GetConstructor(new Type[0]);
                object obj = c.Invoke(new object[0]);
                Window window = obj as Window;
                windows[name] = window;
            }
 
        }

        public static void Show(Windows window)
        {
            //Show Window Logic here.
            string windowName = Enum.GetName(typeof(Windows), window);
            Window win = windows[windowName];
            win.Enabled = true;
        }

        public static void Update()
        {
            foreach (Window w in windows.Values)
                w.Update();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Window w in windows.Values)
                w.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
