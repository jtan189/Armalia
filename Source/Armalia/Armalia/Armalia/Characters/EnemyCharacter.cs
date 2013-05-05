using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Sprites;
using Armalia.Levels;
using Microsoft.Xna.Framework.Graphics;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    abstract class EnemyCharacter : CombatableCharacter
    {
        private static Random rand = new Random();

        public static readonly int ATTACK_RANGE = 50;

        private GameplayScreen gameplayScreen;
        private MainCharacter mainCharacter;

        private EnemyState currentState;
        private List<Point> patrolTargets;
        private Point currentTarget;

        public EnemyCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, GameplayScreen gameplayScreen, List<Point> patrolTargets, MainCharacter mainCharacter)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed)
        {
            this.gameplayScreen = gameplayScreen;
            this.patrolTargets = patrolTargets;
            this.mainCharacter = mainCharacter;

            currentState = EnemyState.Patrol;
            currentTarget = patrolTargets[0];
        }

        // TODO: incorporate path finding algorithm or something
        // for now, just oscilate between two x coordinates
        public bool IsNearPatrolTarget()
        {
            if (Math.Abs(Position.X - currentTarget.X) <= speed.X)
            {
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime, Map currentMap)
        {
            // act based on state
            bool hasCollided;
            if (currentState == EnemyState.Patrol)
            {
                Move(gameTime, currentMap, out hasCollided);
            }
        }

        public bool IsNearPlayer()
        {
            if (Vector2.Distance(this.Position, this.mainCharacter.Position) <= (32 * 3))
            {
                return true;
            }
            return false;
        }

        public bool PlayerInAttackRange()
        {
            int playerXCoord = mainCharacter.AsRectangle().X + mainCharacter.AsRectangle().Width / 2; // take midpoint
            int playerYCoord = mainCharacter.AsRectangle().Y + mainCharacter.AsRectangle().Height / 2;
            int enemyXCoord = AsRectangle().X + AsRectangle().Width / 2;
            int enemyYCoord = AsRectangle().Y + AsRectangle().Height / 2;

            if ((Math.Abs(enemyXCoord - playerXCoord) <= ATTACK_RANGE) && (enemyYCoord == playerYCoord) ||
                (Math.Abs(enemyYCoord - playerYCoord) <= ATTACK_RANGE) && (enemyXCoord == playerXCoord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Move(GameTime gameTime, Map currentMap, out bool hasCollided)
        {
            Rectangle cameraView = gameplayScreen.CameraView;
            bool hasMoved = false;
            MoveDirection moveDir = MoveDirection.Right;
            hasCollided = false;

            if (IsNearPatrolTarget())
            {
                // choose random new target
                currentTarget = patrolTargets[rand.Next(patrolTargets.Count)];
            }
            else
            {
                if (IsNearPlayer() && !(this.mainCharacter.AsRectangle().Intersects(this.AsRectangle())))
                {
                    if (this.Position.X > this.mainCharacter.Position.X)
                    {
                        moveDir = MoveDirection.Left;
                    }
                    else if (this.Position.X < this.mainCharacter.Position.X)
                    {
                        moveDir = MoveDirection.Right;
                    }
                    else
                    {
                        if (this.Position.Y > this.mainCharacter.Position.Y)
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
                    if (Position.X < currentTarget.X)
                    {
                        moveDir = MoveDirection.Right;
                    }
                    else
                    {
                        moveDir = MoveDirection.Left;

                    }
                }
                hasMoved = base.Move(moveDir, currentMap, out hasCollided);
                if (hasCollided)
                {
                    switch (moveDir)
                    {
                        case MoveDirection.Up:
                            moveDir = MoveDirection.Down;
                            break;
                        case MoveDirection.Down:
                            moveDir = MoveDirection.Up;
                            break;
                        case MoveDirection.Left:
                            moveDir = MoveDirection.Right;
                            break;
                        case MoveDirection.Right:
                            moveDir = MoveDirection.Left;
                            break;
                    }
                    currentTarget = patrolTargets[rand.Next(patrolTargets.Count)];
                    hasMoved = base.Move(moveDir, currentMap, out hasCollided);

                }
            }

            base.Update(gameTime, moveDir, currentMap);

            return hasMoved;
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
