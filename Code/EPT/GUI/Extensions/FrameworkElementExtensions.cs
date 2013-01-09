using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EPT.GUI.Helpers;

namespace EPT.GUI.Extensions
{
    /// <summary>
    ///     Static Helperfunctions to Create, Manipulate and Save Image Files
    /// </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary>
        ///     Copies a framework element to clipboard.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void CopyToClipboard(this FrameworkElement element)
        {
            element.EnforceSize();
            var bounds = VisualTreeHelper.GetDescendantBounds(element);
            if (!bounds.AreValid()) return;
            var visual = MeasureAndArrange(element);
            var renderTargetBitmap = new RenderTargetBitmap((int) Math.Round(element.ActualWidth),
                                                            (int) Math.Round(element.ActualHeight), 96, 96,
                                                            PixelFormats.Default);
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                var vb = new VisualBrush(visual);
                drawingContext.DrawRectangle(vb, null,
                                             new Rect(new Point(), new Size(element.ActualWidth, element.ActualHeight)));
                drawingContext.Close();
            }
            renderTargetBitmap.Render(drawingVisual);
            Clipboard.SetImage(renderTargetBitmap);
        }

        /// <summary>
        ///     This method ensures that the Widths and Heights are initialized.  Sizing to content produces
        ///     Width and Height values of Double.NaN.
        /// </summary>
        public static void EnforceSize(this FrameworkElement element)
        {
            if (element.Width.Equals(Double.NaN))
                element.Width = element.DesiredSize.Width;
            if (element.Height.Equals(Double.NaN))
                element.Height = element.DesiredSize.Height;

            var parent = element.Parent as FrameworkElement;
            if (parent == null) return;

            element.MaxHeight = parent.ActualHeight;
            element.MaxWidth = parent.ActualWidth;
        }

        /// <summary>
        ///     Saves a BitmapSource as  JPG to a Directory with timestamp
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="directoryPath">The directory path.</param>
        public static void SaveJpgTo(this BitmapSource element, string directoryPath)
        {
            if (element == null || directoryPath == null) return;
            ImageHelper.SaveBufferTo(element.ToEncodedBuffer(ImageFormat.JPG), directoryPath);
        }

        /// <summary>
        ///     Renders a Visual as JPG to an specified directory location.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="directoryName">The file location.</param>
        public static void SaveJpgTo(this Visual element, string directoryName)
        {
            if (element == null) return;
            SaveJpgTo(element.ToRenderedBitmap(), directoryName);
        }

        /// <summary>
        ///     Checks if Width and Height is valid.
        /// </summary>
        /// <param name="visualBounds">The visual bounds validated.</param>
        /// <returns>True if width and height is set</returns>
        public static bool AreValid(this Rect visualBounds)
        {
            return !Double.IsNaN(visualBounds.Height) && !Double.IsInfinity(visualBounds.Height) &&
                   !Double.IsNaN(visualBounds.Width) && !Double.IsInfinity(visualBounds.Width);
        }

        /// <summary>
        ///     Checks if Width and Height is valid.
        /// </summary>
        /// <param name="element">the FrameworkElement </param>
        /// <returns>True if width and height is set</returns>
        public static bool BoundsAreValid(this Visual element)
        {
            var bounds = VisualTreeHelper.GetDescendantBounds(element);
            return bounds.AreValid();
        }

        /// <summary>
        ///     Renders a visual to a RenderTargetBitmap
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <returns>RenderTargetBitmap</returns>
        public static RenderTargetBitmap ToRenderedBitmap(this Visual visual)
        {
            var renderTargetBitmap = default(RenderTargetBitmap);

            var bounds = VisualTreeHelper.GetDescendantBounds(visual);
            if (bounds.AreValid())
            {
                renderTargetBitmap = new RenderTargetBitmap((int) Math.Round(bounds.Width),
                                                            (int) Math.Round(bounds.Height), 96, 96,
                                                            PixelFormats.Pbgra32);

                renderTargetBitmap.Render(visual);
                renderTargetBitmap.Freeze();
            }
            return renderTargetBitmap;
        }

        /// <summary>
        ///     returns a measured, arranged UIElement, this is sometimes important if you want to render UiElements without displaying them in the Current active UI (Window)
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <returns></returns>
        public static UIElement MeasureAndArrange(this UIElement visual)
        {
            visual.Measure(new Size
                {
                    Width = Double.PositiveInfinity,
                    Height = Double.PositiveInfinity
                });

            visual.Arrange(new Rect(0, 0, visual.DesiredSize.Width, visual.DesiredSize.Height));

            visual.UpdateLayout();

            return visual;
        }


        /// <summary>
        ///     Autoe crop bitmap will remove alpha pixels from image
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static CroppedBitmap AutoCropBitmap(this BitmapSource source)
        {
            if (source == null) throw new ArgumentException("source");

            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source,
                                                   PixelFormats.Bgra32, null, 0);

            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int bytesPerPixel = source.Format.BitsPerPixel/8;
            int stride = width*bytesPerPixel;

            var pixelBuffer = new byte[height*stride];
            source.CopyPixels(pixelBuffer, stride, 0);

            int cropTop = height, cropBottom = 0, cropLeft = width, cropRight = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int offset = (y*stride + x*bytesPerPixel);
                    //byte blue = pixelBuffer[offset];
                    //byte green = pixelBuffer[offset + 1];
                    //byte red = pixelBuffer[offset + 2];
                    byte alpha = pixelBuffer[offset + 3];

                    //TODO: Define a threshold when a pixel has a content
                    bool hasContent = alpha > 10;

                    if (hasContent)
                    {
                        cropLeft = Math.Min(x, cropLeft);
                        cropRight = Math.Max(x, cropRight);
                        cropTop = Math.Min(y, cropTop);
                        cropBottom = Math.Max(y, cropBottom);
                    }
                }
            }

            return new CroppedBitmap(source,
                                     new Int32Rect(cropLeft, cropTop, cropRight - cropLeft,
                                                   cropBottom - cropTop));
        }

        /// <summary>
        ///     Resizes the original while keeping its aspect ratio
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static RenderTargetBitmap ResizeTo(this BitmapSource source, int width, int height)
        {
            var rect = new Rect(0, 0, width, height);

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(source, rect);
                drawingContext.Close();
            }

            // Use RenderTargetBitmap to resize the original image
            var resizedImage = new RenderTargetBitmap(
                (int) rect.Width, (int) rect.Height, // Resized dimensions
                96, 96, // Default DPI values
                PixelFormats.Default); // Default pixel format

            resizedImage.Render(drawingVisual);
            resizedImage.Freeze();

            // Return the resized image
            return resizedImage;
        }

        /// <summary>
        ///     Resizes the original while keeping its aspect ratio
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="maximumWidthOrHeight">Maximum height of the width or.</param>
        /// <returns></returns>
        public static RenderTargetBitmap ResizeTo(this BitmapSource source, double maximumWidthOrHeight)
        {
            if (source == null || Double.IsInfinity(source.Width) || Double.IsInfinity(source.Height) ||
                Double.IsNaN(source.Width) || Double.IsNaN(source.Height))
                throw new ArgumentOutOfRangeException("source");

            //  Calculate the Width/Height scale factors based on the original
            double scaleFactor = maximumWidthOrHeight/Math.Max(source.Width, source.Height);
            var width = (int) Math.Round(scaleFactor*source.Width);
            var height = (int) Math.Round(scaleFactor*source.Height);
            // Resize the original while maintaining its aspect ratio
            return source.ResizeTo(width, height);
        }

        /// <summary>
        ///     Resizes the original while keeping its aspect ratio
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="maximumWidthOrHeight">Maximum height of the width or.</param>
        /// <returns></returns>
        public static RenderTargetBitmap ResizeTo(this Visual source, double maximumWidthOrHeight)
        {
            var renderTargetBitmap = source.ToRenderedBitmap();
            var rezized = renderTargetBitmap.ResizeTo(maximumWidthOrHeight);
            return rezized;
        }

        /// <summary>
        ///     To the bitmap frame.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static BitmapFrame ToBitmapFrame(this BitmapSource source, double size)
        {
            return BitmapFrame.Create(source.ResizeTo(size));
        }

        /// <summary>
        ///     Resizes the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <param name="scalingMode">The scaling mode.</param>
        /// <returns>a Rendered BitmapFrame</returns>
        public static BitmapFrame Resize(this BitmapFrame image, int width, int height, BitmapScalingMode scalingMode)
        {
            var group = new DrawingGroup();

            RenderOptions.SetBitmapScalingMode(@group, scalingMode);
            @group.Children.Add(new ImageDrawing(image, new Rect(0, 0, width, height)));
            var targetVisual = new DrawingVisual();
            using (var targetContext = targetVisual.RenderOpen())
            {
                targetContext.DrawDrawing(@group);
                var target = new RenderTargetBitmap(
                    width, height, 96, 96, PixelFormats.Default);
                targetContext.Close();
                target.Render(targetVisual);
                BitmapFrame targetFrame = BitmapFrame.Create(target);
                return targetFrame;
            }
        }

        /// <summary>
        ///     Gets a Byte Array from an FrameworkElement
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <returns>byte[]</returns>
        public static byte[] ToPngEncodedBuffer(this FrameworkElement element)
        {
            //get the dimensions of the FrameworkElement
            var margin = element.Margin.Left;
            var width = element.ActualWidth - margin;
            var height = element.ActualHeight - margin;

            return ToPngEncodedBuffer(element, width, height);
        }

        /// <summary>
        ///     Gets a Byte Array from an FrameworkElement using PngBitmapEncoder
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="width">width of the rendered element</param>
        /// <param name="height">height of the rendered element</param>
        /// <returns>byte[]</returns>
        public static byte[] ToPngEncodedBuffer(this FrameworkElement element, double width, double height)
        {
            //render FrameworkElement to bitmap
            var rtb = new RenderTargetBitmap((int) width, (int) height, 96d, 96d, PixelFormats.Default);
            rtb.Render(element);
            rtb.Freeze();

            //save the FrameworkElement to a memory stream
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(rtb));

            byte[] bitmapBytes;
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                //get the bitmap bytes from the memory stream
                ms.Position = 0;
                bitmapBytes = ms.ToArray();
                ms.Close();
            }
            return bitmapBytes;
        }

        /// <summary>
        ///     Gets a Byte Array from an BitmapImage
        /// </summary>
        /// <param name="imageSource">The image source.</param>
        /// <returns></returns>
        public static byte[] ToBuffer(this BitmapImage imageSource)
        {
            var stream = imageSource.StreamSource;
            byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (var br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32) stream.Length);
                }
            }
            return buffer;
        }
    }
}