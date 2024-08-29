using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPA_Vorprojekt_2024.Classes
{
    public class UserValues
    {
        public double Jahresgewinn { get { return _jahresgewinn; } }
        public double AktienUndPartizipationskapital { get { return _aktienUndPartizipationskapital; } }
        public double GesetzlicheReserven { get { return _gesetzlicheReserven; } }
        public double GewinnOderVerlustvortrag { get { return _gewinnOderVerlustvortrag; } }
        public double GewünschteDividende { get { return _gewünschteDividende; } }

        private double _jahresgewinn;
        private double _aktienUndPartizipationskapital;
        private double _gesetzlicheReserven;
        private double _gewinnOderVerlustvortrag;
        private double _gewünschteDividende;

        UserValues(double jahresgewinn, double aktienUndPartizipationskapital, double gesetzlicheReserven, double gewinnOderVerlustvortrag, double gewünschteDividende)
        {
            _jahresgewinn = jahresgewinn;
            _aktienUndPartizipationskapital = aktienUndPartizipationskapital;
            _gesetzlicheReserven = gesetzlicheReserven;
            _gewinnOderVerlustvortrag = gewinnOderVerlustvortrag;
            _gewünschteDividende = gewünschteDividende;
        }
    }
}
