using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRace
{
    public partial class GameScreen : UserControl

    {
        //movement boolean for p1 and p2
        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;
        Boolean wArrowDown, aArrowDown, sArrowDown, dArrowDown;

        //color of bubble
        SolidBrush BubbleBrush = new SolidBrush(Color.White);

        //list for bubble coming on screen
        List<bubble> left = new List<bubble>();
        List<bubble> Right = new List<bubble>();
        int leftX = 200;
        int gap = 150;


        //random generator
        Random randGen = new Random();

        //location and size of bubble
        int bubbleX, bubbleY;
        int foodSize = 3;
        //starting player sizes
        int p1Size, p2Size;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }


        public void OnStart()
        {
            p1Size = p2Size = 5;

            bubble newbubble = new bubble(50, 0,20);
            bubble newBubble2 = new bubble(50 + 400, 0,20);
            left.Add(newbubble);
            Right.Add(newBubble2);
        }
         
        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.W:
                    wArrowDown = true;
                    break;
                case Keys.D:
                    dArrowDown = true;
                    break;
                case Keys.A:
                    aArrowDown = true;
                    break;
                case Keys.S:
                    sArrowDown = true;
                    break;
                case Keys.Escape:
                    //end program
                    Application.Exit();
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.W:
                    wArrowDown = false;
                    break;
                case Keys.D:
                    dArrowDown = false;
                    break;
                case Keys.A:
                    aArrowDown = false;
                    break;
                case Keys.S:
                    sArrowDown = false;
                    break;             
            }
        }

        private void gameloop_Tick(object sender, EventArgs e)
        {
            // update location of bubbles
            foreach (bubble b in left)
            {
                b.Move(5);
            }
            foreach (bubble b in Right)
            {
                b.Move(5);
            }

            // remove bubble if it has gone off screen
            if (left[0].x > 400)
            {
                left.RemoveAt(0);
                Right.RemoveAt(0);
            }
            else if( Right[0].x < this.Width)
            {
                left.RemoveAt(0);
                Right.RemoveAt(0);
            }
            // controlling rocket
            
            // check for collision

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            {
                //TODO - draw bubbles to screen
                foreach (bubble b in left)
                {
                    BubbleBrush.Color = b.color;
                    e.Graphics.FillEllipse(BubbleBrush, b.x, b.y, b.size, b.size);


                }
                foreach (bubble b in Right)
                {
                    BubbleBrush.Color = b.color;
                    e.Graphics.FillEllipse(BubbleBrush, b.x, b.y, b.size, b.size);
                }
                
            }
        }
    }
}
