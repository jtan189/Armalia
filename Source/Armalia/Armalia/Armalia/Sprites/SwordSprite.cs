using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Characters;
using Armalia.Levels;

namespace Armalia.Sprites
{
    // Note: Xbox Live's "Collision Series 3: 2D Collision with Transformed Objects" tutorial was heavily
    // used for implementing collision detection between characters and swords.
    // (tutorial url: http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel_transformed )
    class SwordSprite
    {
        private const int DEFAULT_MS_PER_FRAME = 17;
        private const float DEFAULT_SCALE = 1f;
        private const float DEFAULT_LAYER_DEPTH = 0F;
        private const float DEFAULT_ROTATION_INCREMENT = .4F;

        private int timeSinceLastFrame = 0;
        private int msPerFrame;

        private Vector2 anchorPosition;
        private float scale;
        private float rotation;
        private float layerDepth;
        private Vector2 rotationPoint;
        private Texture2D texture;
        private Color[] textureData;
        private CombatableCharacter swordOwner;

        // used to make sure characters hit only once per attack
        private List<CombatableCharacter> charactersHit;

        public bool Animating { get; set; }

        public SwordSprite(Texture2D texture, int msPerFrame, float scale, Vector2 rotationPoint, CombatableCharacter swordOwner)
        {
            this.layerDepth = DEFAULT_LAYER_DEPTH;
            this.msPerFrame = msPerFrame;
            this.texture = texture;
            this.scale = scale;
            this.rotationPoint = rotationPoint;
            this.rotation = 0;
            this.swordOwner = swordOwner;
            Animating = false;
            charactersHit = new List<CombatableCharacter>();

            // Extract collision data
            textureData =
                new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        // source: http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel_transformed
        bool Intersects(CombatableCharacter character, GameLevel currentLevel)
        {
            bool intersectionOccured = false;

            // Update the character's transform
            Matrix characterTransform =
                Matrix.CreateTranslation(new Vector3(character.Position, 0.0f));

            // Build the sword's transform
            Matrix swordTransform =
                Matrix.CreateTranslation(new Vector3(-rotationPoint, 0.0f)) *
                Matrix.CreateScale(scale) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateTranslation(new Vector3(anchorPosition, 0.0f));

            // Calculate the bounding rectangle of this block in world space
            int anchorOffset = (int)(rotationPoint.Y - texture.Height);
            Rectangle swordRectangle = CalculateBoundingRectangle(
                     new Rectangle(0, 0,
                         (int)(2 * (anchorOffset + texture.Height)),
                         (int)(2 * (anchorOffset + texture.Height))),
                     swordTransform);

            // The per-pixel check is expensive, so check the bounding rectangles
            // first to prevent testing pixels when collisions are impossible.
            if (character.AsRectangle().Intersects(swordRectangle))
            {
                // Check collision with person
                if (IntersectPixels(characterTransform, character.CharacterSprite.Texture.Width,
                                    character.CharacterSprite.Texture.Height, character.TextureData,
                                    swordTransform, texture.Width,
                                    texture.Height, textureData))
                {
                    intersectionOccured = true;
                    character.IsInPain = true;
                }
                else
                {
                    character.IsInPain = false;
                }
            }

            return intersectionOccured;
        }

        public void Update(GameTime gameTime, GameLevel currentLevel, bool playerPressedAttack)
        {
            bool shouldStopTorture = false;

            if (!Animating && playerPressedAttack)
            {
                Animating = true;
            }

            if (Animating)
            {

                // set anchor position of sword to be at the center of the character
                anchorPosition.X = swordOwner.Position.X + (swordOwner.CharacterSprite.FrameSize.X / 2);
                anchorPosition.Y = swordOwner.Position.Y + (swordOwner.CharacterSprite.FrameSize.Y / 2);

                // update frame if time to do so, based on framerate
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > msPerFrame)
                {
                    // reset time since last frame
                    timeSinceLastFrame = 0;
                    if (rotation + DEFAULT_ROTATION_INCREMENT <= (2 * Math.PI))
                    {
                        rotation += DEFAULT_ROTATION_INCREMENT;
                    }
                    else
                    {
                        rotation = 0;
                        Animating = false;
                        shouldStopTorture = true;
                        charactersHit.Clear();
                    }
                }

                // check if attack hits enemy
                foreach (CombatableCharacter character in currentLevel.Enemies)
                {
                    if (Intersects(character, currentLevel))
                    {
                        if (!charactersHit.Contains(character))
                        {
                            swordOwner.Attack(character);
                            charactersHit.Add(character);
                            Console.WriteLine("Enemy hit! HP = {0}", character.HitPoints);
                        }
                    }
                }
            }

            if (shouldStopTorture)
            {
                foreach (CombatableCharacter character in currentLevel.Enemies)
                {
                    character.IsInPain = false;
                }
            }

        }

        /// <summary>
        /// This draws the animated sprite onto the map or whatever.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch object of the game.</param>
        /// <param name="spritePosition">The position of the sprite</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 spritePosition)
        {
            if (Animating)
            {
                // draw the sprite
                spriteBatch.Draw(texture, spritePosition,
                    null, Color.White, rotation, rotationPoint,
                    scale, SpriteEffects.None, layerDepth);
            }
        }

        // source: http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel_transformed
        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                           Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

        // source: http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel_transformed
        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels between two
        /// sprites.
        /// </summary>
        /// <param name="transformA">World transform of the first sprite.</param>
        /// <param name="widthA">Width of the first sprite's texture.</param>
        /// <param name="heightA">Height of the first sprite's texture.</param>
        /// <param name="dataA">Pixel color data of the first sprite.</param>
        /// <param name="transformB">World transform of the second sprite.</param>
        /// <param name="widthB">Width of the second sprite's texture.</param>
        /// <param name="heightB">Height of the second sprite's texture.</param>
        /// <param name="dataB">Pixel color data of the second sprite.</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(
                            Matrix transformA, int widthA, int heightA, Color[] dataA,
                            Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }

        // source: http://xbox.create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel_transformed
        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily
        /// transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                           Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}
