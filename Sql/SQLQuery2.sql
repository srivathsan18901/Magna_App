USE MagnaDB;
GO

CREATE TABLE TestLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LoggedAt DATETIME NOT NULL,
    Shift NVARCHAR(10),
    Variant NVARCHAR(10),
    SerialNumber NVARCHAR(50),
    Result NVARCHAR(20),

    -- Functional Test Data
    SealLoad_Min FLOAT,
    SealLoad_Max FLOAT,
    SealLoad_Actual FLOAT,

    PowerLockCurrent_Min FLOAT,
    PowerLockCurrent_Max FLOAT,
    PowerLockCurrent_Actual FLOAT,

    PowerUnlockCurrent_Min FLOAT,
    PowerUnlockCurrent_Max FLOAT,
    PowerUnlockCurrent_Actual FLOAT,

    KeyLockEffort_Min FLOAT,
    KeyLockEffort_Max FLOAT,
    KeyLockEffort_Actual FLOAT,

    KeyLockPreTravel_Min FLOAT,
    KeyLockPreTravel_Max FLOAT,
    KeyLockPreTravel_Actual FLOAT,

    KeyLockLockTravel_Min FLOAT,
    KeyLockLockTravel_Max FLOAT,
    KeyLockLockTravel_Actual FLOAT,

    KeyLockFullTravel_Min FLOAT,
    KeyLockFullTravel_Max FLOAT,
    KeyLockFullTravel_Actual FLOAT,

    KeyUnlockEffort_Min FLOAT,
    KeyUnlockEffort_Max FLOAT,
    KeyUnlockEffort_Actual FLOAT,

    KeyUnlockPreTravel_Min FLOAT,
    KeyUnlockPreTravel_Max FLOAT,
    KeyUnlockPreTravel_Actual FLOAT,

    KeyUnlockLockTravel_Min FLOAT,
    KeyUnlockLockTravel_Max FLOAT,
    KeyUnlockLockTravel_Actual FLOAT,

    KeyUnlockFullTravel_Min FLOAT,
    KeyUnlockFullTravel_Max FLOAT,
    KeyUnlockFullTravel_Actual FLOAT,

    ChildLockEffort_Min FLOAT,
    ChildLockEffort_Max FLOAT,
    ChildLockEffort_Actual FLOAT,

    ChildLockTravel_Min FLOAT,
    ChildLockTravel_Max FLOAT,
    ChildLockTravel_Actual FLOAT,

    ChildUnlockEffort_Min FLOAT,
    ChildUnlockEffort_Max FLOAT,
    ChildUnlockEffort_Actual FLOAT,

    ChildUnlockTravel_Min FLOAT,
    ChildUnlockTravel_Max FLOAT,
    ChildUnlockTravel_Actual FLOAT,

    EmgLockTorque_Min FLOAT,
    EmgLockTorque_Max FLOAT,
    EmgLockTorque_Actual FLOAT,

    EmgLockAngle_Min FLOAT,
    EmgLockAngle_Max FLOAT,
    EmgLockAngle_Actual FLOAT,

    -- Travel & Endurance Test Data
    InsideLockEffort_Min FLOAT,
    InsideLockEffort_Max FLOAT,
    InsideLockEffort_Actual FLOAT,

    InsideLockTravel_Min FLOAT,
    InsideLockTravel_Max FLOAT,
    InsideLockTravel_Actual FLOAT,

    InsideUnlockEffort_Min FLOAT,
    InsideUnlockEffort_Max FLOAT,
    InsideUnlockEffort_Actual FLOAT,

    InsideUnlockTravel_Min FLOAT,
    InsideUnlockTravel_Max FLOAT,
    InsideUnlockTravel_Actual FLOAT,

    InsideReleaseEffort_Min FLOAT,
    InsideReleaseEffort_Max FLOAT,
    InsideReleaseEffort_Actual FLOAT,

    InsideReleasePreTravel_Min FLOAT,
    InsideReleasePreTravel_Max FLOAT,
    InsideReleasePreTravel_Actual FLOAT,

    InsideReleaseReleaseTravel_Min FLOAT,
    InsideReleaseReleaseTravel_Max FLOAT,
    InsideReleaseReleaseTravel_Actual FLOAT,

    InsideReleaseFullTravel_Min FLOAT,
    InsideReleaseFullTravel_Max FLOAT,
    InsideReleaseFullTravel_Actual FLOAT,

    OutsideReleaseEffort_Min FLOAT,
    OutsideReleaseEffort_Max FLOAT,
    OutsideReleaseEffort_Actual FLOAT,

    OutsideReleasePreTravel_Min FLOAT,
    OutsideReleasePreTravel_Max FLOAT,
    OutsideReleasePreTravel_Actual FLOAT,

    OutsideReleaseReleaseTravel_Min FLOAT,
    OutsideReleaseReleaseTravel_Max FLOAT,
    OutsideReleaseReleaseTravel_Actual FLOAT,

    OutsideReleaseFullTravel_Min FLOAT,
    OutsideReleaseFullTravel_Max FLOAT,
    OutsideReleaseFullTravel_Actual FLOAT
);
GO