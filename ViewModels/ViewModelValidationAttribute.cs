namespace Com.MarcusTS.SharedForms.ViewModels
{
   using System;
   using System.Runtime.CompilerServices;
   using Xamarin.Forms;

   public enum InputTypes
   {
      TextInput,
      StateInput,
      MonthInput,
      ExpirationYearInput,
      DateTimeInput,
      CheckBoxInput
   }

   public enum NumericTypes
   {
      NoNumericType,
      DoubleNumericType,
      NullableDoubleNumericType,
      IntNumericType,
      NullableIntNumericType,
      LongNumericType,
      NullableLongNumericType
   }

   public enum ValidationTypes
   {
      AnyText,
      WholeNumber,
      DecimalNumber
   }

   public interface ICommonValidationProps
   {
      bool            DoNotForceMaskInitially { get; set; }
      string          ExcludedChars           { get; set; }
      string          Mask                    { get; set; }
      int             MaxLength               { get; set; }
      int             MinLength               { get; set; }
      ValidationTypes ValidationType          { get; set; }
   }

   public interface IHaveMinAndMaxNumbers
   {
      double       MaxDecimalNumber { get; set; }
      double       MinDecimalNumber { get; set; }
      NumericTypes NumericType      { get; set; }
   }

   public interface IViewModelValidationAttribute : ICommonValidationProps, IHaveMinAndMaxNumbers
   {
      double        BorderViewHeight                   { get; set; }
      BindingMode   BoundMode                          { get; set; }
      bool          CanUnmaskPassword                  { get; set; }
      int           CharsToRightOfDecimal              { get; set; }
      int           DisplayOrder                       { get; set; }
      double        FixedColumnWidth                   { get; set; }
      string        FontFamilyOverride                 { get; set; }
      InputTypes    InputType                          { get; set; }
      double        InstructionsHeight                 { get; set; }
      string        InstructionsText                   { get; set; }
      bool          IsPassword                         { get; set; }
      string        KeyboardName                       { get; set; }
      TextAlignment LabelTextAlignment                 { get; set; }
      double        PlaceholderHeight                  { get; set; }
      string        PlaceholderText                    { get; set; }
      bool          ShowInstructionsOrValidations      { get; set; }
      bool          ShowValidationErrorsAsInstructions { get; set; }
      string        StringFormat                       { get; set; }
      LineBreakMode TextWrapMode                       { get; set; }
      Type          ValidatorType                      { get; set; }
      string        ViewModelPropertyName              { get; set; }
   }

   [AttributeUsage(AttributeTargets.Property)]
   public class ViewModelValidationAttribute : Attribute, IViewModelValidationAttribute
   {
      public ViewModelValidationAttribute
      (
         [CallerMemberName] string viewModelPropertyName              = "",
         BindingMode               boundMode                          = BindingMode.TwoWay,
         double                    borderViewHeight                   = default,
         bool                      canUnmaskPassword                  = true,
         int                       charsToRightOfDecimal              = 0,
         int                       displayOrder                       = 0,
         bool                      doNotForceMaskInitially            = false,
         string                    excludedChars                      = "",
         double                    fixedColumnWidth                   = default,
         string                    fontFamilyOverride                 = null,
         InputTypes                inputType                          = InputTypes.TextInput,
         double                    instructionsHeight                 = default,
         string                    instructionsText                   = "",
         bool                      isPassword                         = false,
         string                    keyboardName                       = null,
         TextAlignment             labelTextAlignment                 = TextAlignment.Start,
         LineBreakMode             textWrapMode                       = LineBreakMode.NoWrap,
         string                    mask                               = "",
         int                       maxLength                          = default,
         double                    maxNumber                          = int.MaxValue,
         int                       minLength                          = default,
         double                    minNumber                          = int.MinValue,
         NumericTypes              numericType                        = NumericTypes.NoNumericType,
         double                    placeholderHeight                  = default,
         string                    placeholderText                    = null,
         bool                      showInstructionsOrValidations      = false,
         bool                      showValidationErrorsAsInstructions = true,
         string                    stringFormat                       = "",
         ValidationTypes           validationType                     = ValidationTypes.AnyText,
         Type                      validatorType                      = default
      )
      {
         BorderViewHeight                   = borderViewHeight;
         BoundMode                          = boundMode;
         CanUnmaskPassword                  = canUnmaskPassword;
         CharsToRightOfDecimal              = charsToRightOfDecimal;
         DisplayOrder                       = displayOrder;
         DoNotForceMaskInitially            = doNotForceMaskInitially;
         ExcludedChars                      = excludedChars;
         FixedColumnWidth                   = fixedColumnWidth;
         FontFamilyOverride                 = fontFamilyOverride;
         InputType                          = inputType;
         InstructionsHeight                 = instructionsHeight;
         InstructionsText                   = instructionsText;
         IsPassword                         = isPassword;
         KeyboardName                       = keyboardName;
         LabelTextAlignment                 = labelTextAlignment;
         TextWrapMode                       = textWrapMode;
         Mask                               = mask;
         MaxLength                          = maxLength;
         MaxDecimalNumber                   = maxNumber;
         MinLength                          = minLength;
         MinDecimalNumber                   = minNumber;
         NumericType                        = numericType;
         PlaceholderHeight                  = placeholderHeight;
         PlaceholderText                    = placeholderText;
         ShowInstructionsOrValidations      = showInstructionsOrValidations;
         ShowValidationErrorsAsInstructions = showValidationErrorsAsInstructions;
         StringFormat                       = stringFormat;
         ValidationType                     = validationType;
         ValidatorType                      = validatorType;
         ViewModelPropertyName              = viewModelPropertyName;
      }

      public double          BorderViewHeight                   { get; set; }
      public BindingMode     BoundMode                          { get; set; }
      public bool            CanUnmaskPassword                  { get; set; }
      public int             CharsToRightOfDecimal              { get; set; }
      public int             DisplayOrder                       { get; set; }
      public bool            DoNotForceMaskInitially            { get; set; }
      public string             ExcludedChars                      { get; set; }
      public double          FixedColumnWidth                   { get; set; }
      public string          FontFamilyOverride                 { get; set; }
      public InputTypes      InputType                          { get; set; }
      public double          InstructionsHeight                 { get; set; }
      public string          InstructionsText                   { get; set; }
      public bool            IsPassword                         { get; set; }
      public string          KeyboardName                       { get; set; }
      public TextAlignment   LabelTextAlignment                 { get; set; }
      public string          Mask                               { get; set; }
      public double          MaxDecimalNumber                   { get; set; }
      public int             MaxLength                          { get; set; }
      public double          MinDecimalNumber                   { get; set; }
      public int             MinLength                          { get; set; }
      public NumericTypes    NumericType                        { get; set; }
      public double          PlaceholderHeight                  { get; set; }
      public string          PlaceholderText                    { get; set; }
      public bool            ShowInstructionsOrValidations      { get; set; }
      public bool            ShowValidationErrorsAsInstructions { get; set; }
      public string          StringFormat                       { get; set; }
      public LineBreakMode   TextWrapMode                       { get; set; }
      public ValidationTypes ValidationType                     { get; set; }
      public Type            ValidatorType                      { get; set; }
      public string          ViewModelPropertyName              { get; set; }
   }
}