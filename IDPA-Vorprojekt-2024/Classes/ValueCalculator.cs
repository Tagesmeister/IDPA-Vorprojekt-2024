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

        private double CalculateNetIncome() //Bilanzgewinn
        {
            return _userValues.GewinnOderVerlustvortrag + _userValues.Jahresgewinn;
        }

        private double CalculateAvailableProfit() //Verfügbarer Gewinn
        {
            return _outputValues.NetIncome - CalculateFirstReserve();
        }

        private double CalculateRemainingAmountForAdditionalDividend() //Rest für zusätzliche Dividende
        {
            double baseDividend = (_userValues.AktienUndPartizipationskapital / 100) * 5;
            return _outputValues.AvailableProfit - baseDividend;
        }

        private double CalculateRetainedEarnings() //Neuer Gewinnvortrag
        {
            int additionalDividendPercentage = (int)Math.Floor(_outputValues.RemainingAmountForAdditionalDividend / (0.011 * _userValues.AktienUndPartizipationskapital));

            if (_userValues.GewünschteDividende > additionalDividendPercentage)
            {
                MessageBox.Show("Die gewünschte Dividende ist grösser als die maximal zulässige Dividende. Für die folgende Berechnung wird die maximal mögliche Dividende verwendet.", "Ungültige gewünschte Dividende");
            }
            else additionalDividendPercentage = (int)_userValues.GewünschteDividende;

            double additionalDividend = additionalDividendPercentage * _userValues.AktienUndPartizipationskapital / 100;
            double secondAdditionalDividend = 0.1 * additionalDividend;
            return _outputValues.RemainingAmountForAdditionalDividend - additionalDividend - secondAdditionalDividend;
        }

        private double CalculateFirstReserve()
        {
            double firstReserve = 0.05 * _userValues.Jahresgewinn; //5% vom RG
            if (IsFirstReserveToHigh(firstReserve))
            {
                firstReserve = 0.2 * _userValues.AktienUndPartizipationskapital - _userValues.GesetzlicheReserven; //Betrag bis 20% AK
            }
            return firstReserve;
        }

        private bool IsFirstReserveToHigh(double firstReserve)
        {
            return firstReserve + _userValues.GesetzlicheReserven > 0.2 * _userValues.AktienUndPartizipationskapital;
        }
    }
}
