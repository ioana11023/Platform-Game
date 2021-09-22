using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Game
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        int enemyThreeSpeed = 3;


        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            //go left and right
            player.Top += jumpSpeed;
            
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
             if (goRight == true)
            {
                player.Left += playerSpeed;
            }
             //jump
            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -40;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 0;
                            player.Top = x.Top - player.Height;
                        }

                        x.BringToFront();
                    }
                    //makes the coins invisible
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    //when the player touches the enemy
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!";
                        }
                    }



                }
            }


            //platforms movement
            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left < 190 || horizontalPlatform.Left > 440 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            
            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 195 || verticalPlatform.Top > 453)
            {
                verticalSpeed = -verticalSpeed;
            }

            //enemies movement
            enemyOne.Left -= enemyOneSpeed;

            if (enemyOne.Left < pictureBox4.Left || enemyOne.Left + enemyOne.Width > pictureBox4.Left + pictureBox4.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;

            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            enemyThree.Left += enemyThreeSpeed;

            if (enemyThree.Left < pictureBox7.Left || enemyThree.Left + enemyThree.Width > pictureBox7.Left + pictureBox7.Width)
            {
                enemyThreeSpeed = -enemyThreeSpeed;
            }


            //stop the game if you drop off the platform
            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death.";
            }

            //when player gets to the door
            if (player.Bounds.IntersectsWith(door.Bounds))
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You have passed this level!";
            }
           
        

        }

        //key is pressed
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

             if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

             if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        //key is not pressed
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Space && jumping == true)
            {
                jumping = false;
            }


            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }



        private void RestartGame()
        {

            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score: " + score;

            foreach (Control  x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            //reset the position of player, platform and enemies

            player.Left = 52;
            player.Top = 673;

            enemyOne.Left = 481;
            enemyOne.Top = 397;

            enemyTwo.Left = 445;
            enemyTwo.Top = 580;

            enemyThree.Left = 476;
            enemyThree.Top = 211;

            gameTimer.Start();





        }






    }
}
