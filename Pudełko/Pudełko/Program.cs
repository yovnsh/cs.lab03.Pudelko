namespace KwadratŚmierdzący
{
    using P = KwadratŚmierdzący.Pudelko;
    using M = KwadratŚmierdzący.UnitOfMasure;
    sealed class Pudelko
    {
        double A { get; init; }
        double B { get; init; }
        double C { get; init; }

        public Pudelko(double? a = null, double? b = null, double? c = null, M XD = M.meter)
        {
            this.A = Math.Round((a != null) ? fromunit(a, XD) : 0.1, 3, MidpointRounding.ToNegativeInfinity);
            this.B = Math.Round((b != null) ? fromunit(b, XD) : 0.1, 3, MidpointRounding.ToNegativeInfinity);
            this.C = Math.Round((c != null) ? fromunit(c, XD) : 0.1, 3, MidpointRounding.ToNegativeInfinity);

            if (this.A <= 0 || this.B <= 0 || this.C <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this.A > 10 || this.B > 10 || this.C > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
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