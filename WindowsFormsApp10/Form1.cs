using System.Collections.Generic;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.White;
            snakePositions.Enqueue(new Point(x0, y0));
            a = pictureBox1.CreateGraphics();
            a.FillRectangle(new SolidBrush(playerColor), x0, y0, squareSize, squareSize);
            DrawApple();
            DrawInterface();
        }

        int x0 = 250;
        int y0 = 250;
        int xya = 0, yya = 0;
        int score = 0;
        int squareSize = 10;
        int sizeSteps = 0;
        Color playerColor = Color.Gray;
        private Queue <Point> snakePositions = new Queue<Point>();
        private int snakeLength = 1;

        private void DrawApple()
        {
            SolidBrush sb = new SolidBrush(Color.Red);
            if (needApple)
            {
                Random rnd = new Random();
                xya = rnd.Next(20) * 10;
                yya = rnd.Next(20) * 10;
                needApple = false;
            }
            a.FillEllipse(sb, xya, yya, 10, 10);
        }

        private void DrawInterface()
        {
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            a.FillRectangle(new SolidBrush(randomColor), 0, 0, 10, 10);
            a.FillRectangle(new SolidBrush(Color.Blue), pictureBox1.Width - 10, 0, 10, 10);
            a.FillRectangle(new SolidBrush(Color.White), 0, pictureBox1.Height - 30, 100, 30);
            a.DrawString($"Score: {score}", new Font("1", 16), new SolidBrush(Color.Black), 0, pictureBox1.Height - 30);
        }

        Graphics a;
        bool needApple = true;
        SolidBrush sb;
        SolidBrush clr;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Random rnd = new Random();
            

            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            int step = e.Shift ? 20 : 10;

            switch (e.KeyCode)
            {
                case Keys.A: Drawsquare(-1, 0, step, randomColor); break;
                case Keys.W: Drawsquare(0, -1, step, randomColor); break;
                case Keys.D: Drawsquare(1, 0, step, randomColor); break;
                case Keys.S: Drawsquare(0, 1, step, randomColor); break;
            }

            if (x0 == xya && y0 == yya)
            {
                needApple = true;
                score++;
                snakeLength++;
                DrawApple();
                DrawInterface(); 
            }

            if (x0 == 0 && y0 == 0)
            {
                playerColor = randomColor;
            }

            if (x0 == pictureBox1.Width - 10 && y0 == 0)
            {
                squareSize = 20;
                sizeSteps = 50;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void Drawsquare(int xz, int yz, int step, Color randomColor)
        {
            sb = new SolidBrush(playerColor);
            x0 += step * xz;
            y0 += step * yz;

            // Добавляем новую позицию головы
            snakePositions.Enqueue(new Point(x0, y0));

            // Если длина превышена, стираем хвост белым цветом
            if (snakePositions.Count > snakeLength)
            {
                Point tail = snakePositions.Dequeue();
                a.FillRectangle(new SolidBrush(Color.White), tail.X, tail.Y, squareSize, squareSize);
            }

            if (sizeSteps > 0)
            {
                sizeSteps--;
                if (sizeSteps == 0)
                    squareSize = 10;
            }

            
            foreach (Point pos in snakePositions)
            {
                a.FillRectangle(sb, pos.X, pos.Y, squareSize, squareSize);
            }

            DrawApple();

            // Обновляем интерфейс
            a.FillRectangle(new SolidBrush(randomColor), 0, 0, 10, 10);
            a.FillRectangle(new SolidBrush(Color.Blue), pictureBox1.Width - 10, 0, 10, 10);
            a.DrawString($"Score: {score}", new Font("1", 16), new SolidBrush(Color.Black), 0, pictureBox1.Height - 30);
            // проверка 228
        }

        
    }
}