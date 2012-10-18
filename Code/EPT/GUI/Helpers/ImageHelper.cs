using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Security;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace EPT.GUI.Helpers
{
    /// <summary>
    /// Static Helperfunctions to Create, Manipulate and Save Image Files
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Disposes the specified RenderTargetBitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public static void Dispose(this RenderTargetBitmap bitmap)
        {
            bitmap = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static Image CreateImage(Uri bitmapUri, int decodePixelWidth)
        {
            var simpleImage = new Image {Width = decodePixelWidth};

            var bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = bitmapUri;
            bi.DecodePixelWidth = decodePixelWidth;
            bi.EndInit();
            // Set the image source.
            simpleImage.Source = bi;

            return simpleImage;
        }

        /// <summary>
        /// Copies a framework element to clipboard.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void CopyToClipboard(this FrameworkElement element)
        {
            var bounds = VisualTreeHelper.GetDescendantBounds(element);
            if (AreValid(bounds))
            {
                var visual = MeasureAndArrange(element);
                var renderTargetBitmap = new RenderTargetBitmap((int)Math.Round(element.ActualWidth), (int)Math.Round(element.ActualHeight), 96, 96, PixelFormats.Default);
                var drawingVisual = new DrawingVisual();
                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    var vb = new VisualBrush(visual);
                    drawingContext.DrawRectangle(vb, null, new Rect(new Point(), new Size(element.ActualWidth, element.ActualHeight)));
                    drawingContext.Close();
                }
                renderTargetBitmap.Render(drawingVisual);
                Clipboard.SetImage(renderTargetBitmap);
            }
        }



        public static void SaveBufferTo(byte[] img, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            string timeString = String.Format("{0}_{1}_{2}_{3}_{4}_{5}_", DateTime.Now.Year,
                                              DateTime.Now.Month,
                                              DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute,
                                              DateTime.Now.Millisecond);
            string filePath = string.Format("{0}{1}{2}", directoryPath, timeString, "Image.jpg");
            File.WriteAllBytes(filePath, img);
        }

        public static void SaveJpgTo(this BitmapSource element, string directoryPath)
        {
            if (element == null || directoryPath == null) return;
            SaveBufferTo(element.ToEncodedBuffer(ImageFormat.JPG), directoryPath);
        }

        /// <summary>
        /// Renders a Visual as JPG to an specified directory location.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="directoryName">The file location.</param>
        public static void SaveJpgTo(this Visual element, string directoryName)
        {
            if (element == null) return;
            SaveJpgTo(element.ToRenderedBitmap(), directoryName);
        }

        /// <summary>
        /// Checks if Width and Height is valid.
        /// </summary>
        /// <param name="visualBounds">The visual bounds validated.</param>
        /// <returns>True if width and height is set</returns>
        public static bool AreValid(this Rect visualBounds)
        {
            return !Double.IsNaN(visualBounds.Height) && !Double.IsInfinity(visualBounds.Height) && !Double.IsNaN(visualBounds.Width) && !Double.IsInfinity(visualBounds.Width);
        }

        /// <summary>
        /// Checks if Width and Height is valid.
        /// </summary>
        /// <param name="element">the FrameworkElement </param>
        /// <returns>True if width and height is set</returns>
        public static bool BoundsAreValid(this Visual element)
        {
            var bounds = VisualTreeHelper.GetDescendantBounds(element);
            return !Double.IsNaN(bounds.Height) && !Double.IsInfinity(bounds.Height) && !Double.IsNaN(bounds.Width) && !Double.IsInfinity(bounds.Width);
        }


        private static string _AssemblyShortName;
        private static string AssemblyShortName
        {
            get
            {
                if (_AssemblyShortName == null)
                {
                    var assembly = typeof(ImageHelper).Assembly;
                    _AssemblyShortName = assembly.ToString().Split(',')[0]; // Pull out the short name.
                }
                return _AssemblyShortName;
            }
        }

        /// <summary>
        /// creates a pack URI.
        /// </summary>
        /// <param name="relativeFile">The relative file.</param>
        /// <returns></returns>
        public static Uri CreatePackUri(string relativeFile)
        {
            var uriString = new StringBuilder();
            uriString.Append("pack://application:,,,");
            uriString.Append("/" + AssemblyShortName + ";component/" + relativeFile);

            return new Uri(uriString.ToString(), UriKind.RelativeOrAbsolute);
        }


        /// <summary>
        /// Renders a visual to a RenderTargetBitmap
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <returns>RenderTargetBitmap</returns>
        public static RenderTargetBitmap ToRenderedBitmap(this Visual visual)
        {
            var renderTargetBitmap = default(RenderTargetBitmap);

            var bounds = VisualTreeHelper.GetDescendantBounds(visual);
            if (bounds.AreValid())
            {
                renderTargetBitmap = new RenderTargetBitmap((int)Math.Round(bounds.Width), (int)Math.Round(bounds.Height), 96, 96, PixelFormats.Pbgra32);

                renderTargetBitmap.Render(visual);
                renderTargetBitmap.Freeze();
            }
            return renderTargetBitmap;
        }

        /// <summary>
        /// returns a measured, arranged and layout updated UIElement
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <returns></returns>
        public static UIElement MeasureAndArrange(this UIElement visual)
        {
            visual.Measure(new Size
                {
                    Width = double.PositiveInfinity,
                    Height = double.PositiveInfinity
                });

            visual.Arrange(new Rect(0, 0, visual.DesiredSize.Width, visual.DesiredSize.Height));

            visual.UpdateLayout();

            return visual;
        }

        /// <summary>
        /// Renders a List of Visuals into a Single RendertargetBitmap wich can be Set es Bitmap/Image-Source
        /// </summary>
        /// <param name="visuals">The visuals.</param>
        /// <returns>a RenderTargetBitmap that can be used as Source</returns>
        public static RenderTargetBitmap RenderVisualsToImageSource(IEnumerable<Visual> visuals)
        {
            int width = 0;
            int height = 0;
            // get the width and height for the larges element
            if (visuals != null)
                foreach (var visual in visuals)
                {
                    var bounds = VisualTreeHelper.GetDescendantBounds(visual);
                    if (AreValid(bounds))
                    {
                        width = (int)bounds.Width;
                        height = (int)bounds.Height;
                    }
                    else
                    {
                        break;
                    }
                }
            var renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);

            foreach (var visual in visuals)
            {
                renderTargetBitmap.Render(visual);
            }
            renderTargetBitmap.Freeze();

            return renderTargetBitmap;
        }

        /// <summary>
        /// Changes the pixel format.
        /// </summary>
        /// <param name="bmpSource">The BitmapSource source.</param>
        /// <param name="pixFormat">The pix format.</param>
        /// <returns></returns>
        public static FormatConvertedBitmap ChangePixelFormat(BitmapSource bmpSource,
                                                              PixelFormat pixFormat)
        {
            if (bmpSource == null)
            {
                return null;
            }

            var bmpConverted = new FormatConvertedBitmap();
            // Change properties within a BeginInit/EndInit block.
            bmpConverted.BeginInit();
            bmpConverted.Source = bmpSource;
            // Set to new pixel format.
            bmpConverted.DestinationFormat = pixFormat;
            bmpConverted.EndInit();


            return bmpConverted;
        }


        /// <summary>
        /// Autoe crop bitmap will remove alpha pixels from image
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
            int bytesPerPixel = source.Format.BitsPerPixel / 8;
            int stride = width * bytesPerPixel;

            var pixelBuffer = new byte[height * stride];
            source.CopyPixels(pixelBuffer, stride, 0);

            int cropTop = height, cropBottom = 0, cropLeft = width, cropRight = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int offset = (y * stride + x * bytesPerPixel);
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
                (int)rect.Width, (int)rect.Height,  // Resized dimensions
                96, 96,                             // Default DPI values
                PixelFormats.Default);              // Default pixel format

            resizedImage.Render(drawingVisual);
            resizedImage.Freeze();

            // Return the resized image
            return resizedImage;
        }

        public static RenderTargetBitmap ResizeTo(this BitmapSource source, double maximumWidthOrHeight)
        {
            if (source == null || Double.IsInfinity(source.Width) || Double.IsInfinity(source.Height) || Double.IsNaN(source.Width) || Double.IsNaN(source.Height))
                return null;

            //  Calculate the Width/Height scale factors based on the original
            var scaleFactor = maximumWidthOrHeight / Math.Max(source.Width, source.Height);
            var width = (int)Math.Round(scaleFactor * source.Width);
            var height = (int)Math.Round(scaleFactor * source.Height);
            // Resize the original while maintaining its aspect ratio
            return ResizeTo(source, width, height);
        }

        public static RenderTargetBitmap ResizeTo(this Visual source, double maximumWidthOrHeight)
        {
            var renderTargetBitmap = source.ToRenderedBitmap();
            var rezized = renderTargetBitmap.ResizeTo(maximumWidthOrHeight);
            renderTargetBitmap.Dispose();
            return rezized;
        }

        public static BitmapFrame ToBitmapFrame(this BitmapSource source, double size)
        {
            return BitmapFrame.Create(source.ResizeTo(size));
        }

        public static BitmapFrame Resize(this BitmapFrame image, int width, int height, BitmapScalingMode scalingMode)
        {
            var group = new DrawingGroup();

            RenderOptions.SetBitmapScalingMode(group, scalingMode);
            group.Children.Add(new ImageDrawing(image,new Rect(0, 0, width, height)));
            var targetVisual = new DrawingVisual();
            using(var targetContext = targetVisual.RenderOpen())
            {
                targetContext.DrawDrawing(group);
                var target = new RenderTargetBitmap(
                    width, height, 96, 96, PixelFormats.Default);
                targetContext.Close();
                target.Render(targetVisual);
                var targetFrame = BitmapFrame.Create(target);
                return targetFrame;
            }
        }

        /// <summary>
        /// Combines a list of ImageSources into a single ImageSource
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <param name="renderWidth">The width.</param>
        /// <param name="renderHeight">The height.</param>
        /// <returns></returns>
        public static RenderTargetBitmap CombineImageSources(IList<ImageSource> sources, int renderWidth, int renderHeight)
        {
            // Target Rect for the resize operation
            var rect = new Rect(0, 0, renderWidth, renderHeight);

            // Create a DrawingVisual/Context to render with
            var drawingVisual = new DrawingVisual();

            using (var drawingContext = drawingVisual.RenderOpen())
            {
                foreach (var imageSource in sources)
                {
                    drawingContext.DrawImage(imageSource, rect);
                }
                drawingContext.Close();
            }

            // Use RenderTargetBitmap to resize the original image
            var renderTargetBitmap = new RenderTargetBitmap(
                (int)rect.Width, (int)rect.Height,  // Resized dimensions
                96, 96,                             // Default DPI values
                PixelFormats.Default);              // Default pixel format

            renderTargetBitmap.Render(drawingVisual);
            renderTargetBitmap.Freeze();

            // Return the resized image
            return renderTargetBitmap;
        }

        /// <summary>
        /// Combines a list of ImageSources into a single ImageSource using RenderTargetBitmap
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <returns></returns>
        public static ImageSource CombineImageSources(IList<ImageSource> sources)
        {
            int width = 0;
            int height = 0;
            foreach (var imgSrc in sources)
            {
                if (imgSrc.Width > width)
                {
                    width = (int)imgSrc.Width;
                }
                if (imgSrc.Height > height)
                {
                    height = (int)imgSrc.Height;
                }
            }
            return CombineImageSources(sources, width, height);
        }



        /// <summary>
        /// Creates a BitmapFrame out of an FrameworkElement
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="filePath">e.g. Environment.CurrentDirectory + "\\temp.bmp"</param>
        /// <returns></returns>
        public static void CaptureBitmap(FrameworkElement element, string filePath)
        {
            EnforceSize(element);

            var renderTargetBitmap = new RenderTargetBitmap((int)element.Width, (int)element.Height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(element);
            renderTargetBitmap.Freeze();

            var bitmapFrame = BitmapFrame.Create(renderTargetBitmap);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(bitmapFrame);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fs);
                fs.Flush();
            }
        }

        /// <summary>
        /// This method ensures that the Widths and Heights are initialized.  Sizing to content produces
        /// Width and Height values of Double.NaN.  
        /// </summary>
        private static void EnforceSize(this FrameworkElement element)
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
        /// Loads Byte Array from filePath
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static byte[] FilePathToBuffer(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    byte[] imageBytes = br.ReadBytes((int)fs.Length);

                    br.Close();
                    return imageBytes;
                }
            }
        }


        /// <summary>
        /// Gets a Byte Array from an FrameworkElement
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <returns>byte[]</returns>
        public static byte[] ToPngEncodedBuffer(this FrameworkElement element)
        {
            //get the dimensions of the FrameworkElement
            double margin = element.Margin.Left;
            double width = element.ActualWidth - margin;
            double height = element.ActualHeight - margin;

            return ToPngEncodedBuffer(element, width, height);
        }

        /// <summary>
        /// Gets a Byte Array from an FrameworkElement using PngBitmapEncoder
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="width">width of the rendered element</param>
        /// <param name="height">height of the rendered element</param>
        /// <returns>byte[]</returns>
        public static byte[] ToPngEncodedBuffer(this FrameworkElement element, double width, double height)
        {
            //render FrameworkElement to bitmap
            var rtb = new RenderTargetBitmap((int)width, (int)height, 96d, 96d, PixelFormats.Default);
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
        /// Gets a Byte Array from an BitmapImage
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
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }
            return buffer;
        }


        /// <summary>
        /// Creates a BitmapImage from Byte Array
        /// </summary>
        /// <param name="bytes">The Byte Array.</param>
        /// <param name="freeze">Freeze the BitmapImage Element</param>
        /// <returns>BitmapImage</returns>
        public static BitmapImage BufferToBitmapImage(Byte[] bytes, bool freeze = true)
        {
            var image = new BitmapImage();
            using (var stream = new MemoryStream(bytes))
            {
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                if (freeze && image.CanFreeze)
                {
                    image.Freeze();
                }
            }
            return image;
        }



        /// <summary>
        /// Saves a Image Buffer FilePath
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <param name="filePath">The file path.</param>
        private static void SaveBufferToFilePath(byte[] imageData, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(imageData);
                    bw.Close();
                }
                fs.Close();
            }
        }


        /// <summary>
        /// Creates a BitmapSource from Byte Array.
        /// </summary>
        /// <param name="imageBuffer">The image data.</param>
        /// <param name="decodePixelWidth">Width of the decode pixel. 0 for natural size</param>
        /// <param name="decodePixelHeight">Height of the decode pixel. 0 for natural size</param>
        /// <returns></returns>
        public static BitmapSource BufferToBitmapSource(byte[] imageBuffer, int decodePixelWidth, int decodePixelHeight)
        {
            if (imageBuffer == null) return null;

            var result = new BitmapImage();
            using (var stream = new MemoryStream(imageBuffer))
            {
                result.BeginInit();
                if (decodePixelWidth > 0)
                {
                    result.DecodePixelWidth = decodePixelWidth;
                }
                if (decodePixelHeight > 0)
                {
                    result.DecodePixelHeight = decodePixelHeight;
                }
                result.StreamSource = stream;
                result.CreateOptions = BitmapCreateOptions.None;
                result.CacheOption = BitmapCacheOption.Default;
                result.EndInit();
                if (result.CanFreeze)
                {
                    result.Freeze();
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a BitmapSource from Byte Array.
        /// </summary>
        /// <param name="imageBuffer">The image buffer.</param>
        /// <returns></returns>
        public static BitmapSource BufferToBitmapSource(byte[] imageBuffer)
        {
            return BufferToBitmapSource(imageBuffer, 0, 0);
        }

        /// <summary>
        /// Gets the encoded image data.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="preferredFormat">The preferred Image format.</param>
        /// <returns></returns>
        public static byte[] ToEncodedBuffer(this BitmapSource image, ImageFormat preferredFormat)
        {
            return ToEncodedBuffer(image, preferredFormat, 95);
        }

        public static byte[] ToEncodedBuffer(this Visual visual, ImageFormat preferredFormat, int qualityLevel = 95)
        {
            var renderTargetBitmap = visual.ToRenderedBitmap();
            var buffer = renderTargetBitmap.ToEncodedBuffer(preferredFormat, qualityLevel);
            // For some Reason, RenderTargetBitmap Resources are released very late.. this extension method solves Memory Leaks
            renderTargetBitmap.Dispose();
            return buffer;
        }

        /// <summary>
        /// Gets the encoded image data.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="preferredFormat">The preferred Image format.</param>
        /// <param name="qualityLevel">JpegBitmapEncoder QualityLevel</param>
        /// <returns></returns>
        public static byte[] ToEncodedBuffer(this BitmapSource image, ImageFormat preferredFormat, int qualityLevel)
        {
            var result = default(byte[]);
            var encoder = default(BitmapEncoder);
            if (image == null) return null;

            switch (preferredFormat)
            {
                case ImageFormat.JPEG:
                case ImageFormat.JPG:
                    encoder = new JpegBitmapEncoder
                        {
                            QualityLevel = qualityLevel
                        };
                    break;
                case ImageFormat.BMP:
                    encoder = new BmpBitmapEncoder();
                    break;

                case ImageFormat.PNG:
                    encoder = new PngBitmapEncoder();
                    break;
                case ImageFormat.TIFF:
                case ImageFormat.TIF:
                    encoder = new TiffBitmapEncoder();
                    break;
                case ImageFormat.GIF:
                    encoder = new GifBitmapEncoder();
                    break;
                case ImageFormat.WMP:
                    encoder = new WmpBitmapEncoder();
                    break;
            }
            using (var stream = new MemoryStream())
            {
                if (encoder != null)
                {
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(stream);
                }
                stream.Seek(0, SeekOrigin.Begin);
                result = new byte[stream.Length];
                using (var br = new BinaryReader(stream))
                {
                    br.Read(result, 0, (int)stream.Length);
                    br.Close();
                }
                stream.Close();
                return result;
            }
        }

        /// <summary>
        /// Gets the enum ImageFormat
        /// </summary>
        /// <param name="extension">The extension string.</param>
        /// <returns></returns>
        private static ImageFormat TryParseImageFormat(string extension)
        {
            return (ImageFormat)Enum.Parse(typeof(ImageFormat), extension);
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static void ResizeImage(string filename, int width, int height)
        {
            string extension = Path.GetExtension(filename);
            string rootFilename = Path.GetDirectoryName(filename)
                                  + @"\" + Path.GetFileNameWithoutExtension(filename);

            // load the file data
            byte[] imageBytes = FilePathToBuffer(filename);

            // decode the image to the requested width and height
            BitmapSource imageSource = BufferToBitmapSource(imageBytes, width, height);

            // encode the image using the original format
            byte[] encodedBytes = ToEncodedBuffer(imageSource, TryParseImageFormat(extension));

            // save the modified image
            filename = rootFilename + "Resized" + extension;
            SaveBufferToFilePath(encodedBytes, filename);

            Console.WriteLine("Resized image to {0} x {1} and saved as {2}.\n",
                              imageSource.PixelWidth, imageSource.PixelHeight, filename);
        }

        /// <summary>
        /// Crops the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public static void CropImage(string filename)
        {
            string extension = Path.GetExtension(filename);
            string rootFilename = Path.GetDirectoryName(filename)
                                  + @"\" + Path.GetFileNameWithoutExtension(filename);

            // load the file data
            byte[] imageBytes = FilePathToBuffer(filename);

            // decode the image to its natural size
            BitmapSource imageSource = BufferToBitmapSource(imageBytes, 0, 0);

            // crop the image so that it is square
            bool isPortrait = imageSource.Height > imageSource.Width;
            int squareLength = Math.Min(imageSource.PixelWidth, imageSource.PixelHeight);
            imageSource = new CroppedBitmap(imageSource,
                                            new Int32Rect(isPortrait ? 0 : (int)((imageSource.PixelWidth - squareLength) / 2),
                                                          isPortrait ? (int)((imageSource.PixelHeight - squareLength) / 2) : 0,
                                                          squareLength, squareLength));

            // encode the image using the original format
            byte[] encodedBytes = ToEncodedBuffer(imageSource, TryParseImageFormat(extension));

            // save the modified image
            filename = rootFilename + "Square" + extension;
            SaveBufferToFilePath(encodedBytes, filename);

            Console.WriteLine("Cropped image to {0} x {1} and saved as {2}.\n",
                              imageSource.PixelWidth, imageSource.PixelHeight, filename);
        }

        /// <summary>
        /// Rotates the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="angle">The angle.</param>
        public static void RotateImage(string filename, double angle)
        {
            string extension = Path.GetExtension(filename);
            string rootFilename = Path.GetDirectoryName(filename)
                                  + @"\" + Path.GetFileNameWithoutExtension(filename);

            // load the file data
            byte[] imageBytes = FilePathToBuffer(filename);

            // decode the image to its natural size
            BitmapSource imageSource = BufferToBitmapSource(imageBytes, 0, 0);

            // apply a rotate transform to the image
            imageSource = new TransformedBitmap(imageSource, new RotateTransform(angle));

            // encode the image using the original format
            byte[] encodedBytes = ToEncodedBuffer(imageSource, TryParseImageFormat(extension));

            // save the modified image
            filename = rootFilename + "Rotated" + extension;
            SaveBufferToFilePath(encodedBytes, filename);

            Console.WriteLine("Rotated image {0} degrees and saved as {1}.\n",
                              angle, filename);
        }



        /// <summary>
        /// Flips the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="isHorizontalFlip">if set to <c>true</c> [is horizontal flip].</param>
        public static void FlipImage(string filename, bool isHorizontalFlip)
        {
            string extension = Path.GetExtension(filename);
            string rootFilename = Path.GetDirectoryName(filename)
                                  + @"\" + Path.GetFileNameWithoutExtension(filename);

            // load the file data
            byte[] imageBytes = FilePathToBuffer(filename);

            // decode the image to its natural size
            BitmapSource imageSource = BufferToBitmapSource(imageBytes, 0, 0);

            // apply a rotate transform to the image
            imageSource = new TransformedBitmap(imageSource, new ScaleTransform(isHorizontalFlip ? -1 : 1, isHorizontalFlip ? 1 : -1));


            // encode the image using the original format
            byte[] encodedBytes = ToEncodedBuffer(imageSource, TryParseImageFormat(extension));

            // save the modified image
            filename = rootFilename + "Flipped" + extension;
            SaveBufferToFilePath(encodedBytes, filename);


            Console.WriteLine("Flipped image {0} and saved as {1}.\n",
                              isHorizontalFlip ? "horizontally" : "vertically", filename);
        }
    }

    public enum ImageFormat
    {
        JPG,
        JPEG,
        BMP,
        PNG,
        TIF,
        TIFF,
        GIF,
        WMP
    }

    /// <summary>
    /// http://social.msdn.microsoft.com/forums/en-US/wpf/thread/a2988ae8-e7b8-4a62-a34f-b851aaf13886#rendertargetbitmap
    /// </summary>
    internal class DeviceHelper
    {
        public static Int32 PixelsPerInch(Orientation orientation)
        {
            Int32 capIndex = (orientation == Orientation.Horizontal) ? 0x58 : 90;
            using (DCSafeHandle handle = UnsafeNativeMethods.CreateDC("DISPLAY"))
            {
                return (handle.IsInvalid ? 0x60 : UnsafeNativeMethods.GetDeviceCaps(handle, capIndex));
            }
        }
    }

    internal sealed class DCSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private DCSafeHandle() : base(true) { }

        protected override Boolean ReleaseHandle()
        {
            return UnsafeNativeMethods.DeleteDC(base.handle);
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern Boolean DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern Int32 GetDeviceCaps(DCSafeHandle hDC, Int32 nIndex);

        [DllImport("gdi32.dll", EntryPoint = "CreateDC", CharSet = CharSet.Auto)]
        public static extern DCSafeHandle IntCreateDC(String lpszDriver,
                                                      String lpszDeviceName, String lpszOutput, IntPtr devMode);

        public static DCSafeHandle CreateDC(String lpszDriver)
        {
            return UnsafeNativeMethods.IntCreateDC(lpszDriver, null, null, IntPtr.Zero);
        }
    }
}