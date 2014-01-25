using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine
{
    public abstract class CoinItem : PickableItem
    {
        abstract protected int moneyValue { get; }

        public override Rectangle BoundBox
        {
            get
            {
                return new Rectangle((int)Position.X - (int)animation.FrameSize.X / 2, (int)Position.Y - (int)animation.FrameSize.Y / 2, (int)animation.FrameSize.X, (int)animation.FrameSize.Y);
            }
        }

        public override string Name
        {
            get { return "Money"; }
        }

        public Animation animation { get; protected set; }


        public CoinItem() { Scale = 1; }

        public override void Pick()
        {
            // here we need to add to the money of the player since we are not treating 'money' as inventory item.
            Session.Singleton.Player.CurrentStatistics.Money += moneyValue;
            this.Alive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Alive) return;
            base.Update();
            animation.Position = new Vector2(Position.X, Position.Y);
            animation.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!Alive) return;
            animation.Draw(sb);
        }
    }

    public class BronzeCoin : CoinItem
    {
        protected override int moneyValue { get { return 10; } }

        public override Texture2D Texture { get { return TextureManager.SetTexture("Items/coin_bronze"); } }

        public BronzeCoin()
        {
            animation = new Animation(TextureManager.SetTexture("Items/coin_bronze"), 4, 1, 7);
        }
    }

    public class GoldCoin : CoinItem
    {
        protected override int moneyValue { get { return 50; } }

        public override Texture2D Texture { get { return TextureManager.SetTexture("coin_gold"); } }

        public GoldCoin()
        {
            animation = new Animation(TextureManager.SetTexture("coin_gold_animation"), 3, 1, 10);
        }

    }
}
