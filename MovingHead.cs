using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives
{
    internal class MovingHead
    {
        int tiltmove = 0;
        int panmove = 0;
        int step = 2;

        

        public int TiltMoveUp()
        {
            if (tiltmove <= 255 - step)
            { tiltmove = tiltmove + step; } 
            return tiltmove;
        }

        public int TiltMoveDown()
        {
            if (tiltmove >= 0 + step)
            { tiltmove = tiltmove - step; }
            return tiltmove;
        }

        public int PanMoveRight()
        {
            if (panmove <= 255 - step)
            { panmove = panmove + step; }
            return panmove;
        }

        public int PanMoveLeft()
        {
            if (panmove >= 0 + step)
            { panmove = panmove - step; }
            return panmove;
        }
    }
}
