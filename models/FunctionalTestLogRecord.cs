namespace Magna_TestApplication.Models
{
    public class FunctionalTestLogRecord
    {
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Shift { get; set; } = "";
        public string Variant { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public string Result { get; set; } = "";

        // Helper computed properties
        public string Date => LoggedAt.ToString("dd-MM-yyyy");
        public string Time => LoggedAt.ToString("HH:mm:ss");

        // Functional Test measurements
        public double SealLoad_Min { get; set; }
        public double SealLoad_Max { get; set; }
        public double SealLoad_Actual { get; set; }

        public double PowerLockCurrent_Min { get; set; }
        public double PowerLockCurrent_Max { get; set; }
        public double PowerLockCurrent_Actual { get; set; }

        public double PowerUnlockCurrent_Min { get; set; }
        public double PowerUnlockCurrent_Max { get; set; }
        public double PowerUnlockCurrent_Actual { get; set; }

        public double KeyLockEffort_Min { get; set; }
        public double KeyLockEffort_Max { get; set; }
        public double KeyLockEffort_Actual { get; set; }

        public double KeyLockPreTravel_Min { get; set; }
        public double KeyLockPreTravel_Max { get; set; }
        public double KeyLockPreTravel_Actual { get; set; }

        public double KeyLockLockTravel_Min { get; set; }
        public double KeyLockLockTravel_Max { get; set; }
        public double KeyLockLockTravel_Actual { get; set; }

        public double KeyLockFullTravel_Min { get; set; }
        public double KeyLockFullTravel_Max { get; set; }
        public double KeyLockFullTravel_Actual { get; set; }

        public double KeyUnlockEffort_Min { get; set; }
        public double KeyUnlockEffort_Max { get; set; }
        public double KeyUnlockEffort_Actual { get; set; }

        public double KeyUnlockPreTravel_Min { get; set; }
        public double KeyUnlockPreTravel_Max { get; set; }
        public double KeyUnlockPreTravel_Actual { get; set; }

        public double KeyUnlockLockTravel_Min { get; set; }
        public double KeyUnlockLockTravel_Max { get; set; }
        public double KeyUnlockLockTravel_Actual { get; set; }

        public double KeyUnlockFullTravel_Min { get; set; }
        public double KeyUnlockFullTravel_Max { get; set; }
        public double KeyUnlockFullTravel_Actual { get; set; }

        public double ChildLockEffort_Min { get; set; }
        public double ChildLockEffort_Max { get; set; }
        public double ChildLockEffort_Actual { get; set; }

        public double ChildLockTravel_Min { get; set; }
        public double ChildLockTravel_Max { get; set; }
        public double ChildLockTravel_Actual { get; set; }

        public double ChildUnlockEffort_Min { get; set; }
        public double ChildUnlockEffort_Max { get; set; }
        public double ChildUnlockEffort_Actual { get; set; }

        public double ChildUnlockTravel_Min { get; set; }
        public double ChildUnlockTravel_Max { get; set; }
        public double ChildUnlockTravel_Actual { get; set; }

        public double EmgLockTorque_Min { get; set; }
        public double EmgLockTorque_Max { get; set; }
        public double EmgLockTorque_Actual { get; set; }

        public double EmgLockAngle_Min { get; set; }
        public double EmgLockAngle_Max { get; set; }
        public double EmgLockAngle_Actual { get; set; }

    }
}