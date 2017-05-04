using InputEngineNS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListMenu
{
    public class Menu
    {
        Rectangle widest;
        LinkedList<MenuItem> MenuList
            = new LinkedList<MenuItem>();

        LinkedListNode<MenuItem> current;
        string SelectedText = string.Empty;

        public Menu(LinkedList<MenuItem> items)
        {
            MenuList = items;
            current = MenuList.First;
            current.Value.InFocus = true;
        }

        public void Update()
        {
            if (InputEngine.IsKeyPressed(Keys.Down))
            {
                if (current.Next != null)
                {
                    current.Value.InFocus = false;
                    current = current.Next;
                    current.Value.InFocus = true;
                }
                else
                {
                    current.Value.InFocus = false;
                    current = MenuList.First;
                    current.Value.InFocus = true;
                }

                // add rap around
            }
            if (InputEngine.IsKeyPressed(Keys.Up))
            {
                if (current.Previous != null)
                {
                    current.Value.InFocus = false;
                    current = current.Previous;
                    current.Value.InFocus = true;
                }
                else
                {
                    current.Value.InFocus = false;
                    current = MenuList.Last;
                    current.Value.InFocus = true;
                }
            }

            // add a check to check and see if one of the menu Items is selected.
            if (InputEngine.IsKeyPressed(Keys.Enter))
                current.Value.Selected = true;

            if (current.Value.Selected)
            {
                SelectedText = current.Value.text;
                current.Value.Selected = false;
            }

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            string longest = getLongestMenuOptionText();
            Vector2 size = font.MeasureString(longest);
            widest = new Rectangle(new Point(0, 0), size.ToPoint());

            Vector2 ScorePos = GamesUtilities.s_graphics_device_ref.Viewport.Bounds.Center.ToVector2();
            Vector2 scoreSize = font.MeasureString(getLongestMenuOptionText());
            // Calculate the position of the Scoreboard allowing for the size and number of scores to be displayed
            ScorePos -= new Vector2(widest.Width / 2, scoreSize.Y * getMenuOptionsText().Count());
            widest.Offset(ScorePos.X, ScorePos.Y);

            spriteBatch.DrawString(font, SelectedText, new Vector2(20, 20), Color.White);
            foreach (var MenuItem in MenuList)
            {
                MenuItem.draw(widest, spriteBatch, font);
                widest.Offset(0, widest.Height + 10);
            }
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
