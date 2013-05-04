using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Levels;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Sprites;
using Armalia.Characters;
using Armalia.Sidebar;
using Microsoft.Xna.Framework.Input;
using Armalia.Exceptions;
using Microsoft.Xna.Framework.Media;
using Armalia.BattleSystem;

namespace Armalia.GameScreens
{
    ///<summary>
    ///This class will be used to control what the player sees.
    ///This includes the current game level and the section of the game level's map.
    ///</summary>
    class GameplayScreen : Screen
    {
        /// <summary>
        /// Default map window size (section of the screen for map)
        /// </summary>
        public static readonly Point DEFAULT_MAP_WINDOW_SIZE = new Point(1000, 720);

        /// <summary>
        /// The default viewable map for the player
        /// </summary>
        public static readonly Point DEFAULT_CAMERA_WINDOW_SIZE = new Point(1000, 720);

        private Player player;
        private GameLevel level;
        private Rectangle mapWindow;
        private PlayerSidebar sidebar;
        private LevelManager levelManager;

        public Battle CurrentBattle { get; set; }
        public Rectangle CameraView { get { return player.CameraView; } }

        /// <summary>
        /// This creates an object for dealing with the gameplay screen (the left portion)
        /// </summary>
        /// <param name="game">The game object of the game</param>
        /// <param name="manager">The screen manager for the game</param>
        public GameplayScreen(ArmaliaGame game, ScreenManager manager)
            : base(game, manager)
        {
            mapWindow = new Rectangle(0, 0, DEFAULT_MAP_WINDOW_SIZE.X, DEFAULT_MAP_WINDOW_SIZE.Y);
        }

        public void Load()
        {
            // TODO: parse all this stuff based on XML config files in DataManager

            # region Player Weapon Initialization Region

            Texture2D swordTexture = game.Content.Load<Texture2D>(@"Attacks\massive_sword_single");
            int swordMsPerFrame = 16;
            float swordScale = 0.5f;
            Vector2 swordRotationPoint = new Vector2(12, 200);

            SwordSprite playerSwordSprite = new SwordSprite(swordTexture, swordMsPerFrame, swordScale, swordRotationPoint);

            # endregion

            # region Player Sprite Initialization Region

            Texture2D playerTexture = game.Content.Load<Texture2D>(@"Characters\vx_chara01_b-1-1");
            Point playerTextureFrameSize = new Point(32, 48);
            int playerCollisionOffset = 0;
            Point playerInitialFrame = new Point(1, 0);
            Point playerSheetSize = new Point(3, 4);

			// TODO: necessary?
            Vector2 playerSpeed = new Vector2(2, 2);
            Vector2 initialPlayerPos = new Vector2(80, 50);

            AnimatedSprite playerSprite = new AnimatedSprite(
                playerTexture, playerTextureFrameSize, playerCollisionOffset, playerInitialFrame, playerSheetSize);

            # endregion

            # region Player Initialization Region

            string playerName = "Justin";
            int playerHP = 100;
            int playerMP = 100;
            int playerXP = 0;
            int playerStrength = 10;
            int playerDefense = 10;
            Vector2 playerSpeed = new Vector2(2, 2);
            Vector2 initialPlayerPos = new Vector2(80, 50);

            Rectangle cameraView = new Rectangle(0, 0, DEFAULT_CAMERA_WINDOW_SIZE.X, DEFAULT_CAMERA_WINDOW_SIZE.Y);

            MainCharacter playerCharacter = new MainCharacter(playerName, playerSprite, initialPlayerPos, playerHP, playerMP,
                playerXP, playerStrength, playerDefense, playerSpeed, cameraView, playerSwordSprite);

            player = new Player(playerCharacter);

            # endregion

            # region Level Initialization Region

            string initialLevelName = "village1";
            levelManager = new LevelManager(game, playerCharacter, this);
            try
            {
                level = levelManager.GetLevel(initialLevelName);

                // start background music
                level.PlayBgMusic();
                MediaPlayer.IsRepeating = true;
            }
            catch (LevelDoesNotExistException)
            {
                Console.WriteLine("Map \"{0}\" not found.", initialLevelName);
                this.game.Exit();
            }

            # endregion

            # region Sidebar Initialization Region

            Rectangle sidebarWindow = new Rectangle(mapWindow.Width, 0,
                game.Window.ClientBounds.Width - mapWindow.Width, mapWindow.Height);
            sidebar = new PlayerSidebar(game, manager, this, sidebarWindow, player.PlayerCharacter);
            sidebar.Load();

            # endregion

        }

        public void Update(GameTime gameTime)
        {

            switch (manager.CurrentState)
            {
                case GameState.Gameplay:

                // level teleportation stuff

                foreach (LevelObject obj in level.LevelObjects)
                {
                    if (obj.GetType() == typeof(Portal))
                    {
                        Portal portal = (Portal)obj;
                        if (portal.Collides(player.PlayerCharacter))
                        {
                            GameLevel teleportLevel = portal.DestinationLevel;
                            level = teleportLevel;
                            
                            playerCharacter.setPosition(portal.CharStartPosition, level.LevelMap);
                        }
                    }
                }

                    player.Update(gameTime, level.LevelMap);
                    level.Update(gameTime);
                    sidebar.Update(gameTime);
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (manager.CurrentState)
            {
                case GameState.Gameplay:
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                    sidebar.Draw(gameTime, spriteBatch);
                    level.Draw(spriteBatch, mapWindow, player.CameraView);
                    player.Draw(spriteBatch);

                    spriteBatch.End();
                    break;
            }
        }

    }
}
