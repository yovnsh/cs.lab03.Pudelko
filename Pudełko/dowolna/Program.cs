using KwadratŚmierdzący;
using System;

namespace pudla
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> pocztapolska = new List<Pudelko>()
            {
                new Pudelko(),
                new Pudelko(2,1,3, UnitOfMeasure.meter),
                new Pudelko(a:2, b:9, unit:UnitOfMeasure.milimeter),
                new Pudelko(a:420, unit:UnitOfMeasure.milimeter),
                new Pudelko(a:21, b:3, c:7, unit:UnitOfMeasure.centimeter),
                new Pudelko(4,6,9).Kompresuj()
            };
            Console.WriteLine("przed sortowaniem:");
            printpudlas(pocztapolska);
            pocztapolska.Sort(segregacja);
            Console.WriteLine("po sortowaniu:");
            printpudlas(pocztapolska);
        }

        public static int segregacja(Pudelko pudlouno, Pudelko pudloduo)
        {
            if (pudlouno.Objetosc != pudloduo.Objetosc)
            {
                return pudlouno.Objetosc < pudloduo.Objetosc ? -1 : 1;
            }
            else if (pudlouno.Pole != pudloduo.Pole)
            {
                return pudlouno.Pole < pudloduo.Pole ? -1 : 1;
            }
            else if (pudlouno.A + pudlouno.B + pudlouno.C != pudloduo.A + pudloduo.B + pudloduo.C)
            {
                return pudlouno.A + pudlouno.B + pudlouno.C < pudloduo.A + pudloduo.B + pudloduo.C ? -1 : 1;
            }
            else
            {
                return 0;
            }
        }

        public static void printpudlas(List<Pudelko> inpost)
        {
            foreach (Pudelko p in inpost)
            {
                Console.WriteLine($"{p.ToString()} objetosc: {p.Objetosc.ToString("0.000000000")}");
            }
        }
    }

}