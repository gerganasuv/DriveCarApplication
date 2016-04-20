namespace DriveCarApplication
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private int anotherCar2X;

        private int anotherCar2Y;

        private int anotherCar2Ypossition;

        private int anotherCarX;

        private int anotherCarY;

        // позицията на първата кола
        private int carX;

        private int carY;

        private int coinX;

        private int coinY;

        bool left;

        private int level = 50;

        private int levelValue;

        // животи и точки
        private int lives = 3;

        private int points = 0;

        private Random random; // кара черната кола да пада хаотично,т.е. не само в левия край на таблото

        bool right;

        private int speed = 15;

        public Form1()
        {
            this.InitializeComponent();
            this.InitializeGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                // проверка за натиснат бутон
                // и променя стойността на координатите 
                this.right = true;
                this.carX += 60;
            }

            if (e.KeyCode == Keys.Left)
            {
                this.left = true;
                this.carX -= 60;
            }

            if (this.carX < 50)
            {
                this.carX = 50;
            }

            // проверка да не излиза за от матрицата
            if (this.carX > 170)
            {
                this.carX = 170;
            }

            // проверка на животите и пускане на играта отново със спейс
            if (this.timer1.Enabled == false)
            {
                if (this.lives > 0)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.timer1.Enabled = true;
                        this.anotherCarY = 50;
                        this.anotherCar2Y = this.anotherCar2Ypossition;
                        if (this.lives > 0)
                        {
                            this.gameover.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Space)
                    {
                        this.timer1.Enabled = false;
                    }
                }
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                this.right = false;
            }

            if (e.KeyCode == Keys.Left)
            {
                this.left = false;
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void InitializeGame()
        {
            // въвежда размерите на колните,редовете и т.н.
            this.carX = 50; // стартова позицияX на колата
            this.carY = 350; // стартова позицияY на колата
            this.anotherCar2X = 110;
            this.anotherCar2Ypossition = 150;
            this.anotherCar2Y = this.anotherCar2Ypossition;
            this.anotherCarX = 110;
            this.anotherCarY = 50;
            this.coinX = 170;
            this.coinY = 100;

            this.random = new Random(31);
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void screen_Paint(object sender, PaintEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.left)
            {
                this.player.Location = new Point(this.carX, this.carY);
            }
 // задава конкретна позиция на колата
            if (this.right)
            {
                this.player.Location = new Point(this.carX, this.carY);
            }

            // увеличава скоростта според точките
            if (this.points == this.level)
            {
                this.level += 50;
                this.speed += 1;
            }

            // задава първа кола на позиция...
            this.anotherCar.Location = new Point(this.anotherCarX, this.anotherCarY);
            this.anotherCarY += this.speed;

            // задава втора кола на позиция...
            this.anotherCar2.Location = new Point(this.anotherCar2X, this.anotherCar2Y);
            this.anotherCar2Y += this.speed;

            // монета
            this.Coin.Location = new Point(this.coinX, this.coinY);
            this.coinY += this.speed;

            // при контакт с първата кола спира играта
            if (this.anotherCarY + 70 >= this.carY && this.anotherCarX == this.carX)
            {
                this.timer1.Enabled = false;
                this.lives--;
                this.label1.Text = this.lives.ToString();
                if (this.lives > 0)
                {
                    this.gameover.Text = "Press space\n to continue.";
                }
            }

            if (this.coinY >= this.carY && this.coinX == this.carX)
            {
                this.lives++;
                this.label1.Text = this.lives.ToString();
                this.coinY = 0;
            }

            this.anotherCar2Ypossition = this.random.Next(0, 30); // избира рандом позиция

            if (this.anotherCar2Ypossition >= 0 && this.anotherCar2Ypossition <= 10)
            {
                this.anotherCar2Ypossition = 50;
            }
            else if (this.anotherCar2Ypossition >= 11 && this.anotherCar2Ypossition <= 20)
            {
                this.anotherCar2Ypossition = 130;
            }
            else
            {
                this.anotherCar2Ypossition = -30;
            }

            // при контакт с втората кола спира играта
            if (this.anotherCar2Y + 70 >= this.carY && this.anotherCar2X == this.carX)
            {
                this.timer1.Enabled = false;
                this.lives--;
                this.label1.Text = this.lives.ToString();
                if (this.lives > 0)
                {
                    this.gameover.Text = "Press space\n to continue.";
                }
            }

            if (this.anotherCar2Ypossition == -30)
            {
                if (this.anotherCar2Y - 20 >= this.carY && this.anotherCar2X == this.carX)
                {
                    this.timer1.Enabled = false;
                    this.lives--;
                    this.label1.Text = this.lives.ToString();
                }
            }

            if (this.anotherCarY >= 390)
            {
                // когато врората кола стигне/подмине дъното( 350 )
                // я пуска отново на първа линия и random позиция
                this.anotherCar2Y = this.anotherCar2Ypossition;
                this.anotherCarY = 50;

                this.anotherCarX = this.random.Next(0, 30);
                {
                    if (this.anotherCarX < 10)
                    {
                        this.anotherCarX = 50;
                    }
                    else if (this.anotherCarX >= 10 && this.anotherCarX < 20)
                    {
                        this.anotherCarX = 110;
                    }
                    else
                    {
                        this.anotherCarX = 170;
                    }

                    this.anotherCar2X = this.random.Next(0, 40);
                    if (this.anotherCarX == 50)
                    {
                        if (this.anotherCar2X >= 10 && this.anotherCar2X < 20)
                        {
                            this.anotherCar2X = 110;
                        }
                        else
                        {
                            this.anotherCar2X = 170;
                        }
                    }

                    if (this.anotherCarX == 110)
                    {
                        if (this.anotherCar2X < 10)
                        {
                            this.anotherCar2X = 50;
                        }
                        else
                        {
                            this.anotherCar2X = 170;
                        }
                    }

                    if (this.anotherCarX == 170)
                    {
                        if (this.anotherCar2X < 10)
                        {
                            this.anotherCar2X = 50;
                        }
                        else
                        {
                            this.anotherCar2X = 110;
                        }
                    }

                    if (this.anotherCar2X > 30 && this.anotherCar2X <= 40)
                    {
                        this.anotherCar2X = 5000;
                    }
                }

                if (this.coinY >= 390)
                {
                    this.coinY = 50;

                    this.coinY = this.random.Next(0, 40);
                    {
                        if (this.coinX < 10)
                        {
                            this.coinX = 170;
                        }
                        else if (this.coinX >= 10 && this.coinX < 20)
                        {
                            this.coinX = 50;
                        }
                        else if (this.coinX >= 21 && this.coinX < 30)
                        {
                            this.coinX = 110;
                        }
                        else if (this.coinX >= 31 && this.coinX < 40)
                        {
                            this.coinX = 500;
                        }
                    }
                }

                this.points += 10;
                this.score.Text = this.points.ToString();
            }

            this.levelValue = this.level / 50;
            this.currentLevel.Text = this.levelValue.ToString();
            if (this.lives == 0)
            {
                this.gameover.Text = "Game Over!";
            }
        }
    }
}