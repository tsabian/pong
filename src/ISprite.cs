using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public interface ISprite
    {
        /// <summary>
        /// Imagem da bola
        /// </summary>
        Texture2D Texture { get; set; }
        /// <summary>
        /// Posição na tela
        /// </summary>
        Vector2 Position { get; set; }
        /// <summary>
        /// Direção de movimento
        /// </summary>
        Vector2 Direction { get; set; }
        /// <summary>
        /// Atualiza a posição da bola
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);
        /// <summary>
        /// Desenha a bola na tela
        /// </summary>
        /// <param name="spritBacth"></param>
        void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// Área de Colisão do objeto
        /// </summary>
        /// <returns></returns>
        Rectangle GetBounding();
    }
}
