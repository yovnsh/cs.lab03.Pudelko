namespace KwadratŚmierdzący
{
    using P = KwadratŚmierdzący.Pudelko;
    using M = KwadratŚmierdzący.UnitOfMasure;
    using System.Net.Http.Headers;

    sealed class Pudelko : IEnumerable<double>
    {
        double A { get; init; }
        double B { get; init; }
        double C { get; init; }

        double Objetosc { get => Math.Round(A * B * C, 9); }
        double Pole { get => Math.Round(2 * (A * B + A * C + B * C), 6); }

        public Pudelko(double? a = null, double? b = null, double? c = null, M XD = M.meter)
        {
            this.A = Math.Round((a != null) ? fromunit(a.Value, XD) : 0.1, 3, MidpointRounding.ToNegativeInfinity);
            this.B = Math.Round((b != null) ? fromunit(b.Value, XD) : 0.1, 3, MidpointRounding.ToNegativeInfinity);
            this.C = Math.Round((c != null) ? fromunit(c.Value, XD) : 0.1, 3, MidpointRounding.ToNegativeInfinity);

            if (this.A <= 0 || this.B <= 0 || this.C <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this.A > 10 || this.B > 10 || this.C > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            return ToString("m");
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                format = "m";
            }
            string nieformat;
            M nazwajednostki;
            switch (format)
            {
                case "m":
                    nazwajednostki = M.meter;
                    nieformat = "0.000";
                    break;
                case "cm":
                    nazwajednostki = M.centimeter;
                    nieformat = "0.0";
                    break;
                case "mm":
                    nazwajednostki = M.milimeter;
                    nieformat = "0";
                    break;
                default: throw new FormatException();
            }
            return $"{assunit(this.A, nazwajednostki).ToString(nieformat)} {format} × {assunit(this.B, nazwajednostki).ToString(nieformat)} {format} × {assunit(this.C, nazwajednostki).ToString(nieformat)} {format}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !this.GetType().Equals (obj.GetType()))
            {
                return false;
            }
            return Equals(obj as Pudelko);
        }

        public bool Equals(Pudelko? innepudelko)
        {
            if (ReferenceEquals(innepudelko, null))
            {
                return false;
            }
            List<double> uno = new List<double>() { this.A, this.B, this.C };

            List<double> dno = new List<double>() { innepudelko.A, innepudelko.B, innepudelko.C };

            foreach (double d in uno)
            {
                if (!dno.Contains(d))
                {
                    return false;
                } 
                dno.Remove(d);
            }
            return true;
        }

        public override int GetHashCode()
        {
            return (A,B,C).GetHashCode();
        }

        public static bool operator == (Pudelko pudlouno, Pudelko pudloduo)
        {
            if (ReferenceEquals (pudlouno, pudloduo))
            {
                return true;
            }

            if (ReferenceEquals (pudloduo, null)) 
            {
                return false;
            }

            if (ReferenceEquals (pudloduo, null))
            {
                return false;
            }
            return pudlouno.Equals (pudloduo);
        }

        public static bool operator !=(Pudelko pudlouno, Pudelko pudloduo)
        {
            return !(pudlouno == pudloduo);
        }

        public static Pudelko operator + (Pudelko pudlouno, Pudelko pudloduo)
        {
            return new Pudelko
                (
                a: pudlouno.A + pudloduo.A,
                b: Math.Max(pudlouno.B, pudloduo.B),
                c: Math.Max(pudlouno.C, pudloduo.C),
                XD: M.meter
                );
        }

        public static explicit operator double[](Pudelko pudlouno)
        {
            return new double[3]
            {
                pudlouno.A, pudlouno.B, pudlouno.C
            };
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> jebacinsert)
        {
            return new Pudelko(a: jebacinsert.Item1, b: jebacinsert.Item2, c: jebacinsert.Item3, XD: M.milimeter);
        }      
        
        public double this[int entf]
        {
            get
            {
                switch (entf)
                {
                    case 0:
                        return this.A;
                    case 1:
                        return this.B;
                    case 2:
                        return this.C;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return this.A;
            yield return this.B;
            yield return this.C;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }      

        public static Pudelko Parse(string dowolnanazwa)
        {
            string[] Values = dowolnanazwa.Split(" × ");
            if (Values.Length < 3)
            {
                throw new ArgumentException("sześcian to sześcian");
            }
            double[] Wymiary = new double[3];

            string?Jednostka = null;
            foreach(string nazwajakas in Values)
            {
                string[] ehe = nazwajakas.Split(' ');
                if (ehe.Length<2)
                {
                    throw new ArgumentException("XD");
                }
                double Value = Convert.ToDouble(ehe[0]);
                string Valuo = ehe[1];
                if (Jednostka == null)
                {
                    Jednostka = Valuo;
                }
                else if (Jednostka != Valuo)
                {
                    throw new ArgumentException();
                }
            }
            M wypluted;
            switch (Jednostka)
            {
                case "m":
                    wypluted = M.meter; break;

                case "cm":
                    wypluted = M.centimeter; break;

                case "mm":
                    wypluted = M.milimeter; break;
                default: throw new ArgumentException();
            }
            return new Pudelko(Wymiary[0], Wymiary[1], Wymiary[2], wypluted);
        }



        public static double mnoznik (M jakasnazwa)
        {
            switch (jakasnazwa) 
            {
                case M.meter:
                    return 1;
                case M.centimeter:
                    return 0.01;
                case M.milimeter:
                    return 0.001;
                default: 
                    throw new NotImplementedException ();
            }
        }
        public static double assunit(double metry, M jakasnazwa)
        {
            return metry / mnoznik(jakasnazwa);
        }
        public static double fromunit(double niemetry, M jakasnazwa)
        {
            return niemetry * mnoznik(jakasnazwa);
        }
    }
}