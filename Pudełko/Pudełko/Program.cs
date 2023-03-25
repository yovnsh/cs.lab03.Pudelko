namespace KwadratŚmierdzący
{
    using P = KwadratŚmierdzący.Pudelko;
    sealed class Pudelko
    {
        double a { get; set; }
        double b { get; set; }
        double c { get; set; }

        public Pudelko (double a = 10, double b = 10, double c = 10)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
}