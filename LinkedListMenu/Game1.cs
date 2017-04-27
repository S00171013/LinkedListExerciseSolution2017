using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using InputEngineNS;

namespace LinkedListMenu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Rectangle widest;
        LinkedList<MenuItem> MenuList
            = new LinkedList<MenuItem>();

        LinkedListNode<MenuItem> current;
        
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
            new InputEngine(this);
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
            font = Content.Load<SpriteFont>("font");
            Texture2D tx = Content.Load<Texture2D>("Background");
            MenuList.AddLast(new MenuItem("Menu Item    1", tx));
            MenuList.AddLast(new MenuItem("Menu Item  2", tx));
            MenuList.AddLast(new MenuItem("Menu Item  3", tx));
            MenuList.AddLast(new MenuItem("Menu Item   4", tx));
            current = MenuList.First;
            current.Value.InFocus = true;
            
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
            if (InputEngine.IsKeyPressed(Keys.Down))
            {
                if (current.Next != null)
                {
                    current.Value.InFocus = false;
                    current = current.Next;
                    current.Value.InFocus = true;
                }
            }
            if (InputEngine.IsKeyPressed(Keys.Up))
            {
                if (current.Previous != null)
                {
                    current.Value.InFocus = false;
                    current = current.Previous;
                    current.Value.InFocus = true;
                }
            }

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

            string longest = getLongestMenuOptionText();
            Vector2 size = font.MeasureString(longest);
            widest = new Rectangle(new Point(0, 0), size.ToPoint());

            Vector2 ScorePos = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            Vector2 scoreSize = font.MeasureString(getLongestMenuOptionText());
            // Calculate the position of the Scoreboard allowing for the size and number of scores to be displayed
            ScorePos -= new Vector2(widest.Width / 2, scoreSize.Y * getMenuOptionsText().Count());
            widest.Offset(ScorePos.X, ScorePos.Y);

            spriteBatch.Begin();
            foreach (var MenuItem in MenuList)
            {
                MenuItem.draw(widest, spriteBatch, font);
                widest.Offset(0, widest.Height + 10);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public string getLongestMenuOptionText()
        {
            return (from s in MenuList
                               orderby s.text.Length descending
                               select s.text).First();

        }

        public List<string> getMenuOptionsText()
        {
            return (from s in MenuList
                    select s.text).ToList();

        }

    }
}
