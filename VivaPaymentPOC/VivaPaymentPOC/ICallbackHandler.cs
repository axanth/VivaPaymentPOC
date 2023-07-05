using System;

namespace VivaPaymentPOC
{
    public interface ICallbackHandler
    {
        event Action<string> Callback;
        void HandleCallback(string callbackData);
    }
}