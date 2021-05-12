using System;
using System.Drawing;
using System.Windows.Forms;

namespace _2048_Test_Task
{
    public partial class Form1 : Form
    {
        private int[,] map = new int[4, 4];
        public Label[,] labels = new Label[4, 4];
        public PictureBox[,] pics = new PictureBox[4, 4];
        private int score = 0;
        int gameover = 0;

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(_keyboardEvent);
            map[0, 0] = 1;
            map[0, 1] = 1;

            CreateMap();
            CreatePics();
            GenerateNumbers();
        }



        private void CreateMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(12 + 56 * j, 104 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.Gray;
                    this.Controls.Add(pic);
                }
            }
        }

        private void GenerateNumbers()
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);

            while (pics[a, b] != null)
            {
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }

            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(12 + b * 56, 104 + 56 * a);
            pics[a, b].BackColor = Color.LightGray;
            pics[a, b].Size = new Size(50, 50);
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }
        private void ChangeColor(int sum, int k, int j)
        {
            if (sum % 1024 == 0) pics[k, j].BackColor = Color.Yellow;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.Azure;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.DarkViolet;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.Pink;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.Red;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.OrangeRed;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.DarkOrange;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.Orange;
            else pics[k, j].BackColor = Color.DarkGray;
        }

        private void CreatePics()
        {
            pics[0, 0] = new PictureBox();
            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = new Point(12, 104);
            pics[0, 0].BackColor = Color.LightGray;
            pics[0, 0].Size = new Size(50, 50);
            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();


            pics[0, 1] = new PictureBox();
            labels[0, 1] = new Label();
            labels[0, 1].Text = "2";
            labels[0, 1].Size = new Size(50, 50);
            labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 1].Controls.Add(labels[0, 1]);
            pics[0, 1].Location = new Point(68, 104);
            pics[0, 1].BackColor = Color.LightGray;
            pics[0, 1].Size = new Size(50, 50);
            this.Controls.Add(pics[0, 1]);
            pics[0, 1].BringToFront();


        }



        private void _keyboardEvent(object sender, KeyEventArgs e)
        {

            bool ifPicsWasMoved = false;

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 2; l >= 0; l--)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = l + 1; i < 4; i++)
                                {
                                    if (map[k, i] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[k, i - 1] = 0;
                                        map[k, i] = 1;
                                        pics[k, i] = pics[k, i - 1];
                                        pics[k, i - 1] = null;
                                        labels[k, i] = labels[k, i - 1];
                                        labels[k, i - 1] = null;
                                        pics[k, i].Location = new Point(pics[k, i].Location.X + 56, pics[k, i].Location.Y);

                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, i].Text);
                                        int b = int.Parse(labels[k, i - 1].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[k, i].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, k, i);
                                            label1.Text = "Score: " + score;
                                            map[k, i - 1] = 0;
                                            this.Controls.Remove(pics[k, i - 1]);
                                            this.Controls.Remove(labels[k, i - 1]);
                                            pics[k, i - 1] = null;
                                            labels[k, i - 1] = null;

                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                case "Left":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 1; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = l - 1; i >= 0; i--)
                                {
                                    if (map[k, i] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[k, i + 1] = 0;
                                        map[k, i] = 1;
                                        pics[k, i] = pics[k, i + 1];
                                        pics[k, i + 1] = null;
                                        labels[k, i] = labels[k, i + 1];
                                        labels[k, i + 1] = null;
                                        pics[k, i].Location = new Point(pics[k, i].Location.X - 56, pics[k, i].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, i].Text);
                                        int b = int.Parse(labels[k, i + 1].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[k, i].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, k, i);
                                            label1.Text = "Score: " + score;
                                            map[k, i + 1] = 0;
                                            this.Controls.Remove(pics[k, i + 1]);
                                            this.Controls.Remove(labels[k, i + 1]);
                                            pics[k, i + 1] = null;
                                            labels[k, i + 1] = null;

                                        }
                                    }
                                }
                            }

                        }
                    }

                    break;

                case "Down":
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = k + 1; i < 4; i++)
                                {
                                    if (map[i, l] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[i - 1, l] = 0;
                                        map[i, l] = 1;
                                        pics[i, l] = pics[i - 1, l];
                                        pics[i - 1, l] = null;
                                        labels[i, l] = labels[i - 1, l];
                                        labels[i - 1, l] = null;
                                        pics[i, l].Location = new Point(pics[i, l].Location.X, pics[i, l].Location.Y + 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[i, l].Text);
                                        int b = int.Parse(labels[i - 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[i, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, i, l);
                                            label1.Text = "Score: " + score;
                                            map[i - 1, l] = 0;
                                            this.Controls.Remove(pics[i - 1, l]);
                                            this.Controls.Remove(labels[i - 1, l]);
                                            pics[i - 1, l] = null;
                                            labels[i - 1, l] = null;

                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;

                case "Up":
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = k - 1; i >= 0; i--)
                                {
                                    if (map[i, l] == 0)
                                    {
                                        ifPicsWasMoved = true;
                                        map[i + 1, l] = 0;
                                        map[i, l] = 1;
                                        pics[i, l] = pics[i + 1, l];
                                        pics[i + 1, l] = null;
                                        labels[i, l] = labels[i + 1, l];
                                        labels[i + 1, l] = null;
                                        pics[i, l].Location = new Point(pics[i, l].Location.X, pics[i, l].Location.Y - 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[i, l].Text);
                                        int b = int.Parse(labels[i + 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicsWasMoved = true;
                                            labels[i, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, i, l);
                                            label1.Text = "Score: " + score;
                                            map[i + 1, l] = 0;
                                            this.Controls.Remove(pics[i + 1, l]);
                                            this.Controls.Remove(labels[i + 1, l]);
                                            pics[i + 1, l] = null;
                                            labels[i + 1, l] = null;

                                        }
                                    }

                                }
                            }
                        }
                    }

                    break;
            }
            if (ifPicsWasMoved)
            {
                GenerateNumbers();
                gameover = 0;
            }
            else
            {
                gameover++;

            }
            if (gameover >= 4)
            {
                MessageBox.Show("Ваш счёт: " + score + " очков", "Вы проиграли!");
            }

        }
    }
}
