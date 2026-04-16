using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Breakout
{

    enum Screen
    {
        Intro,
        Game,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;
        Screen screen;

        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState, prevKeyboardState;

        Texture2D ballTexture, paddleTexture, brickTexture;
        Rectangle ballRect, paddleRect, brickRect;

        Paddle paddle;
        List<Brick> bricks;
        Ball ball;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // gonna defenestrate you
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            screen = Screen.Game;

            // TODO: Add your initialization logic here

            base.Initialize();

            // paddle size (114, 23)
            paddle = new Paddle(paddleTexture, new Rectangle(343, 400, 114, 23), window);
            // ball size (25, 25)
            ball = new Ball(ballTexture, new Rectangle(400, 250, 25, 25), new Vector2(2, 2), Color.White);
            // what's the brick size
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            paddleTexture = Content.Load<Texture2D>("Images/paddle");
            brickTexture = Content.Load<Texture2D>("Images/brick");
            ballTexture = Content.Load<Texture2D>("Images/ball");
        }

        protected override void Update(GameTime gameTime)
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            switch (screen)
            {
                case Screen.Intro:
                    // buttons
                    break;
                case Screen.Game:
                    // all the classes lol, and maybe scorekeeping
                    paddle.Update(keyboardState, window);
                    ball.Update(window, paddle, bricks);
                    break;
                case Screen.End:
                    // also buttons
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            switch (screen)
            {
                case Screen.Intro:
                    // put stuff here btw
                    break;
                case Screen.Game:
                    // background?
                    paddle.Draw(_spriteBatch);
                    ball.Draw(_spriteBatch);
                    foreach (Brick brick in bricks)
                        brick.Draw(_spriteBatch);
                    break;
                case Screen.End:
                    // don't forget this area
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
