using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Maps;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Sprites;
using Armalia.Characters;
using Armalia.Sidebar;
using Microsoft.Xna.Framework.Input;
using Armalia.Exceptions;

namespace Armalia.GameScreens
{
    ///<summary>
    ///This class will be used to control what the player sees. This includes the current game level and the section of the game level's map.
    ///</summary>
    class GameplayScreen : Screen
    {
        public static readonly Point DEFAULT_MAP_WINDOW_SIZE = new Point(1000, 720); // (prev: 800, 800)
        public static readonly Point DEFAULT_CAMERA_WINDOW_SIZE = new Point(1000, 720); // 853 ~ (2/3)*1280; (prev: 800, 800 )

        private Player player;
        private GameLevel level;
        private ArmaliaGame game;
        private MapMaker mapMaker;
        private ScreenManager manager;
        private PlayerSidebar sidebar;
        private MapHandler mapHandler;
        private Texture2D splash;
        private Texture2D box;
        private Vector2 borderPos = Vector2.Zero;
        
        // screen components
        private Rectangle mapWindow;
        // private Rectangle hudWindow
        // private List<Tiles or something> uncoveredArea

        public GameplayScreen(ArmaliaGame game, ScreenManager manager)
        {
            this.game = game;
            this.manager = manager;
            mapWindow = new Rectangle(0, 0, DEFAULT_MAP_WINDOW_SIZE.X, DEFAULT_MAP_WINDOW_SIZE.Y);
            mapMaker = new MapMaker(game);
            mapHandler = new MapHandler(game);
        }

        public void Load()
        {
            // TODO: parse all this stuff based on XML config files in DataManager

            // load stuff for cursor thing
            box = game.Content.Load<Texture2D>(@"SpriteImages\border");
            splash = game.Content.Load<Texture2D>(@"SpriteImages\splash");

            try
            {
                level = mapHandler.getMap("village1");
            }
            catch (MapDoesNotExistException e)
            {
                this.game.Exit();
            }
            // create player
            int playerHP = 100;
            int playerMP = 100;
            int playerXP = 0;
            int playerStrength = 10;
            int playerDefense = 10;

            Rectangle cameraView = new Rectangle(0, 0, DEFAULT_CAMERA_WINDOW_SIZE.X, DEFAULT_CAMERA_WINDOW_SIZE.Y);
            Texture2D playerTexture = game.Content.Load<Texture2D>(@"Characters\vx_chara01_b-1-1");
            Point playerTextureFrameSize = new Point(32, 48);
            int playerCollisionOffset = 0;
            Point playerInitialFrame = new Point(1, 0);
            Point playerSheetSize = new Point(3, 4);
            Vector2 playerSpeed = new Vector2(2, 2);
            Vector2 initialPlayerPos = new Vector2(80, 50);

            AnimatedSprite playerSprite = new AnimatedSprite(
                playerTexture, playerTextureFrameSize, playerCollisionOffset, playerInitialFrame, playerSheetSize);

            MainCharacter playerCharacter = new MainCharacter(playerSprite, initialPlayerPos, playerHP, playerMP,
                playerXP, playerStrength, playerDefense, playerSpeed, cameraView);

            player = new Player(playerCharacter);

            // load on-screen menu
            // use RolePlayingGameWindows HUD as example
            Rectangle sidebarWindow = new Rectangle(mapWindow.Width, 0,
                game.Window.ClientBounds.Width - mapWindow.Width, mapWindow.Height);
            sidebar = new PlayerSidebar(game, sidebarWindow, player.PlayerCharacter);
            sidebar.Load();
        }

        public void Update(GameTime gameTime)
        {
            // TODO: move handling of state to ScreenManager
            MouseState mouseState = Mouse.GetState();
            if (manager.CurrentState == GameState.Splash)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    manager.CurrentState = GameState.Exploration;
                }
            }
            else if (manager.CurrentState == GameState.Exploration)
            {
                // draw cursor border
                borderPos.X = (int)Math.Floor((float)(mouseState.X / 32));
                borderPos.Y = (int)Math.Floor((float)(mouseState.Y / 32));
                borderPos.X = (borderPos.X * 32) + (32);
                borderPos.Y = (borderPos.Y * 32) + (32);

                player.Update(gameTime, level.LevelMap); // new Point(1600,1600)
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (manager.CurrentState == GameState.Exploration)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                // draw sidebar
                sidebar.Draw(gameTime, spriteBatch);

                // draw level
                level.Draw(spriteBatch, mapWindow, player.CameraView);

                // draw player
                player.Draw(spriteBatch);

                // draw cursor box
                spriteBatch.Draw(box,
                    borderPos,
                     new Rectangle(0, 0, 32, 32),
                     Color.White,
                     0.0f,
                     Vector2.Zero,
                     1.0f,
                     SpriteEffects.None, 0f);

                spriteBatch.End();
            }
            else if (manager.CurrentState == GameState.Splash)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(splash, new Rectangle(0, 0, splash.Width, splash.Height), Color.White);
                spriteBatch.End();
            }
        }
    }
}
