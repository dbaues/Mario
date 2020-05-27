using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarioBros
{
    public partial class SMario : Form
    {
        #region Fields
        // Thread Fields.
        private Thread gameThread;
        private double FPS, UPS, frames, ticks, delta;
        private bool Running = true;
        private bool shouldRender;

        // Game Fields.
        public static int Speed = 10;

        // Miscellaneous.
        private PictureBox pBox;
        private Font font;

        // Objects
        private Player player;
        private Level lvl1;

        public int x, y;
        #endregion

        #region Constructor
        public SMario()
        {
            // Create a Thread object.
            this.gameThread = new Thread(this.Run);

            InitializeComponent();
            this.font = new Font("Consolas", 10);
            this.pBox = new PictureBox();
            this.x = 50;
            this.y = 50;

            this.player = new Player(this);
            this.lvl1 = new Level(this);
        }
        #endregion

        /// <summary>
        /// Main Game loop that Renders and Updates.
        /// </summary>
        private void Run()
        {
            long lastTime = DateTime.Now.Ticks * 100;
            double nsPerTick = 10000000000D / 60D;
            long lastTimer = DateTime.Now.Millisecond;
            delta = 0D;

            // Game Loop.
            while(Running)
            {

                long now = DateTime.Now.Ticks;
                delta += (now - lastTime) / nsPerTick;
                lastTime = now;
                shouldRender = false;
                while(delta >= 1)
                {
                    ticks++;
                    this.Tick();
                    delta--;
                    shouldRender = true;
                }
                if(shouldRender)
                {
                    frames++;
                    //this.Render();
                }
                if(DateTime.Now.Millisecond - lastTime >= 1000)
                {
                    lastTimer += 1000;
                    FPS = frames;
                    UPS = ticks;
                    frames = 0;
                    ticks = 0;
                }



                // Methods called every 17 milliseconds (60 FPS).
                //this.Tick();
                //this.Render();

                // Controls Thread Framerate.
                //Thread.Sleep(16);
            }
        }

        public void Start()
        {
            Running = true;
        }

        /// <summary>
        /// Game Method called 60 times a second.
        /// </summary>
        private void Tick()
        {
            //lvl1.Tick();
            //lvl1.x += 1;
            //lvl1.y += 1;
            x += 1;
        }

        /*
        /// <summary>
        /// Renders game objects 60 times a second.
        /// </summary>
        private void Render()
        {

        }
        */

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dock the PictureBox to the form and set its background to white.
            pBox.Dock = DockStyle.Fill;
            pBox.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            pBox.Paint += new PaintEventHandler(this.Render);

            // Add the PictureBox control to the Form.
            this.Controls.Add(pBox);
        }

        private void Render(object sender, PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            g.DrawString("This is a diagonal line drawn on the control",
                font, Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            g.DrawLine(Pens.Red, pBox.Left, pBox.Top, pBox.Right, pBox.Bottom);
            // Draw an Ellipse.
            g.DrawEllipse(Pens.Black, new Rectangle(x, y, pBox.Width - x, pBox.Height - y));

            // Draws Game elements.
            player.Render(g);
            lvl1.Render(g);
            new Block(x, 100).Render(g);

            pBox.Refresh();
        }


        private void SMario_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameThread.Abort();
        }

        private void SMario_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(e.KeyChar.ToString());
        }

        /// <summary>
        /// Currently starts gameThread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, EventArgs e)
        {
            if(Running) { return; }
            Running = true;
            gameThread.Start();
        }
    }

    public interface Sprite
    {
        void Render(Graphics g);

        void CheckCollisions();
    }
}
