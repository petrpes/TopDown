using System;

namespace Components.RoomsManager
{
    public interface IRoomTransitionHandler<R> : IRoomTransition<R>
    {
        /// <summary>
        /// Subscribe transition object
        /// </summary>
        /// <param name="onComplete">An action that will run on transition process is complete. 
        /// Bool parameter is whether transition was successfully completed</param>
        void SubscribeTransistor(IRoomTransition<R> transistor, Action<bool> onComplete);
        void UnSubscribeTransistor(IRoomTransition<R> transistor);
    }
}

