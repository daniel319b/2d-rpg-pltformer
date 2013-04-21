using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGPlatformerEngine.UI
{
    public class PlayerUpgradesWindow : Window
    {    
        List<PlayerUpgrade> upgrades;

        public PlayerUpgradesWindow(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            upgrades = new List<PlayerUpgrade>();
            for (int i = 1; i < 5; i++)
                upgrades.Add(TextureManager.Content.Load<PlayerUpgrade>("Upgrade" + i));

            AddControls(new Label(new Vector2(10, 10), "Upgrade:"),
                        new Label(new Vector2(220, 10), "Description:"),
                        new Label(new Vector2(530, 10), "Amount of upgrade:"),
                        new Label(new Vector2(730, 10), "Level:"));

            int offset = 60;
            //Create lables with the text of the upgrades.
            foreach (PlayerUpgrade upgrade in upgrades)
            {
                Button b = new Button()
                {
                    Position = new Vector2(800, offset),
                    Text = "Upgrade",
                    EventArgs = new UpgradeEventArgs(upgrade)
                };
                b.Clicked += new EventHandler(upgrade_Clicked);

                AddControls(new Label(new Vector2(10, offset), upgrade.Name),
                            new Label(new Vector2(220, offset), upgrade.Description),
                            new Label(new Vector2(530, offset), upgrade.UpgradePercent.ToString() + "  %"),
                            new Label(new Vector2(730, offset), upgrade.Level.ToString()),
                            b);

                offset += 30;
            }
        }

        void upgrade_Clicked(object sender, EventArgs e)
        {
            PlayerUpgrade upgrade = ((UpgradeEventArgs)e).upgrade;//get the clicked upgrade.
            UpgradesEffects.effects[upgrade.Name](upgrade);//call the upgrade method.

            upgrade.Level++;
            Session.Singleton.Player.CurrentStatistics.UpgradePoints--;

            //Update the lables.
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is Label && ((Label)Controls[i]).Text == upgrade.Description)
                {
                    ((Label)Controls[i + 2]).Text = upgrade.Level.ToString();
                    ((Label)Controls[i + 1]).Text = upgrade.UpgradePercent + "  %";
                }
            }
        }

    }


    class UpgradeEventArgs : EventArgs
    {
        public PlayerUpgrade upgrade;

        public UpgradeEventArgs(PlayerUpgrade upgrade)
        {
            this.upgrade = upgrade;
        }
    }

    delegate void UpgradePlayer(PlayerUpgrade upgrade);

    class UpgradesEffects
    {
        public static Dictionary<string, UpgradePlayer> effects = new Dictionary<string, UpgradePlayer>()
        {
            {"Health", UpgradeHealth},
            {"Weapon Damage", UpgradeWeaponDamage},
            {"Grenade Damage", UpgradeGranadeDamage},
            {"Reload Speed", UpgradeReloadSpeed}
        };

        static void UpgradeHealth(PlayerUpgrade upgrade)
        {
            int maxhealth = Session.Singleton.Player.CurrentStatistics.MaxHealth;
            maxhealth += (int)(maxhealth + maxhealth * upgrade.UpgradePercent);
            Session.Singleton.Player.CurrentStatistics.MaxHealth = maxhealth;
        }

        static void UpgradeWeaponDamage(PlayerUpgrade upgrade)
        {

        }

        static void UpgradeGranadeDamage(PlayerUpgrade upgrade)
        {

        }

        static void UpgradeReloadSpeed(PlayerUpgrade upgrade)
        {

        }
    }
}
