using System.Windows;

namespace IDPA_Vorprojekt_2024.Classes
{
    public class ValueCalculator
    {
        private UserValues _userValues;
        private OutputValues _outputValues = new OutputValues();

        public ValueCalculator(UserValues userValues)
        {
            _userValues = userValues;
            CalculateOutputValues();
        }

        private void CalculateOutputValues()
        {
            if(_userValues.GewinnOderVerlustvortrag < 0) VerlustVortragBeseitigen();

            _outputValues.NetIncome = CalculateNetIncome();
            _outputValues.AvailableProfit = CalculateAvailableProfit();
            _outputValues.RemainingAmountForAdditionalDividend = CalculateRemainingAmountForAdditionalDividend();
            _outputValues.RetainedEarnings = CalculateRetainedEarnings();

            MessageBox.Show($"Beitrag in die gesetzliche Reserve: {_outputValues.BeitragInDieGesetzlicheReserve} CHF\nBeschreibung: Der Beitrag in die gesetzliche Reserve ist der Teil des Gewinns, der zur finanziellen Stabilität des Unternehmens einbehalten wird.\n\nBetrag der Ausschüttung (Dividende): {_outputValues.BetragDerAusschüttungVonDividenden} CHF\nBeschreibung: Der Betrag der Ausschüttung (Dividende) ist der Teil des Gewinns, der an die Aktionäre ausgezahlt wird.\n\nNeuer Gewinn- oder Verlustvortrag: {_outputValues.RetainedEarnings} CHF\nBeschreibung: Der neue Gewinn- oder Verlustvortrag bezeichnet den nicht ausgeschütteten Gewinn oder Verlust, der in die nächste Periode übertragen wird.", "Ausgabe");
        }

        public void VerlustVortragBeseitigen()
        {
            //BESEITIGEN MIT GESETZLICHEN RESERVEN (bis 0)
            double benötigteGesetzlicheReserven = _userValues.GewinnOderVerlustvortrag * -1;
            if(_userValues.GesetzlicheReserven >= benötigteGesetzlicheReserven)
            {
                _userValues.GewinnOderVerlustvortrag = 0;
                _userValues.GesetzlicheReserven -= benötigteGesetzlicheReserven;      
            }
            else
            {
                _userValues.GewinnOderVerlustvortrag += _userValues.GesetzlicheReserven;
                _userValues.GesetzlicheReserven = 0;
            }

            //BESEITIGEN MIT JAHRESGEWINN
            if (_userValues.GewinnOderVerlustvortrag < 0) //Wenn IMMERNOCH Verlustvortrag dann mit Jahresgewinn decken (bis 0)
            {
                double benötigterReingewinn = _userValues.GewinnOderVerlustvortrag * -1;
                if (_userValues.Jahresgewinn >= benötigterReingewinn)
                {
                    _userValues.GewinnOderVerlustvortrag = 0;
                    _userValues.Jahresgewinn -= benötigterReingewinn;
                }
                else
                {
                    _userValues.GewinnOderVerlustvortrag += _userValues.Jahresgewinn;
                    _userValues.Jahresgewinn = 0;
                }
            }
        }

        public double CalculateNetIncome() //Bilanzgewinn
        {
            return _userValues.GewinnOderVerlustvortrag + _userValues.Jahresgewinn;
        }

        public double CalculateAvailableProfit() //Verfügbarer Gewinn
        {
            return _outputValues.NetIncome - CalculateFirstReserve();
        }

        public double CalculateRemainingAmountForAdditionalDividend() //Rest für zusätzliche Dividende
        {
            double baseDividend = _userValues.AktienUndPartizipationskapital * 0.05;
            _outputValues.BetragDerAusschüttungVonDividenden = baseDividend;
            return _outputValues.AvailableProfit - baseDividend;
        }

        public double CalculateRetainedEarnings() //Neuer Gewinnvortrag
        {
            int additionalDividendPercentage = (int)Math.Floor(_outputValues.RemainingAmountForAdditionalDividend / (0.011 * _userValues.AktienUndPartizipationskapital));
            if (additionalDividendPercentage < 0) additionalDividendPercentage = 0;

            if (_userValues.GewünschteDividende > additionalDividendPercentage)
            {
                MessageBox.Show($"Die maximal zulässige Dividende beträgt {additionalDividendPercentage}%.\n\nDie gewünschte Dividende ({_userValues.GewünschteDividende}%) ist jedoch grösser als die maximal zulässige Dividende.\nFür die folgende Berechnung wird die maximal mögliche Dividende verwendet.", "Ungültige gewünschte Dividende");
            }
            else additionalDividendPercentage = (int)_userValues.GewünschteDividende;
            if (additionalDividendPercentage < 0) additionalDividendPercentage = 0;

            double additionalDividend = additionalDividendPercentage * _userValues.AktienUndPartizipationskapital / 100;
            _outputValues.BetragDerAusschüttungVonDividenden += additionalDividend;
            return _outputValues.RemainingAmountForAdditionalDividend - additionalDividend;
        }

        private double CalculateFirstReserve() 
        {
            double firstReserveLimit = 0.5 * _userValues.AktienUndPartizipationskapital;

            double firstReserve = 0.05 * _userValues.Jahresgewinn;
            if (IsFirstReserveToHigh(firstReserve, firstReserveLimit))
            {
                firstReserve = firstReserveLimit - _userValues.GesetzlicheReserven;
            }
            _outputValues.BeitragInDieGesetzlicheReserve = firstReserve;
            return firstReserve;
        }

        private bool IsFirstReserveToHigh(double firstReserve, double firstReserveLimit)
        {
            return firstReserve + _userValues.GesetzlicheReserven > firstReserveLimit;
        }
    }
}
