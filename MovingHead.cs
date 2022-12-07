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
        int step = 3;

        

        public int TiltMoveUp()
        {
            tiltmove = tiltmove + step; 
            return tiltmove;
        }

        public int TiltMoveDown()
        {
            tiltmove = tiltmove - step;
            return tiltmove;
        }

        public int PanMoveRight()
        {
            panmove = panmove + step;
            return panmove;
        }

        public int PanMoveLeft()
        {
            panmove = panmove - step;
            return panmove;
        }
    }
}
