namespace JunimoServer.Services.HostAutomation
{
    public abstract class Activity
    {
        private bool _enabled = false;
        private readonly int _everyXTicks;
        private int _ticksToWaitRemaining = 0;

        public Activity(int everyXTicks = 1)
        {
            _everyXTicks = everyXTicks;
        }

        protected virtual void OnTick() { }

        protected virtual void OnEnabled() { }

        protected virtual void OnDisabled() { }

        protected virtual void OnDayStart() { }

        public void HandleDayStart()
        {
            if (!_enabled)
            {
                return;
            }

            OnDayStart();
        }

        public void HandleTick()
        {
            if (!_enabled)
            {
                return;
            }

            if (_ticksToWaitRemaining <= 0)
            {
                OnTick();
                _ticksToWaitRemaining += _everyXTicks;
            }

            _ticksToWaitRemaining--;
        }

        public void Enable()
        {
            if (_enabled)
            {
                return;
            }

            _enabled = true;
            OnEnabled();
        }

        public void Disable()
        {
            if (!_enabled)
            {
                return;
            }

            _enabled = false;
            OnDisabled();
        }

        public void PauseForNumTicks(int numTicks)
        {
            _ticksToWaitRemaining = numTicks;
        }
    }
}
