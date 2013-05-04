using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Armalia.Characters;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Armalia.Levels;

namespace Armalia
{
    class Player
    {

        public MainCharacter PlayerCharacter { get; set; }
        public Rectangle CameraView { get { return PlayerCharacter.CameraView; } }

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

        public Player(MainCharacter character)
        {
            PlayerCharacter = character;
        }


        public bool PressedAttack()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.K))
                return true;
            else
                return false;
        }

        public void Update(GameTime gameTime, Map currentMap)
        {
            PlayerCharacter.Update(gameTime, KeyInputDirection, currentMap, PressedAttack());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerCharacter.Draw(spriteBatch);
        }

    }
}
