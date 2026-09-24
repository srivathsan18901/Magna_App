namespace Magna_TestApplication.Models
{
    public class TravelEnduranceLogRecord
    {
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Shift { get; set; } = "";
        public string Variant { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public string Result { get; set; } = "";

        // Travel & Endurance measurements
        public double InsideLockEffort_Min { get; set; }
        public double InsideLockEffort_Max { get; set; }
        public double InsideLockEffort_Actual { get; set; }

        public double InsideLockTravel_Min { get; set; }
        public double InsideLockTravel_Max { get; set; }
        public double InsideLockTravel_Actual { get; set; }

        public double InsideUnlockEffort_Min { get; set; }
        public double InsideUnlockEffort_Max { get; set; }
        public double InsideUnlockEffort_Actual { get; set; }

        public double InsideUnlockTravel_Min { get; set; }
        public double InsideUnlockTravel_Max { get; set; }
        public double InsideUnlockTravel_Actual { get; set; }

        public double InsideReleaseEffort_Min { get; set; }
        public double InsideReleaseEffort_Max { get; set; }
        public double InsideReleaseEffort_Actual { get; set; }

        public double InsideReleasePreTravel_Min { get; set; }
        public double InsideReleasePreTravel_Max { get; set; }
        public double InsideReleasePreTravel_Actual { get; set; }

        public double InsideReleaseReleaseTravel_Min { get; set; }
        public double InsideReleaseReleaseTravel_Max { get; set; }
        public double InsideReleaseReleaseTravel_Actual { get; set; }

        public double InsideReleaseFullTravel_Min { get; set; }
        public double InsideReleaseFullTravel_Max { get; set; }
        public double InsideReleaseFullTravel_Actual { get; set; }

        public double OutsideReleaseEffort_Min { get; set; }
        public double OutsideReleaseEffort_Max { get; set; }
        public double OutsideReleaseEffort_Actual { get; set; }

        public double OutsideReleasePreTravel_Min { get; set; }
        public double OutsideReleasePreTravel_Max { get; set; }
        public double OutsideReleasePreTravel_Actual { get; set; }

        public double OutsideReleaseReleaseTravel_Min { get; set; }
        public double OutsideReleaseReleaseTravel_Max { get; set; }
        public double OutsideReleaseReleaseTravel_Actual { get; set; }

        public double OutsideReleaseFullTravel_Min { get; set; }
        public double OutsideReleaseFullTravel_Max { get; set; }
        public double OutsideReleaseFullTravel_Actual { get; set; }

        // Helper
        public string Date => LoggedAt.ToString("dd-MM-yyyy");
        public string Time => LoggedAt.ToString("HH:mm:ss");
    }
}