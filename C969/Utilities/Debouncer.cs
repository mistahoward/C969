using System;
using System.Timers;
public class Debouncer
{
    private readonly Timer timer;
    private readonly Action methodToExecute;

    /// <summary>
    /// A class that allows a method to be debounced, meaning it will only execute after a certain amount of time has passed without the method being called again
    /// </summary>
    /// <param name="interval">The amount of time, in milliseconds, to wait before executing the debounced method</param>
    /// <param name="workingMethod">The method to be debounced</param>
    public Debouncer(int interval, Action workingMethod)
    {
        methodToExecute = workingMethod;
        timer = new Timer(interval);
        timer.Elapsed += TimerElapsed;
    }

    /// <summary>
    /// Handles the Elapsed event of the Timer object, stopping the timer and invoking the debounced method
    /// </summary>
    /// <param name="sender">The Timer object that raised the event</param>
    /// <param name="e">The ElapsedEventArgs object that contains event data</param>
    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        timer.Stop();
        methodToExecute?.Invoke();
    }

    ///<summary>
    ///Restarts the timer preventing the execution of the debounced method
    ///</summary>
    public void Debounce()
    {
        timer.Stop();
        timer.Start();
    }
}
