using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGPlatformerEngine.UI;

namespace RPGPlatformerEngine
{
    public class ConfirmBox : GameObject
    {
        public Button Yes, No;
        string title;

        public ConfirmBox(Vector2 position,string text) : base(position)
        {
            texture = TextureManager.SetTexture("ConfirmBox");
            title = text;
            boundBox = new Rectangle((int)position.X,(int) position.Y, texture.Width, texture.Height);
            Yes = new Button()
            {
                Text = "Yes",
                Position = new Vector2(position.X, boundBox.Bottom - Yes.Texture.Height - 10)
            };

            No = new Button()
            {
                Text = "No",
                Position = new Vector2((Yes.Position.X + Yes.Texture.Width ) + 10, boundBox.Bottom - No.Texture.Height - 10)
            };

            Color = Color.Yellow;
        }

        public override void Update()
        {  
            Yes.Update();
            No.Update();         
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            sb.Begin();
            sb.DrawString(Font.MenuEntry, title, new Vector2(boundBox.Center.X - (Font.MenuEntry.MeasureString(title).X)/2, boundBox.Top + (Font.MenuEntry.MeasureString(title).Y)/2), Color.Black);
            sb.End();

            No.Draw(sb);
            Yes.Draw(sb);
        }
    }
}
