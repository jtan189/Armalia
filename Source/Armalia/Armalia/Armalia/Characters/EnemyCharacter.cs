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
        public const int VISION_RANGE = 100;
        public const int ATTACK_RANGE = 40;

        protected EnemyState currentState;
        private List<Vector2> patrolTargets;
        private Vector2 currentTarget;
        public MainCharacter PlayerCharacter { get; set; }

        public EnemyCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed,
            List<Vector2> patrolTargets, MainCharacter mainCharacter, GameplayScreen gameplayScreen)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed, gameplayScreen)
        {
            this.patrolTargets = patrolTargets;
            this.PlayerCharacter = mainCharacter;

            currentState = EnemyState.Patrol;
            currentTarget = patrolTargets[0];
        }

        public virtual void Update(GameTime gameTime, GameLevel currentLevel)
        {
            // act based on state
            bool hasCollided;
            if (PlayerInVisionRange())
            {
                if (PlayerInAttackRange())
                {
                    currentState = EnemyState.Battle;
                }
                else
                {
                    currentState = EnemyState.Detection;
                }
            }
            else
            {
                currentState = EnemyState.Patrol;
            }

            Move(gameTime, currentLevel.LevelMap, out hasCollided);

        }

        public bool PlayerInVisionRange()
        {
            if (Vector2.Distance(this.Position, this.PlayerCharacter.Position) <= VISION_RANGE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlayerInAttackRange()
        {
            //int playerXCoord = PlayerCharacter.AsRectangle().X + PlayerCharacter.AsRectangle().Width / 2; // take midpoint
            //int playerYCoord = PlayerCharacter.AsRectangle().Y + PlayerCharacter.AsRectangle().Height / 2;
            //int enemyXCoord = AsRectangle().X + AsRectangle().Width / 2;
            //int enemyYCoord = AsRectangle().Y + AsRectangle().Height / 2;

            //if ((Math.Abs(enemyXCoord - playerXCoord) <= ATTACK_RANGE) && (enemyYCoord == playerYCoord) ||
            //    (Math.Abs(enemyYCoord - playerYCoord) <= ATTACK_RANGE) && (enemyXCoord == playerXCoord))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            bool inRange = false;
            if ((PlayerCharacter.Position.X <= Position.X) &&
                PlayerCharacter.Position.X + PlayerCharacter.CharacterSprite.FrameSize.X >= Position.X)
            {
                inRange = true;
            }
            else if ((PlayerCharacter.Position.X >= Position.X) &&
                PlayerCharacter.Position.X <= Position.X + CharacterSprite.FrameSize.X)
            {
                inRange = true;
            }
            else if ((PlayerCharacter.Position.Y <= Position.Y) &&
                PlayerCharacter.Position.X + PlayerCharacter.CharacterSprite.FrameSize.Y >= Position.Y)
            {
                inRange = true;
            }
            else if ((PlayerCharacter.Position.Y >= Position.Y) &&
                PlayerCharacter.Position.Y <= Position.Y + CharacterSprite.FrameSize.Y)
            {
                inRange = true;
            }

            return inRange;
        }

        // TODO: incorporate path finding algorithm or something
        // for now, just oscilate between two x coordinates
        public bool HasReachedTarget()
        {
            return AsRectangle().Contains(new Point((int)currentTarget.X, (int)currentTarget.Y));
        }

        public void MoveTowardPlayer(Map currentMap, GameTime gameTime, out bool hasMoved, out bool hasCollided)
        {

            MoveDirection direction = MoveDirection.None;
            if ((PlayerCharacter.Position.X < Position.X) &&
                PlayerCharacter.Position.X + ATTACK_RANGE < Position.X)
            {
                direction = MoveDirection.Left;
            }
            else if ((PlayerCharacter.Position.X > Position.X) &&
              PlayerCharacter.Position.X - ATTACK_RANGE > Position.X)
            {
                direction = MoveDirection.Right;
            }
            else if ((PlayerCharacter.Position.Y < Position.Y) &&
              PlayerCharacter.Position.Y + ATTACK_RANGE < Position.X)
            {
                direction = MoveDirection.Up;
            }
            else if ((PlayerCharacter.Position.Y > Position.Y) &&
              PlayerCharacter.Position.Y - ATTACK_RANGE > Position.Y)
            {
                direction = MoveDirection.Down;
            }

            hasMoved = base.Move(direction, currentMap, out hasCollided);
            base.Update(gameTime, direction, currentMap);
        }

        public void MoveTowardTarget(Vector2 destination, Map currentMap, GameTime gameTime, out bool hasMoved, out bool hasCollided)
        {
            MoveDirection direction = MoveDirection.None;
            if ((destination.X < Position.X) &&
                destination.X < Position.X)
            {
                direction = MoveDirection.Left;
            }
            else if ((destination.X > Position.X) &&
              destination.X  > Position.X)
            {
                direction = MoveDirection.Right;
            }
            else if ((destination.Y < Position.Y) &&
              destination.Y  < Position.X)
            {
                direction = MoveDirection.Up;
            }
            else if ((destination.Y > Position.Y) &&
              destination.Y  > Position.Y)
            {
                direction = MoveDirection.Down;
            }

            hasMoved = base.Move(direction, currentMap, out hasCollided);
            base.Update(gameTime, direction, currentMap);
        }

        public bool Move(GameTime gameTime, Map currentMap, out bool hasCollided)
        {
            Rectangle cameraView = gameplayScreen.CameraView;
            bool hasMoved;

            switch (currentState)
            {
                case EnemyState.Patrol:

                    if (HasReachedTarget())
                    {
                        // choose random new target
                        currentTarget = patrolTargets[rand.Next(patrolTargets.Count)];
                        hasMoved = false;
                        hasCollided = false;
                    }
                    else
                    {
                        MoveTowardTarget(currentTarget, currentMap, gameTime, out hasMoved, out hasCollided);
                    }
                    break;

                case EnemyState.Detection:

                    if (!PlayerInAttackRange())
                    {
                        // move towards player
                        MoveTowardPlayer(currentMap, gameTime, out hasMoved, out hasCollided);
                    }
                    else
                    {
                        hasMoved = false;
                        hasCollided = false;
                    }
                    break;

                default:
                    hasMoved = false;
                    hasCollided = false;
                    break;
            }

            return hasMoved;
        }

        protected enum EnemyState
        {
            Patrol,
            Detection,
            Battle,
            Defeat
        }
    }
}
