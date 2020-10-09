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
        Font drawFont = new Font("Courier New", 50);

        //list for bubble coming on screen
        List<bubble> left = new List<bubble>();
        List<bubble> right = new List<bubble>();
        int player1Score = 0;
        int gameWinScore = 2;
        Boolean newgame = true;




        //random generator
        Random randGen = new Random();

        
        
       
   
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
           

            //hero = new bubble(250, 400, 20);
            hero = new bubble(this.Width / 2 - heroSize / 2, 370, heroSize);

            MakeBubble();

            scoreLabel.Text = player1Score + "";      
        }

        

        public void MakeBubble()
        {
            int randValue = randGen.Next(1, 450);

            bubble newbubble = new bubble(0, randValue, 20);
            left.Add(newbubble);

            randValue = randGen.Next(1, 450);
            bubble newBubble2 = new bubble(this.Width, randValue, 20);
            right.Add(newBubble2);

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
                case Keys.Space:
                    if (newgame)
                    {
                        Form f = this.FindForm();
                        f.Controls.Remove(this);

                        MainScreen ms = new MainScreen();
                        f.Controls.Add(ms);
                        ms.Focus();
                    }
                    break;
                case Keys.N:
                    if (newgame)
                    {
                        Application.Exit();
                    }
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
            foreach (bubble b in right)
            {
                b.Move(-5);
            }

            BubbleTimer++;

            if (BubbleTimer == 20)
            {
                MakeBubble();
            }


            if (player1Score == gameWinScore)
            {
              GameOver("");
            }
            



            // controlling rocket
            if (leftArrowDown == true && hero.y > 0)
            {
                hero.Move(heroSpeed, false);
            }

            if (rightArrowDown == true && hero.y < this.Width - heroSize)
            {
                hero.Move(heroSpeed, true);
            }

            if (upArrowDown == true)
            {
                hero.MoveY(heroSpeed, false);
            }

            if (downArrowDown == true)
            {
                hero.MoveY(heroSpeed, true);
            }

            if (upArrowDown == true && hero.y < 0)
            {
                hero.y = this.Height;
                player1Score++;
                scoreLabel.Text  = player1Score + "";

            }

            //check for collision
            Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.size, hero.size);

            foreach (bubble b in left)
            {
                
            }
            
          //if (left.Count >= 10)
          //{
          //    //0-3
          //    for (int i = 0; i < 10; i++)
          //    {
          //        Rectangle boxRec = new Rectangle(left[i].x, left[i].y, left[i].size, left[i].size);
          //        Rectangle rightboxRec = new Rectangle(right[i].x, right[i].y, right[i].size, right[i].size);

          //        if (heroRec.IntersectsWith(rightboxRec) || heroRec.IntersectsWith(boxRec))
          //        {

          //            outputLabel.Text = "Press space bar to play again, or press n to exit";
          //        }

          //    }

          //  }
            Refresh();
        }
        private void GameOver(string winner)
        {
            newgame = true;

          
            gameloop.Stop();

            outputLabel.Visible = true;
            if (winner == "p1")
            { outputLabel.Text = "You win!"; }           
         
            this.Refresh();
            Thread.Sleep(2000);
           
            newgame = true;
            outputLabel.Text = "Press space bar to play again, or press n to exit";

        }
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            {
                //TODO - draw bubbles to screen
                foreach (bubble b in left)
                {

                    e.Graphics.FillEllipse(b.bubbleBrush, b.x, b.y, b.size, b.size);


                }
                foreach (bubble b in right)
                {

                    e.Graphics.FillEllipse(b.bubbleBrush, b.x, b.y, b.size, b.size);
                }

                e.Graphics.FillEllipse(BubbleBrush, hero.x, hero.y, hero.size, hero.size);
                e.Graphics.DrawImage(Properties.Resources.rocket, hero.x, hero.y, hero.size, hero.size);
            }
        }
    }
}
