using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IDPA_Vorprojekt_2024.Classes
{
    public class ValueCalculator
    {
        private List<double> _calculatedPlan;
        private UserValues _userValue;


        private double _percentReserveFromStock;

        private double _financialStatementWin;
        private double _firstReserve;

        private double _availableWin;
        private double _baseDividend;

        private double _availableRestForDividend;
        private double _additionalDivident;
        private double _secondReserve;

        private double _newProfitCarriedForward;


        public ValueCalculator(UserValues userValue)
        {
            _userValue = userValue;
            StartCalculatePlan();
        }

        public void StartCalculatePlan()
        {
            _calculatedPlan = new List<double>();

            _calculatedPlan.Add(_financialStatementWin = CalculateFinancialStatement());
            _calculatedPlan.Add(_availableWin = CalculateAvailableWin());
            _calculatedPlan.Add(_availableRestForDividend = CalculateAvailableRestForDividend());
            _calculatedPlan.Add(_newProfitCarriedForward = CalculateNewProfitCarriedForward());


        }
        public double CalculateFinancialStatement()
        {
            return _userValue.GewinnOderVerlustvortrag + _userValue.Jahresgewinn;
        }
        public double CalculateAvailableWin()
        {

            if (IsFirstReserveToHigh())
            {

                _firstReserve = (_userValue.Jahresgewinn / 100) * 5;

                if ((_userValue.AktienUndPartizipationskapital / 100) * _percentReserveFromStock + _firstReserve > (_userValue.AktienUndPartizipationskapital / 100) * 20)
                {
                    double overShoot = (_userValue.AktienUndPartizipationskapital / 100) * _percentReserveFromStock + _firstReserve - (_userValue.AktienUndPartizipationskapital / 100) * 20;

                    _firstReserve = _firstReserve - overShoot;

                    _firstReserve = Math.Round(_firstReserve, 2);

                }
            }

            return _financialStatementWin - _firstReserve;
        }
        public bool IsFirstReserveToHigh()
        {
            _percentReserveFromStock = (100 / _userValue.AktienUndPartizipationskapital) * _userValue.GesetzlicheReserven;

            if (_percentReserveFromStock <= 20)
            {
                return true;
            }
            return false;
        }
        public double CalculateAvailableRestForDividend()
        {
            _baseDividend = (_userValue.AktienUndPartizipationskapital / 100) * 5;
            return _availableWin - _baseDividend;
        }
        public double CalculateNewProfitCarriedForward()
        {
            double _maxAdditionalDivident = Math.Floor(_availableRestForDividend / ((_userValue.AktienUndPartizipationskapital / 100) * 1.1));

            if (_userValue.GewünschteDividende > 0 && _userValue.GewünschteDividende <= _maxAdditionalDivident)
            {
                _additionalDivident = (_userValue.AktienUndPartizipationskapital / 100) * _userValue.GewünschteDividende;
                _secondReserve = (_additionalDivident / 100) * 10;
            }
            return _availableRestForDividend - _additionalDivident - _secondReserve;

        }
    }
}
