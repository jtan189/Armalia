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
        private ScreenManager manager;
        private GameplayScreen gameplayScreen;
        private MainCharacter mainCharacter;

        private Texture2D sidebarBackground;
        private Rectangle sidebarWindow;

        Vector2 mainCharNameLocation;
        Vector2 mainCharHpLocation;
        Vector2 mainCharMpLocation;
        Vector2 mainCharXpLocation;
        Vector2 mainCharStrengthLocation;
        Vector2 mainCharDefenseLocation;

        Vector2 enemyCharNameLocation;
        Vector2 enemyCharHpLocation;
        Vector2 enemyCharMpLocation;
        Vector2 enemyCharXpLocation;
        Vector2 enemyCharStrengthLocation;
        Vector2 enemyCharDefenseLocation;

        SpriteFont charStatsFont;

        public PlayerSidebar(ArmaliaGame game, ScreenManager manager, GameplayScreen gameplayScreen, Rectangle sidebarWindow, MainCharacter mainCharacter)
        {
            this.game = game;
            this.sidebarWindow = sidebarWindow;
            this.mainCharacter = mainCharacter;
            this.manager = manager;
            this.gameplayScreen = gameplayScreen;

            mainCharNameLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 10);
            mainCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 30);
            mainCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 50);
            mainCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 70);
            mainCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 90);
            mainCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 110);

            enemyCharNameLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 150);
            enemyCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 170);
            enemyCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 190);
            enemyCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 210);
            enemyCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 230);
            enemyCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 250);
        }

        public void Load()
        {
            sidebarBackground = game.Content.Load<Texture2D>(@"Sidebar\sidebar_bg");
            charStatsFont = game.Content.Load<SpriteFont>(@"SpriteFonts\MainCharacterStats");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            # region Draw Main Character Stats

            spriteBatch.Draw(sidebarBackground, sidebarWindow, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.DrawString(charStatsFont, mainCharacter.Name + ":", mainCharNameLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "HP: " + mainCharacter.HitPoints.ToString(), mainCharHpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "MP: " + mainCharacter.ManaPoints.ToString(), mainCharMpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "XP: " + mainCharacter.ExpLevel.ToString(), mainCharXpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "Strength: " + mainCharacter.Strength.ToString(), mainCharStrengthLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "Defense: " + mainCharacter.Defense.ToString(), mainCharDefenseLocation, Color.Black);

            # endregion

            //EnemyCharacter enemy = gameplayScreen.CurrentBattle.Enemy;
            //spriteBatch.DrawString(charStatsFont, enemy.GetType().Name + ":", enemyCharNameLocation, Color.Black);
            //spriteBatch.DrawString(charStatsFont, "HP: " + enemy.HitPoints.ToString(), enemyCharHpLocation, Color.Black);
            //spriteBatch.DrawString(charStatsFont, "MP: " + enemy.ManaPoints.ToString(), enemyCharMpLocation, Color.Black);
            //spriteBatch.DrawString(charStatsFont, "XP: " + enemy.ExpLevel.ToString(), enemyCharXpLocation, Color.Black);
            //spriteBatch.DrawString(charStatsFont, "Strength: " + enemy.Strength.ToString(), enemyCharStrengthLocation, Color.Black);
            //spriteBatch.DrawString(charStatsFont, "Defense: " + enemy.Defense.ToString(), enemyCharDefenseLocation, Color.Black);

        }
    }
}
