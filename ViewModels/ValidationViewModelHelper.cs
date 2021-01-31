#define FORCE_REVALIDATION

namespace Com.MarcusTS.SharedForms.ViewModels
{
   using System.Collections.Generic;
   using System.Linq;
   using Common.Behaviors;
   using Common.Interfaces;
   using SharedUtils.Utils;

   public interface IHaveValidationViewModelHelper
   {
      IValidationViewModelHelper ValidationHelper { get; set; }
   }

   public interface IValidationViewModelHelper : IPropertyChangedBase
   {
      bool MultipleSubHelpersMustAllValidateTrue { get; set; }
      bool ValidatesTrueWhenEmpty { get; set; }
      event EventUtils.GenericDelegate<bool> PageIsValidChanged;
      bool AnyPropertyValueHasChanged { get; }
      bool PageIsValid { get; }
      bool ProceedWithoutPropertyChanges { get; }
      void AddBehaviors(ICanBeValid[] behaviors);
      void AddSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers);
      void RemoveSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers);
      void KillBehaviors();
      void RevalidateBehaviors(bool forceAll);
      void RevalidateBehaviors();
      bool PageIsNeverValid { get; set; }
      ICanBeValid[] GetBehaviors();
   }

   public class ValidationViewModelHelper : PropertyChangedBase, IValidationViewModelHelper
   {
      private readonly IList<ICanBeValid> _behaviors = new List<ICanBeValid>();
      private readonly IList<IHaveValidationViewModelHelper> _subViewModelHelpers = new List<IHaveValidationViewModelHelper>();
      private volatile bool _anyPropertyValueHasChanged;
      private volatile bool _pageIsValid;
      private volatile bool _pageIsNeverValid;
      private volatile bool _revalidateBehaviorsEntered;

      public ValidationViewModelHelper()
      {
         AnyPropertyValueHasChanged = false;
         PageIsValid = ValidatesTrueWhenEmpty;
      }

      public bool MultipleSubHelpersMustAllValidateTrue { get; set; }

      public bool ValidatesTrueWhenEmpty { get; set; }

      public event EventUtils.GenericDelegate<bool> PageIsValidChanged;

      public bool AnyPropertyValueHasChanged
      {
         get => _anyPropertyValueHasChanged;

         private set
         {
            _anyPropertyValueHasChanged = value;
            OnAnyPropertyValueHasChanged();
         }
      }

      public bool PageIsValid
      {
         get => _pageIsValid;
         protected set
         {
            if (_pageIsValid != value && !PageIsNeverValid)
            {
               _pageIsValid = value;
               PageIsValidChanged?.Invoke(_pageIsValid);
            }
         }
      }

      public bool ProceedWithoutPropertyChanges { get; protected set; }

      public void AddBehaviors(ICanBeValid[] behaviors)
      {
         foreach (var behavior in behaviors)
         {
            if (!_behaviors.Contains(behavior))
            {
               behavior.IsValidChanged += b => { RevalidateBehaviors(); };
               _behaviors.Add(behavior);
            }
         }

         RevalidateBehaviors();
      }

      public void KillBehaviors()
      {
         _behaviors.Clear();
      }

      public ICanBeValid[] GetBehaviors()
      {
         return _behaviors.ToArray();
      }

      public void RevalidateBehaviors(bool forceAll)
      {
         if (_revalidateBehaviorsEntered)
         {
            return;
         }

         _revalidateBehaviorsEntered = true;

         if (_subViewModelHelpers.IsNotEmpty())
         {
            PageIsValid =
               (
                  (ValidatesTrueWhenEmpty && _subViewModelHelpers.IsEmpty())
                ||
                  (MultipleSubHelpersMustAllValidateTrue &&
                   _subViewModelHelpers.All(h => h.ValidationHelper.PageIsValid))
                ||
                  (!MultipleSubHelpersMustAllValidateTrue &&
                   _subViewModelHelpers.Any(h => h.ValidationHelper.PageIsValid))
               );
         }
         else
         {
            if (forceAll)
            {
               // Run through all behaviors; ask to validate; respond only once at the end
               if (_behaviors.IsNotEmpty())
               {
                  foreach (var behavior in _behaviors)
                  {
                     behavior.Revalidate();
                  }
               }
            }

            PageIsValid = _behaviors.IsEmpty() || _behaviors.All(b => b.IsValid.IsTrue());
         }

         _revalidateBehaviorsEntered = false;
      }

      public void RevalidateBehaviors()
      {
         RevalidateBehaviors(
#if FORCE_REVALIDATION
                    true
#else
            false
#endif
            );
      }

      public void AddSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers)
      {
         foreach (var helper in helpers)
         {
            _subViewModelHelpers.Add(helper);
         }
      }

      public void RemoveSubViewModelHelpers(IHaveValidationViewModelHelper[] helpers)
      {
         foreach (var helper in helpers)
         {
            if (_subViewModelHelpers.Contains(helper))
            {
               _subViewModelHelpers.Remove(helper);
            }
         }
      }

      protected virtual void OnAnyPropertyValueHasChanged()
      {
      }

      public bool PageIsNeverValid
      {
         get => _pageIsNeverValid;
         set
         {
            _pageIsNeverValid = value;

            if (_pageIsNeverValid)
            {
               PageIsValid = false;
            }
         }
      }
   }
}
