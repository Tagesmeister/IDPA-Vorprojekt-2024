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

            MessageBox.Show($"Neuer Gewinnvortrag: {_outputValues.RetainedEarnings}\nBeitrag in die gesetzliche Reserve: {_outputValues.BeitragInDieGesetzlicheReserve}\nBetrag der Ausschüttung von Dividenden: {_outputValues.BetragDerAusschüttungVonDividenden}");
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

            MessageBox.Show($"Ein Verlustvortrag lag vor.\nAktueller Verlustvortrag: {_userValues.GewinnOderVerlustvortrag}\nAktuelle gesetzlichen Reserven: {_userValues.GesetzlicheReserven}\nAktueller Jahresgewinn: {_userValues.Jahresgewinn}");
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

            if (_userValues.GewünschteDividende > additionalDividendPercentage)
            {
                MessageBox.Show("Die gewünschte Dividende ist grösser als die maximal zulässige Dividende. Für die folgende Berechnung wird die maximal mögliche Dividende verwendet.", "Ungültige gewünschte Dividende");
            }
            else additionalDividendPercentage = (int)_userValues.GewünschteDividende;
            if (additionalDividendPercentage < 0) additionalDividendPercentage = 0;

            double additionalDividend = additionalDividendPercentage * _userValues.AktienUndPartizipationskapital / 100;
            _outputValues.BetragDerAusschüttungVonDividenden += additionalDividend;
            //double secondAdditionalReserve = 0.1 * additionalDividend;
            return _outputValues.RemainingAmountForAdditionalDividend - additionalDividend; //- secondAdditionalReserve;
        }

        private double CalculateFirstReserve() 
        {
            double firstReserveLimit = 0.5 * _userValues.AktienUndPartizipationskapital; //Betrag bis 50% AK --> Art. 672 Abs. 2

            double firstReserve = 0.05 * _userValues.Jahresgewinn; //5% vom RG --> Art. 672 Abs. 1
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
