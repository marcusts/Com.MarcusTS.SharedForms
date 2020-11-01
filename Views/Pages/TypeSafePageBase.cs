// *********************************************************************************
// Assembly         : Com.MarcusTS.SharedForms
// Author           : Stephen Marcus (Marcus Technical Services, Inc.)
// Created          : 12-23-2018
// Last Modified On : 12-23-2018
//
// <copyright file="TypeSafePageBase.cs" company="Marcus Technical Services, Inc.">
//     Copyright @2018 Marcus Technical Services, Inc.
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

using Com.MarcusTS.SharedForms.Common.Utils;

namespace Com.MarcusTS.SharedForms.Views.Pages
{
   using Common.Notifications;
   using SharedUtils.Interfaces;
   using System;
   using System.Diagnostics;
   using Xamarin.Forms;

   /// <summary>
   /// Interface ITypeSafePageBase
   /// Implements the <see cref="Com.MarcusTS.SharedUtils.Interfaces.IProvidePageEvents" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SharedUtils.Interfaces.IProvidePageEvents" />
   /// <remarks>WARNING: .Net does not provide an IContentPage interface, so we cannot reference the page from
   /// this interface without a hard cast!</remarks>
   public interface ITypeSafePageBase : IProvidePageEvents
   { }

   /// <summary>
   /// A base class for content pages that protects the type safety of the binding context.
   /// Implements the <see cref="Xamarin.Forms.ContentPage" />
   /// Implements the <see cref="ITypeSafePageBase" />
   /// </summary>
   /// <typeparam name="InterfaceT">The required interface for this view.</typeparam>
   /// <seealso cref="Xamarin.Forms.ContentPage" />
   /// <seealso cref="ITypeSafePageBase" />
   /// <remarks>This code is similar to that at <see cref="TypeSafeViewBase{InterfaceT}" /> except it manages a
   /// page rather than a view.</remarks>
   public abstract class TypeSafePageBase<InterfaceT> : ContentPage, ITypeSafePageBase
      where InterfaceT : class
   {
      /// <summary>
      /// The content relative layout
      /// </summary>
      private readonly RelativeLayout _contentRelativeLayout = FormsUtils.GetExpandingRelativeLayout();

      /// <summary>
      /// Initializes a new instance of the <see cref="TypeSafePageBase{InterfaceT}" /> class.
      /// </summary>
      protected TypeSafePageBase()
      {
         NavigationPage.SetHasNavigationBar(this, false);
         BackgroundColor = Color.Transparent;

         ConstructTypeSafePageView();
      }

      /// <summary>
      /// InterfaceT is cast to the base to make it type-safe.
      /// NOTE: This hides the base BindingContext.
      /// </summary>
      /// <value>An <see cref="T:System.Object" /> that contains the properties that will be targeted by the bound properties that belong to this <see cref="T:Xamarin.Forms.BindableObject" />. This is a bindable property.</value>
      /// <remarks><block subset="none" type="note">Typically, the runtime performance is better if  <see cref="P:Xamarin.Forms.BindableObject.BindingContext" /> is set after all calls to <see cref="M:Xamarin.Forms.BindableObject.SetBinding(Xamarin.Forms.BindableProperty,Xamarin.Forms.BindingBase)" /> have been made.</block>
      /// <para>The following example shows how to apply a BindingContext and a Binding to a Label (inherits from BindableObject):</para>
      /// <example>
      ///  <code lang="C#"><![CDATA[
      ///var label = new Label ();
      ///label.SetBinding (Label.TextProperty, "Name");
      ///label.BindingContext = new {Name = "John Doe", Company = "Xamarin"};
      ///Debug.WriteLine (label.Text); //prints "John Doe"
      ///]]></code>
      /// </example></remarks>
      public new InterfaceT BindingContext
      {
         get => base.BindingContext as InterfaceT;
         set => base.BindingContext = value;
      }

      /// <summary>
      /// Gets the get event broadcaster.
      /// </summary>
      /// <value>The get event broadcaster.</value>
      public Func<object> GetEventBroadcaster => () => this;

      /// <summary>
      /// Afters the content set.
      /// </summary>
      /// <param name="layout">The layout.</param>
      protected virtual void AfterContentSet(RelativeLayout layout)
      { }

      /// <summary>
      /// Requests that the deriver create the physical view.
      /// </summary>
      /// <returns>View.</returns>
      protected abstract View ConstructPageView();

      /// <summary>
      /// Called when the page appears.
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnAppearing()
      {
         FormsMessengerUtils.Send(new PageLifecycleMessage(this, PageLifecycleEvents.BeforeAppearing));

         base.OnAppearing();

         FormsMessengerUtils.Send(new PageLifecycleMessage(this, PageLifecycleEvents.AfterAppearing));
      }

      /// <summary>
      /// Called when [disappearing].
      /// </summary>
      protected override void OnDisappearing()
      {
         FormsMessengerUtils.Send(new PageLifecycleMessage(this, PageLifecycleEvents.BeforeDisappearing));

         base.OnDisappearing();

         FormsMessengerUtils.Send(new PageLifecycleMessage(this, PageLifecycleEvents.AfterDisappearing));
      }

      /// <summary>
      /// We create an "is busy" view by default so it is always available. We insert the deriver's
      /// content below this is busy view.
      /// </summary>
      private void ConstructTypeSafePageView()
      {
         FormsMessengerUtils.Send(new PageLifecycleMessage(this, PageLifecycleEvents.BeforeConstructing));

         try
         {
            var derivedView = ConstructPageView();

            _contentRelativeLayout.CreateRelativeOverlay(derivedView);

            Content = _contentRelativeLayout;

            // Notify derivers of this final step
            AfterContentSet(_contentRelativeLayout);
         }
         catch (Exception ex)
         {
            Debug.WriteLine("TYPE SAFE PAGE BASE: ConstructTypeSafePageView: ERROR ->" + ex.Message + "<-");
         }
         finally
         {
            FormsMessengerUtils.Send(new PageLifecycleMessage(this, PageLifecycleEvents.AfterConstructing));
         }
      }
   }
}
