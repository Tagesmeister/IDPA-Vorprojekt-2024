namespace IDPA_Vorprojekt_2024.Classes
{
    public class UserValues
    {
        public double Jahresgewinn { get { return _jahresgewinn; } set { _jahresgewinn = value; } }
        public double AktienUndPartizipationskapital { get { return _aktienUndPartizipationskapital; } }
        public double GesetzlicheReserven { get { return _gesetzlicheReserven; } set { _gesetzlicheReserven = value; } }
        public double GewinnOderVerlustvortrag { get { return _gewinnOderVerlustvortrag; } set { _gewinnOderVerlustvortrag = value; } }
        public double GewünschteDividende { get { return _gewünschteDividende; } }

        private double _jahresgewinn;
        private double _aktienUndPartizipationskapital;
        private double _gesetzlicheReserven;
        private double _gewinnOderVerlustvortrag;
        private double _gewünschteDividende;

        public UserValues(string jahresgewinn, string aktienUndPartizipationskapital, string gesetzlicheReserven, string gewinnOderVerlustvortrag, string gewünschteDividende)
        {
            _jahresgewinn = Convert.ToDouble(jahresgewinn);
            _aktienUndPartizipationskapital = Convert.ToDouble(aktienUndPartizipationskapital);
            _gesetzlicheReserven = Convert.ToDouble(gesetzlicheReserven);
            _gewinnOderVerlustvortrag = Convert.ToDouble(gewinnOderVerlustvortrag);
            _gewünschteDividende = Convert.ToDouble(gewünschteDividende);
        }
    }
}
