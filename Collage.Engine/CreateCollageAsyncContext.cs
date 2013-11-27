namespace Collage.Engine
{
    internal class CreateCollageAsyncContext
    {
        private readonly object _sync = new object();
        private bool _isCancelling;

        public bool IsCancelling
        {
            get { lock (_sync) { return _isCancelling; } }
        }

        public void Cancel()
        {
            lock (_sync) { _isCancelling = true; }
        }
    }
}
