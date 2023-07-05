using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VivaPaymentPOC
{
    public partial class MainPage : ContentPage
    {
        private readonly ICallbackHandler _callbackHandler;
        public string CallBackData { get; set; }
        public MainPage()
        {
            InitializeComponent();
             _callbackHandler =  DependencyService.Get<ICallbackHandler>();
            _callbackHandler.Callback += CallbackHandler_Callback;
            BindingContext = this;
        }

        private void CallbackHandler_Callback(string obj)
        {
            CallBackData = obj; 
            OnPropertyChanged(nameof(CallBackData));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var supportsUri = await Launcher.CanOpenAsync("vivapayclient://pay/v1");
            if (supportsUri)
                await Launcher.OpenAsync(@"vivapayclient://pay/v1
                                 ?appId=com.companyname.vivapaymentpoc
                                 &action=sale
                                 &clientTransactionId=1234567890123
                                 &amount=1200
                                 &tipAmount=0
                                 &show_receipt=false
                                 &show_transaction_result=true
                                 &show_rating=true
                                 &callback=mycallbackscheme://callback");

        }
    }
}
