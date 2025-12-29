namespace GestionStock.Contracts.Common
{
    public static class ValidationConstants
    {
        // Text lengths
        public const int CodeMax = 30;
        public const int NameMax = 200;
        public const int SkuMax = 40;
        public const int RefMax = 50;
        public const int NotesMax = 1000;

        // Numeric ranges
        // Range attribute: use decimal as string bounds to be safe
        public const string MoneyMin = "0";
        public const string MoneyMax = "999999999999"; // 10^12-1 (large)
        public const string QtyMin = "0.0001";
        public const string QtyMax = "999999999999";

        // Regex examples (adapt to your company rules)
        public const string SkuRegex = @"^[A-Z0-9][A-Z0-9\-_]{1,39}$";     // e.g. ABC-123
        public const string CodeRegex = @"^[A-Z0-9][A-Z0-9\-_]{1,29}$";    // e.g. WH-CASA-01
    }
}
