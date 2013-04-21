using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    public class Window : Control
    {
        protected List<Control> Controls;

        Vector2 prevPosition; 

        public Window(Vector2 position,Texture2D texture) : base(position,texture)
        {
            Controls = new List<Control>();
        }

        public override void Update()
        {
            prevPosition = Position;
            HandleDragging();
            foreach (Control c in Controls)
                c.Update();
        }

        private void HandleDragging()
        {
            if (Input.MouseRectangle.Intersects(BoundBox) && Input.LeftButtonDown())
                Position += Input.MousePosition - Input.PrevMousePosition;
            //Update each of the controls' positions.
            foreach (Control c in Controls)
                c.Position += Position - prevPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Control c in Controls)
                c.Draw(spriteBatch);
        }

        public void AddControls(params Control[] controls)
        {
            Controls.AddRange(controls);
            foreach (Control c in controls)
                c.Position += Position;
        }
    }
}
