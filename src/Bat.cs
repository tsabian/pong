using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Bat : ISprite 
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position {get;set;}
        public Vector2 Direction { get; set; }

        private float _velocity;

        public Bat(Game game, Vector2 position)
        {
            Texture = game.Content.Load<Texture2D>(@"Textures\bat");
            Position = position;
            Direction = new Vector2(0.0f, 0.0f);
            _velocity = 250.0f;
        }
        public Rectangle GetBounding()
        {
            return new Rectangle((int)Position.X,
                                (int)Position.Y,
                                (int)Texture.Width,
                                (int)Texture.Height);
        }
        public void Update(GameTime gameTime)
        {
            Position += (Direction * _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
