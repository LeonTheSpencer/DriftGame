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
    public static class LeoKeyboard
    {
        public static void StartUpdate(KeyboardState K)
        {
            mainState = K;
        }
        public static void EndUpdate()
        {
            prevState = mainState;
        }
        public static int MustPass = 140;
        public static string Text;
        public static string GetTyped()
        {
            Text = "";
            string input = Convert(mainState.GetPressedKeys());
            string prevInput = Convert(prevState.GetPressedKeys());
            DateTime now = DateTime.Now;
            //make sure 100ms (with a few measurements) has passed
            int time = MustPass;
            if (input == "\b")
                time -= 25;
            if (now.Subtract(prevUpdate).Milliseconds >= time)
            {
                foreach (char x in input)
                {
                    //process backspace
                    if (x == '\b')
                    {
                        if (Text.Length >= 1)
                        {
                            Text = Text.Remove(Text.Length - 1, 1);
                        }
                    }
                    else
                        Text += x;
                }
                if (!string.IsNullOrEmpty(input))
                    prevUpdate = now;
            }
            return Text;
        }

        static KeyboardState mainState;
        static  KeyboardState prevState;
        static DateTime prevUpdate = DateTime.Now;
        public static string Convert(Keys[] keys)
        {
            //Original code by "SixOfEleven" (http://www.dreamincode.net/forums/user/120864-sixofeleven/)
            //Modified code by "MATTtheSEAHAWK" from (http://www.dreamincode.net/forums/topic/257077-good-xna-keyboard-input/)
            string output = "";
            bool usesShift = (keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));

            foreach (Keys key in keys)
            {
                //thanks SixOfEleven @ DIC
                if (prevState.IsKeyUp(key))
                    continue;

                if (key >= Keys.A && key <= Keys.Z)
                    output += key.ToString();
                else if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
                    output += ((int)(key - Keys.NumPad0)).ToString();
                else if (key >= Keys.D0 && key <= Keys.D9)
                {
                    string num = ((int)(key - Keys.D0)).ToString();
                    #region special num chars
                    if (usesShift)
                    {
                        switch (num)
                        {
                            case "1":
                                {
                                    num = "!";
                                }
                                break;
                            case "2":
                                {
                                    num = "@";
                                }
                                break;
                            case "3":
                                {
                                    num = "#";
                                }
                                break;
                            case "4":
                                {
                                    num = "$";
                                }
                                break;
                            case "5":
                                {
                                    num = "%";
                                }
                                break;
                            case "6":
                                {
                                    num = "^";
                                }
                                break;
                            case "7":
                                {
                                    num = "&";
                                }
                                break;
                            case "8":
                                {
                                    num = "*";
                                }
                                break;
                            case "9":
                                {
                                    num = "(";
                                }
                                break;
                            case "0":
                                {
                                    num = ")";
                                }
                                break;
                            default:
                                //wtf?
                                break;
                        }
                    }
                    #endregion
                    output += num;
                }
                else if (key == Keys.OemPeriod)
                    output += ".";
                else if (key == Keys.OemTilde)
                    output += "'";
                else if (key == Keys.Space)
                    output += " ";
                else if (key == Keys.OemMinus)
                    output += "-";
                else if (key == Keys.OemPlus)
                    output += "+";
                else if (key == Keys.OemQuestion && usesShift)
                    output += "?";
                else if (key == Keys.Back) //backspace
                    output += "\b";

                if (!usesShift) //shouldn't need to upper because it's automagically in upper case
                    output = output.ToLower();
            }
            return output;
        }
    }
}
