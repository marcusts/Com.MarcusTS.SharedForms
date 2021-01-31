// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=WizardViewModel.cs
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

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using System.Threading.Tasks;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   public enum Outcomes
   {
      Next,
      Cancel,
      TryAgain
   }

   public interface IWizardViewModel : ITitledViewModel, IHaveValidationViewModelHelper
   {
      Command                                  CancelCommand    { get; set; }
      string                                   CancelState      { get; set; }
      Command                                  NextCommand      { get; set; }
      string                                   NextState        { get; set; }
      Delegates.HandleOnOutcomeChangedDelegate OnOutcomeChanged { get; set; }
      Outcomes                                 Outcome          { get; }
      Delegates.FinalValidationDelegate        FinalValidation  { get; set; }

      Task SetOutcome(Outcomes outcome);
   }

   public class WizardViewModel : TitledViewModel, IWizardViewModel
   {
      public WizardViewModel()
      {
         NextCommand   = new Command(async () => await RunFinalValidation(), () => ValidationHelper.PageIsValid);
         CancelCommand = new Command(async () => await SetOutcome(Outcomes.Cancel));

         ValidationHelper.PageIsValidChanged +=
            val => { VerifyCommandCanExecute(); };

         // Safe dummy
         OnOutcomeChanged = (model, state, cancelState) => Task.CompletedTask;
      }

      public Command                                  CancelCommand    { get; set; }
      public string                                   CancelState      { get; set; }
      public Command                                  NextCommand      { get; set; }
      public string                                   NextState        { get; set; }
      public Delegates.HandleOnOutcomeChangedDelegate OnOutcomeChanged { get; set; }
      public Outcomes                                 Outcome          { get; private set; }
      public IValidationViewModelHelper               ValidationHelper { get; set; } = new ValidationViewModelHelper();
      public Delegates.FinalValidationDelegate        FinalValidation  { get; set; }

      protected void VerifyCommandCanExecute()
      {
         NextCommand.ChangeCanExecute();
      }
      
      public async Task SetOutcome(Outcomes outcome)
      {
         Outcome = outcome;

         await OnOutcomeChanged.Invoke(this, NextState, CancelState);
      }

      private async Task RunFinalValidation()
      {
         if (FinalValidation.IsNullOrDefault())
         {
            await OnOutcomeChanged.Invoke(this, NextState, CancelState);
         }
         else
         {
            await FinalValidation.Invoke(this);
         }
      }
   }
}