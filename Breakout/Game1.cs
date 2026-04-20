using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
//using System.Numerics;

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
            ball = new Ball(ballTexture, new Rectangle(343, 350, 25, 25), new Vector2(2, 3), Color.White);
            // what's the brick size (90, 40)
            bricks = new List<Brick>();
            //for (int i = 0; i < 7; i++) // red bricks
            //    bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * i), 30, 90, 20), Color.Red));
            //for (int j = 0; j < 7; j++) // orange bricks
            //    bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * j), 55, 90, 20), Color.Orange));
            //for (int k = 0; k < 7; k++) // yellow bricks
            //    bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * k), 80, 90, 20), Color.Yellow));
            //for (int l = 0; l < 7; l++) // green
            //    bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * l), 105, 90, 20), Color.Green));
            //for (int m = 0; m < 7; m++)
            //    bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * m), 130, 90, 20), Color.Blue));
            for (int i = 0; i < 49; i++)
            {
                int x = i % 7;
                int y = 25 * (i / 7 % 7);
                Color color = Color.White;
                switch (i / 7 % 7)
                {
                    case 0:
                        color = Color.Red;
                        break;
                    case 1:
                        color = Color.Orange;
                        break;
                    case 2:
                        color = Color.Yellow;
                        break;
                    case 3:
                        color = Color.Green;
                        break;
                    case 4:
                        color = Color.Blue;
                        break;
                    case 5:
                        color = Color.Magenta;
                        break;
                    case 6:
                        color = Color.Violet;
                        break;
                }
                bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * x), 30 + y, 90, 20), color));
            }
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
