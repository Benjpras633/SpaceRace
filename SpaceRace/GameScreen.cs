using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace SpaceRace
{
    public partial class GameScreen : UserControl

    {
        //movement boolean for p1 
        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;
        

        //color of bubble
        SolidBrush BubbleBrush = new SolidBrush(Color.White);

        //list for bubble coming on screen
        List<bubble> left = new List<bubble>();
        List<bubble> Right = new List<bubble>();
        int playerScore = 0;
        



        //random generator
        Random randGen = new Random();

        //location and size of bubble
        int bubbleX, bubbleY;
        int bubbleSize = 3;
        //starting player sizes
        int p1Size, p2Size;
        Boolean moveRight = true;
     

        int BubbleTimer = 0;
        bubble hero;
        
        int heroSpeed = 10;
        int heroSize = 30;


        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }


        public void OnStart()
        {
            p1Size = p2Size = 5;

            //hero = new bubble(250, 400, 20);
            hero = new bubble(this.Width / 2 - heroSize / 2, 370, heroSize);

            MakeBubble();
            outputLabel.Text = "" + playerScore;
        }
        public void MakeBubble()
        {
            int randValue = randGen.Next(1, 450);

            bubble newbubble = new bubble(0, randValue, 20);
            left.Add(newbubble);

            randValue = randGen.Next(1, 450);
            bubble newBubble2 = new bubble(this.Width, randValue, 20);
            Right.Add(newBubble2);

            BubbleTimer = 0;
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
                b.Move(-5);
            }

            BubbleTimer++;

            if(BubbleTimer == 20)
            {
                MakeBubble();
            }

            // remove bubble if it has gone off screen
            //if (left.Count > 0)
            //{



            //    if (left[0].x > 400)
            //    {
            //        left.RemoveAt(0);
            //        Right.RemoveAt(0);
            //    }
            //    else if (Right[0].x < this.Width)
            //    {
            //        left.RemoveAt(0);
            //        Right.RemoveAt(0);
            //    }


            //}
            // controlling rocket
            if(leftArrowDown == true)
            {
                hero.Move(heroSpeed, false);
            }

            if(rightArrowDown == true)
            {
                hero.Move(heroSpeed, true);
            }
            // check for collision
            bubble hero = new bubble(hero.x, hero.y, hero.size, hero.size);
            if (left.Count <= 4)

                //0-3
                for (int i = 0; i < 4; i++)
                {
                    bubble hero = new bubble(left[i].x, left[i].y, left[i].size, left[i].size);
                    bubble bubble = new bubble(right[i].x, right[i].y, right[i].size,Right[i].size);
                    if (hero.IntersectsWith(bubble))
                    {
                        gameLoop.Enabled = false;
                    }
                }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            {
                //TODO - draw bubbles to screen
                foreach (bubble b in left)
                {
                   
                    e.Graphics.FillEllipse(b.bubbleBrush, b.x, b.y, b.size, b.size);


                }
                foreach (bubble b in Right)
                {
                    
                    e.Graphics.FillEllipse(b.bubbleBrush, b.x, b.y, b.size, b.size);
                }

                e.Graphics.FillEllipse(BubbleBrush, hero.x, hero.y, hero.size, hero.size);
                e.Graphics.DrawImage(Properties.Resources.rocket, hero.x, hero.y,hero.size,hero.size);
            }
        }
    }
}
