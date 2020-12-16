using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Ball : ISprite
    {

        #region Properties
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        #endregion Properties

        private float _velocity { get; set; }

        #region Methods
        /// <summary>
        /// Construtor da classe bola
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        public Ball(Game game, Vector2 position)
        {
            Texture = game.Content.Load<Texture2D>(@"Textures\ball");
            Position = position;
            Direction = new Vector2(1.0f, 1.0f);
            _velocity = 250.0f;
        }
        public Rectangle GetBounding()
        {
            return new Rectangle((int) Position.X, 
                                (int)Position.Y,
                                (int) Texture.Width, 
                                (int) Texture.Height);
        }
        public void Update(GameTime gameTime)
        {
            Position += (Direction * _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        public void Draw(SpriteBatch spritBacth)
        {
            spritBacth.Draw(Texture, Position, Color.White);
        }
        #endregion Methods
    }
}
