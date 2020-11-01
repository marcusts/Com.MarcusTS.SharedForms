// *********************************************************************************
// Copyright @2020 Marcus Technical Services, Inc.
// <copyright
// file=ConnectivityService.cs
// company="Marcus Technical Services, Inc.">
// </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Common.Services
{
   using Notifications;
   using Plugin.Connectivity;
   using Plugin.Connectivity.Abstractions;
   using System.Threading.Tasks;
   using Xamarin.Forms;

   public interface IConnectivityService : IFragileService
   {
   }

   public class ConnectivityService : FragileServiceBase, IConnectivityService
   {
      public override bool CanBeForcedOn => false;
      public override bool CanTurnOnFromSettings => false;
      public override bool IsAvailable => CrossConnectivity.Current.IsConnected;
      public override bool IsOn => IsAvailable && CrossConnectivity.Current.IsConnected;
      public override bool IsServiceListening => IsAvailable;
      public override string ServiceName => "Connectivity";

      public override void GoToSettings()
      {
         // Can do nothing
      }

      public override Task<bool> OfferToForceOn()
      {
         // Can do nothing
         return Task.FromResult(false);
      }

      protected override void AddListeners()
      {
         CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
      }

      protected override void RemoveListeners()
      {
         CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
      }

      protected override Task StartListeningToService()
      {
         // Do nothing
         return Task.FromResult(true);
      }

      protected override Task StopListeningToService()
      {
         // Do nothing
         return Task.FromResult(true);
      }

      private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs connectivityChangedEventArgs)
      {
         Device.BeginInvokeOnMainThread
         (
            () =>
            {
               FormsMessengerUtils.Send
               (
                  new ConnectivityChangedMessage
                  {
                     Payload = connectivityChangedEventArgs.IsConnected
                  }
               );
            }
         );
      }
   }
}
