using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceRace
{

    class bubble
    {
        //variables in my class
        public SolidBrush bubbleBrush;
        public int size, y, x;
        public Color color;

        //random gen
        Random randGen = new Random();

        public bubble(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;

            int randValue = randGen.Next(1, 8);

            //makes the bubble random colours
            if (randValue == 1)
            {
                bubbleBrush = new SolidBrush(Color.Red);
            }
            else if (randValue == 2)
            {
                bubbleBrush = new SolidBrush(Color.Orange);
            }
            else if (randValue == 3)
            {
                bubbleBrush = new SolidBrush(Color.Yellow);
            }
            else if (randValue == 4)
            {
                bubbleBrush = new SolidBrush(Color.Blue);
            }
            else if (randValue == 5)
            {
                bubbleBrush = new SolidBrush(Color.Purple);
            }
            else if (randValue == 6)
            {
                bubbleBrush = new SolidBrush(Color.LightGreen);
            }
            else if (randValue == 7)
            {
                bubbleBrush = new SolidBrush(Color.Pink);
            }
        }
        public bubble(int _x, int _y, int _size, Color _color)
        {
            x = _x;
            y = _y;
            size = _size;
            color = _color;
        }

        public void Move(int speed)
        {
            x += speed;


        }

        public void Move(int speed, Boolean direction)
        {
            if (direction)
            {
                x += speed;
            }
            else
            {
                x -= speed;
            }

        }

        public void MoveY(int speed, Boolean direction)
        {
            if (direction)
            {
                y += speed;
            }
            else
            {
                y -= speed;
            }
        }       
    }
}
