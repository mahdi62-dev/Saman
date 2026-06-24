using System.IO;

namespace App;

public class SaveFileToDB
{
    public static void SaveToDb(string filePath)
    {
        //Open file into a filestream and 
        //read data in a byte array.

        //Initialize byte array with a null value initially.
        byte[] data = null;

        //Use FileInfo object to get file size.
        var fInfo = new FileInfo(filePath);
        var numBytes = fInfo.Length;

        //Open FileStream to read file
        var fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        //Use BinaryReader to read file stream into byte array.
        var br = new BinaryReader(fStream);

        //When you use BinaryReader, you need to supply number of bytes to read from file.
        //In this case we want to read entire file. So supplying total number of bytes.

        data = br.ReadBytes((int)numBytes);


        //Read Image Bytes into a byte array
        var imageData = data;

        /////// DAL.MatabFile.InsRecord(imageData);
        fStream.Close();

        /*
//Initialize SQL Server Connection
SqlConnection CN = new SqlConnection(txtConnectionString.Text);

//Set insert query
string qry = "insert into ImagesStore (OriginalPath,ImageData) _
values(@OriginalPath, @ImageData)";

//Initialize SqlCommand object for insert.
SqlCommand SqlCom = new SqlCommand(qry, CN);

//We are passing Original Image Path and
//Image byte data as sql parameters.
SqlCom.Parameters.Add(new SqlParameter("@OriginalPath",
(object)txtImagePath.Text));

SqlCom.Parameters.Add(new SqlParameter("@ImageData",
(object)imageData));

//Open connection and execute insert query.
CN.Open();
SqlCom.ExecuteNonQuery();
CN.Close();

         */
        //Close form and return to list or images.
        // this.Close();
    }
}