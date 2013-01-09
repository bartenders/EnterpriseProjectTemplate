using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EPT.GUI.Extensions;
using Microsoft.Win32.SafeHandles;

namespace EPT.GUI.Helpers
{
    /// <summary>
    ///     Static Helperfunctions to Create, Manipulate and Save Image Files
    /// </summary>
    public static class ImageHelper
    {
        private static string _AssemblyShortName;

        private static string AssemblyShortName
        {
            get
            {
                if (_AssemblyShortName == null)
                {
                    Assembly assembly = typeof (ImageHelper).Assembly;
                    _AssemblyShortName = assembly.ToString().Split(',')[0]; // Pull out the short name.
                }
                return _AssemblyShortName;
            }
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

        /// <summary>
        ///     creates a pack URI.
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
        ///     Renders a List of Visuals into a Single RendertargetBitmap wich can be Set es Bitmap/Image-Source
        /// </summary>
        /// <param name="visuals">The visuals.</param>
        /// <returns>a RenderTargetBitmap that can be used as Source</returns>
        public static RenderTargetBitmap RenderVisualsToImageSource(IEnumerable<Visual> visuals)
        {
            int width = 0;
            int height = 0;
            // get the width and height for the larges element
            if (visuals != null)
                foreach (Visual visual in visuals)
                {
                    Rect bounds = VisualTreeHelper.GetDescendantBounds(visual);
                    if (bounds.AreValid())
                    {
                        width = (int) bounds.Width;
                        height = (int) bounds.Height;
                    }
                    else
                    {
                        break;
                    }
                }
            var renderTargetBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);

            foreach (Visual visual in visuals)
            {
                renderTargetBitmap.Render(visual);
            }
            renderTargetBitmap.Freeze();

            return renderTargetBitmap;
        }

        /// <summary>
        ///     Changes the pixel format.
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
        ///     Combines a list of ImageSources into a single ImageSource
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <param name="renderWidth">The width.</param>
        /// <param name="renderHeight">The height.</param>
        /// <returns></returns>
        public static RenderTargetBitmap CombineImageSources(IList<ImageSource> sources, int renderWidth,
                                                             int renderHeight)
        {
            // Target Rect for the resize operation
            var rect = new Rect(0, 0, renderWidth, renderHeight);

            // Create a DrawingVisual/Context to render with
            var drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                foreach (ImageSource imageSource in sources)
                {
                    drawingContext.DrawImage(imageSource, rect);
                }
                drawingContext.Close();
            }

            // Use RenderTargetBitmap to resize the original image
            var renderTargetBitmap = new RenderTargetBitmap(
                (int) rect.Width, (int) rect.Height, // Resized dimensions
                96, 96, // Default DPI values
                PixelFormats.Default); // Default pixel format

            renderTargetBitmap.Render(drawingVisual);
            renderTargetBitmap.Freeze();

            // Return the resized image
            return renderTargetBitmap;
        }

        /// <summary>
        ///     Combines a list of ImageSources into a single ImageSource using RenderTargetBitmap
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <returns></returns>
        public static ImageSource CombineImageSources(IList<ImageSource> sources)
        {
            int width = 0;
            int height = 0;
            foreach (ImageSource imgSrc in sources)
            {
                if (imgSrc.Width > width)
                {
                    width = (int) imgSrc.Width;
                }
                if (imgSrc.Height > height)
                {
                    height = (int) imgSrc.Height;
                }
            }
            return CombineImageSources(sources, width, height);
        }


        /// <summary>
        ///     Creates a BitmapFrame out of an FrameworkElement
        /// </summary>
        /// <param name="element">The FrameworkElement.</param>
        /// <param name="filePath">e.g. Environment.CurrentDirectory + "\\temp.bmp"</param>
        /// <returns></returns>
        public static void CaptureBitmap(FrameworkElement element, string filePath)
        {
            element.EnforceSize();

            var renderTargetBitmap = new RenderTargetBitmap((int) element.Width, (int) element.Height, 96, 96,
                                                            PixelFormats.Pbgra32);
            renderTargetBitmap.Render(element);
            renderTargetBitmap.Freeze();

            BitmapFrame bitmapFrame = BitmapFrame.Create(renderTargetBitmap);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(bitmapFrame);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fs);
                fs.Flush();
            }
        }

        /// <summary>
        ///     Loads Byte Array from filePath
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static byte[] FilePathToBuffer(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    byte[] imageBytes = br.ReadBytes((int) fs.Length);

                    br.Close();
                    return imageBytes;
                }
            }
        }

        /// <summary>
        ///     Creates a BitmapImage from Byte Array
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
        ///     Saves a Image Buffer FilePath
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
        ///     Creates a BitmapSource from Byte Array.
        /// </summary>
        /// <param name="imageBuffer">The image data.</param>
        /// <param name="decodePixelWidth">Width of the decode pixel. 0 for natural size</param>
        /// <param name="decodePixelHeight">Height of the decode pixel. 0 for natural size</param>
        /// <param name="options">BitmapCacheOption</param>
        /// <returns></returns>
        public static BitmapSource BufferToBitmapSource(byte[] imageBuffer, int decodePixelWidth, int decodePixelHeight,
                                                        BitmapCacheOption options = BitmapCacheOption.Default)
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
                result.CacheOption = options;
                result.EndInit();
                if (result.CanFreeze)
                {
                    result.Freeze();
                }
            }

            return result;
        }

        /// <summary>
        ///     Creates a BitmapSource from Byte Array.
        /// </summary>
        /// <param name="imageBuffer">The image buffer.</param>
        /// <returns></returns>
        public static BitmapSource BufferToBitmapSource(byte[] imageBuffer)
        {
            return BufferToBitmapSource(imageBuffer, 0, 0);
        }

        /// <summary>
        ///     Gets the encoded image data.
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
            RenderTargetBitmap renderTargetBitmap = visual.ToRenderedBitmap();
            byte[] buffer = renderTargetBitmap.ToEncodedBuffer(preferredFormat, qualityLevel);
            // For some Reason, RenderTargetBitmap Resources are released very late.. this extension method solves Memory Leaks
            return buffer;
        }

        /// <summary>
        ///     Gets the encoded image data.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="preferredFormat">The preferred Image format.</param>
        /// <param name="qualityLevel">JpegBitmapEncoder QualityLevel</param>
        /// <returns></returns>
        public static byte[] ToEncodedBuffer(this BitmapSource image, ImageFormat preferredFormat, int qualityLevel)
        {
            byte[] result = default(byte[]);
            BitmapEncoder encoder = default(BitmapEncoder);
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
                    br.Read(result, 0, (int) stream.Length);
                    br.Close();
                }
                stream.Close();
                return result;
            }
        }

        /// <summary>
        ///     Gets the enum ImageFormat
        /// </summary>
        /// <param name="extension">The extension string.</param>
        /// <returns></returns>
        private static ImageFormat TryParseImageFormat(string extension)
        {
            return (ImageFormat) Enum.Parse(typeof (ImageFormat), extension);
        }

        /// <summary>
        ///     Resizes the image.
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
        ///     Crops the image.
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
                                            new Int32Rect(isPortrait ? 0 : ((imageSource.PixelWidth - squareLength)/2),
                                                          isPortrait ? ((imageSource.PixelHeight - squareLength)/2) : 0,
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
        ///     Rotates the image.
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
        ///     Flips the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="isHorizontalFlip">
        ///     if set to <c>true</c> [is horizontal flip].
        /// </param>
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
            imageSource = new TransformedBitmap(imageSource,
                                                new ScaleTransform(isHorizontalFlip ? -1 : 1, isHorizontalFlip ? 1 : -1));


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
    ///     http://social.msdn.microsoft.com/forums/en-US/wpf/thread/a2988ae8-e7b8-4a62-a34f-b851aaf13886#rendertargetbitmap
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
        private DCSafeHandle() : base(true)
        {
        }

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
            return IntCreateDC(lpszDriver, null, null, IntPtr.Zero);
        }
    }
}