using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarioBros
{
    class Player : Sprite
    {
        private SMario main;
        private int _x;
        private int _y;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public Player(SMario m)
        {
            this.main = m;
            this._x = 50;
            this._y = 50;
        }

        public void Render(Graphics g)
        {
            // g.FillRectangle()
            g.FillRectangle(Brushes.Red, new Rectangle(_x, _y, 50, 100));
        }

        public void CheckCollisions()
        {

        }
    }
}
