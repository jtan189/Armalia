using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Armalia.Characters;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Armalia
{
    class Player
    {

        private MainCharacter character;

        public Player(MainCharacter character)
        {
            this.character = character;
        }

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

        public void Update(GameTime gameTime, Point mapSize)
        {
            character.Update(gameTime, KeyInputDirection, mapSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            character.Draw(spriteBatch);
        }

    }
}
