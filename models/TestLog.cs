namespace Magna_TestApplication.Models
{
    public class TestLog
    {
        // --- Identity ---
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; }

        // --- Communication / Meta ---
        public string Shift { get; set; } = "";
        public string Variant { get; set; } = "";
        public string SerialNumber { get; set; } = "";
        public string Result { get; set; } = "";

        // =========================================================
        // FUNCTIONAL TEST
        // =========================================================

        // Seal Load
        public double SealLoad_Min { get; set; }
        public double SealLoad_Max { get; set; }
        public double SealLoad_Actual { get; set; }

        // Power Lock Current
        public double PowerLockCurrent_Min { get; set; }
        public double PowerLockCurrent_Max { get; set; }
        public double PowerLockCurrent_Actual { get; set; }

        // Power Unlock Current
        public double PowerUnlockCurrent_Min { get; set; }
        public double PowerUnlockCurrent_Max { get; set; }
        public double PowerUnlockCurrent_Actual { get; set; }

        // Key Lock - Effort
        public double KeyLockEffort_Min { get; set; }
        public double KeyLockEffort_Max { get; set; }
        public double KeyLockEffort_Actual { get; set; }

        // Key Lock - Pre Travel
        public double KeyLockPreTravel_Min { get; set; }
        public double KeyLockPreTravel_Max { get; set; }
        public double KeyLockPreTravel_Actual { get; set; }

        // Key Lock - Lock Travel
        public double KeyLockLockTravel_Min { get; set; }
        public double KeyLockLockTravel_Max { get; set; }
        public double KeyLockLockTravel_Actual { get; set; }

        // Key Lock - Full Travel
        public double KeyLockFullTravel_Min { get; set; }
        public double KeyLockFullTravel_Max { get; set; }
        public double KeyLockFullTravel_Actual { get; set; }

        // Key Unlock - Effort
        public double KeyUnlockEffort_Min { get; set; }
        public double KeyUnlockEffort_Max { get; set; }
        public double KeyUnlockEffort_Actual { get; set; }

        // Key Unlock - Pre Travel
        public double KeyUnlockPreTravel_Min { get; set; }
        public double KeyUnlockPreTravel_Max { get; set; }
        public double KeyUnlockPreTravel_Actual { get; set; }

        // Key Unlock - Lock Travel
        public double KeyUnlockLockTravel_Min { get; set; }
        public double KeyUnlockLockTravel_Max { get; set; }
        public double KeyUnlockLockTravel_Actual { get; set; }

        // Key Unlock - Full Travel
        public double KeyUnlockFullTravel_Min { get; set; }
        public double KeyUnlockFullTravel_Max { get; set; }
        public double KeyUnlockFullTravel_Actual { get; set; }

        // Child Lock - Effort
        public double ChildLockEffort_Min { get; set; }
        public double ChildLockEffort_Max { get; set; }
        public double ChildLockEffort_Actual { get; set; }

        // Child Lock - Travel
        public double ChildLockTravel_Min { get; set; }
        public double ChildLockTravel_Max { get; set; }
        public double ChildLockTravel_Actual { get; set; }

        // Child Unlock - Effort
        public double ChildUnlockEffort_Min { get; set; }
        public double ChildUnlockEffort_Max { get; set; }
        public double ChildUnlockEffort_Actual { get; set; }

        // Child Unlock - Travel
        public double ChildUnlockTravel_Min { get; set; }
        public double ChildUnlockTravel_Max { get; set; }
        public double ChildUnlockTravel_Actual { get; set; }

        // EMG Lock - Torque
        public double EmgLockTorque_Min { get; set; }
        public double EmgLockTorque_Max { get; set; }
        public double EmgLockTorque_Actual { get; set; }

        // EMG Lock - Angle
        public double EmgLockAngle_Min { get; set; }
        public double EmgLockAngle_Max { get; set; }
        public double EmgLockAngle_Actual { get; set; }

        // =========================================================
        // TRAVEL & ENDURANCE TEST
        // =========================================================

        // Inside Lock - Effort
        public double InsideLockEffort_Min { get; set; }
        public double InsideLockEffort_Max { get; set; }
        public double InsideLockEffort_Actual { get; set; }

        // Inside Lock - Travel
        public double InsideLockTravel_Min { get; set; }
        public double InsideLockTravel_Max { get; set; }
        public double InsideLockTravel_Actual { get; set; }

        // Inside Unlock - Effort
        public double InsideUnlockEffort_Min { get; set; }
        public double InsideUnlockEffort_Max { get; set; }
        public double InsideUnlockEffort_Actual { get; set; }

        // Inside Unlock - Travel
        public double InsideUnlockTravel_Min { get; set; }
        public double InsideUnlockTravel_Max { get; set; }
        public double InsideUnlockTravel_Actual { get; set; }

        // Inside Release - Effort
        public double InsideReleaseEffort_Min { get; set; }
        public double InsideReleaseEffort_Max { get; set; }
        public double InsideReleaseEffort_Actual { get; set; }

        // Inside Release - Pre Travel
        public double InsideReleasePreTravel_Min { get; set; }
        public double InsideReleasePreTravel_Max { get; set; }
        public double InsideReleasePreTravel_Actual { get; set; }

        // Inside Release - Release Travel
        public double InsideReleaseReleaseTravel_Min { get; set; }
        public double InsideReleaseReleaseTravel_Max { get; set; }
        public double InsideReleaseReleaseTravel_Actual { get; set; }

        // Inside Release - Full Travel
        public double InsideReleaseFullTravel_Min { get; set; }
        public double InsideReleaseFullTravel_Max { get; set; }
        public double InsideReleaseFullTravel_Actual { get; set; }

        // Outside Release - Effort
        public double OutsideReleaseEffort_Min { get; set; }
        public double OutsideReleaseEffort_Max { get; set; }
        public double OutsideReleaseEffort_Actual { get; set; }

        // Outside Release - Pre Travel
        public double OutsideReleasePreTravel_Min { get; set; }
        public double OutsideReleasePreTravel_Max { get; set; }
        public double OutsideReleasePreTravel_Actual { get; set; }

        // Outside Release - Release Travel
        public double OutsideReleaseReleaseTravel_Min { get; set; }
        public double OutsideReleaseReleaseTravel_Max { get; set; }
        public double OutsideReleaseReleaseTravel_Actual { get; set; }

        // Outside Release - Full Travel
        public double OutsideReleaseFullTravel_Min { get; set; }
        public double OutsideReleaseFullTravel_Max { get; set; }
        public double OutsideReleaseFullTravel_Actual { get; set; }

        // =========================================================
        // HELPER PROPERTIES (for UI display only, not stored in DB)
        // =========================================================
        public string Date => LoggedAt.ToString("dd-MM-yyyy");
        public string Time => LoggedAt.ToString("HH:mm:ss");
    }
}