using Magna_TestApplication.Models;

namespace Magna_TestApplication.services
{
    public class SampleDataService
    {
        private int _sampleSerialNumber = 0;
        private readonly Random _random = new Random();

        public FunctionalTestLog GenerateNextLog()
        {
            DateTime now = DateTime.Now;
            _sampleSerialNumber++;

            return new FunctionalTestLog
            {
                SNo = _sampleSerialNumber,
                LoggedAt = now,
                Shift = GetSampleShift(),
                Variant = GetSampleVariant(),
                Result = GetSampleResult()
            };
        }

        private string GetSampleShift()
        {
            string[] shifts = { "A", "B", "C" };
            return shifts[_random.Next(shifts.Length)];
        }

        private string GetSampleVariant()
        {
            string[] variants = { "01", "02", "03", "04" };
            return variants[_random.Next(variants.Length)];
        }

        private string GetSampleResult()
        {
            return _random.Next(0, 2) == 0 ? "PASS" : "FAIL";
        }
    }
}