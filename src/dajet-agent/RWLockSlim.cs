using System;
using System.Threading;

namespace DaJet.Agent.Service
{
    public sealed class RWLockSlim : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        public ReadLockToken ReadLock()
        {
            return new ReadLockToken(this);
        }
        public WriteLockToken WriteLock()
        {
            return new WriteLockToken(this);
        }
        public void Dispose()
        {
            _lock.Dispose();
        }
        public struct ReadLockToken : IDisposable
        {
            private readonly RWLockSlim _this;
            public ReadLockToken(in RWLockSlim @this)
            {
                _this = @this;
                _this._lock.EnterReadLock();
            }
            public void Dispose()
            {
                _this._lock.ExitReadLock();
            }
        }
        public struct WriteLockToken : IDisposable
        {
            private readonly RWLockSlim _this;
            public WriteLockToken(in RWLockSlim @this)
            {
                _this = @this;
                _this._lock.EnterWriteLock();
            }
            public void Dispose()
            {
                _this._lock.ExitWriteLock();
            }
        }
    }
}