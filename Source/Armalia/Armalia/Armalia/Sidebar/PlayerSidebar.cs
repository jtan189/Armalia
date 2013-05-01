using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Characters;
using Armalia.Maps;

namespace Armalia.Sidebar
{
    class PlayerSidebar
    {

        public const String SIDEBAR_BG_FILENAME = @"Sidebar\sidebar_bg";

        private ArmaliaGame game;
        private Texture2D sidebarBackground;
        private Rectangle sidebarWindow;

        private MainCharacter mainCharacter;
        private EnemyCharacter enemy;

        private GameLevel gamelevel;
        private Rectangle view = new Rectangle(0, 0, 1600, 1600);
        private Rectangle map = new Rectangle(1000, 0, 300, 300);



        public PlayerSidebar(ArmaliaGame game, Rectangle sidebarWindow, MainCharacter mainCharacter,
            GameLevel gameLevel)
        {
            this.game = game;
            this.sidebarWindow = sidebarWindow;
            this.mainCharacter = mainCharacter;
            //this.enemy = enemy;
            this.gamelevel = gameLevel;
        }

        public void Load()
        {

            sidebarBackground = game.Content.Load<Texture2D>(@"Sidebar\sidebar_bg");

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //if(battlemode == true)
            {
                //int mapheight = 300;

                //Vector2 mainCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 10 + mapheight);
                //Vector2 mainCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 30 + mapheight);
                //Vector2 mainCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 50 + mapheight);
                //Vector2 mainCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 70 + mapheight);
                //Vector2 mainCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 90 + mapheight);
                //SpriteFont mainCharStats = game.Content.Load<SpriteFont>(@"SpriteFonts\MainCharacterStats");


                //spriteBatch.Draw(sidebarBackground, sidebarWindow, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                //spriteBatch.DrawString(mainCharStats, "HP: " + mainCharacter.HitPoints.ToString(), mainCharHpLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "MP: " + mainCharacter.ManaPoints.ToString(), mainCharMpLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "XP: " + mainCharacter.ExpLevel.ToString(), mainCharXpLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "Strength: " + mainCharacter.Strength.ToString(), mainCharStrengthLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "Defense: " + mainCharacter.Defense.ToString(), mainCharDefenseLocation, Color.Black);

                //Vector2 enemyCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 210);
                //Vector2 enemyCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 230);
                //Vector2 enemyCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 250);
                //Vector2 enemyCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 270);
                //Vector2 enemyCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 290);

                //spriteBatch.DrawString(mainCharStats, "HP: " + enemy.HitPoints.ToString(), enemyCharHpLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "MP: " + enemy.ManaPoints.ToString(), enemyCharMpLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "XP: " + enemy.ExpLevel.ToString(), enemyCharXpLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "Strength: " + enemy.Strength.ToString(), enemyCharStrengthLocation, Color.Black);
                //spriteBatch.DrawString(mainCharStats, "Defense: " + enemy.Defense.ToString(), enemyCharDefenseLocation, Color.Black);


                ////Items Menu
                //Vector2 itemLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 130 + mapheight);
                //spriteBatch.DrawString(mainCharStats, "Item: ", itemLocation, Color.CornflowerBlue);

                ////Weapon or armor menu
                //Vector2 weaponLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 260 + mapheight);
                //spriteBatch.DrawString(mainCharStats, "Weapon: ", weaponLocation, Color.CornflowerBlue);
            }

            //else
            {
                //implement non-attack sidebar

                int mapheight = 300;

                Vector2 mainCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 10 + mapheight);
                Vector2 mainCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 30 + mapheight);
                Vector2 mainCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 50 + mapheight);
                Vector2 mainCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 70 + mapheight);
                Vector2 mainCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 90 + mapheight);
                SpriteFont mainCharStats = game.Content.Load<SpriteFont>(@"SpriteFonts\MainCharacterStats");

                Rectangle view = new Rectangle(0, 0, 1600, 1600);
                Rectangle map = new Rectangle(sidebarWindow.X, sidebarWindow.Y, 300, 300);

                gamelevel.Draw(spriteBatch, map, view);

                spriteBatch.Draw(sidebarBackground, sidebarWindow, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                spriteBatch.DrawString(mainCharStats, "HP: " + mainCharacter.HitPoints.ToString(), mainCharHpLocation, Color.Black);
                spriteBatch.DrawString(mainCharStats, "MP: " + mainCharacter.ManaPoints.ToString(), mainCharMpLocation, Color.Black);
                spriteBatch.DrawString(mainCharStats, "XP: " + mainCharacter.ExpLevel.ToString(), mainCharXpLocation, Color.Black);
                spriteBatch.DrawString(mainCharStats, "Strength: " + mainCharacter.Strength.ToString(), mainCharStrengthLocation, Color.Black);
                spriteBatch.DrawString(mainCharStats, "Defense: " + mainCharacter.Defense.ToString(), mainCharDefenseLocation, Color.Black);

                //Items Menu
                Vector2 itemLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 130 + mapheight);
                spriteBatch.DrawString(mainCharStats, "Item: ", itemLocation, Color.Black);

                //Weapon or armor menu
                Vector2 weaponLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 260 + mapheight);
                spriteBatch.DrawString(mainCharStats, "Weapon: ", weaponLocation, Color.Black);
            }

        }

    }
}




