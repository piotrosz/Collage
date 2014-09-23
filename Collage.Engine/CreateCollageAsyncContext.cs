namespace Collage.Engine
{
    internal class CreateCollageAsyncContext
    {
        private readonly object sync = new object();
        private bool isCancelling;

        public bool IsCancelling
        {
            get { lock (this.sync) { return this.isCancelling; } }
        }

        public void Cancel()
        {
            lock (this.sync) { this.isCancelling = true; }
        }
    }
}
