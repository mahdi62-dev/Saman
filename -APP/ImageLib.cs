using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Saman.APP;

public class ImageLib
{
    public static void convertComperessTif(string strPathFileMabdah,
        string strPathFileMaghsad,
        EncoderValue NohComperess)
    {
        ImageCodecInfo myImageCodecInfo;
        Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;

        // Create a Bitmap object based on a BMP file.
        //myBitmap = new Bitmap("Shapes.bmp");
        var myBitmap = new Bitmap(strPathFileMabdah);
        // Get an ImageCodecInfo object that represents the TIFF codec.
        myImageCodecInfo = GetEncoderInfo("image/tiff");

        // Create an Encoder object based on the GUID
        // for the Compression parameter category.
        myEncoder = Encoder.Compression;

        // Create an EncoderParameters object.
        // An EncoderParameters object has an array of EncoderParameter
        // objects. In this case, there is only one
        // EncoderParameter object in the array.
        myEncoderParameters = new EncoderParameters(1);

        // Save the bitmap as a TIFF file with LZW compression.
        //smail
        myEncoderParameter = new EncoderParameter(
            myEncoder,
            (long)NohComperess);

        myEncoderParameters.Param[0] = myEncoderParameter;
        //smail
        myBitmap.Save(strPathFileMaghsad, myImageCodecInfo, myEncoderParameters);
        myBitmap.Dispose();
        myBitmap = null;
    }


    public byte[] ComperessTifArray(string strPathFileMabdah, int Width, int Height)
    {
        var NohComperess = EncoderValue.CompressionCCITT4;

        ImageCodecInfo myImageCodecInfo;
        Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;

        // Create a Bitmap object based on a BMP file.
        //myBitmap = new Bitmap("Shapes.bmp");
        var myBitmap = new Bitmap(strPathFileMabdah);
        myBitmap = ResizeBitmap(myBitmap, Width, Height);

        // Get an ImageCodecInfo object that represents the TIFF codec.
        myImageCodecInfo = GetEncoderInfo("image/tiff");

        // Create an Encoder object based on the GUID
        // for the Compression parameter category.
        myEncoder = Encoder.Compression;

        // Create an EncoderParameters object.
        // An EncoderParameters object has an array of EncoderParameter
        // objects. In this case, there is only one
        // EncoderParameter object in the array.
        myEncoderParameters = new EncoderParameters(1);

        // Save the bitmap as a TIFF file with LZW compression.
        //smail
        myEncoderParameter = new EncoderParameter(
            myEncoder,
            (long)NohComperess);

        myEncoderParameters.Param[0] = myEncoderParameter;
        //smail

        var myStream = new MemoryStream();
        myBitmap.Save(myStream, myImageCodecInfo, myEncoderParameters);

        // myBitmap.Save(strPathFileMaghsad, myImageCodecInfo, myEncoderParameters);


        myBitmap.Dispose();
        myBitmap = null;

        return myStream.ToArray();
    }

    public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
    {
        var result = new Bitmap(width, height);
        using (var g = Graphics.FromImage(result))
        {
            g.DrawImage(bmp, 0, 0, width, height);
        }

        return result;
    }

    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
            if (encoders[j].MimeType == mimeType)
                return encoders[j];

        return null;
    }

    public Image ByteArrayToImage(byte[] byteArrayIn)
    {
        var ms = new MemoryStream(byteArrayIn);
        var returnImage = Image.FromStream(ms);
        return returnImage;
    }
}