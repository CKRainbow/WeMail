using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace WeMail.Common.ValidationRules
{
    public class ValidationErrorMappingBehavior : Behavior<FrameworkElement>
    {
        #region Fields
        private Window _hostWindow;
        #endregion

        #region Properties


        public ObservableCollection<ValidationError> ValidationErrors
        {
            get
            {
                return (ObservableCollection<ValidationError>)GetValue(ValidationErrorsProperty);
            }
            set { SetValue(ValidationErrorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValidationErrorsProperty =
            DependencyProperty.Register(
                "ValidationErrors",
                typeof(ObservableCollection<ValidationError>),
                typeof(ValidationErrorMappingBehavior),
                new PropertyMetadata(new ObservableCollection<ValidationError>())
            );

        public bool HasValidationError
        {
            get { return (bool)GetValue(HasValidationErrorProperty); }
            set { SetValue(HasValidationErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasValidationError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasValidationErrorProperty =
            DependencyProperty.Register(
                "HasValidationError",
                typeof(bool),
                typeof(ValidationErrorMappingBehavior),
                new PropertyMetadata(false)
            );

        #endregion

        #region Constructors

        public ValidationErrorMappingBehavior()
            : base() { }

        #endregion

        #region Events & Event Methods

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                if (!ValidationErrors.Contains(e.Error))
                {
                    ValidationErrors.Add(e.Error);
                }
            }
            else if (e.Action == ValidationErrorEventAction.Removed)
            {
                if (ValidationErrors.Contains(e.Error))
                {
                    ValidationErrors.Remove(e.Error);
                }
            }

            this.HasValidationError = ValidationErrors.Count > 0;
        }

        #endregion

        #region Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            // 关键改动：不在 OnAttached 中直接操作，而是订阅 Loaded 事件
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            // 找到窗口后，立即取消订阅，避免重复执行
            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;

            // 在 Loaded 事件中，可以安全地获取到 Window
            _hostWindow = Window.GetWindow(this.AssociatedObject);

            if (_hostWindow != null)
            {
                // 在这里附加错误处理器
                Validation.AddErrorHandler(_hostWindow, Validation_Error);
            }
            else
            {
                //如果在这里仍然找不到，说明有更严重的问题，但理论上不可能发生
                throw new InvalidOperationException("Could not find the host Window.");
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // 确保在 OnDetaching 时也取消订阅 Loaded 事件，防止内存泄漏
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            }

            // 使用存储的窗口引用来移除处理器
            if (_hostWindow != null)
            {
                Validation.RemoveErrorHandler(_hostWindow, Validation_Error);
            }
        }

        #endregion
    }
}
