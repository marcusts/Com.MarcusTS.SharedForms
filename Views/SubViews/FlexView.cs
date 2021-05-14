// #define SHOW_BACK_COLOR

namespace Com.MarcusTS.SharedForms.Views.SubViews
{
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;
   using System.Threading.Tasks;
   using Common.Interfaces;
   using Common.Utils;
   using Controls;
   using SharedUtils.Utils;
   using ViewModels;
   using Xamarin.Forms;

   // No IStackLayout available
   public interface IFlexView
   {
      double               FontSize     { get; set; }
      double               ItemHeight   { get; set; }
      double               ItemWidth    { get; set; }
      IAnimatedStackLayout MasterLayout { get; }
      double               StackPadding { get; set; }
      double               StackSpacing { get; set; }
   }

   /// <summary>
   ///    A view that inflates a view model into a scrollable list based on custom attributes.
   ///    The "list" is actually a stack MasterLayout for easier management and access.
   ///    This is not intended for large collections.
   /// </summary>
   public abstract class FlexView : ContentView, IFlexView
   {
      public static readonly    double DEFAULT_ENTRY_FONT_SIZE = NamedSize.Small.AdjustForOsAndDevice(1.1);
      public static readonly    double DEFAULT_ENTRY_HEIGHT    = 45.0.AdjustForOsAndDevice();
      public static readonly    double DEFAULT_ENTRY_WIDTH     = 275.00.AdjustForOsAndDevice();
      protected static readonly double BUTTON_HEIGHT           = 35.0.AdjustForOsAndDevice();
      protected static readonly double BUTTON_RADIUS           = 3.5.AdjustForOsAndDevice();
      protected static readonly double BUTTON_WIDTH            = 175.0.AdjustForOsAndDevice();

      // This is an adjustment for short devices
      private static readonly double DEFAULT_VIEW_LAYOUT_PADDING = 15.0.AdjustForOsAndDevice();

      private static readonly double                         DEFAULT_VIEW_LAYOUT_SPACING = 15.0.AdjustForOsAndDevice();
      private readonly        IList<ICanBeValid>             _allBehaviors               = new List<ICanBeValid>();
      private                 int                            _nextTabIndex;
      private                 double                         _stackPadding = DEFAULT_VIEW_LAYOUT_PADDING;
      private                 double                         _stackSpacing = DEFAULT_VIEW_LAYOUT_SPACING;
      private                 IHaveValidationViewModelHelper _viewModelAsValidator;

      protected FlexView(bool useScrollView = true)
      {
         IsClippedToBounds = true;
         BackgroundColor   = Color.Transparent;

         if (useScrollView)
         {
            var scroller = FormsUtils.GetExpandingScrollView();
            scroller.BindingContext = BindingContext;
            scroller.Content        = MasterLayoutAsView;

            //scroller.VerticalOptions   = LayoutOptions.Start;
            //scroller.HorizontalOptions = LayoutOptions.FillAndExpand;

#if SHOW_BACK_COLOR
            scroller.BackgroundColor = Color.Cyan;
            BackgroundColor          = Color.Orange;
#endif

            Content = scroller;
         }
         else
         {
            Content = MasterLayoutAsView;
         }

         MasterLayoutAsView.BindingContext = BindingContext;

         //VerticalOptions   = LayoutOptions.Start;
         //HorizontalOptions = LayoutOptions.FillAndExpand;

         //Content.VerticalOptions   = LayoutOptions.Start;
         //Content.HorizontalOptions = LayoutOptions.FillAndExpand;
      }

      // Convenience only
      protected AnimatedStackLayout MasterLayoutAsView => MasterLayout as AnimatedStackLayout;

      public double FontSize { get; set; } = DEFAULT_ENTRY_FONT_SIZE;

      public double ItemHeight { get; set; } = DEFAULT_ENTRY_HEIGHT;

      public double ItemWidth { get; set; } = DEFAULT_ENTRY_WIDTH;

      public IAnimatedStackLayout MasterLayout { get; } = new AnimatedStackLayout();

      public double StackPadding
      {
         get => _stackPadding;
         set
         {
            _stackPadding              = value;
            MasterLayoutAsView.Padding = StackSpacing;
            OnPropertyChanged();
         }
      }

      public double StackSpacing
      {
         get => _stackSpacing;
         set
         {
            _stackSpacing              = value;
            MasterLayoutAsView.Spacing = StackSpacing;
            OnPropertyChanged();
         }
      }

      protected virtual Task AfterSourceViewsLoaded()
      {
         return Task.CompletedTask;
      }

      protected ITriStateImageLabelButton CreateButton(string text, Command command, int tabIndex, bool allowDisable,
         Color?                                             textColor = default, double? fontSize = null, Color? backColor = default)
      {
         ITriStateImageLabelButton button = 
            SimpleImageLabelButton.CreateSimpleImageLabelButton
            (
               text,
               textColor ?? Color.White,
               fontSize ?? NamedSize.Small.AdjustForOsAndDevice(),
               BUTTON_WIDTH,
               BUTTON_HEIGHT,
               BindingContext,
               backColor ?? ThemeUtils.DARK_THEME_COLOR,
               default,
               HorizontalOptions = LayoutOptions.Center,
               VerticalOptions = LayoutOptions.Center
            );

         button.ButtonCommand           = command;
         button.ButtonCornerRadiusFixed = BUTTON_RADIUS;
         ((View) button).IsTabStop      = true;
         ((View) button).TabIndex       = tabIndex;
         button.CanSelect               = false;

         if (allowDisable)
         {
            button.CanDisable          = true;
            button.ButtonDisabledStyle = ImageLabelButtonBase.CreateButtonStyle(Color.DarkGray);
            button.ButtonDeselectedStyle =
               ImageLabelButtonBase.CreateButtonStyle(backColor ?? ThemeUtils.DARK_THEME_COLOR);
         }

         return button;
      }

      protected virtual (View, ICanBeValid) CreateEditableEntry
      (
         PropertyInfo                  propInfo,
         IViewModelValidationAttribute attribute,
         int?                          allBehaviorsCount)
      {
         var result =
            FormsUtils.CreateValidatableEditorsForAttribute
            (
               _viewModelAsValidator,
               attribute,
               ItemHeight,
               ItemWidth,
               FontSize,
               _nextTabIndex
            );

         return (result.Item1, result.Item2);
      }

      protected override void OnBindingContextChanged()
      {
         base.OnBindingContextChanged();

         MasterLayoutAsView.SourceViews.Clear();
         MasterLayoutAsView.Children.Clear();
         _allBehaviors.Clear();

         if (BindingContext.IsNullOrDefault())
         {
            return;
         }

         // ELSE

         _viewModelAsValidator = BindingContext as IHaveValidationViewModelHelper;

         if (_viewModelAsValidator.IsNullOrDefault())
         {
            /*
                       way too aggressive -- binding context can be temporarily set to some odd view model, ad then re-assigned.
            
                     ErrorUtils.ThrowArgumentError(nameof(FlexView) + ": " + nameof(OnBindingContextChanged) +
                        ": The binding context must implement " +
                        nameof(IHaveValidationViewModelHelper) + ".");
                     */
            
            return;
         }
         
         // ELSE

         // Get the custom attributes from the view model
         var propInfoDict =
            BindingContext.GetType().CreateViewModelValidationPropertiesDict();

         var retViews = new List<View>();

         _nextTabIndex = 0;

         foreach (var keyValuePair in propInfoDict.OrderBy(kvp => kvp.Value.DisplayOrder))
         {
            var result =
               CreateEditableEntry(keyValuePair.Key, keyValuePair.Value, _allBehaviors?.Count);

            var view          = result.Item1;
            var viewValidator = result.Item2;

            if (view.IsNotNullOrDefault())
            {
               view.BindingContext = BindingContext;

               // HACK around broken binding
               if (view is IValidatableDateTime viewAsValidatableDateTime && BindingContext is IHandleNullableDateTimeChanges bindingContextAsNullableDateTimeHandler)
               {
                  viewAsValidatableDateTime.NullableResultChanged +=
                     bindingContextAsNullableDateTimeHandler.HandleNullableResultChanged;
               }
               
               retViews.Add(view);

               if (viewValidator.IsNotNullOrDefault())
               {
                  _allBehaviors?.Add(viewValidator);
               }
               else
               {
                  // The validator does not apply to optional fields OR boolean-style check boxes. So just issuing a
                  // warning.
                  Debug.WriteLine(nameof(FlexView) + ": " + nameof(OnBindingContextChanged) +
                     ": failed to create a validator for property ->" +
                     keyValuePair.Key.Name + "<-");
               }
            }
            else
            {
               Debug.WriteLine(nameof(FlexView) + ": " + nameof(OnBindingContextChanged) +
                  ": failed to create a view for property ->" +
                  keyValuePair.Key.Name + "<-");
            }
         }

         MasterLayout.SourceViews = retViews;

         // Critical to bubble validations up to the view model and commands
         _viewModelAsValidator?.ValidationHelper.AddBehaviors(_allBehaviors.ToArray());

         TaskHelper.RunParallel(AfterSourceViewsLoaded(), actionCallback: MasterLayout.AnimateIn);
      }
   }
}