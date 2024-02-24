using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ITMS.Business.Extensions
{
    public static class ImageExtenstion
    {
        private const int ThumbnailSize = 200;

        public static Image Resize(this Image image, int maxWidth, int maxHeight)
        {
            image.RotateExif();

            // Don't resize small images
            var ratio = Math.Min(1, Math.Min((double)maxWidth / image.Width, (double)maxHeight / image.Height));
            var newImage = new Bitmap((int)(image.Width * ratio), (int)(image.Height * ratio));

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.Clear(Color.White); // Transparant background color
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height));

                return newImage;
            }
        }

        public static void RotateExif(this Image image)
        {
            // Convert exif rotation to image rotation
            // Property 274 (0x0112) is 'Orientation'
            if (!image.PropertyIdList.Contains(274))
            {
                return;
            }
            var orientation = (int)image.GetPropertyItem(274).Value[0];
            switch (orientation)
            {
                case 2:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 4:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 5:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    image.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                default:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
            }

            image.RemovePropertyItem(274);
        }

        public static Stream ConvertToJpeg(this Image image, int quality)
        {
            if (quality < 0 || quality > 100)
            {
                throw new ArgumentException(@"Invalid image quality, the value should be between 0 and 100", "quality");
            }

            // JPG Encoder to set image compression
            var imageEncoder = ImageCodecInfo.GetImageDecoders().Single(m => m.FormatID == ImageFormat.Jpeg.Guid);
            var imageEncoderParameters = new EncoderParameters(1) { Param = { [0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Convert.ToInt64(quality)) } };

            var ms = new MemoryStream();
            image.Save(ms, imageEncoder, imageEncoderParameters);
            ms.Position = 0;
            return ms;
        }

        public static Stream ConvertToThumbnail(this Image image)
        {
            var size = Math.Min(image.Width, image.Height);
            int centerX = (image.Width / 2) - (size / 2);
            int centerY = (image.Height / 2) - (size / 2);

            var area = new Rectangle(centerX, centerY, size, size);

            using (var newImage = new Bitmap(ThumbnailSize, ThumbnailSize))
            {
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.Clear(Color.White); // Transparant background color
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    var ratio = Math.Min((double)ThumbnailSize / area.Width, (double)ThumbnailSize / area.Height);
                    var width = (int)(area.Width * ratio);
                    var height = (int)(area.Height * ratio);

                    graphics.DrawImage(
                        image,
                        new Rectangle((ThumbnailSize / 2) - (width / 2), (ThumbnailSize / 2) - (height / 2), width, height),
                        area,
                        GraphicsUnit.Pixel);

                    return newImage.ConvertToJpeg(80);
                }
            }
        }
    }
}
