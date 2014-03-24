using System;
using System.Diagnostics;
using System.ServiceModel;

namespace LosPollosHermanos.Infrastructure
{
    public class Proxy<TService> : IDisposable
    {
        private readonly ChannelFactory<TService> _factory;
        private readonly TService _channel;
        private bool _disposed = false;

        public Proxy(string endPointName)
        {
            this._factory = new ChannelFactory<TService>(endPointName);
            this._channel = this._factory.CreateChannel();
        }

        public TResult Call<TResult>(Func<TService, TResult> action)
        {
            try
            {
                return action(this._channel);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error: {0}", ex.Message);
                throw;
            }
        }

        public void Call(Action<TService> action)
        {
            try
            {
                action(this._channel);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error: {0}", ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    ((IDisposable)this._channel).Dispose();
                    this._factory.Close();
                    this._disposed = true;
                }
            }
        }
    }
}
