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
    static class LeoMouse
    {
        public class Vector2INT
        {
            public int X;
            public int Y;
            public Vector2INT(int A, int B)
            {
                X = A;
                Y = B;
            }
        }
        public static MouseState New;
        public static MouseState Old;
        public static Vector2INT StartDrag;
        public static bool Dragging;
        public static bool WasDragging;
        public static bool IsDragging()
        {
            return Dragging;
        }
        public static bool Held()
        {
            return (New.LeftButton==ButtonState.Pressed);
        }
        public static bool JustReleased() 
        {
            return (New.LeftButton == ButtonState.Released && Old.LeftButton == ButtonState.Pressed);
        }
        public static bool JustPressed() //I can add support for other mouse buttons if needed
        {
            return (New.LeftButton == ButtonState.Pressed && Old.LeftButton == ButtonState.Released);
        }
        public static void StartUpdate(MouseState In)//Must be at the start of the update function
        {
            New = In;
            if (JustPressed())
            {
                StartDrag = new Vector2INT(New.X, New.Y);
                Dragging = true;
                WasDragging = true;
            }
            else if (!Held())
            {
                Dragging = false;
            }
        }
        public static void EndUpdate()
        {
            Old = New;
            WasDragging = Dragging;
        }
    }
}
