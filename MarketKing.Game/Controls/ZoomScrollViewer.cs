using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace MarketKing.Game
{
    public class ZoomScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty ZoomProperty;
        public static readonly DependencyProperty MaxZoomProperty;
        public static readonly DependencyProperty MinZoomProperty;
        public static readonly DependencyProperty FitToScreenProperty;
        public static readonly DependencyProperty FitToScreenLevelProperty;
        public static readonly DependencyProperty ZoomToNormalSizeProperty;

        static ZoomScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(typeof(ZoomScrollViewer)));
            ZoomProperty = DependencyProperty.RegisterAttached("Zoom", typeof(double), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnZoomChanged)), new ValidateValueCallback(CheckNonNegative));
            MinZoomProperty = DependencyProperty.RegisterAttached("MinZoom", typeof(double), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(Double.NaN), new ValidateValueCallback(CheckNonNegative));
            MaxZoomProperty = DependencyProperty.RegisterAttached("MaxZoom", typeof(double), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(Double.NaN), new ValidateValueCallback(CheckNonNegative));

            ZoomToNormalSizeProperty = DependencyProperty.RegisterAttached("ZoomToNormalSize", typeof(bool), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnZoomToNormalSizeChanged)));
            FitToScreenProperty = DependencyProperty.RegisterAttached("FitToScreen", typeof(bool), typeof(ZoomScrollViewer), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnFitToScreenChanged)));

            FitToScreenLevelProperty = DependencyProperty.RegisterAttached("FitToScreenLevel", typeof(double), typeof(ZoomScrollViewer));
        }

        private static bool CheckNonNegative(object value)
        {
            double num = (double)value;
            return (double.IsNaN(num) || num >= 0.0);
        }

        public double Zoom
        {
            get
            {
                return (double)base.GetValue(ZoomProperty);
            }
            set
            {
                base.SetValue(ZoomProperty, value);
            }
        }

        public bool FitToScreen
        {
            get
            {
                return (bool)base.GetValue(FitToScreenProperty);
            }
            set
            {
                base.SetValue(FitToScreenProperty, value);
            }
        }

        public double FitToScreenLevel
        {
            get
            {
                return (double)base.GetValue(FitToScreenLevelProperty);
            }
            set
            {
                base.SetValue(FitToScreenLevelProperty, value);
            }
        }

        public bool ZoomToNormalSize
        {
            get
            {
                return (bool)base.GetValue(ZoomToNormalSizeProperty);
            }
            set
            {
                base.SetValue(ZoomToNormalSizeProperty, value);
            }
        }

        // Zoom changed
        private static void OnZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZoomScrollViewer viewer = d as ZoomScrollViewer;
            if (viewer != null)
            {
                viewer.OnZoomChanged();
            }
        }

        private void OnZoomChanged()
        {
            if (_contentPresenter != null)
            {
                if (!(_contentPresenter.LayoutTransform is ScaleTransform))
                    _contentPresenter.LayoutTransform = new ScaleTransform();
                ScaleTransform st = _contentPresenter.LayoutTransform as ScaleTransform;
                st.ScaleX = Zoom;
                st.ScaleY = Zoom;
            }
        }

        // Fit to screen
        private static void OnFitToScreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZoomScrollViewer viewer = d as ZoomScrollViewer;
            if (viewer != null)
            {
                viewer.OnFitToScreenChanged();
            }
        }

        private void OnFitToScreenChanged()
        {
            if (_contentPresenter != null)
            {
                if (!(_contentPresenter.LayoutTransform is ScaleTransform))
                    _contentPresenter.LayoutTransform = new ScaleTransform();
                ScaleTransform st = _contentPresenter.LayoutTransform as ScaleTransform;
                st.ScaleX = FitToScreenLevel;
                st.ScaleY = FitToScreenLevel;
            }
        }


        // 100 %
        private static void OnZoomToNormalSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZoomScrollViewer viewer = d as ZoomScrollViewer;
            if (viewer != null)
            {
                viewer.OnZoomToNormalSizeChanged();
            }
        }

        private void OnZoomToNormalSizeChanged()
        {
            if (_contentPresenter != null)
            {
                if (!(_contentPresenter.LayoutTransform is ScaleTransform))
                    _contentPresenter.LayoutTransform = new ScaleTransform();
                ScaleTransform st = _contentPresenter.LayoutTransform as ScaleTransform;
                st.ScaleX = 1;
                st.ScaleY = 1;
            }
        }


        public double MaxZoom
        {
            get
            {
                return (double)base.GetValue(MaxZoomProperty);
            }
            set
            {
                base.SetValue(MaxZoomProperty, value);
            }
        }

        public double MinZoom
        {
            get
            {
                return (double)base.GetValue(MinZoomProperty);
            }
            set
            {
                base.SetValue(MinZoomProperty, value);
            }
        }

        ScrollContentPresenter _scrollContentPresenter;
        ContentPresenter _contentPresenter;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentPresenter = base.GetTemplateChild("PART_ContentPresenter") as ContentPresenter;
            _scrollContentPresenter = base.GetTemplateChild("PART_ScrollContentPresenter") as ScrollContentPresenter;
        }

        #region Zoom

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                Point center = e.MouseDevice.GetPosition(this as IInputElement);
                double zoom = Math.Pow(1.1, e.Delta / 120.0);
                Vector translate = new Vector();
                ZoomAt(center, zoom, translate);
            }

            if (Keyboard.Modifiers != ModifierKeys.Shift)
                base.OnMouseWheel(e);
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            //if (skipManipulation)
            //    return;
            base.OnManipulationStarting(e);

            e.Mode |= ManipulationModes.Scale;
            e.Handled = true;
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            //if (skipManipulation)
            //    return;
            base.OnManipulationDelta(e);
            if (_contentPresenter == null | _scrollContentPresenter == null)
                return;

            Point center = e.ManipulationOrigin;
            double zoom = e.DeltaManipulation.Scale.X;
            Vector translate = e.DeltaManipulation.Translation;

            ZoomAt(center, zoom, translate);
            e.Handled = true;
        }

        public void ZoomAt(Point center, double zoom)
        {
            ZoomAt(center, zoom, new Vector());
        }
        private void ZoomAt(Point center, double zoom, Vector translate)
        {
            double zoomLevel = Zoom;
            double hoffset = this.HorizontalOffset - _contentPresenter.Margin.Left;
            double voffset = this.VerticalOffset - _contentPresenter.Margin.Top;
            Point anchor = new Point((hoffset + center.X) / zoomLevel, (voffset + center.Y) / zoomLevel);


            zoomLevel *= zoom;
            if (!double.IsNaN(MinZoom) && zoomLevel < MinZoom)
                zoomLevel = MinZoom;
            if (!double.IsNaN(MaxZoom) && zoomLevel > MaxZoom)
                zoomLevel = MaxZoom;

            Zoom = zoomLevel;

            Point resulting = new Point((hoffset + center.X) / zoomLevel, (voffset + center.Y) / zoomLevel);
            this.ScrollToHorizontalOffset(hoffset - (resulting.X - anchor.X) * zoomLevel - translate.X + _contentPresenter.Margin.Left);
            this.ScrollToVerticalOffset(voffset - (resulting.Y - anchor.Y) * zoomLevel - translate.Y + _contentPresenter.Margin.Top);
        }

        #endregion

        private Point startPoint;
        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (!e.Handled)
            {
                startPoint = e.MouseDevice.GetPosition(this as IInputElement);
                if (e.MouseDevice.MiddleButton == MouseButtonState.Pressed || e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                {
                    CaptureMouse();
                    e.Handled = true;
                }
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (IsMouseCaptured && e.MouseDevice.MiddleButton == MouseButtonState.Released && e.MouseDevice.LeftButton == MouseButtonState.Released)
            {
                Cursor = null;
                ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Handled)
                return;

            if (IsMouseCaptured && (e.MouseDevice.MiddleButton == MouseButtonState.Pressed || e.MouseDevice.LeftButton == MouseButtonState.Pressed))
            {
                Cursor = Cursors.Hand;
                Point current = e.MouseDevice.GetPosition(this as IInputElement);

                double vertOffset = startPoint.Y - current.Y;
                double horOffset = startPoint.X - current.X;

                this.ScrollToHorizontalOffset(this.HorizontalOffset - (-horOffset));
                this.ScrollToVerticalOffset(this.VerticalOffset - (-vertOffset));
                startPoint = current;
            }
        }

    }

}
