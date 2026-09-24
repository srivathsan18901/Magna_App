-- Meta (always show in report)
UPDATE PlcRegisterMappings SET LogPropertyName = 'Shift',   ShowInReport = 0 WHERE RegisterAddress = 'D105';
UPDATE PlcRegisterMappings SET LogPropertyName = 'Variant', ShowInReport = 0 WHERE RegisterAddress = 'D104';
UPDATE PlcRegisterMappings SET LogPropertyName = 'Result',  ShowInReport = 0 WHERE RegisterAddress = 'D103';

-- =====================================================
-- FUNCTIONAL TEST
-- Rule: Min and Max → ShowInReport = 0
--       Actual       → ShowInReport = 1
-- =====================================================

-- Seal Load
UPDATE PlcRegisterMappings SET LogPropertyName = 'SealLoad_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D106';
UPDATE PlcRegisterMappings SET LogPropertyName = 'SealLoad_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D107';
UPDATE PlcRegisterMappings SET LogPropertyName = 'SealLoad_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D108';

-- Power Lock Current
UPDATE PlcRegisterMappings SET LogPropertyName = 'PowerLockCurrent_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D109';
UPDATE PlcRegisterMappings SET LogPropertyName = 'PowerLockCurrent_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D110';
UPDATE PlcRegisterMappings SET LogPropertyName = 'PowerLockCurrent_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D111';

-- Power Unlock Current
UPDATE PlcRegisterMappings SET LogPropertyName = 'PowerUnlockCurrent_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D112';
UPDATE PlcRegisterMappings SET LogPropertyName = 'PowerUnlockCurrent_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D113';
UPDATE PlcRegisterMappings SET LogPropertyName = 'PowerUnlockCurrent_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D114';

-- Key Lock Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D115';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D116';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D117';

-- Key Lock Pre Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockPreTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D118';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockPreTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D119';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockPreTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D120';

-- Key Lock Lock Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockLockTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D121';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockLockTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D122';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockLockTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D123';

-- Key Lock Full Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockFullTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D124';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockFullTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D125';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyLockFullTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D126';

-- Key Unlock Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D127';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D128';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D129';

-- Key Unlock Pre Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockPreTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D130';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockPreTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D131';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockPreTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D132';

-- Key Unlock Lock Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockLockTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D133';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockLockTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D134';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockLockTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D135';

-- Key Unlock Full Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockFullTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D136';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockFullTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D137';
UPDATE PlcRegisterMappings SET LogPropertyName = 'KeyUnlockFullTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D138';

-- Child Lock Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildLockEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D139';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildLockEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D140';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildLockEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D141';

-- Child Lock Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildLockTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D142';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildLockTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D143';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildLockTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D144';

-- Child Unlock Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildUnlockEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D145';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildUnlockEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D146';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildUnlockEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D147';

-- Child Unlock Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildUnlockTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D148';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildUnlockTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D149';
UPDATE PlcRegisterMappings SET LogPropertyName = 'ChildUnlockTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D150';

-- EMG Lock Torque
UPDATE PlcRegisterMappings SET LogPropertyName = 'EmgLockTorque_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D151';
UPDATE PlcRegisterMappings SET LogPropertyName = 'EmgLockTorque_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D152';
UPDATE PlcRegisterMappings SET LogPropertyName = 'EmgLockTorque_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D153';

-- EMG Lock Angle
UPDATE PlcRegisterMappings SET LogPropertyName = 'EmgLockAngle_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D154';
UPDATE PlcRegisterMappings SET LogPropertyName = 'EmgLockAngle_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D155';
UPDATE PlcRegisterMappings SET LogPropertyName = 'EmgLockAngle_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D156';

-- =====================================================
-- TRAVEL & ENDURANCE TEST
-- =====================================================

-- Inside Lock Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideLockEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D157';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideLockEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D158';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideLockEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D159';

-- Inside Lock Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideLockTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D160';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideLockTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D161';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideLockTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D162';

-- Inside Unlock Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideUnlockEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D163';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideUnlockEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D164';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideUnlockEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D165';

-- Inside Unlock Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideUnlockTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D166';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideUnlockTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D167';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideUnlockTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D168';

-- Inside Release Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D169';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D170';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D171';

-- Inside Release Pre Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleasePreTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D172';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleasePreTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D173';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleasePreTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D174';

-- Inside Release Release Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseReleaseTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D175';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseReleaseTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D176';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseReleaseTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D177';

-- Inside Release Full Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseFullTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D178';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseFullTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D179';
UPDATE PlcRegisterMappings SET LogPropertyName = 'InsideReleaseFullTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D180';

-- Outside Release Effort
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseEffort_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D181';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseEffort_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D182';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseEffort_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D183';

-- Outside Release Pre Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleasePreTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D184';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleasePreTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D185';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleasePreTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D186';

-- Outside Release Release Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseReleaseTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D187';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseReleaseTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D188';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseReleaseTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D189';

-- Outside Release Full Travel
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseFullTravel_Min',    ShowInReport = 0 WHERE RegisterAddress = 'D190';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseFullTravel_Max',    ShowInReport = 0 WHERE RegisterAddress = 'D191';
UPDATE PlcRegisterMappings SET LogPropertyName = 'OutsideReleaseFullTravel_Actual', ShowInReport = 1 WHERE RegisterAddress = 'D192';

-- Verify
SELECT RegisterAddress, ParameterName, ValueType, LogPropertyName, ShowInReport
FROM PlcRegisterMappings
ORDER BY Id;