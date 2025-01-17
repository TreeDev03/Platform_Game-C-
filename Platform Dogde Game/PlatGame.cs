﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Dogde_Game
{

    public partial class PlatGame : Form
    {
        bool goLeft = false;
        bool goRight = false;
        bool jumping = false;
        bool isGameOver;


        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 3;
        int enemyTwoSpeed = 3;
        int enemyThreeSpeed = 3;

        public PlatGame()
        {
            InitializeComponent();
        }

        //
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
            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
            
        }

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
            if (jumping)
            {
                jumping = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
            if (e.KeyCode == Keys.H && isGameOver == true)
            {
                RestartGameHard();
                
                enemy1.BackColor = Color.Red;
                enemy2.BackColor = Color.Red;
                enemy3.BackColor = Color.Red;
            }
            if (e.KeyCode == Keys.E && isGameOver == true)
            {
                RestartGameEasy();
                enemy1.BackColor = Color.LightPink;
                enemy2.BackColor = Color.LightPink;
                enemy3.BackColor = Color.LightPink;
            }
           

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
        
            txtModes.Visible = false;
            txtScore.Text = "Score: " + score;
            txtGoal.Text = "Collect All The Coins";
            txtDirections.Text = "USE THE ARROW KEYS TO MOVE AND HOLD SPACEBAR TO JUMP.\n                         \t            " +
                "HAPPY JUMPING!!! ";
           txtModes.Text =  "Press H to change to hard mode\n" +
                            "Press E to change to easy mode"
                    ;




            player.Top += jumpSpeed;

            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (goLeft)
            {
                player.Left -= 5;
            }

            if (goRight)
            {
                player.Left += 5;
            }

            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            foreach (Control x in this.Controls)
            {

                
                if ((string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;



                        if ((string)x.Name == "horizontalPlat" && goLeft == false || (string)x.Name == "horizontalPlat" && goRight == false)
                        {
                            player.Left -= horizontalSpeed;
                        }
                        if ((string)x.Name == "horizontalPlat2" && goLeft == false || (string)x.Name == "horizontalPlat2" && goRight == false)
                        {
                            player.Left -= horizontalSpeed;
                        }

                    }
                    // Post the controls for a brief period of time.
                    else if (player.Bounds.IntersectsWith(x.Bounds) && score > 6)
                    {
                        txtDirections.Visible = false;
                    }


                    x.BringToFront();

                }


                if ((String)x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score++;
                    }
                    
                }
                if ((string)x.Tag == "enemy")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        isGameOver = true;
                        txtDeath.Text = "Score: " + score + Environment.NewLine + "You were killed by an enemy!!\n" +
                            "Press enter to retry!!";
                        txtModes.Visible = true;
                        txtDeath.Visible = true;
                    }



                }
            }

            // Adds movement for the enemy and certain platforms.

            horizontalPlat.Left -= horizontalSpeed;
            if (horizontalPlat.Left < 0 || horizontalPlat.Left + horizontalPlat.Width > this.ClientSize.Width/2)
            {
                horizontalSpeed = -horizontalSpeed;
            }
            
            horizontalPlat2.Left -= horizontalSpeed;
            if (horizontalPlat2.Left < 0 || horizontalPlat2.Left + horizontalPlat2.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlat.Top += verticalSpeed;
            if (verticalPlat.Top < 492 || verticalPlat.Top > 639)
            {
                verticalSpeed = -verticalSpeed;
            }
            verticalPlat2.Top += verticalSpeed;
            if (verticalPlat2.Top < 77 || verticalPlat2.Top > 305)
            {
                verticalSpeed = -verticalSpeed;
            }


            enemy1.Left -= enemyOneSpeed;


            if (enemy1.Left < pictureBox1.Left || enemy1.Left + enemy1.Width > pictureBox1.Left + pictureBox1.Width)
            {

                enemyOneSpeed = -enemyOneSpeed;

            }


            enemy2.Left -= enemyTwoSpeed;

            if (enemy2.Left < pictureBox9.Left || enemy2.Left + enemy2.Width > pictureBox9.Left + pictureBox9.Width)
            {

                enemyTwoSpeed = -enemyTwoSpeed;

            }
            enemy3.Left -= enemyThreeSpeed;


            if (enemy3.Left < pictureBox11.Left || enemy3.Left + enemy3.Width > pictureBox11.Left + pictureBox11.Width)
            {

                enemyThreeSpeed = -enemyThreeSpeed;

            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtModes.Visible = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You fell to your death!!\n" +
                    "Press enter to retry!!";
                
               


            }

            if (player.Bounds.IntersectsWith(finish.Bounds) && score == 29)
            {


                gameTimer.Stop();
                isGameOver = true;
                txtModes.Visible = true;
                txtScore.Text = "Score: " + score + Environment.NewLine + "You Won Your Journey Is Complete!!\n" +
                    "Press enter to retry!!\n";






               
            }
            
        }

        //Reset the postion of player, platform and enemies.

        public void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;


            

            txtScore.Text = "Score " + score;
            txtDeath.Visible = false;
            txtDirections.Visible = true;
            txtModes.Visible = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 198;
            player.Top = 630;

            enemy1.Left = 384;
            enemy2.Left = 419;
            enemy3.Left = 419;

            gameTimer.Start();


        }

        // Easy and Hard game modes!!
        public void RestartGameEasy()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;


            

            txtScore.Text = "Score " + score;
            txtDeath.Visible = false;
            txtDirections.Visible = true;
            txtModes.Visible = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 198;
            player.Top = 630;

            enemy1.Left = 384;
            enemy2.Left = 419;
            enemy3.Left = 419;

            enemyOneSpeed = 3;
            enemyTwoSpeed = 3;
            enemyThreeSpeed = 3;



            gameTimer.Start();


        }

        public void RestartGameHard()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;


            

            txtScore.Text = "Score " + score;
            txtDeath.Visible = false;
            txtDirections.Visible = true;
            txtModes.Visible = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 198;
            player.Top = 630;

            enemy1.Left = 384;
            enemy2.Left = 419;
            enemy3.Left = 419;

            enemyOneSpeed = 5;
            enemyTwoSpeed = 5;
            enemyThreeSpeed = 5;

            
            

            gameTimer.Start();


        }


    }
}






