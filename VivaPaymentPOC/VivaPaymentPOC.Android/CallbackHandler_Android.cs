using System;

//[assembly: Dependency(typeof(CallbackHandler_Android,))]
namespace VivaPaymentPOC.Droid
{
    public class CallbackHandler_Android : ICallbackHandler
    {
        public event Action<string> Callback;

        public void HandleCallback(string callbackData)
        {
            Callback?.Invoke(callbackData); 
            Console.WriteLine($"Received callback data in Android: {callbackData}");
        }
    }
}

