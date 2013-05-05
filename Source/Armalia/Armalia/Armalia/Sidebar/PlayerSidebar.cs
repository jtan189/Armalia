using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Characters;
using Armalia.GameScreens;
using Armalia.Levels;

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
        private Rectangle minimapArea;

        Vector2 mainCharNameLocation;
        Vector2 mainCharHpLocation;
        Vector2 mainCharMpLocation;
        Vector2 mainCharXpLocation;
        Vector2 mainCharStrengthLocation;
        Vector2 mainCharDefenseLocation;

        SpriteFont charStatsFont;

        public PlayerSidebar(ArmaliaGame game, ScreenManager manager, GameplayScreen gameplayScreen, Rectangle sidebarWindow, MainCharacter mainCharacter)
        {
            this.game = game;
            this.sidebarWindow = sidebarWindow;
            this.mainCharacter = mainCharacter;
            this.manager = manager;
            this.gameplayScreen = gameplayScreen;

            minimapArea = new Rectangle(sidebarWindow.X, sidebarWindow.Y, sidebarWindow.Width, sidebarWindow.Width);

            mainCharNameLocation = new Vector2(sidebarWindow.X + 10, minimapArea.Height + 10);
            mainCharHpLocation = new Vector2(sidebarWindow.X + 10, minimapArea.Height + 30);
            mainCharMpLocation = new Vector2(sidebarWindow.X + 10, minimapArea.Height + 50);
            mainCharXpLocation = new Vector2(sidebarWindow.X + 10, minimapArea.Height + 70);
            mainCharStrengthLocation = new Vector2(sidebarWindow.X + 10, minimapArea.Height + 90);
            mainCharDefenseLocation = new Vector2(sidebarWindow.X + 10, minimapArea.Height + 110);
        }

        public void Load()
        {
            sidebarBackground = game.Content.Load<Texture2D>(@"Sidebar\sidebar_bg");
            charStatsFont = game.Content.Load<SpriteFont>(@"SpriteFonts\MainCharacterStats");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameLevel currentLevel)
        {
            # region Draw Minimap

            spriteBatch.Draw(currentLevel.LevelMap.MapImage, minimapArea, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);

            # endregion

            # region Draw Main Character Stats

            spriteBatch.Draw(sidebarBackground, sidebarWindow, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.DrawString(charStatsFont, mainCharacter.Name + ":", mainCharNameLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "HP: " + mainCharacter.HitPoints.ToString(), mainCharHpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "MP: " + mainCharacter.ManaPoints.ToString(), mainCharMpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "XP: " + mainCharacter.ExpLevel.ToString(), mainCharXpLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "Strength: " + mainCharacter.Strength.ToString(), mainCharStrengthLocation, Color.Black);
            spriteBatch.DrawString(charStatsFont, "Defense: " + mainCharacter.Defense.ToString(), mainCharDefenseLocation, Color.Black);

            # endregion

        }
    }
}
