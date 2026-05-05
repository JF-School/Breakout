using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
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

        Texture2D ballTexture, paddleTexture, brickTexture, introBack, endBack;
        Texture2D logoTexture, leaveTexture, howTexture, playTexture, creditsTexture, backTexture, 
            instructTexture, nothingTexture, growTexture;
        Rectangle logoRect, howBtn, playBtn, creditsBtn, backBtn, growBtn;
        SpriteFont ScoreFont, RoundFont, EndScoreFont, TotalScoreFont;
        Rectangle paddleRectLeft, paddleRectCenter, paddleRectRight, ballHitbox;
        int padLeft, padRight, padCenter;

        SoundEffect bounce;
        SoundEffectInstance bounceInstance;

        Paddle paddle;
        List<Brick> bricks;
        Ball ball;

        int round, tab;
        bool codeInput, growShow;
        float timer;
        bool gameEnd;
        bool hitboxes;

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

            screen = Screen.Intro;
            round = 1;
            tab = 0; // tab = 1 = how to play, tab = 2 = credits
            gameEnd = false;
            hitboxes = false;
            codeInput = false;

            timer = 0f;

            // TODO: Add your initialization logic here

            base.Initialize();

            // paddle size (114, 23)
            paddle = new Paddle(paddleTexture, new Rectangle(343, 400, 114, 23), window);

            padLeft = (paddle.Rect.Center.X) / 2;
            padRight = (paddle.Rect.Center.X + paddle.Rect.Right) / 2;
            padCenter = padRight - padLeft;
            paddleRectLeft = new Rectangle(343, 400, 38, 23);
            paddleRectCenter = new Rectangle(343, 400, 38, 23);
            paddleRectRight = new Rectangle(343, 400, 38, 23);
            // ball size (25, 25) // 368, 350
            ball = new Ball(ballTexture, new Rectangle(paddle.Rect.Center.X, paddle.Rect.Y - 50, 25, 25), new Vector2(2, 2), Color.White);
            ballHitbox = new Rectangle(368, 350, 25, 25);
            // what's the brick size (90, 40)
            bricks = new List<Brick>();
            GenerateBricks();

            logoRect = new Rectangle(200, 25, 400, 160);
            playBtn = new Rectangle(350, 200, 100, 100);
            growBtn = new Rectangle(350, 300, 100, 100);
            howBtn = new Rectangle(460, 200, 100, 100);
            creditsBtn = new Rectangle(240, 200, 100, 100);
            backBtn = new Rectangle(10, 10, 50, 50);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // intro screen
            introBack = Content.Load<Texture2D>("Images/atariBackground");
            logoTexture = Content.Load<Texture2D>("Images/breakoutLogo");
            playTexture = Content.Load<Texture2D>("Images/playbutton");
            leaveTexture = Content.Load<Texture2D>("Images/leavebutton");
            howTexture = Content.Load<Texture2D>("Images/howbutton");
            creditsTexture = Content.Load<Texture2D>("Images/creditsbutton");
            backTexture = Content.Load<Texture2D>("Images/backbutton");
            instructTexture = Content.Load<Texture2D>("Images/howtoplay");
            nothingTexture = Content.Load<Texture2D>("Images/nothingburger");
            growTexture = Content.Load<Texture2D>("Images/growbutton");

            // game screen
            paddleTexture = Content.Load<Texture2D>("Images/paddle");
            brickTexture = Content.Load<Texture2D>("Images/brick");
            ballTexture = Content.Load<Texture2D>("Images/ball");

            // end screen
            endBack = Content.Load<Texture2D>("Images/endscreen");

            // fonts
            ScoreFont = Content.Load<SpriteFont>("Fonts/ScoreFont");
            EndScoreFont = Content.Load<SpriteFont>("Fonts/EndScoreFont");
            TotalScoreFont = Content.Load<SpriteFont>("Fonts/TotalScoreFont");
            RoundFont = Content.Load<SpriteFont>("Fonts/RoundFont");
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
                    switch (tab)
                    {
                        case 0:
                            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                            {
                                if (playBtn.Contains(mouseState.Position))
                                    screen = Screen.Game;
                                if (howBtn.Contains(mouseState.Position))
                                    tab = 1;
                                if (creditsBtn.Contains(mouseState.Position))
                                    tab = 2;
                            }
                            break;
                        case 1:
                            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                                if (backBtn.Contains(mouseState.Position))
                                    tab = 0;
                            break;
                        case 2:
                            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                                if (backBtn.Contains(mouseState.Position))
                                    tab = 0;
                            if (keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.G))
                                growShow = true;
                            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                                if (growBtn.Contains(mouseState.Position))
                                {
                                    codeInput = true;
                                }

                            break;
                    }
                    break;
                case Screen.Game:
                    // idk
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    ball.BallState(window, paddle, bricks, keyboardState);
                    paddle.Update(keyboardState, window);
                    if (keyboardState.IsKeyDown(Keys.LeftAlt) && prevKeyboardState.IsKeyUp(Keys.LeftAlt))
                    {
                        if (!hitboxes)
                            hitboxes = true;
                        else if (hitboxes)
                            hitboxes = false;
                    }
                    if (gameEnd)
                        screen = Screen.End;
                    break;
                case Screen.End:
                    //this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (screen)
            {
                case Screen.Intro:
                    if (!growShow)
                        codeInput = false;
                    break;
                case Screen.Game:
                    if (codeInput)
                    {
                        ball.Size = 50;
                    }
                    else
                    {
                        ball.Size = 25;
                    }
                    if (bricks.Count == 0)
                    {
                        ball.BallBool = true; ;
                        GenerateBricks();
                        round++;
                        ball.Lives++;
                    }
                    if (ball.Lives == 0)
                        screen = Screen.End;
                    if (hitboxes)
                    {
                        paddleRectLeft.X = paddle.Rect.X;
                        paddleRectCenter.X = paddle.Rect.X + 38;
                        paddleRectRight.X = paddle.Rect.X + (38 * 2);

                        ballHitbox.X = ball.Rect.X;
                        ballHitbox.Y = ball.Rect.Y;
                    }
                    break;
                case Screen.End:
                    break;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            switch (screen)
            {
                case Screen.Intro:
                    // put stuff here btw
                    switch (tab)
                    {
                        case 0: // main intro
                            _spriteBatch.Draw(introBack, window, Color.White);
                            _spriteBatch.Draw(logoTexture, logoRect, Color.White);
                            _spriteBatch.Draw(playTexture, playBtn, Color.White);
                            _spriteBatch.Draw(creditsTexture, creditsBtn, Color.White);
                            _spriteBatch.Draw(howTexture, howBtn, Color.White);
                            break;
                        case 1: // how to play?
                            _spriteBatch.Draw(instructTexture, window, Color.White);
                            _spriteBatch.Draw(backTexture, backBtn, Color.White);
                            break;
                        case 2: // credits
                            _spriteBatch.Draw(nothingTexture, window, Color.White);
                            _spriteBatch.Draw(backTexture, backBtn, Color.White);
                            if (growShow)
                                _spriteBatch.Draw(growTexture, growBtn, Color.White);
                            break;
                    }

                    break;
                case Screen.Game:
                    // background? no
                    paddle.Draw(_spriteBatch);
                    ball.Draw(_spriteBatch);
                    foreach (Brick brick in bricks)
                        brick.Draw(_spriteBatch);
                    _spriteBatch.DrawString(ScoreFont, $"Score: {ball.Score}", new Vector2(0, 0), Color.White);
                    _spriteBatch.DrawString(ScoreFont, $"Round: {round}", new Vector2(350, 0), Color.White);
                    if (ball.Lives > 1)
                        _spriteBatch.DrawString(ScoreFont, $"{ball.Lives} balls left", new Vector2(600, 0), Color.White);
                    else if (ball.Lives == 1 && ball.Lives > 0)
                        _spriteBatch.DrawString(ScoreFont, $"{ball.Lives} ball left", new Vector2(600, 0), Color.White);
                    // keep at bottom
                    if (hitboxes)
                    {
                        _spriteBatch.Draw(brickTexture, ball.Rect, Color.Red * 0.5f);
                        _spriteBatch.Draw(brickTexture, paddleRectLeft, Color.Red * 0.5f);
                        _spriteBatch.Draw(brickTexture, paddleRectCenter, Color.Orange * 0.5f);
                        _spriteBatch.Draw(brickTexture, paddleRectRight, Color.Yellow * 0.5f);
                    }
                    break;
                case Screen.End:
                    // don't forget this area
                    _spriteBatch.Draw(endBack, window, Color.White);
                    _spriteBatch.DrawString(RoundFont, $"{round}", new Vector2(420, 85), Color.Red);
                    _spriteBatch.DrawString(TotalScoreFont, $"{Math.Round(timer, 0)}s", new Vector2(135, 155), Color.Black);
                    _spriteBatch.DrawString(EndScoreFont, $"{ball.Score / 50}", new Vector2(540, 155), Color.Black);
                    _spriteBatch.DrawString(TotalScoreFont, $"{ball.Score}", new Vector2(310, 310), Color.Black);
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GenerateBricks()
        {
            bricks = new List<Brick>();
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
                        color = Color.Purple;
                        break;
                    case 6:
                        color = Color.HotPink;
                        break;
                }
                bricks.Add(new Brick(brickTexture, new Rectangle(50 + (100 * x), 30 + y, 90, 20), color));
            }


        }

    }
}
