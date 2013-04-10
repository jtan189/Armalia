using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Sprites;
using Armalia.Maps;
using Microsoft.Xna.Framework.Graphics;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    abstract class EnemyCharacter : CombatableCharacter
    {
        private static Random rand = new Random();

        private List<Point> patrolTargets;
        private Point currentTarget;
        private EnemyState currentState;
        private GameplayScreen gameplayScreen;
        // private SomeShape fieldOfVision;
        private Character player;
        public EnemyCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, GameplayScreen gameplayScreen, List<Point> patrolTargets, Character p)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed)
        {
            this.gameplayScreen = gameplayScreen;
            this.patrolTargets = patrolTargets;
            currentState = EnemyState.Patrol;
            currentTarget = patrolTargets[0];
            this.player = p;
        }

        // TODO: incorporate path finding algorithm or something
        // for now, just oscilate between two x coordinates
        public bool IsNearPatrolTarget()
        {
            if (Math.Abs(position.X - currentTarget.X) <= speed.X)
            {
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime, Map currentMap)
        {

            // see if should change state

            // act based on state
            bool hasCollided;
            if (currentState == EnemyState.Patrol)
            {
                Move(gameTime, currentMap, out hasCollided);
            }
        }
        public bool isNearPLayer()
        {
            if (Vector2.Distance(this.position, this.player.position) <= (32*5))
            {
                return true;
            }
            return false;
        }
        public bool Move(GameTime gameTime, Map currentMap, out bool hasCollided)
        {
            Rectangle cameraView = gameplayScreen.CameraView;
            bool hasMoved = false;
            MoveDirection moveDir = MoveDirection.None;
            hasCollided = false;
          

            if (IsNearPatrolTarget())
            {
                // choose random new target
                currentTarget = patrolTargets[rand.Next(patrolTargets.Count)];
            }
            else
            {
                if (isNearPLayer() && !(this.player.getRectangle().Intersects( this.getRectangle() ) ) )
                {
                    if (this.position.X > this.player.position.X)
                    {
                        moveDir = MoveDirection.Left;
                    }
                    else if (this.position.X < this.player.position.X)
                    {
                        moveDir = MoveDirection.Right;
                    }
                    else
                    {
                        if (this.position.Y > this.player.position.Y)
                        {
                            moveDir = MoveDirection.Up;
                        }
                        else
                        {
                            moveDir = MoveDirection.Down;
                        }
                    }
                }
                
                else
                {
                    if (position.X < currentTarget.X)
                    {
                        moveDir = MoveDirection.Right;
                    }
                    else
                    {
                        moveDir = MoveDirection.Left;
                        
                    }
                }
                hasMoved = base.Move(moveDir, currentMap, out hasCollided);
            }

            // this is gross. change it
            base.Update(gameTime, moveDir, currentMap);

            return hasMoved;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int xOffset = (int)position.X - gameplayScreen.CameraView.X;
            int yOffset = (int)position.Y - gameplayScreen.CameraView.Y;
            Vector2 drawPosition = new Vector2(xOffset, yOffset);
            // normalize position relative to camera view
            sprite.Draw(spriteBatch, drawPosition, DEFAULT_LAYER_DEPTH);
        }

        enum StatusEffect
        {
            Cursed
        }

        enum EnemyState
        {
            Patrol,
            Detection,
            Battle,
            Defeat
        }
    }
}
