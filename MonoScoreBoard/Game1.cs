using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoScoreBoard
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        public static LinkedList<Score> ScoreBoard = new LinkedList<Score>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            OrderedInsert(ScoreBoard, new Score { PlayerName = "PP", score = 10 });
            OrderedInsert(ScoreBoard, new Score { PlayerName = "BB", score = 30 });
            OrderedInsert(ScoreBoard, new Score { PlayerName = "AA", score = 20 });

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            font = Content.Load<SpriteFont>("font");
            // TODO: Add your update logic here

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
            showScoreBoard();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void showScoreBoard()
        {
            // Get the first Score node
            LinkedListNode<Score> first = ScoreBoard.First;
            // Measure 
            Vector2 scoreSize = font.MeasureString(first.Value.ToString());
            // Get the center of the viewport
            Vector2 ScorePos = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            // Calculate the position of the Scoreboard allowing for the size and number of scores to be displayed
            ScorePos -= new Vector2(scoreSize.X / 2, scoreSize.Y * ScoreBoard.Count);
            foreach (var item in ScoreBoard)
            {
                string scoreText = item.PlayerName + " " + item.score.ToString();
                spriteBatch.DrawString(font, scoreText, ScorePos, Color.White);
                ScorePos += new Vector2(0, font.MeasureString(scoreText).Y + 10);
            }

        }

        public void OrderedInsert(LinkedList<Score> list, Score newScore)
        {
            LinkedListNode<Score> node = list.First;
            while (node != null && node.Value.score <= newScore.score)
            {
                node = node.Next;
            }
            if (node == null && list.First == null)
                list.AddFirst(newScore);
            else if (node == null)
            {
                list.AddAfter(list.Last, newScore);
            }
            else list.AddBefore(node, newScore);

        }
    }
}
