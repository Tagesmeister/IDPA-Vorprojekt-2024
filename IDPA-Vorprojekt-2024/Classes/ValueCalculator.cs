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
            _outputValues.NetIncome = CalculateNetIncome();
            _outputValues.AvailableProfit = CalculateAvailableProfit();
            _outputValues.RemainingAmountForAdditionalDividend = CalculateRemainingAmountForAdditionalDividend();
            _outputValues.RetainedEarnings = CalculateRetainedEarnings();

            MessageBox.Show($"Bilanzgewinn: {_outputValues.NetIncome}\nVerfügbarer Gewinn: {_outputValues.AvailableProfit}\nRest für zus. Dividende: {_outputValues.RemainingAmountForAdditionalDividend}\nNeuer Gewinnvortrag: {_outputValues.RetainedEarnings}");
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

            double additionalDividend = additionalDividendPercentage * _userValues.AktienUndPartizipationskapital / 100;
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
            return firstReserve;
        }

        private bool IsFirstReserveToHigh(double firstReserve, double firstReserveLimit)
        {
            return firstReserve + _userValues.GesetzlicheReserven > firstReserveLimit;
        }
    }
}
