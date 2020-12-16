using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// Classe Sprite
    /// </summary>
    public class Sprite
    {
        #region Global
        
        private ContentManager _content;
        private SpriteBatch _spriteBath;

        public Texture2D image;
        public Vector2 position;
        public Color color;

        #endregion Global

        #region Methods
        public Sprite(ContentManager content, SpriteBatch spritBath)
        {
            _content = content;
            _spriteBath = spritBath;
            image = null;
            position = Vector2.Zero;
            color = Color.White;
        }
        public bool Load(string assetName)
        {
            try
            {
                image = _content.Load<Texture2D>(assetName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void Draw()
        {
            _spriteBath.Draw(image, position, color);
        }
        #endregion Methods
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite _background;
        Ball _ball;
        Bat _bat1, _bat2;

        int[] score = new int[2];

        SpriteFont _fontScore;

        public enum PameState
        { 
            IntroScreen,
            SinglePlayer,
            MultPlayer,
            GameOver
        }

        #region Global Errors
        bool _errorState;
        string _errorMessage;
        SpriteFont _errorFont;
        #endregion Global Errors

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            /// Define a resolução do game para 800x600
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            /// Define o Titulo da janela para o texto PONG
            Window.Title = @"PONG";
            
            _errorMessage = string.Empty;
            _errorState = false;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            score[0] = 0;
            score[1] = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _background = new Sprite(Content, spriteBatch);
            if (!_background.Load(@"Textures\gameplay"))
            {
                _errorFont = Content.Load<SpriteFont>(@"Fonts\FontError");
                _errorState = true;
                _errorMessage = "O Arquivo de imagem nao foi encontrado";
                return;
            }
            _background.image = Content.Load<Texture2D>(@"Textures\gameplay");
            _background.position = Vector2.Zero;
            _background.color = Color.White;

            _ball = new Ball(this, new Vector2(386.0f, 310.0f));
            _bat1 = new Bat(this, new Vector2(10.0f, 290.0f));
            _bat2 = new Bat(this, new Vector2(765.0f, 290.0f));

            _fontScore = Content.Load<SpriteFont>(@"Fonts\FontScore");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyState.IsKeyDown(Keys.W))
                _bat1.Direction = new Vector2(0.0f, -1.0f);
            else if (keyState.IsKeyDown(Keys.S))
                _bat1.Direction = new Vector2(0.0f, 1.0f);
            else if (keyState.IsKeyDown(Keys.D))
                _bat1.Direction = new Vector2(1.0f, 0.0f);
            else if (keyState.IsKeyDown(Keys.A))
                _bat1.Direction=new Vector2(-1.0f, 0.0f);
            else
                _bat1.Direction = Vector2.Zero;

            _bat1.Update(gameTime);

            if (true) /// Implementar menu e seleção de nível de AI
            {
                AI ai = new AI(_ball, _bat2);
            }
            else
            {
                if (keyState.IsKeyDown(Keys.Up))
                    _bat2.Direction = new Vector2(0.0f, -1.0f);
                else if (keyState.IsKeyDown(Keys.Down))
                    _bat2.Direction = new Vector2(0.0f, 1.0f);
                else
                    _bat2.Direction = Vector2.Zero;
            }
            _bat2.Update(gameTime);

            if ((_ball.Position.Y + _ball.Texture.Height) > 570.0f)
            {
                _ball.Direction *= new Vector2(1.0f, -1.0f);
            }
            if (_ball.Position.Y < 70.0f)
            {
                _ball.Direction *= new Vector2(1.0f, -1.0f);
            }
            _ball.Update(gameTime);
            if ((_ball.Position.X + _ball.Texture.Width) > 800.0f)
            {
                score[0] += 1;
                _ball.Position = new Vector2(386.0f, 310.0f);
                _ball.Direction *= new Vector2(-1.0f, 1.0f);
            }
            else if (_ball.Position.X < 0.0f)
            {
                score[1] += 1;
                _ball.Position = new Vector2(386.0f, 310.0f);
                _ball.Direction *= new Vector2(-1.0f, 1.0f);
            }
            if (_ball.GetBounding().Intersects(_bat1.GetBounding()))
            {
                Vector2 cBall = new Vector2(_ball.GetBounding().Center.X, _ball.GetBounding().Center.Y);
                cBall.Normalize();
                Vector2 cBat = new Vector2(_bat1.GetBounding().Center.X, _bat1.GetBounding().Center.Y);
                cBat.Normalize();
                double andDir = Math.Atan2((cBall.Y - cBat.Y), (cBall.X - cBat.X));
                _ball.Direction = new Vector2((float)Math.Cos(andDir), (float)Math.Sin(andDir));
                //_ball.Direction *= new Vector2(-1.0f, 1.0f);
            }
            if (_ball.GetBounding().Intersects(_bat2.GetBounding()))
            {
                
                Vector2 cBall = new Vector2(_ball.GetBounding().Center.X, _ball.GetBounding().Center.Y);
                cBall.Normalize();
                Vector2 cBat = new Vector2(_bat2.GetBounding().Center.X, _bat2.GetBounding().Center.Y);
                cBat.Normalize();
                double andDir = Math.Atan2((cBall.Y - cBat.Y), (cBall.X - cBat.X));
                _ball.Direction = new Vector2((float)Math.Cos(andDir), (float)Math.Sin(andDir));

                //_ball.Direction *= new Vector2(-1.0f, 1.0f);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (_errorState)
            {
                spriteBatch.DrawString(_errorFont, "ERRO CRITICO BACKGROUND 001", Vector2.Zero, Color.White);
                spriteBatch.DrawString(_errorFont, _errorMessage, new Vector2(0, 100), Color.White);
            }
            else
            {
                _background.Draw();
                _ball.Draw(spriteBatch);
                
                Vector2 textSize = _fontScore.MeasureString(score[0].ToString("000"));
                spriteBatch.DrawString(_fontScore, score[0].ToString("000"), new Vector2(260, 0), Color.Black);

                textSize = _fontScore.MeasureString(score[1].ToString("000"));
                spriteBatch.DrawString(_fontScore, score[1].ToString("000"), new Vector2(460, 0), Color.Black);

                _bat1.Draw(spriteBatch);
                _bat2.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
