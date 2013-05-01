using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Armalia.Characters;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Armalia.Maps;

namespace Armalia
{
    class Player
    {

        private MainCharacter character;

        public Player(MainCharacter character)
        {
            this.character = character;
        }

        public MainCharacter PlayerCharacter { get { return character; } }

        public Character.MoveDirection KeyInputDirection
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

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

        public bool PressedAttack()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.K))
                return true;
            else
                return false;
        }

        public Rectangle CameraView { get { return character.CameraView; } }

        public void Update(GameTime gameTime, Map currentMap)
        {
            character.Update(gameTime, KeyInputDirection, currentMap);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            character.Draw(spriteBatch);
        }

    }
}
