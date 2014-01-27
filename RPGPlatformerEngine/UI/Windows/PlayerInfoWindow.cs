using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    public class PlayerInfoWindow : Window
    {
        Label lblMoney, lblExp, lblHealth, lblLevel, lblNextLvlPoints; 
        public PlayerInfoWindow()
            : base(new Vector2(10), TextureManager.SetTexture("UI/form"))
        {

            AddControls(lblMoney = new Label(new Vector2(10, 10), ""),
                        lblExp = new Label(new Vector2(10, 50), ""),
                        lblLevel = new Label(new Vector2(10, 90), ""),
                        lblHealth = new Label(new Vector2(10, 140), ""),
                        lblNextLvlPoints = new Label(new Vector2(10, 190), ""));
        }

        public override void Update()
        {
            base.Update();
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            var stats = Session.Singleton.Player.CurrentStatistics;
            lblMoney.Text = "Money: " + stats.Money;
            lblHealth.Text = "Health: " + stats.Health;
            lblExp.Text = "Experience Points: " + stats.ExperiencePoints;
            lblLevel.Text = "Level: " + stats.Level;
            lblNextLvlPoints.Text = "Points to next level: " + stats.ExpPointsToNextLevel;
        }
    }
}
