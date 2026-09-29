namespace Magna_TestApplication.services
{
    public static class PlcRegisterConfig
    {
        public static List<PlcRegisterMap> GetMappings() => new()
        {
            // ==========================================================
            // COMMUNICATION / META (Int16 — single registers)
            // ==========================================================
            new() { RegisterAddress = "D100", Category = "Communication", ParameterName = "Communication",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D101", Category = "Communication", ParameterName = "FT_Sequence Start",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D102", Category = "Communication", ParameterName = "FT_Sequence Start Acknowledgement",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D103", Category = "Communication", ParameterName = "FT_Result",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = "Result", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D104", Category = "Communication", ParameterName = "FT_Variant",
                    ValueType = "Status", DataType = "Int16", UiControlName = "FtVarient_TXT",
                    LogPropertyName = "Variant", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D105", Category = "Communication", ParameterName = "FT_Shift",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = "Shift", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D208", Category = "Communication", ParameterName = "TET_Sequence Start",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D209", Category = "Communication", ParameterName = "TET_Sequence Start Acknowledgement",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D210", Category = "Communication", ParameterName = "TET_Result",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = "Result", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D211", Category = "Communication", ParameterName = "TET_Variant",
                    ValueType = "Status", DataType = "Int16", UiControlName = "TetVarient_TXT",
                    LogPropertyName = "Variant", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D212", Category = "Communication", ParameterName = "TET_Shift",
                    ValueType = "Status", DataType = "Int16", UiControlName = "",
                    LogPropertyName = "Shift", LogGroup = "TET", ShowInReport = false },

            // ==========================================================
            // FUNCTIONAL TEST — all Float32 (register pairs)
            // ==========================================================

            // Seal Load
            new() { RegisterAddress = "D106", RegisterAddress2 = "D107", Category = "FT", ParameterName = "Seal Load",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtSealLoadMax",
                    LogPropertyName = "SealLoad_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D108", RegisterAddress2 = "D109", Category = "FT", ParameterName = "Seal Load",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtSealLoadMin",
                    LogPropertyName = "SealLoad_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D110", RegisterAddress2 = "D111", Category = "FT", ParameterName = "Seal Load",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtSealLoadActual",
                    LogPropertyName = "SealLoad_Actual", LogGroup = "FT", ShowInReport = true },

            // Power Lock Current
            new() { RegisterAddress = "D112", RegisterAddress2 = "D113", Category = "FT", ParameterName = "Power Lock Current",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtPowerLockMax",
                    LogPropertyName = "PowerLockCurrent_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D114", RegisterAddress2 = "D115", Category = "FT", ParameterName = "Power Lock Current",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtPowerLockMin",
                    LogPropertyName = "PowerLockCurrent_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D116", RegisterAddress2 = "D117", Category = "FT", ParameterName = "Power Lock Current",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtPowerLockActual",
                    LogPropertyName = "PowerLockCurrent_Actual", LogGroup = "FT", ShowInReport = true },

            // Power Unlock Current
            new() { RegisterAddress = "D118", RegisterAddress2 = "D119", Category = "FT", ParameterName = "Power Unlock Current",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtPowerUnlockMax",
                    LogPropertyName = "PowerUnlockCurrent_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D120", RegisterAddress2 = "D121", Category = "FT", ParameterName = "Power Unlock Current",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtPowerUnlockMin",
                    LogPropertyName = "PowerUnlockCurrent_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D122", RegisterAddress2 = "D123", Category = "FT", ParameterName = "Power Unlock Current",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtPowerUnlockActual",
                    LogPropertyName = "PowerUnlockCurrent_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Lock Effort
            new() { RegisterAddress = "D124", RegisterAddress2 = "D125", Category = "FT", ParameterName = "Key Lock - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyLockEffortMax",
                    LogPropertyName = "KeyLockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D126", RegisterAddress2 = "D127", Category = "FT", ParameterName = "Key Lock - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyLockEffortMin",
                    LogPropertyName = "KeyLockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D128", RegisterAddress2 = "D129", Category = "FT", ParameterName = "Key Lock - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyLockEffortActual",
                    LogPropertyName = "KeyLockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Lock Pre Travel
            new() { RegisterAddress = "D130", RegisterAddress2 = "D131", Category = "FT", ParameterName = "Key Lock - Pre Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyLockPreTravelMax",
                    LogPropertyName = "KeyLockPreTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D132", RegisterAddress2 = "D133", Category = "FT", ParameterName = "Key Lock - Pre Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyLockPreTravelMin",
                    LogPropertyName = "KeyLockPreTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D134", RegisterAddress2 = "D135", Category = "FT", ParameterName = "Key Lock - Pre Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyLockPreTravelActual",
                    LogPropertyName = "KeyLockPreTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Lock Lock Travel
            new() { RegisterAddress = "D136", RegisterAddress2 = "D137", Category = "FT", ParameterName = "Key Lock - Lock Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyLockLockTravelMax",
                    LogPropertyName = "KeyLockLockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D138", RegisterAddress2 = "D139", Category = "FT", ParameterName = "Key Lock - Lock Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyLockLockTravelMin",
                    LogPropertyName = "KeyLockLockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D140", RegisterAddress2 = "D141", Category = "FT", ParameterName = "Key Lock - Lock Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyLockLockTravelActual",
                    LogPropertyName = "KeyLockLockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Lock Full Travel
            new() { RegisterAddress = "D142", RegisterAddress2 = "D143", Category = "FT", ParameterName = "Key Lock - Full Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyLockFullTravelMax",
                    LogPropertyName = "KeyLockFullTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D144", RegisterAddress2 = "D145", Category = "FT", ParameterName = "Key Lock - Full Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyLockFullTravelMin",
                    LogPropertyName = "KeyLockFullTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D146", RegisterAddress2 = "D147", Category = "FT", ParameterName = "Key Lock - Full Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyLockFullTravelActual",
                    LogPropertyName = "KeyLockFullTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Unlock Effort
            new() { RegisterAddress = "D148", RegisterAddress2 = "D149", Category = "FT", ParameterName = "Key Unlock - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyUnlockEffortMax",
                    LogPropertyName = "KeyUnlockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D150", RegisterAddress2 = "D151", Category = "FT", ParameterName = "Key Unlock - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyUnlockEffortMin",
                    LogPropertyName = "KeyUnlockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D152", RegisterAddress2 = "D153", Category = "FT", ParameterName = "Key Unlock - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyUnlockEffortActual",
                    LogPropertyName = "KeyUnlockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Unlock Pre Travel
            new() { RegisterAddress = "D154", RegisterAddress2 = "D155", Category = "FT", ParameterName = "Key Unlock - Pre Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyUnlockPreTravelMax",
                    LogPropertyName = "KeyUnlockPreTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D156", RegisterAddress2 = "D157", Category = "FT", ParameterName = "Key Unlock - Pre Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyUnlockPreTravelMin",
                    LogPropertyName = "KeyUnlockPreTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D158", RegisterAddress2 = "D159", Category = "FT", ParameterName = "Key Unlock - Pre Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyUnlockPreTravelActual",
                    LogPropertyName = "KeyUnlockPreTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Unlock Lock Travel
            new() { RegisterAddress = "D160", RegisterAddress2 = "D161", Category = "FT", ParameterName = "Key Unlock - Lock Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyUnlockLockTravelMax",
                    LogPropertyName = "KeyUnlockLockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D162", RegisterAddress2 = "D163", Category = "FT", ParameterName = "Key Unlock - Lock Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyUnlockLockTravelMin",
                    LogPropertyName = "KeyUnlockLockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D164", RegisterAddress2 = "D165", Category = "FT", ParameterName = "Key Unlock - Lock Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyUnlockLockTravelActual",
                    LogPropertyName = "KeyUnlockLockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Key Unlock Full Travel
            new() { RegisterAddress = "D166", RegisterAddress2 = "D167", Category = "FT", ParameterName = "Key Unlock - Full Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtKeyUnlockFullTravelMax",
                    LogPropertyName = "KeyUnlockFullTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D168", RegisterAddress2 = "D169", Category = "FT", ParameterName = "Key Unlock - Full Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtKeyUnlockFullTravelMin",
                    LogPropertyName = "KeyUnlockFullTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D170", RegisterAddress2 = "D171", Category = "FT", ParameterName = "Key Unlock - Full Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtKeyUnlockFullTravelActual",
                    LogPropertyName = "KeyUnlockFullTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Child Lock Effort
            new() { RegisterAddress = "D172", RegisterAddress2 = "D173", Category = "FT", ParameterName = "Child Lock - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtChildLockEffortMax",
                    LogPropertyName = "ChildLockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D174", RegisterAddress2 = "D175", Category = "FT", ParameterName = "Child Lock - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtChildLockEffortMin",
                    LogPropertyName = "ChildLockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D176", RegisterAddress2 = "D177", Category = "FT", ParameterName = "Child Lock - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtChildLockEffortActual",
                    LogPropertyName = "ChildLockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // Child Lock Travel
            new() { RegisterAddress = "D178", RegisterAddress2 = "D179", Category = "FT", ParameterName = "Child Lock - Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtChildLockTravelMax",
                    LogPropertyName = "ChildLockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D180", RegisterAddress2 = "D181", Category = "FT", ParameterName = "Child Lock - Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtChildLockTravelMin",
                    LogPropertyName = "ChildLockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D182", RegisterAddress2 = "D183", Category = "FT", ParameterName = "Child Lock - Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtChildLockTravelActual",
                    LogPropertyName = "ChildLockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // Child Unlock Effort
            new() { RegisterAddress = "D184", RegisterAddress2 = "D185", Category = "FT", ParameterName = "Child Unlock - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtChildUnlockEffortMax",
                    LogPropertyName = "ChildUnlockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D186", RegisterAddress2 = "D187", Category = "FT", ParameterName = "Child Unlock - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtChildUnlockEffortMin",
                    LogPropertyName = "ChildUnlockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D188", RegisterAddress2 = "D189", Category = "FT", ParameterName = "Child Unlock - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtChildUnlockEffortActual",
                    LogPropertyName = "ChildUnlockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // Child Unlock Travel
            new() { RegisterAddress = "D190", RegisterAddress2 = "D191", Category = "FT", ParameterName = "Child Unlock - Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtChildUnlockTravelMax",
                    LogPropertyName = "ChildUnlockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D192", RegisterAddress2 = "D193", Category = "FT", ParameterName = "Child Unlock - Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtChildUnlockTravelMin",
                    LogPropertyName = "ChildUnlockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D194", RegisterAddress2 = "D195", Category = "FT", ParameterName = "Child Unlock - Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtChildUnlockTravelActual",
                    LogPropertyName = "ChildUnlockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // EMG Lock Torque
            new() { RegisterAddress = "D196", RegisterAddress2 = "D197", Category = "FT", ParameterName = "EMG Lock - Torque",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtEmgLockTorqueMax",
                    LogPropertyName = "EmgLockTorque_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D198", RegisterAddress2 = "D199", Category = "FT", ParameterName = "EMG Lock - Torque",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtEmgLockTorqueMin",
                    LogPropertyName = "EmgLockTorque_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D200", RegisterAddress2 = "D201", Category = "FT", ParameterName = "EMG Lock - Torque",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtEmgLockTorqueActual",
                    LogPropertyName = "EmgLockTorque_Actual", LogGroup = "FT", ShowInReport = true },

            // EMG Lock Angle
            new() { RegisterAddress = "D202", RegisterAddress2 = "D203", Category = "FT", ParameterName = "EMG Lock - Angle",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtEmgLockAngleMax",
                    LogPropertyName = "EmgLockAngle_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D204", RegisterAddress2 = "D205", Category = "FT", ParameterName = "EMG Lock - Angle",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtEmgLockAngleMin",
                    LogPropertyName = "EmgLockAngle_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D206", RegisterAddress2 = "D207", Category = "FT", ParameterName = "EMG Lock - Angle",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtEmgLockAngleActual",
                    LogPropertyName = "EmgLockAngle_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — all Float32 (register pairs)
            // ==========================================================

            // Inside Lock Effort
            new() { RegisterAddress = "D213", RegisterAddress2 = "D214", Category = "TET", ParameterName = "Inside Lock - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideLockEffortMax",
                    LogPropertyName = "InsideLockEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D215", RegisterAddress2 = "D216", Category = "TET", ParameterName = "Inside Lock - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideLockEffortMin",
                    LogPropertyName = "InsideLockEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D217", RegisterAddress2 = "D218", Category = "TET", ParameterName = "Inside Lock - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideLockEffortActual",
                    LogPropertyName = "InsideLockEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Lock Travel
            new() { RegisterAddress = "D219", RegisterAddress2 = "D220", Category = "TET", ParameterName = "Inside Lock - Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideLockTravelMax",
                    LogPropertyName = "InsideLockTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D221", RegisterAddress2 = "D222", Category = "TET", ParameterName = "Inside Lock - Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideLockTravelMin",
                    LogPropertyName = "InsideLockTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D223", RegisterAddress2 = "D224", Category = "TET", ParameterName = "Inside Lock - Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideLockTravelActual",
                    LogPropertyName = "InsideLockTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Unlock Effort
            new() { RegisterAddress = "D225", RegisterAddress2 = "D226", Category = "TET", ParameterName = "Inside Unlock - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideUnlockEffortMax",
                    LogPropertyName = "InsideUnlockEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D227", RegisterAddress2 = "D228", Category = "TET", ParameterName = "Inside Unlock - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideUnlockEffortMin",
                    LogPropertyName = "InsideUnlockEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D229", RegisterAddress2 = "D230", Category = "TET", ParameterName = "Inside Unlock - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideUnlockEffortActual",
                    LogPropertyName = "InsideUnlockEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Unlock Travel
            new() { RegisterAddress = "D231", RegisterAddress2 = "D232", Category = "TET", ParameterName = "Inside Unlock - Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideUnlockTravelMax",
                    LogPropertyName = "InsideUnlockTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D233", RegisterAddress2 = "D234", Category = "TET", ParameterName = "Inside Unlock - Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideUnlockTravelMin",
                    LogPropertyName = "InsideUnlockTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D235", RegisterAddress2 = "D236", Category = "TET", ParameterName = "Inside Unlock - Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideUnlockTravelActual",
                    LogPropertyName = "InsideUnlockTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Release Effort
            new() { RegisterAddress = "D237", RegisterAddress2 = "D238", Category = "TET", ParameterName = "Inside Release - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideReleaseEffortMax",
                    LogPropertyName = "InsideReleaseEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D239", RegisterAddress2 = "D240", Category = "TET", ParameterName = "Inside Release - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideReleaseEffortMin",
                    LogPropertyName = "InsideReleaseEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D241", RegisterAddress2 = "D242", Category = "TET", ParameterName = "Inside Release - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideReleaseEffortActual",
                    LogPropertyName = "InsideReleaseEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Release Pre Travel
            new() { RegisterAddress = "D243", RegisterAddress2 = "D244", Category = "TET", ParameterName = "Inside Release - Pre Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideReleasePreTravelMax",
                    LogPropertyName = "InsideReleasePreTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D245", RegisterAddress2 = "D246", Category = "TET", ParameterName = "Inside Release - Pre Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideReleasePreTravelMin",
                    LogPropertyName = "InsideReleasePreTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D247", RegisterAddress2 = "D248", Category = "TET", ParameterName = "Inside Release - Pre Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideReleasePreTravelActual",
                    LogPropertyName = "InsideReleasePreTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Release Release Travel
            new() { RegisterAddress = "D249", RegisterAddress2 = "D250", Category = "TET", ParameterName = "Inside Release - Release Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideReleaseReleaseTravelMax",
                    LogPropertyName = "InsideReleaseReleaseTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D251", RegisterAddress2 = "D252", Category = "TET", ParameterName = "Inside Release - Release Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideReleaseReleaseTravelMin",
                    LogPropertyName = "InsideReleaseReleaseTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D253", RegisterAddress2 = "D254", Category = "TET", ParameterName = "Inside Release - Release Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideReleaseReleaseTravelActual",
                    LogPropertyName = "InsideReleaseReleaseTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Inside Release Full Travel
            new() { RegisterAddress = "D255", RegisterAddress2 = "D256", Category = "TET", ParameterName = "Inside Release - Full Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtInsideReleaseFullTravelMax",
                    LogPropertyName = "InsideReleaseFullTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D257", RegisterAddress2 = "D258", Category = "TET", ParameterName = "Inside Release - Full Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtInsideReleaseFullTravelMin",
                    LogPropertyName = "InsideReleaseFullTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D259", RegisterAddress2 = "D260", Category = "TET", ParameterName = "Inside Release - Full Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtInsideReleaseFullTravelActual",
                    LogPropertyName = "InsideReleaseFullTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Outside Release Effort
            new() { RegisterAddress = "D261", RegisterAddress2 = "D262", Category = "TET", ParameterName = "Outside Release - Effort",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtOutsideReleaseEffortMax",
                    LogPropertyName = "OutsideReleaseEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D263", RegisterAddress2 = "D264", Category = "TET", ParameterName = "Outside Release - Effort",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtOutsideReleaseEffortMin",
                    LogPropertyName = "OutsideReleaseEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D265", RegisterAddress2 = "D266", Category = "TET", ParameterName = "Outside Release - Effort",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtOutsideReleaseEffortActual",
                    LogPropertyName = "OutsideReleaseEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // Outside Release Pre Travel
            new() { RegisterAddress = "D267", RegisterAddress2 = "D268", Category = "TET", ParameterName = "Outside Release - Pre Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtOutsideReleasePreTravelMax",
                    LogPropertyName = "OutsideReleasePreTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D269", RegisterAddress2 = "D270", Category = "TET", ParameterName = "Outside Release - Pre Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtOutsideReleasePreTravelMin",
                    LogPropertyName = "OutsideReleasePreTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D271", RegisterAddress2 = "D272", Category = "TET", ParameterName = "Outside Release - Pre Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtOutsideReleasePreTravelActual",
                    LogPropertyName = "OutsideReleasePreTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Outside Release Release Travel
            new() { RegisterAddress = "D273", RegisterAddress2 = "D274", Category = "TET", ParameterName = "Outside Release - Release Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtOutsideReleaseReleaseTravelMax",
                    LogPropertyName = "OutsideReleaseReleaseTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D275", RegisterAddress2 = "D276", Category = "TET", ParameterName = "Outside Release - Release Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtOutsideReleaseReleaseTravelMin",
                    LogPropertyName = "OutsideReleaseReleaseTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D277", RegisterAddress2 = "D278", Category = "TET", ParameterName = "Outside Release - Release Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtOutsideReleaseReleaseTravelActual",
                    LogPropertyName = "OutsideReleaseReleaseTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // Outside Release Full Travel
            new() { RegisterAddress = "D279", RegisterAddress2 = "D280", Category = "TET", ParameterName = "Outside Release - Full Travel",
                    ValueType = "Maximum", DataType = "Float32", UiControlName = "txtOutsideReleaseFullTravelMax",
                    LogPropertyName = "OutsideReleaseFullTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D281", RegisterAddress2 = "D282", Category = "TET", ParameterName = "Outside Release - Full Travel",
                    ValueType = "Minimum", DataType = "Float32", UiControlName = "txtOutsideReleaseFullTravelMin",
                    LogPropertyName = "OutsideReleaseFullTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D283", RegisterAddress2 = "D284", Category = "TET", ParameterName = "Outside Release - Full Travel",
                    ValueType = "Actual", DataType = "Float32", UiControlName = "txtOutsideReleaseFullTravelActual",
                    LogPropertyName = "OutsideReleaseFullTravel_Actual", LogGroup = "TET", ShowInReport = true },
        };
    }
}