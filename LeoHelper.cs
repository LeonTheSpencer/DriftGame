using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GUILib
{
    public static class LeoHelper
    {
        public static void DoAllUpdateStarts(MouseState M, KeyboardState K)
        {
            LeoMouse.StartUpdate(M);
            LeoKeyboard.StartUpdate(K);
            Controls.StartUpdate(K);
        }

        public static void DoAllUpdateEnds()
        {
            Controls.EndUpdate();
            LeoMouse.EndUpdate();
            LeoKeyboard.EndUpdate();
        }
    }
}
