using System;

namespace Components.RoomsManager
{
    /// <summary>
    /// Represents a process that is happening during transition from one room to another.
    /// </summary>
    /// <typeparam name="R">Room type</typeparam>
    public interface IRoomTransition<R>
    {
        /// <summary>
        /// Running the action of transition
        /// </summary>
        /// <param name="onComplete">Make sure this action is invoked when the process is finished.</param>
        void InvokeTransitionAction(R oldRoom, R newRoom, Action onComplete);
        void ForceStop();
        bool IsInProcess { get; }

        void Pause();
        void UnPause();
    }
}

