// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ViewModelValidationAttribute.cs
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
    using Common.Utils;
    using SharedUtils.Utils;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public enum InputTypes
    {
        TextInput,
        StateInput,
        MonthInput,
        ExpirationYearInput,
        DateTimeInput,
        NullableDateTimeInput,
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

    public interface ICommonEditorValidationProps
    {
        string ExcludedChars { get; set; }
        int MaxLength { get; set; }
        int MinLength { get; set; }
        int TextMustChange { get; set; }
        ValidationTypes ValidationType { get; set; }
    }

    public interface ICommonEntryValidationProps : ICommonEditorValidationProps
    {
        int DoNotForceMaskInitially { get; set; }
        string Mask { get; set; }
        string StringFormat { get; set; }
    }

    public interface ICommonPasswordProps
    {
        int MaxCharacterCount { get; set; }
        int MaxRepeatChars { get; set; }
        int MinCapitalCharacterCount { get; set; }
        int MinCharacterCount { get; set; }
        int MinLowCaseCharacterCount { get; set; }
        int MinNumericCharacterCount { get; set; }
        int MinSpecialCharacterCount { get; set; }
    }

    public interface ICommonValidationProps : ICommonEntryValidationProps
    {
    }

    public interface IHaveHexColors
    {
        string BorderViewBorderColorHex { get; set; }
        string InstructionsTextColorHex { get; set; }
        string InvalidTextColorHex { get; set; }
        string PlaceholderBackColorHex { get; set; }
        string PlaceholderTextColorHex { get; set; }
        string ValidTextColorHex { get; set; }
    }

    public interface IHaveMinAndMaxNumbers
    {
        int CharsToRightOfDecimal { get; set; }
        double MaxDecimalNumber { get; set; }
        double MinDecimalNumber { get; set; }
        NumericTypes NumericType { get; set; }
    }

    public interface IViewModelValidationAttribute :
       IViewModelValidationAttributeBase,
       IHaveHexColors,
       ICommonValidationProps,
       IHaveMinAndMaxNumbers,
       ICommonPasswordProps
    {
        int CanUnmaskPassword { get; set; }
        int DisplayOrder { get; set; }
        double EditableEntryMarginBottom { get; set; }
        double EditableEntryMarginLeft { get; set; }
        double EditableEntryMarginRight { get; set; }
        double EditableEntryMarginTop { get; set; }

        // TODO Not used currently
        double FixedColumnWidth { get; set; }

        InputTypes InputType { get; set; }
        int IsPassword { get; set; }
        string KeyboardName { get; set; }

        // TODO Not used currently
        TextAlignment LabelTextAlignment { get; set; }

        // TODO Not used currently
        LineBreakMode TextWrapMode { get; set; }

        Type ValidatorType { get; set; }
    }

    public interface IViewModelValidationAttributeBase
    {
        double BorderViewBorderWidth { get; set; }
        double BorderViewCornerRadiusFactor { get; set; }
        double BorderViewHeight { get; set; }
        BindingMode BoundMode { get; set; }
        string FontFamilyOverride { get; set; }
        double GridSinglePadding { get; set; }
        FontAttributes InstructionsFontAttributes { get; set; }
        double InstructionsHeight { get; set; }
        double InstructionsLabelFontSizeFactor { get; set; }
        string InstructionsText { get; set; }
        FontAttributes InvalidFontAttributes { get; set; }
        double PlaceholderHeight { get; set; }
        double PlaceholderInset { get; set; }
        double PlaceholderLabelFontSizeFactor { get; set; }
        double PlaceholderLabelSideMargin { get; set; }
        string PlaceholderText { get; set; }
        double PlaceholderTopMarginAdjustment { get; set; }
        int ShowInstructions { get; set; }
        int ShowValidationErrors { get; set; }
        int ShowValidationErrorsWithInstructions { get; set; }
        double ValidationFontSizeFactor { get; set; }
        FontAttributes ValidFontAttributes { get; set; }
        string ViewModelPropertyName { get; set; }
    }

    public static class ViewModelValidationAttribute_Static
    {
        public const int FALSE_BOOL = 0;
        public const int TRUE_BOOL = 1;
        public const int UNSET_BOOL = -1;
        public const double UNSET_DOUBLE = -100.0d;
        public const int UNSET_INT = -100;

        public const string UNSET_STRING =
           "32r08932fuhvoiwgfpo'iwgpo34[098tr32 9io3g4gop43megfwv-09k=j31T2Q-03FmpkcEQWPLmFvew poiBVWERNI'JRewbpogfvEWPOKFjewp[o";

        public static readonly string[] PROPERTIES_REQUIRING_OS_ADJUSTMENT =
        {
         nameof(IViewModelValidationAttribute.EditableEntryMarginBottom),
         nameof(IViewModelValidationAttribute.EditableEntryMarginLeft),
         nameof(IViewModelValidationAttribute.EditableEntryMarginRight),
         nameof(IViewModelValidationAttribute.EditableEntryMarginTop),
         nameof(IViewModelValidationAttribute.FixedColumnWidth),
         nameof(IViewModelValidationAttribute.BorderViewBorderWidth),
         nameof(IViewModelValidationAttribute.BorderViewHeight),
         nameof(IViewModelValidationAttribute.GridSinglePadding),
         nameof(IViewModelValidationAttribute.InstructionsHeight),
         nameof(IViewModelValidationAttribute.PlaceholderHeight),
         nameof(IViewModelValidationAttribute.PlaceholderInset),
         nameof(IViewModelValidationAttribute.PlaceholderLabelSideMargin),
         nameof(IViewModelValidationAttribute.PlaceholderTopMarginAdjustment)
      };

        private const string HEX_SUFFIX = "Hex";

        public static void CopyCommonAttributeValuesUsingAttribute<ViewT, AttributeT>(
         this ViewT toView,
         AttributeT attribute
      )
        {
            // 1. Coerce and copy the hex colors
            // 2. Copy properties *if* they are set, *else ignore*
            toView.CoerceSettablePropertyValuesFrom(
                attribute,
                (info, sourceViewModel) =>
                {
                    // Source properties contain Hex colors as strings
                    if (info.Name.EndsWith(HEX_SUFFIX))
                    {
                        // Remove the "Hex" suffix
                        var retPropName =
                           info.Name.Substring(0, info.Name.Length - HEX_SUFFIX.Length);

                        // Get the string representation of the color
                        var interimPropValue = info.GetValue(sourceViewModel);

                        // Convert to a Xamarin.Forms.Color
                        var success = interimPropValue.ToString()
                                                      .CanBeParsedFromHex(out
                                                                          var retColor);

                        return (success, retPropName, retColor);
                    }
                    else
                    {
                        // If this property contains a "set" value according to the rules
                        // (see ViewModelValidationAttribute_Static), copy the value -
                        // else ignore.

                        // NO change in the property name
                        var retPropName = info.Name;
                        var retPropValue = info.GetValue(sourceViewModel);
                        var skipIt = false;

                        if (retPropValue is int retPropValueAsInt)
                        {
                            skipIt = retPropValueAsInt.IsUnset();
                        }
                        else if (retPropValue is double retPropValueAsDouble)
                        {
                            skipIt = retPropValueAsDouble.IsUnset();

                            // Corner case for certain doubles: the attribute value is
                            // primitive, so cannot be adjusted on the attribute itself.
                            if (!skipIt &&
                                PROPERTIES_REQUIRING_OS_ADJUSTMENT.Contains(retPropName))
                            {
                                retPropValue = retPropValueAsDouble.AdjustForOsAndDevice();
                            }
                        }
                        else if (retPropValue is string retPropValueAsString)
                        {
                            skipIt = retPropValueAsString.IsUnset();
                        }

                        return (!skipIt, retPropName, retPropValue);
                    }
                }
               );
        }

        public static double GetUSetValueOrDefault(this double d)
        {
            return d.IsUnset() ? default : d;
        }

        public static bool IsFalse(this int i)
        {
            return i == FALSE_BOOL;
        }

        public static bool IsTrue(this int i)
        {
            return i == TRUE_BOOL;
        }

        public static bool IsUnset(this int i)
        {
            return i == UNSET_INT;
        }

        public static bool IsUnset(this double d)
        {
            return d.IsSameAs(UNSET_DOUBLE);
        }

        public static bool IsUnset(this string s)
        {
            return s.IsSameAs(UNSET_STRING);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ViewModelValidationAttribute : Attribute, IViewModelValidationAttribute
    {
        public ViewModelValidationAttribute
        (
           // IMPORTANT
           [CallerMemberName] string viewModelPropertyName = "",
           BindingMode boundMode = BindingMode.OneWay,

           // IMPORTANT
           string borderViewBorderColorHex = "",
           double borderViewBorderWidth = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double borderViewCornerRadiusFactor = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double borderViewHeight = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           int canUnmaskPassword = ViewModelValidationAttribute_Static.UNSET_INT,
           int charsToRightOfDecimal = ViewModelValidationAttribute_Static.UNSET_INT,
           int displayOrder = ViewModelValidationAttribute_Static.UNSET_INT,
           int doNotForceMaskInitially = ViewModelValidationAttribute_Static.UNSET_INT,
           double editableEntryMarginBottom = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double editableEntryMarginLeft = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double editableEntryMarginRight = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double editableEntryMarginTop = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           string excludedChars = ViewModelValidationAttribute_Static.UNSET_STRING,
           double fixedColumnWidth = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           string fontFamilyOverride = ViewModelValidationAttribute_Static.UNSET_STRING,
           double gridSinglePadding = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           InputTypes inputType = InputTypes.TextInput,
           FontAttributes instructionsFontAttributes = default,
           double instructionsLabelFontSizeFactor = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double instructionsHeight = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           string instructionsText = ViewModelValidationAttribute_Static.UNSET_STRING,

           // IMPORTANT
           string instructionsTextColorHex = "",
           FontAttributes invalidFontAttributes = default,

           // IMPORTANT
           string invalidTextColorHex = "",
           int isPassword = ViewModelValidationAttribute_Static.UNSET_BOOL,
           string keyboardName = ViewModelValidationAttribute_Static.UNSET_STRING,
           TextAlignment labelTextAlignment = TextAlignment.Start,

           // IMPORTANT
           string placeholderBackColorHex = "",
           LineBreakMode textWrapMode = LineBreakMode.NoWrap,
           string mask = ViewModelValidationAttribute_Static.UNSET_STRING,
           int maxCharacterCount = ViewModelValidationAttribute_Static.UNSET_INT,
           int maxLength = ViewModelValidationAttribute_Static.UNSET_INT,
           double maxNumber = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           int maxRepeatChars = ViewModelValidationAttribute_Static.UNSET_INT,
           int minCapitalCharacterCount = ViewModelValidationAttribute_Static.UNSET_INT,
           int minCharacterCount = ViewModelValidationAttribute_Static.UNSET_INT,
           int minNumericCharacterCount = ViewModelValidationAttribute_Static.UNSET_INT,
           int minLength = ViewModelValidationAttribute_Static.UNSET_INT,
           int minLowCaseCharacterCount = ViewModelValidationAttribute_Static.UNSET_INT,
           double minNumber = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           int minSpecialCharacterCount = ViewModelValidationAttribute_Static.UNSET_INT,
           NumericTypes numericType = NumericTypes.NoNumericType,
           double placeholderHeight = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double placeholderLabelFontSizeFactor = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double placeholderLabelSideMargin = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           double placeholderInset = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           string placeholderText = ViewModelValidationAttribute_Static.UNSET_STRING,

           // IMPORTANT
           string placeholderTextColorHex = "",
           double placeholderTopMarginAdjustment = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           int showInstructions = ViewModelValidationAttribute_Static.UNSET_INT,
           int showValidationErrors = ViewModelValidationAttribute_Static.UNSET_INT,
           int showValidationErrorsWithInstructions = ViewModelValidationAttribute_Static.UNSET_INT,

           // IMPORTANT
           string stringFormat = "",
           int textMustChange = ViewModelValidationAttribute_Static.UNSET_INT,
           double validationFontSizeFactor = ViewModelValidationAttribute_Static.UNSET_DOUBLE,
           ValidationTypes validationType = ValidationTypes.AnyText,
           Type validatorType = default,
           FontAttributes validFontAttributes = default,

           // IMPORTANT
           string validTextColorHex = ""
        )
        {
            BorderViewBorderColorHex = borderViewBorderColorHex;
            BorderViewBorderWidth = borderViewBorderWidth;
            BorderViewCornerRadiusFactor = borderViewCornerRadiusFactor;
            BorderViewHeight = borderViewHeight;
            BoundMode = boundMode;
            CanUnmaskPassword = canUnmaskPassword;
            CharsToRightOfDecimal = charsToRightOfDecimal;
            DisplayOrder = displayOrder;
            DoNotForceMaskInitially = doNotForceMaskInitially;
            EditableEntryMarginBottom = editableEntryMarginBottom;
            EditableEntryMarginLeft = editableEntryMarginLeft;
            EditableEntryMarginRight = editableEntryMarginRight;
            EditableEntryMarginTop = editableEntryMarginTop;
            ExcludedChars = excludedChars;
            FixedColumnWidth = fixedColumnWidth;
            FontFamilyOverride = fontFamilyOverride;
            GridSinglePadding = gridSinglePadding;
            InputType = inputType;
            InstructionsFontAttributes = instructionsFontAttributes;
            InstructionsHeight = instructionsHeight;
            InstructionsLabelFontSizeFactor = instructionsLabelFontSizeFactor;
            InstructionsText = instructionsText;
            InstructionsTextColorHex = instructionsTextColorHex;
            InvalidFontAttributes = invalidFontAttributes;
            InvalidTextColorHex = invalidTextColorHex;
            IsPassword = isPassword;
            KeyboardName = keyboardName;
            LabelTextAlignment = labelTextAlignment;
            Mask = mask;
            MaxCharacterCount = maxCharacterCount;
            MaxDecimalNumber = maxNumber;
            MaxLength = maxLength;
            MaxRepeatChars = maxRepeatChars;
            MinCapitalCharacterCount = minCapitalCharacterCount;
            MinCharacterCount = minCharacterCount;
            MinDecimalNumber = minNumber;
            MinLength = minLength;
            MinLowCaseCharacterCount = minLowCaseCharacterCount;
            MinNumericCharacterCount = minNumericCharacterCount;
            MinSpecialCharacterCount = minSpecialCharacterCount;
            NumericType = numericType;
            PlaceholderBackColorHex = placeholderBackColorHex;
            PlaceholderHeight = placeholderHeight;
            PlaceholderInset = placeholderInset;
            PlaceholderLabelFontSizeFactor = placeholderLabelFontSizeFactor;
            PlaceholderLabelSideMargin = placeholderLabelSideMargin;
            PlaceholderText = placeholderText;
            PlaceholderTextColorHex = placeholderTextColorHex;
            PlaceholderTopMarginAdjustment = placeholderTopMarginAdjustment;
            ShowInstructions = showInstructions;
            ShowValidationErrors = showValidationErrors;
            ShowValidationErrorsWithInstructions = showValidationErrorsWithInstructions;
            StringFormat = stringFormat;
            TextMustChange = textMustChange;
            TextWrapMode = textWrapMode;
            ValidationFontSizeFactor = validationFontSizeFactor;
            ValidFontAttributes = validFontAttributes;
            ValidTextColorHex = validTextColorHex;
            ValidationType = validationType;
            ValidatorType = validatorType;
            ViewModelPropertyName = viewModelPropertyName;
        }

        public string BorderViewBorderColorHex { get; set; }
        public double BorderViewBorderWidth { get; set; }
        public double BorderViewCornerRadiusFactor { get; set; }
        public double BorderViewHeight { get; set; }
        public BindingMode BoundMode { get; set; }
        public int CanUnmaskPassword { get; set; }
        public int CharsToRightOfDecimal { get; set; }
        public int DisplayOrder { get; set; }
        public int DoNotForceMaskInitially { get; set; }
        public double EditableEntryMarginBottom { get; set; }
        public double EditableEntryMarginLeft { get; set; }
        public double EditableEntryMarginRight { get; set; }
        public double EditableEntryMarginTop { get; set; }
        public string ExcludedChars { get; set; }
        public double FixedColumnWidth { get; set; }
        public string FontFamilyOverride { get; set; }
        public double GridSinglePadding { get; set; }
        public InputTypes InputType { get; set; }
        public FontAttributes InstructionsFontAttributes { get; set; }
        public double InstructionsHeight { get; set; }
        public double InstructionsLabelFontSizeFactor { get; set; }
        public string InstructionsText { get; set; }
        public string InstructionsTextColorHex { get; set; }
        public FontAttributes InvalidFontAttributes { get; set; }
        public string InvalidTextColorHex { get; set; }
        public int IsPassword { get; set; }
        public string KeyboardName { get; set; }
        public TextAlignment LabelTextAlignment { get; set; }
        public string Mask { get; set; }
        public int MaxCharacterCount { get; set; }
        public double MaxDecimalNumber { get; set; }
        public int MaxLength { get; set; }
        public int MaxRepeatChars { get; set; }
        public int MinCapitalCharacterCount { get; set; }
        public int MinCharacterCount { get; set; }
        public double MinDecimalNumber { get; set; }
        public int MinLength { get; set; }
        public int MinLowCaseCharacterCount { get; set; }
        public int MinNumericCharacterCount { get; set; }
        public int MinSpecialCharacterCount { get; set; }
        public NumericTypes NumericType { get; set; }
        public string PlaceholderBackColorHex { get; set; }
        public double PlaceholderHeight { get; set; }
        public double PlaceholderInset { get; set; }
        public double PlaceholderLabelFontSizeFactor { get; set; }
        public double PlaceholderLabelSideMargin { get; set; }
        public string PlaceholderText { get; set; }
        public string PlaceholderTextColorHex { get; set; }
        public double PlaceholderTopMarginAdjustment { get; set; }
        public int ShowInstructions { get; set; }
        public int ShowValidationErrors { get; set; }
        public int ShowValidationErrorsWithInstructions { get; set; }
        public string StringFormat { get; set; }
        public int TextMustChange { get; set; }
        public LineBreakMode TextWrapMode { get; set; }
        public double ValidationFontSizeFactor { get; set; }
        public ValidationTypes ValidationType { get; set; }
        public Type ValidatorType { get; set; }
        public FontAttributes ValidFontAttributes { get; set; }
        public string ValidTextColorHex { get; set; }
        public string ViewModelPropertyName { get; set; }
    }
}
