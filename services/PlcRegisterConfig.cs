using Magna_TestApplication.Models;

namespace Magna_TestApplication.services
{
    public static class PlcRegisterConfig
    {
        public static List<PlcRegisterMap> GetMappings() => new()
        {
            // ==========================================================
            // COMMUNICATION / META
            // ==========================================================
            new() { RegisterAddress = "D100", Category = "Communication", ParameterName = "Communication",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D101", Category = "Communication", ParameterName = "FT_Sequence Start",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D102", Category = "Communication", ParameterName = "FT_Sequence Start Acknowledgement",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },
            // FT meta
            new() { RegisterAddress = "D103", Category = "Communication", ParameterName = "FT_Result",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = "Result", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D104", Category = "Communication", ParameterName = "FT_Variant",
                    ValueType = "Status", UiControlName = "FtVarient_TXT",
                    LogPropertyName = "Variant", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D105", Category = "Communication", ParameterName = "FT_Shift",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = "Shift", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D157", Category = "Communication", ParameterName = "TET_Sequence Start",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },

            new() { RegisterAddress = "D158", Category = "Communication", ParameterName = "TET_Sequence Start Acknowledgement",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = null, LogGroup = null, ShowInReport = false },
            // TET meta
            new() { RegisterAddress = "D159", Category = "Communication", ParameterName = "TET_Result",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = "Result", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D160", Category = "Communication", ParameterName = "TET_Variant",
                    ValueType = "Status", UiControlName = "TetVarient_TXT",
                    LogPropertyName = "Variant", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D161", Category = "Communication", ParameterName = "TET_Shift",
                    ValueType = "Status", UiControlName = "",
                    LogPropertyName = "Shift", LogGroup = "TET", ShowInReport = false },

            // ==========================================================
            // FUNCTIONAL TEST — Seal Load
            // ==========================================================
            new() { RegisterAddress = "D106", Category = "FT", ParameterName = "Seal Load",
                    ValueType = "Maximum", UiControlName = "txtSealLoadMax",
                    LogPropertyName = "SealLoad_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D107", Category = "FT", ParameterName = "Seal Load",
                    ValueType = "Minimum", UiControlName = "txtSealLoadMin",
                    LogPropertyName = "SealLoad_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D108", Category = "FT", ParameterName = "Seal Load",
                    ValueType = "Actual", UiControlName = "txtSealLoadActual",
                    LogPropertyName = "SealLoad_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Power Lock Current
            // ==========================================================
            new() { RegisterAddress = "D109", Category = "FT", ParameterName = "Power Lock Current",
                    ValueType = "Maximum", UiControlName = "txtPowerLockMax",
                    LogPropertyName = "PowerLockCurrent_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D110", Category = "FT", ParameterName = "Power Lock Current",
                    ValueType = "Minimum", UiControlName = "txtPowerLockMin",
                    LogPropertyName = "PowerLockCurrent_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D111", Category = "FT", ParameterName = "Power Lock Current",
                    ValueType = "Actual", UiControlName = "txtPowerLockActual",
                    LogPropertyName = "PowerLockCurrent_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Power Unlock Current
            // ==========================================================
            new() { RegisterAddress = "D112", Category = "FT", ParameterName = "Power Unlock Current",
                    ValueType = "Maximum", UiControlName = "txtPowerUnlockMax",
                    LogPropertyName = "PowerUnlockCurrent_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D113", Category = "FT", ParameterName = "Power Unlock Current",
                    ValueType = "Minimum", UiControlName = "txtPowerUnlockMin",
                    LogPropertyName = "PowerUnlockCurrent_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D114", Category = "FT", ParameterName = "Power Unlock Current",
                    ValueType = "Actual", UiControlName = "txtPowerUnlockActual",
                    LogPropertyName = "PowerUnlockCurrent_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key Lock - Effort
            // ==========================================================
            new() { RegisterAddress = "D115", Category = "FT", ParameterName = "Key Lock - Effort",
                    ValueType = "Maximum", UiControlName = "txtKeyLockEffortMax",
                    LogPropertyName = "KeyLockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D116", Category = "FT", ParameterName = "Key Lock - Effort",
                    ValueType = "Minimum", UiControlName = "txtKeyLockEffortMin",
                    LogPropertyName = "KeyLockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D117", Category = "FT", ParameterName = "Key Lock - Effort",
                    ValueType = "Actual", UiControlName = "txtKeyLockEffortActual",
                    LogPropertyName = "KeyLockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key Lock - Pre Travel
            // ==========================================================
            new() { RegisterAddress = "D118", Category = "FT", ParameterName = "Key Lock - Pre Travel",
                    ValueType = "Maximum", UiControlName = "txtKeyLockPreTravelMax",
                    LogPropertyName = "KeyLockPreTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D119", Category = "FT", ParameterName = "Key Lock - Pre Travel",
                    ValueType = "Minimum", UiControlName = "txtKeyLockPreTravelMin",
                    LogPropertyName = "KeyLockPreTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D120", Category = "FT", ParameterName = "Key Lock - Pre Travel",
                    ValueType = "Actual", UiControlName = "txtKeyLockPreTravelActual",
                    LogPropertyName = "KeyLockPreTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key Lock - lock travel
            // ==========================================================
            new() { RegisterAddress = "D121", Category = "FT", ParameterName = "Key Lock - Lock Travel",
                    ValueType = "Maximum", UiControlName = "txtKeyLockLockTravelMax",
                    LogPropertyName = "KeyLockLockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D122", Category = "FT", ParameterName = "Key Lock - Lock Travel",
                    ValueType = "Minimum", UiControlName = "txtKeyLockLockTravelMin",
                    LogPropertyName = "KeyLockLockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D123", Category = "FT", ParameterName = "Key Lock - Lock Travel",
                    ValueType = "Actual", UiControlName = "txtKeyLockLockTravelActual",
                    LogPropertyName = "KeyLockLockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key Lock - full travel
            // ==========================================================
            new() { RegisterAddress = "D124", Category = "FT", ParameterName = "Key Lock - Full Travel",
                    ValueType = "Maximum", UiControlName = "txtKeyLockFullTravelMax",
                    LogPropertyName = "KeyLockFullTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D125", Category = "FT", ParameterName = "Key Lock - Full Travel",
                    ValueType = "Minimum", UiControlName = "txtKeyLockFullTravelMin",
                    LogPropertyName = "KeyLockFullTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D126", Category = "FT", ParameterName = "Key Lock - Full Travel",
                    ValueType = "Actual", UiControlName = "txtKeyLockFullTravelActual",
                    LogPropertyName = "KeyLockFullTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key UnLock - Effort
            // ==========================================================
            new() { RegisterAddress = "D127", Category = "FT", ParameterName = "Key Unlock - Effort",
                    ValueType = "Maximum", UiControlName = "txtKeyUnlockEffortMax",
                    LogPropertyName = "KeyUnlockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D128", Category = "FT", ParameterName = "Key Unlock - Effort",
                    ValueType = "Minimum", UiControlName = "txtKeyUnlockEffortMin",
                    LogPropertyName = "KeyUnlockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D129", Category = "FT", ParameterName = "Key Unlock - Effort",
                    ValueType = "Actual", UiControlName = "txtKeyUnlockEffortActual",
                    LogPropertyName = "KeyUnlockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key UnLock - pre travel
            // ==========================================================
            new() { RegisterAddress = "D130", Category = "FT", ParameterName = "Key Unlock - Pre Travel",
                    ValueType = "Maximum", UiControlName = "txtKeyUnlockPreTravelMax",
                    LogPropertyName = "KeyUnlockPreTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D131", Category = "FT", ParameterName = "Key Unlock - Pre Travel",
                    ValueType = "Minimum", UiControlName = "txtKeyUnlockPreTravelMin",
                    LogPropertyName = "KeyUnlockPreTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D132", Category = "FT", ParameterName = "Key Unlock - Pre Travel",
                    ValueType = "Actual", UiControlName = "txtKeyUnlockPreTravelActual",
                    LogPropertyName = "KeyUnlockPreTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key UnLock - lock travel
            // ==========================================================
            new() { RegisterAddress = "D133", Category = "FT", ParameterName = "Key Unlock - Lock Travel",
                    ValueType = "Maximum", UiControlName = "txtKeyUnlockLockTravelMax",
                    LogPropertyName = "KeyUnlockLockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D134", Category = "FT", ParameterName = "Key Unlock - Lock Travel",
                    ValueType = "Minimum", UiControlName = "txtKeyUnlockLockTravelMin",
                    LogPropertyName = "KeyUnlockLockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D135", Category = "FT", ParameterName = "Key Unlock - Lock Travel",
                    ValueType = "Actual", UiControlName = "txtKeyUnlockLockTravelActual",
                    LogPropertyName = "KeyUnlockLockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Key UnLock - full travel
            // ==========================================================
            new() { RegisterAddress = "D136", Category = "FT", ParameterName = "Key Unlock - Full Travel",
                    ValueType = "Maximum", UiControlName = "txtKeyUnlockFullTravelMax",
                    LogPropertyName = "KeyUnlockFullTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D137", Category = "FT", ParameterName = "Key Unlock - Full Travel",
                    ValueType = "Minimum", UiControlName = "txtKeyUnlockFullTravelMin",
                    LogPropertyName = "KeyUnlockFullTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D138", Category = "FT", ParameterName = "Key Unlock - Full Travel",
                    ValueType = "Actual", UiControlName = "txtKeyUnlockFullTravelActual",
                    LogPropertyName = "KeyUnlockFullTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Child Lock - Effort
            // ==========================================================
            new() { RegisterAddress = "D139", Category = "FT", ParameterName = "Child Lock - Effort",
                    ValueType = "Maximum", UiControlName = "txtChildLockEffortMax",
                    LogPropertyName = "ChildLockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D140", Category = "FT", ParameterName = "Child Lock - Effort",
                    ValueType = "Minimum", UiControlName = "txtChildLockEffortMin",
                    LogPropertyName = "ChildLockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D141", Category = "FT", ParameterName = "Child Lock - Effort",
                    ValueType = "Actual", UiControlName = "txtChildLockEffortActual",
                    LogPropertyName = "ChildLockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Child Lock - Travel
            // ==========================================================
            new() { RegisterAddress = "D142", Category = "FT", ParameterName = "Child Lock - Travel",
                    ValueType = "Maximum", UiControlName = "txtChildLockTravelMax",
                    LogPropertyName = "ChildLockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D143", Category = "FT", ParameterName = "Child Lock - Travel",
                    ValueType = "Minimum", UiControlName = "txtChildLockTravelMin",
                    LogPropertyName = "ChildLockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D144", Category = "FT", ParameterName = "Child Lock - Travel",
                    ValueType = "Actual", UiControlName = "txtChildLockTravelActual",
                    LogPropertyName = "ChildLockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Child Unlock - Effort
            // ==========================================================
            new() { RegisterAddress = "D145", Category = "FT", ParameterName = "Child Unlock - Effort",
                    ValueType = "Maximum", UiControlName = "txtChildUnlockEffortMax",
                    LogPropertyName = "ChildUnlockEffort_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D146", Category = "FT", ParameterName = "Child Unlock - Effort",
                    ValueType = "Minimum", UiControlName = "txtChildUnlockEffortMin",
                    LogPropertyName = "ChildUnlockEffort_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D147", Category = "FT", ParameterName = "Child Unlock - Effort",
                    ValueType = "Actual", UiControlName = "txtChildUnlockEffortActual",
                    LogPropertyName = "ChildUnlockEffort_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — Child Unlock - Travel
            // ==========================================================
            new() { RegisterAddress = "D148", Category = "FT", ParameterName = "Child Unlock - Travel",
                    ValueType = "Maximum", UiControlName = "txtChildUnlockTravelMax",
                    LogPropertyName = "ChildUnlockTravel_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D149", Category = "FT", ParameterName = "Child Unlock - Travel",
                    ValueType = "Minimum", UiControlName = "txtChildUnlockTravelMin",
                    LogPropertyName = "ChildUnlockTravel_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D150", Category = "FT", ParameterName = "Child Unlock - Travel",
                    ValueType = "Actual", UiControlName = "txtChildUnlockTravelActual",
                    LogPropertyName = "ChildUnlockTravel_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — EMG Lock - Torque
            // ==========================================================
            new() { RegisterAddress = "D151", Category = "FT", ParameterName = "EMG Lock - Torque",
                    ValueType = "Maximum", UiControlName = "txtEmgLockTorqueMax",
                    LogPropertyName = "EmgLockTorque_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D152", Category = "FT", ParameterName = "EMG Lock - Torque",
                    ValueType = "Minimum", UiControlName = "txtEmgLockTorqueMin",
                    LogPropertyName = "EmgLockTorque_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D153", Category = "FT", ParameterName = "EMG Lock - Torque",
                    ValueType = "Actual", UiControlName = "txtEmgLockTorqueActual",
                    LogPropertyName = "EmgLockTorque_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // FUNCTIONAL TEST — EMG Lock - Angle
            // ==========================================================
            new() { RegisterAddress = "D154", Category = "FT", ParameterName = "EMG Lock - Angle",
                    ValueType = "Maximum", UiControlName = "txtEmgLockAngleMax",
                    LogPropertyName = "EmgLockAngle_Max", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D155", Category = "FT", ParameterName = "EMG Lock - Angle",
                    ValueType = "Minimum", UiControlName = "txtEmgLockAngleMin",
                    LogPropertyName = "EmgLockAngle_Min", LogGroup = "FT", ShowInReport = false },

            new() { RegisterAddress = "D156", Category = "FT", ParameterName = "EMG Lock - Angle",
                    ValueType = "Actual", UiControlName = "txtEmgLockAngleActual",
                    LogPropertyName = "EmgLockAngle_Actual", LogGroup = "FT", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside Lock - Effort
            // ==========================================================
            new() { RegisterAddress = "D162", Category = "TET", ParameterName = "Inside Lock - Effort",
                    ValueType = "Maximum", UiControlName = "txtInsideLockEffortMax",
                    LogPropertyName = "InsideLockEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D163", Category = "TET", ParameterName = "Inside Lock - Effort",
                    ValueType = "Minimum", UiControlName = "txtInsideLockEffortMin",
                    LogPropertyName = "InsideLockEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D164", Category = "TET", ParameterName = "Inside Lock - Effort",
                    ValueType = "Actual", UiControlName = "txtInsideLockEffortActual",
                    LogPropertyName = "InsideLockEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside Lock - Travel
            // ==========================================================
            new() { RegisterAddress = "D165", Category = "TET", ParameterName = "Inside Lock - Travel",
                    ValueType = "Maximum", UiControlName = "txtInsideLockTravelMax",
                    LogPropertyName = "InsideLockTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D166", Category = "TET", ParameterName = "Inside Lock - Travel",
                    ValueType = "Minimum", UiControlName = "txtInsideLockTravelMin",
                    LogPropertyName = "InsideLockTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D167", Category = "TET", ParameterName = "Inside Lock - Travel",
                    ValueType = "Actual", UiControlName = "txtInsideLockTravelActual",
                    LogPropertyName = "InsideLockTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside Unlock - Effort
            // ==========================================================
            new() { RegisterAddress = "D168", Category = "TET", ParameterName = "Inside Unlock - Effort",
                    ValueType = "Maximum", UiControlName = "txtInsideUnlockEffortMax",
                    LogPropertyName = "InsideUnlockEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D169", Category = "TET", ParameterName = "Inside Unlock - Effort",
                    ValueType = "Minimum", UiControlName = "txtInsideUnlockEffortMin",
                    LogPropertyName = "InsideUnlockEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D170", Category = "TET", ParameterName = "Inside Unlock - Effort",
                    ValueType = "Actual", UiControlName = "txtInsideUnlockEffortActual",
                    LogPropertyName = "InsideUnlockEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside Unlock - Travel
            // ==========================================================
            new() { RegisterAddress = "D171", Category = "TET", ParameterName = "Inside Unlock - Travel",
                    ValueType = "Maximum", UiControlName = "txtInsideUnlockTravelMax",
                    LogPropertyName = "InsideUnlockTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D172", Category = "TET", ParameterName = "Inside Unlock - Travel",
                    ValueType = "Minimum", UiControlName = "txtInsideUnlockTravelMin",
                    LogPropertyName = "InsideUnlockTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D173", Category = "TET", ParameterName = "Inside Unlock - Travel",
                    ValueType = "Actual", UiControlName = "txtInsideUnlockTravelActual",
                    LogPropertyName = "InsideUnlockTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside release - Effort
            // ==========================================================
            new() { RegisterAddress = "D174", Category = "TET", ParameterName = "Inside Release - Effort",
                    ValueType = "Maximum", UiControlName = "txtInsideReleaseEffortMax",
                    LogPropertyName = "InsideReleaseEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D175", Category = "TET", ParameterName = "Inside Release - Effort",
                    ValueType = "Minimum", UiControlName = "txtInsideReleaseEffortMin",
                    LogPropertyName = "InsideReleaseEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D176", Category = "TET", ParameterName = "Inside Release - Effort",
                    ValueType = "Actual", UiControlName = "txtInsideReleaseEffortActual",
                    LogPropertyName = "InsideReleaseEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside release - Pre travel
            // ==========================================================
            new() { RegisterAddress = "D177", Category = "TET", ParameterName = "Inside Release - Pre Travel",
                    ValueType = "Maximum", UiControlName = "txtInsideReleasePreTravelMax",
                    LogPropertyName = "InsideReleasePreTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D178", Category = "TET", ParameterName = "Inside Release - Pre Travel",
                    ValueType = "Minimum", UiControlName = "txtInsideReleasePreTravelMin",
                    LogPropertyName = "InsideReleasePreTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D179", Category = "TET", ParameterName = "Inside Release - Pre Travel",
                    ValueType = "Actual", UiControlName = "txtInsideReleasePreTravelActual",
                    LogPropertyName = "InsideReleasePreTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside release - Release travel
            // ==========================================================
            new() { RegisterAddress = "D180", Category = "TET", ParameterName = "Inside Release - Release Travel",
                    ValueType = "Maximum", UiControlName = "txtInsideReleaseReleaseTravelMax",
                    LogPropertyName = "InsideReleaseReleaseTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D181", Category = "TET", ParameterName = "Inside Release - Release Travel",
                    ValueType = "Minimum", UiControlName = "txtInsideReleaseReleaseTravelMin",
                    LogPropertyName = "InsideReleaseReleaseTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D182", Category = "TET", ParameterName = "Inside Release - Release Travel",
                    ValueType = "Actual", UiControlName = "txtInsideReleaseReleaseTravelActual",
                    LogPropertyName = "InsideReleaseReleaseTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Inside release - Full travel
            // ==========================================================
            new() { RegisterAddress = "D183", Category = "TET", ParameterName = "Inside Release - Full Travel",
                    ValueType = "Maximum", UiControlName = "txtInsideReleaseFullTravelMax",
                    LogPropertyName = "InsideReleaseFullTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D184", Category = "TET", ParameterName = "Inside Release - Full Travel",
                    ValueType = "Minimum", UiControlName = "txtInsideReleaseFullTravelMin",
                    LogPropertyName = "InsideReleaseFullTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D185", Category = "TET", ParameterName = "Inside Release - Full Travel",
                    ValueType = "Actual", UiControlName = "txtInsideReleaseFullTravelActual",
                    LogPropertyName = "InsideReleaseFullTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Outside release - Effort
            // ==========================================================
            new() { RegisterAddress = "D186", Category = "TET", ParameterName = "Outside Release - Effort",
                    ValueType = "Maximum", UiControlName = "txtOutsideReleaseEffortMax",
                    LogPropertyName = "OutsideReleaseEffort_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D187", Category = "TET", ParameterName = "Outside Release - Effort",
                    ValueType = "Minimum", UiControlName = "txtOutsideReleaseEffortMin",
                    LogPropertyName = "OutsideReleaseEffort_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D188", Category = "TET", ParameterName = "Outside Release - Effort",
                    ValueType = "Actual", UiControlName = "txtOutsideReleaseEffortActual",
                    LogPropertyName = "OutsideReleaseEffort_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Outside release - Pre travel
            // ==========================================================
            new() { RegisterAddress = "D189", Category = "TET", ParameterName = "Outside Release - Pre Travel",
                    ValueType = "Maximum", UiControlName = "txtOutsideReleasePreTravelMax",
                    LogPropertyName = "OutsideReleasePreTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D190", Category = "TET", ParameterName = "Outside Release - Pre Travel",
                    ValueType = "Minimum", UiControlName = "txtOutsideReleasePreTravelMin",
                    LogPropertyName = "OutsideReleasePreTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D191", Category = "TET", ParameterName = "Outside Release - Pre Travel",
                    ValueType = "Actual", UiControlName = "txtOutsideReleasePreTravelActual",
                    LogPropertyName = "OutsideReleasePreTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Outside release - Release travel
            // ==========================================================
            new() { RegisterAddress = "D192", Category = "TET", ParameterName = "Outside Release - Release Travel",
                    ValueType = "Maximum", UiControlName = "txtOutsideReleaseReleaseTravelMax",
                    LogPropertyName = "OutsideReleaseReleaseTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D193", Category = "TET", ParameterName = "Outside Release - Release Travel",
                    ValueType = "Minimum", UiControlName = "txtOutsideReleaseReleaseTravelMin",
                    LogPropertyName = "OutsideReleaseReleaseTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D194", Category = "TET", ParameterName = "Outside Release - Release Travel",
                    ValueType = "Actual", UiControlName = "txtOutsideReleaseReleaseTravelActual",
                    LogPropertyName = "OutsideReleaseReleaseTravel_Actual", LogGroup = "TET", ShowInReport = true },

            // ==========================================================
            // TRAVEL & ENDURANCE — Outside release - Full travel
            // ==========================================================
            new() { RegisterAddress = "D195", Category = "TET", ParameterName = "Outside Release - Full Travel",
                    ValueType = "Maximum", UiControlName = "txtOutsideReleaseFullTravelMax",
                    LogPropertyName = "OutsideReleaseFullTravel_Max", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D196", Category = "TET", ParameterName = "Outside Release - Full Travel",
                    ValueType = "Minimum", UiControlName = "txtOutsideReleaseFullTravelMin",
                    LogPropertyName = "OutsideReleaseFullTravel_Min", LogGroup = "TET", ShowInReport = false },

            new() { RegisterAddress = "D197", Category = "TET", ParameterName = "Outside Release - Full Travel",
                    ValueType = "Actual", UiControlName = "txtOutsideReleaseFullTravelActual",
                    LogPropertyName = "OutsideReleaseFullTravel_Actual", LogGroup = "TET", ShowInReport = true },
        };
    }
}