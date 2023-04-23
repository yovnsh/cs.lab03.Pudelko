using KwadratŚmierdzący;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pudla
{
    public static class pudlowpudle
    {
        public static Pudelko Kompresuj(this Pudelko wypudlaj)
        {
            double boka = Math.Pow(wypudlaj.Objetosc, (1.0 / 3.0));
            return new Pudelko(boka, boka, boka);
        }
    }
}
