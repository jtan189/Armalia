using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Characters;

namespace Armalia.Sidebar
{
    class PlayerSidebar
    {

        public const String SIDEBAR_BG_FILENAME = @"Sidebar\sidebar_bg";

        private ArmaliaGame game;
        private Texture2D sidebarBackground;
        private Rectangle sidebarWindow;

        private MainCharacter mainCharacter;

        public PlayerSidebar(ArmaliaGame game, Rectangle sidebarWindow, MainCharacter mainCharacter)
        {
            this.game = game;
            this.sidebarWindow = sidebarWindow;
            this.mainCharacter = mainCharacter;
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
            Vector2 mainCharHpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 10);
            Vector2 mainCharMpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 30);
            Vector2 mainCharXpLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 50);
            Vector2 mainCharStrengthLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 70);
            Vector2 mainCharDefenseLocation = new Vector2(sidebarWindow.X + 10, sidebarWindow.Y + 90);
            SpriteFont mainCharStats = game.Content.Load<SpriteFont>(@"SpriteFonts\MainCharacterStats");
            
            spriteBatch.Draw(sidebarBackground, sidebarWindow, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.DrawString(mainCharStats, "HP: " + mainCharacter.HitPoints.ToString(), mainCharHpLocation, Color.Black);
            spriteBatch.DrawString(mainCharStats, "MP: " + mainCharacter.ManaPoints.ToString(), mainCharMpLocation, Color.Black);
            spriteBatch.DrawString(mainCharStats, "XP: " + mainCharacter.ExpLevel.ToString(), mainCharXpLocation, Color.Black);
            spriteBatch.DrawString(mainCharStats, "Strength: " + mainCharacter.Strength.ToString(), mainCharStrengthLocation, Color.Black);
            spriteBatch.DrawString(mainCharStats, "Defense: " + mainCharacter.Defense.ToString(), mainCharDefenseLocation, Color.Black);

        }
    }
}
