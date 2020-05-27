using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioBros
{
    class Level
    {
        #region Fields
        public int x, y;
        private SMario main;
        private List<Block> blocks;
        #endregion

        public Level(SMario m)
        {
            this.main = m;
            this.blocks = new List<Block>();
            this.x = 200;
            this.y = 200;

            blocks.Add(new Block(main.x, y));
            blocks.Add(new Block(main.x + 32, y));
            blocks.Add(new Block(main.x + 64, y));
        }

        public void Render(Graphics g)
        {
            foreach (Block b in blocks)
                b.Render(g);
        }

        public void Tick()
        {
            foreach (Block b in blocks)
                b.UpdatePoint(1, 0);
        }
    }

    class Block
    {
        #region Fields
        private Size dim;
        private Point p;
        private int type;

        #region Dimensions
        public int X { get { return p.X; } }
        public int Y { get { return p.Y; } }
        public int WIDTH { get { return 16 * scale; } }
        public int HEIGHT { get { return 16 * scale; } }
        #endregion
        #region Colors
        private Color Base = Color.FromArgb(219, 100, 26);
        private Color Black = Color.FromKnownColor(KnownColor.Black);
        private Color White = Color.FromKnownColor(KnownColor.White);
        #endregion

        // Scale for sizing.
        private int scale = 2;
        #endregion



        #region Constructor
        public Block(int x, int y)
        {
            this.p = new Point(x, y);
            this.dim = new Size(WIDTH, HEIGHT);
        }

        public Block(int x, int y, int type)
        {
            this.p = new Point(x, y);
            this.dim = new Size(WIDTH, HEIGHT);
            this.type = type;
        }
        #endregion

        public void Render(Graphics g)
        {
            SolidBrush BBase = new SolidBrush(Base);
            SolidBrush BBlack = new SolidBrush(Black);
            SolidBrush BWhite = new SolidBrush(White);

            // Base.
            g.FillRectangle(BBase, new Rectangle(p, dim));
            // Edge.
            g.FillRectangle(BWhite, new Rectangle(
                new Point(p.X, p.Y + scale), 
                new Size(scale, 14 * scale)));
            g.FillRectangle(BWhite, new Rectangle(
                new Point(p.X + scale, p.Y), 
                new Size(14 * scale, scale)));
            g.FillRectangle(BBlack, new Rectangle(
                new Point(p.X + (15 * scale), p.Y + scale),
                new Size(scale, 14 * scale)));
            g.FillRectangle(BBlack, new Rectangle(
                new Point(p.X + scale, p.Y + (15 * scale)),
                new Size(14 * scale, scale)));
            // Details.
            
            // Outline.
            g.DrawRectangle(Pens.Black, new Rectangle(p, dim));

            BBase.Dispose(); BBlack.Dispose(); BWhite.Dispose();
        }

        public void UpdatePoint(int deltaX, int deltaY)
        {
            p.X += deltaX;
            p.Y += deltaY;
        }
    }
}
