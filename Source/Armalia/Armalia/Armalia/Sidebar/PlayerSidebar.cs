using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Characters;
using Armalia.GameScreens;

namespace Armalia.Sidebar
{
    class PlayerSidebar
    {

        public const String SIDEBAR_BG_FILENAME = @"Sidebar\sidebar_bg";

        private ArmaliaGame game;
        private Texture2D sidebarBackground;
        private Rectangle sidebarWindow;
        private ScreenManager manager;

        private MainCharacter mainCharacter;
        public EnemyCharacter Enemy {get; set;};

        public PlayerSidebar(ArmaliaGame game, ScreenManager manager, Rectangle sidebarWindow, MainCharacter mainCharacter)
        {
            this.game = game;
            this.sidebarWindow = sidebarWindow;
            this.mainCharacter = mainCharacter;
            this.manager = manager;

        }

        public void Load()
        {

            sidebarBackground = game.Content.Load<Texture2D>(@"Sidebar\sidebar_bg");

        }

        public void Update(GameTime gameTime)
        {
          
        }

        public void Update(GameTime gameTime, EnemyCharacter enemy)
        {
            Enemy = enemy;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 mainCharNameLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 10);
            Vector2 mainCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 30);
            Vector2 mainCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 50);
            Vector2 mainCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 70);
            Vector2 mainCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 90);
            Vector2 mainCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 110);

            Vector2 enemyCharNameLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 150);
            Vector2 enemyCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 170);
            Vector2 enemyCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 190);
            Vector2 enemyCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 210);
            Vector2 enemyCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 230);
            Vector2 enemyCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 250);

            SpriteFont charStatsFont = game.Content.Load<SpriteFont>(@"SpriteFonts\MainCharacterStats");
            
            spriteBatch.Draw(sidebarBackground, sidebarWindow, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.DrawString(charStatsFont, mainCharacter.Name + ":", mainCharNameLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "HP: " + mainCharacter.HitPoints.ToString(), mainCharHpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "MP: " + mainCharacter.ManaPoints.ToString(), mainCharMpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "XP: " + mainCharacter.ExpLevel.ToString(), mainCharXpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "Strength: " + mainCharacter.Strength.ToString(), mainCharStrengthLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "Defense: " + mainCharacter.Defense.ToString(), mainCharDefenseLocation, Color.Black);

            if (manager.CurrentState == GameState.TransitionToBattle)
            {
                spriteBatch.DrawString(charStatsFont, Enemy.GetType().Name + ":", enemyCharNameLocation, Color.Black);
                spriteBatch.DrawString(charStatsFont, "HP: " + Enemy.HitPoints.ToString(), enemyCharHpLocation, Color.Black);
                spriteBatch.DrawString(charStatsFont, "MP: " + Enemy.ManaPoints.ToString(), enemyCharMpLocation, Color.Black);
                spriteBatch.DrawString(charStatsFont, "XP: " + Enemy.ExpLevel.ToString(), enemyCharXpLocation, Color.Black);
                spriteBatch.DrawString(charStatsFont, "Strength: " + Enemy.Strength.ToString(), enemyCharStrengthLocation, Color.Black);
                spriteBatch.DrawString(charStatsFont, "Defense: " + Enemy.Defense.ToString(), enemyCharDefenseLocation, Color.Black);
            }

        }
    }
}
