namespace Qc4Launcher.Logic.MultiVariate
{
    internal class PSMOutputData
    {
        public int dataCount { get; set; }
        public int validCount { get; set; }
        public double[] ResultPrices { get; set; }
        public double[,] NumericalArray { get; set; }
        public double[][] psmDataPercentageBefore { get; set; }
        public double[][] psmDataPercentage { get; set; }
        public double[][] psmData { get; set; }
        public double?[,] statasticData { get; set; }
    }
}