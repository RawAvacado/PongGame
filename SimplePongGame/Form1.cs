using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePongGame
{
    public partial class Form1 : Form
    {
        // The width and heigth of the from
        const int ScreenWidth = 800;
        const int ScreenHeight = 400;

        // The width and heigth of the paddle
        int paddleHeigth = 100;
        int paddleWidth = 20;

        // The size of the ball and speed
        int ballHeigth = 10;
        int ballWidth = 10;
        int ballSpeedX = 2;
        int ballSpeedY = 2;

        // Time interval in ms
        int timeInterval = 1;

        public Form1()
        {
            InitializeComponent();

            // Set the size of the window and start position.
            this.Width = ScreenWidth;
            this.Height = ScreenHeight;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set the size and color of the paddle for the player
            PictureBox paddlePlayer = new PictureBox();
            paddlePlayer.Height = paddleHeigth;
            paddlePlayer.Width = paddleWidth;
            paddlePlayer.BackColor = Color.CadetBlue;
            paddlePlayer.Location = new Point(ClientSize.Width - paddleWidth, ClientSize.Height / 2 - paddleHeigth / 2);
            this.Controls.Add(paddlePlayer);

            // Set the size and color of the paddle for the AI
            PictureBox paddleAI = new PictureBox();
            paddleAI.Height = paddleHeigth;
            paddleAI.Width = paddleWidth;
            paddleAI.BackColor = Color.CadetBlue;
            paddleAI.Location = new Point(0, ClientSize.Height / 2 - paddleHeigth / 2);
            this.Controls.Add(paddleAI);

            // Set the size and color of the ball
            PictureBox ball = new PictureBox();
            ball.Height = ballHeigth;
            ball.Width = ballWidth;
            ball.BackColor = Color.CadetBlue;
            ball.Location = new Point(ClientSize.Width / 2 - ballWidth / 2, ClientSize.Height / 2 - ballHeigth / 2);
            this.Controls.Add(ball);

            // Initializes Timer class
            Timer gameTime = new Timer();

            //Enable the timer and set interval time
            gameTime.Enabled = true;
            gameTime.Interval = timeInterval;

            // Create the timer tick event
            gameTime.Tick += new EventHandler(gameTimeTick);

            // Timer tick event
            void gameTimeTick(object sender, EventArgs e)
            {
                ball.Location = new Point(ball.Location.X + ballSpeedX, ball.Location.Y + ballSpeedY); //Update position every 1 ms
                borderCollision(ball); // Check for collision with the borders of the game.
                paddleCollision(ball, paddleAI, paddlePlayer); // Check for collision with the borders of the game.
                playerPosition(paddlePlayer); // Updates the player postion.
                AIPosition(paddleAI, ball); // Updates the AI position.
            }

        }

        // Invert the vertical speed when ball hit buttom and top sides.
        private void borderCollision(PictureBox ball)
        {
            if (ball.Location.Y > ClientSize.Height - ball.Height || ball.Location.Y < 0)
            {
                ballSpeedY = -ballSpeedY;
            }
            else if (ball.Location.X > ClientSize.Width)
            {
                resetBall(ball);
            }
            else if (ball.Location.X < 0)
            {
                resetBall(ball);
            }
        }

        // Reset the ball location to middel position.
        private void resetBall(PictureBox ball)
        {
            ball.Location = new Point(ClientSize.Width / 2 - ballWidth / 2, ClientSize.Height / 2 - ballHeigth / 2);
        }

        // Player movement
        private void playerPosition(PictureBox paddlePlayer)
        {
            if (this.PointToClient(MousePosition).Y >= paddlePlayer.Height / 2 && this.PointToClient(MousePosition).Y <= ClientSize.Height - paddlePlayer.Height / 2)
            {
                int playerX = ClientSize.Width - paddleWidth;
                int playerY = this.PointToClient(MousePosition).Y - paddlePlayer.Height / 2;

                paddlePlayer.Location = new Point(playerX, playerY);
            }
        }

        // AI movement
        private void AIPosition(PictureBox paddleAI, PictureBox ball)
        {
            if (ballSpeedX < 0)
            {
                paddleAI.Location = new Point(0, ball.Location.Y - paddleAI.Height / 2);
            }
        }

        // Intersect with paddle
        private void paddleCollision(PictureBox ball, PictureBox paddleAI, PictureBox paddlePlayer)
        {
            if (ball.Bounds.IntersectsWith(paddleAI.Bounds))
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ball.Bounds.IntersectsWith(paddlePlayer.Bounds))
            {
                ballSpeedX = -ballSpeedX;
            }
        }
    }
}
