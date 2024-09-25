namespace IDPA_Vorprojekt_2024.Classes
{
    class OutputValues
    {
        public double NetIncome { get; set; }       //Bilanzgewinn
        public double AvailableProfit { get; set; } //Verfügbarer Gewinn
        public double RemainingAmountForAdditionalDividend { get; set; } //Rest für zusätzliche Dividende
        public double RetainedEarnings { get; set; } //Gewinnvortrag

        public double BeitragInDieGesetzlicheReserve { get; set; }
        public double BetragDerAusschüttungVonDividenden { get; set; }
    }
}