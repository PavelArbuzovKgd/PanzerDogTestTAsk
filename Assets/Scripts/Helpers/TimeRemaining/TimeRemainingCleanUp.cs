using System.Collections.Generic;


public sealed class TimeRemainingCleanUp : ICleanUp
{
    #region Fields

    private readonly List<ITimeRemaining> _timeRemainings;

    #endregion


    #region ClassLifeCycles

    public TimeRemainingCleanUp()
    {
        _timeRemainings = TimeRemainingExtensions.TimeRemainings;
    }

    #endregion


    #region ICleanUp

    public void Clean()
    {
        _timeRemainings.Clear();
    }

    #endregion
}
