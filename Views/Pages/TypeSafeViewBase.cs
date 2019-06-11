// *********************************************************************************
// <copyright file=TypeSafeViewBase.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
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

namespace Com.MarcusTS.SharedForms.Views.Pages
{
   using Com.MarcusTS.SharedForms.Common.Interfaces;
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface ITypeSafeViewBase
   /// </summary>
   public interface ITypeSafeViewBase
   {
   }

   /// <summary>
   ///    A base class for content views that protects the type safety of the binding context.
   ///    Implements the <see cref="Xamarin.Forms.ContentView" />
   ///    Implements the <see cref="Com.MarcusTS.SharedForms.Views.Pages.ITypeSafeViewBase" />
   /// </summary>
   /// <typeparam name="InterfaceT">The required interface for this view.</typeparam>
   /// <seealso cref="Xamarin.Forms.ContentView" />
   /// <seealso cref="Com.MarcusTS.SharedForms.Views.Pages.ITypeSafeViewBase" />
   /// <remarks>
   ///    This code is similar to that at <see cref="TypeSafePageBase{InterfaceT}" /> except it manages a
   ///    view rather than a page.
   /// </remarks>
   public abstract class TypeSafeViewBase<InterfaceT> : ContentView, ITypeSafeViewBase
      where InterfaceT : class
   {
      /// <summary>
      ///    The content relative layout
      /// </summary>
      private readonly RelativeLayout _contentRelativeLayout = FormsUtils.GetExpandingRelativeLayout();

      /// <summary>
      ///    The page event provider
      /// </summary>
      private readonly IProvidePageEvents _pageEventProvider;

      /// <summary>
      ///    Initializes a new instance of the <see cref="TypeSafeViewBase{InterfaceT}" /> class.
      /// </summary>
      /// <param name="pageEventProvider">The page event provider.</param>
      protected TypeSafeViewBase(IProvidePageEvents pageEventProvider = null)
      {
         // PageEventProvider = pageEventProvider;
         BackgroundColor = Color.Transparent;

         // Resharper doesn't like the derived methods in the constructor, but there's not much we can do about it.
         // We could move this to the page events and catch the page OnAppearing, but if our page event provider is null, that will not occur.
         // ReSharper disable once VirtualMemberCallInConstructor
#pragma warning disable CC0067 // Virtual Method Called On Constructor
         Content = ConstructView();
#pragma warning restore CC0067 // Virtual Method Called On Constructor
      }

      /// <summary>
      ///    T is normally an interface -- not a class -- but there is no such constraint available.
      /// </summary>
      /// <value>
      ///    An <see cref="T:System.Object" /> that contains the properties that will be targeted by the bound properties
      ///    that belong to this <see cref="T:Xamarin.Forms.BindableObject" />. This is a bindable property.
      /// </value>
      /// <remarks>
      ///    <block subset="none" type="note">
      ///       Typically, the runtime performance is better if
      ///       <see cref="P:Xamarin.Forms.BindableObject.BindingContext" /> is set after all calls to
      ///       <see cref="M:Xamarin.Forms.BindableObject.SetBinding(Xamarin.Forms.BindableProperty,Xamarin.Forms.BindingBase)" />
      ///       have been made.
      ///    </block>
      ///    <para>
      ///       The following example shows how to apply a BindingContext and a Binding to a Label (inherits from
      ///       BindableObject):
      ///    </para>
      ///    <example>
      ///       <code lang="csharp lang-csharp"><![CDATA[
      /// var label = new Label ();
      /// label.SetBinding (Label.TextProperty, "Name");
      /// label.BindingContext = new {Name = "John Doe", Company = "Xamarin"};
      /// Debug.WriteLine (label.Text); //prints "John Doe"
      /// ]]></code>
      ///    </example>
      /// </remarks>
      public new InterfaceT BindingContext
      {
         get => base.BindingContext as InterfaceT;
         set => base.BindingContext = value;
      }

      /*
      /// <summary>
      /// Gets or sets the page event provider.
      /// </summary>
      /// <value>The page event provider.</value>
      public IProvidePageEvents PageEventProvider
      {
         get => _pageEventProvider;
         set
         {
            _pageEventProvider = value;

            if (_pageEventProvider == null)
            {
               RemovePageProviderListeners();
            }
            else
            {
               AddPageProviderListeners();
            }
         }
      }
      */

      /// <summary>
      ///    Afters the content set.
      /// </summary>
      /// <param name="layout">The layout.</param>
      protected virtual void AfterContentSet(RelativeLayout layout)
      {
      }

      /// <summary>
      ///    Requests that the deriver create the physical view.
      /// </summary>
      /// <returns>View.</returns>
      protected virtual View ConstructView()
      {
         return new ContentView();
      }

      /// <summary>
      /// Called when [page lifecycle change].
      /// </summary>
      /// <param name="pageEvent">The page event.</param>
      //protected virtual void OnPageLifecycleChange(PageLifecycleEventsEnum pageEvent)
      //{ }

      /*
      /// <summary>
      /// Adds the page provider listeners.
      /// </summary>
      private void AddPageProviderListeners()
      {
         if (PageEventProvider == null)
         {
            return;
         }

         FormsMessengerUtils.Subscribe<PageLifecycleMessage>(this, HandlePageLifeCycleChange);
      }

      /// <summary>
      /// Handles the page life cycle change.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="args">The arguments.</param>
      private void HandlePageLifeCycleChange(object               sender,
                                             PageLifecycleMessage args)
      {
         // Must verify that the sender is our page lifecycle broadcaster; it could belong to someone else.
         var ourBroadcaster = PageEventProvider?.GetEventBroadcaster?.Invoke();

         if (args.Payload.SendingPage == null || ourBroadcaster == null ||
             !ReferenceEquals(ourBroadcaster, args.Payload.SendingPage))
         {
            return;
         }

         // Call the protected virtual method so derivers can manage the event
         OnPageLifecycleChange(args.Payload.PageEvent);
      }

      /// <summary>
      /// Removes the page provider listeners.
      /// </summary>
      private void RemovePageProviderListeners()
      {
         FormsMessengerUtils.Unsubscribe<PageLifecycleMessage>(this);
      }
      */
   }
}