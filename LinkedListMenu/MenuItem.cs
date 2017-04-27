using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkedListMenu
{
    public class MenuItem
    {
        public string text;
        public Texture2D tx;
        public bool InFocus;

        public MenuItem(string txt, Texture2D texture)
        {
            text = txt;
            tx = texture;
        }


        public void draw(Rectangle bound, SpriteBatch sp, SpriteFont sf)
        {
            Color color = Color.White;
            if (InFocus)
            {
                color = new Color(color,128);
            }
            sp.Draw(tx, bound, color);
            sp.DrawString(sf, text, new Vector2(bound.X,bound.Y), Color.White);

            

        }
    }
}
