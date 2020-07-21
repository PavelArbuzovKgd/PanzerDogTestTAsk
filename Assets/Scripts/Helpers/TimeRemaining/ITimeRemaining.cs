using System;


public interface ITimeRemaining
{
    #region Properties

    Action Method { get; }
    bool IsRepeating { get; }
    float Time { get; }
    float CurrentTime { get; set; }

    #endregion
}

