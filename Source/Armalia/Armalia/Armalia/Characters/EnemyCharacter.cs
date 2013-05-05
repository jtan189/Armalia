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

        protected EnemyState currentState;
        private List<Vector2> patrolTargets;
        private Vector2 currentTarget;
        public MainCharacter PlayerCharacter { get; set; }

        private MoveDirection DirectionStuckIn { get; set; }
        private int UnstickAttempts;
        Random randomMover;

        public EnemyCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed,
            List<Vector2> patrolTargets, MainCharacter mainCharacter, GameplayScreen gameplayScreen)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed, gameplayScreen)
        {
            this.patrolTargets = patrolTargets;
            this.PlayerCharacter = mainCharacter;

            DirectionStuckIn = MoveDirection.None;
            UnstickAttempts = 0;
            randomMover = new Random();

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

            Move(gameTime, currentLevel, out hasCollided);

        }

        public bool PlayerInVisionRange()
        {
            return Vector2.Distance(this.SpriteCenter, this.PlayerCharacter.SpriteCenter)
                - (PlayerCharacter.CharacterSprite.FrameSize.X / 2) < VISION_RANGE;
        }

        // source: http://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection
        public bool PlayerInAttackRange()
        {
            return (Vector2.Distance(this.SpriteCenter, this.PlayerCharacter.SpriteCenter) - (PlayerCharacter.CharacterSprite.FrameSize.X / 4)) < (Sword.AttackRange);
        }

        // TODO: incorporate path finding algorithm or something
        // for now, just oscilate between two x coordinates
        public bool HasReachedTarget()
        {
            Rectangle proximityRect = new Rectangle(AsRectangle().X - (int)speed.X, AsRectangle().Y - (int)speed.Y,
                AsRectangle().Width + (int)(2 * speed.X), AsRectangle().Width + (int)(2 * speed.Y));
            return proximityRect.Contains(new Point((int)currentTarget.X, (int)currentTarget.Y));
        }

        public void MoveTowardPlayer(Map currentMap, GameTime gameTime, out bool hasMoved, out bool hasCollided)
        {

            MoveDirection direction = MoveDirection.None;
            if (PlayerCharacter.Position.X < Position.X
                && (Position.X - speed.X) > PlayerCharacter.Position.X)
            {
                direction = MoveDirection.Left;
            }
            else if (PlayerCharacter.Position.X > Position.X
                && (Position.X + speed.X) < PlayerCharacter.Position.X)
            {
                direction = MoveDirection.Right;
            }
            else if (PlayerCharacter.Position.Y < Position.Y
                && (Position.Y - speed.Y) > PlayerCharacter.Position.Y)
            {
                direction = MoveDirection.Up;
            }
            else if (PlayerCharacter.Position.Y > Position.Y
                && (Position.Y + speed.Y) < PlayerCharacter.Position.Y)
            {
                direction = MoveDirection.Down;
            }

            hasMoved = base.Move(direction, currentMap, out hasCollided);
            base.Update(gameTime, direction, currentMap);
        }

        public void MoveTowardTarget(Vector2 destination, Map currentMap, GameTime gameTime, out bool hasMoved, out bool hasCollided)
        {
            MoveDirection direction = MoveDirection.None;

            if (DirectionStuckIn == MoveDirection.None)
            {
                if ((destination.X < Position.X) &&
                    (Position.X - speed.X) > destination.X)
                {
                    direction = MoveDirection.Left;
                }
                else if ((destination.X > Position.X) &&
                  (Position.X + speed.X) < destination.X)
                {
                    direction = MoveDirection.Right;
                }
                else if ((destination.Y < Position.Y) &&
                  (Position.Y - speed.Y) > destination.Y)
                {
                    direction = MoveDirection.Up;
                }
                else if ((destination.Y > Position.Y) &&
                  (Position.Y + speed.Y) < destination.Y)
                {
                    direction = MoveDirection.Down;
                }
            }
            else
            {
                // get unstuck
                switch (DirectionStuckIn)
                {
                    case MoveDirection.Up:
                        direction = MoveDirection.Left;
                        break;
                    case MoveDirection.Left:
                        direction = MoveDirection.Down;
                        break;
                    case MoveDirection.Down:
                        direction = MoveDirection.Right;
                        break;
                    case MoveDirection.Right:
                        direction = MoveDirection.Up;
                        break;
                }
                UnstickAttempts++;
            }

            hasMoved = base.Move(direction, currentMap, out hasCollided);
            base.Update(gameTime, direction, currentMap);

            // cheap way to try to get enemies unstuck
            if (hasCollided)
            {
                // randomly pick the direction stuck in
                int randMove = randomMover.Next(3);
                switch (direction)
                {
                    case MoveDirection.Up:
                        DirectionStuckIn = randMove < 2 ? MoveDirection.Up : MoveDirection.Down;
                        break;
                    case MoveDirection.Left:
                        DirectionStuckIn = randMove < 2 ? MoveDirection.Left : MoveDirection.Right;
                        break;
                    case MoveDirection.Down:
                        DirectionStuckIn = randMove < 2 ? MoveDirection.Down : MoveDirection.Up;
                        break;
                    case MoveDirection.Right:
                        DirectionStuckIn = randMove < 2 ? MoveDirection.Right : MoveDirection.Left;
                        break;
                }

            }
            else
            {

                if (UnstickAttempts > 100)
                {
                    DirectionStuckIn = MoveDirection.None;
                    UnstickAttempts = 0;
                }

            }

        }

        public bool Move(GameTime gameTime, GameLevel currentLevel, out bool hasCollided)
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
                        MoveTowardTarget(currentTarget, currentLevel.LevelMap, gameTime, out hasMoved, out hasCollided);
                    }
                    break;

                case EnemyState.Detection:

                    if (!PlayerInAttackRange())
                    {
                        // move towards player
                        MoveTowardPlayer(currentLevel.LevelMap, gameTime, out hasMoved, out hasCollided);
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
