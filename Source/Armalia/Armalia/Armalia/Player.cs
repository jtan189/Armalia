﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Armalia.Characters;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Armalia.Levels;
using Armalia.GameScreens;

namespace Armalia
{
    class Player
    {
        private GameplayScreen gameplayScreen;
        public MainCharacter PlayerCharacter { get; set; }

        public Rectangle CameraView {
            get { return gameplayScreen.CameraView; }
            
        }
        
        public Character.MoveDirection KeyInputDirection
        {
            get
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    return Character.MoveDirection.Up;
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    return Character.MoveDirection.Down;
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                    return Character.MoveDirection.Left;
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    return Character.MoveDirection.Right;
                else
                    return Character.MoveDirection.None;
            }
        }

        public Player(MainCharacter character, GameplayScreen gameplayScreen)
        {
            PlayerCharacter = character;
            this.gameplayScreen = gameplayScreen;
        }

        public bool PressedAttack()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.K))
                return true;
            else
                return false;
        }

        public void Update(GameTime gameTime, GameLevel currentLevel)
        {
            PlayerCharacter.Update(gameTime, KeyInputDirection, currentLevel, PressedAttack());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerCharacter.Draw(spriteBatch, CameraView);
        }

    }
}
