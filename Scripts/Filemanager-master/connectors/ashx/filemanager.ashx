<%@ WebHandler Language="C#" Class="filemanager" %>

//	** Filemanager ASP.NET connector
//
//	** .NET Framework >= 2.0
//
//	** license	    MIT License
//	** author		Ondřej "Yumi Yoshimido" Brožek | <cholera@hzspraha.cz>
//	** Copyright	Author

using System;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using System.Web;

public class filemanager : IHttpHandler 
{

    //===================================================================
    //==================== EDIT CONFIGURE HERE ==========================
    //===================================================================

    public string IconDirectory = "/Scripts/Filemanager-master/images/fileicons/"; // Icon directory for filemanager. [string]
    public string StorageDirectory = "/Storage/Img/";
    public string[] imgExtensions = new string[] { ".jpg", ".png", ".jpeg", ".gif", ".bmp" };

    //===================================================================
    //========================== END EDIT ===============================
    //===================================================================       


    
    
    
    private string RemoveRootBeforeFormResponse(string path)
{
return path.StartsWith(this.StorageDirectory)
? path.Remove(0, this.StorageDirectory.Length) : path;
}
    
    
    
    
    
    
    
    
    
    
    
    
    private bool IsImage(FileInfo fileInfo)
    {
        foreach (string ext in imgExtensions)
        {
            if (Path.GetExtension(fileInfo.FullName) == ext)
            {
                return true;
            }
        }

        return false;
    }
    
    
    private string getFolderInfo(string path)
    {
        DirectoryInfo RootDirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("{");

        int i = 0;
        
        foreach (DirectoryInfo DirInfo in RootDirInfo.GetDirectories()) 
        {
            if (i > 0) 
            {
                sb.Append(",");
                sb.AppendLine();
            }

            sb.AppendLine("\"" + Path.Combine(RemoveRootBeforeFormResponse(path), DirInfo.Name) + "\": {");
            sb.AppendLine("\"Path\": \"" + Path.Combine(RemoveRootBeforeFormResponse(path), DirInfo.Name) + "/\",");
            sb.AppendLine("\"Filename\": \"" + DirInfo.Name + "\",");
            sb.AppendLine("\"File Type\": \"dir\",");
            sb.AppendLine("\"Preview\": \"" + IconDirectory + "_Open.png\",");
            sb.AppendLine("\"Properties\": {");
            sb.AppendLine("\"Date Created\": \"" + DirInfo.CreationTime.ToString() + "\", ");
            sb.AppendLine("\"Date Modified\": \"" + DirInfo.LastWriteTime.ToString() + "\", ");
            sb.AppendLine("\"Height\": 0,");
            sb.AppendLine("\"Width\": 0,");
            sb.AppendLine("\"Size\": 0 ");
            sb.AppendLine("},");
            sb.AppendLine("\"Error\": \"\",");
            sb.AppendLine("\"Code\": 0	");
            sb.Append("}");

            i++;
        }

        foreach (FileInfo fileInfo in RootDirInfo.GetFiles())
        {
            if (i > 0)
            {
                sb.Append(",");
                sb.AppendLine();
            }

            sb.AppendLine("\"" + Path.Combine(RemoveRootBeforeFormResponse(path), fileInfo.Name) + "\": {");
            sb.AppendLine("\"Path\": \"" + Path.Combine(RemoveRootBeforeFormResponse(path), fileInfo.Name) + "\",");
            sb.AppendLine("\"Filename\": \"" + fileInfo.Name + "\",");
            sb.AppendLine("\"File Type\": \"" + fileInfo.Extension.Replace(".","") + "\",");
            
            if (IsImage(fileInfo))
            {
                sb.AppendLine("\"Preview\": \"" + Path.Combine(path, fileInfo.Name) + "\",");
            }
            else
            {
                sb.AppendLine("\"Preview\": \"" + String.Format("{0}{1}.png", IconDirectory, fileInfo.Extension.Replace(".", "")) + "\",");
            }
            
            sb.AppendLine("\"Properties\": {");
            sb.AppendLine("\"Date Created\": \"" + fileInfo.CreationTime.ToString() + "\", ");
            sb.AppendLine("\"Date Modified\": \"" + fileInfo.LastWriteTime.ToString() + "\", ");

            if (IsImage(fileInfo)) 
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(fileInfo.FullName))
                {
                    sb.AppendLine("\"Height\": " + img.Height.ToString() + ",");
                    sb.AppendLine("\"Width\": " + img.Width.ToString() + ",");
                }
            }
                   
            sb.AppendLine("\"Size\": " + fileInfo.Length.ToString() + " ");
            sb.AppendLine("},");
            sb.AppendLine("\"Error\": \"\",");
            sb.AppendLine("\"Code\": 0	");
            sb.Append("}");

            i++;
        }
        
        sb.AppendLine();
        sb.AppendLine("}");

        return sb.ToString();
    }

    private string getInfo(string path) 
    {
        StringBuilder sb = new StringBuilder();

        FileAttributes attr = File.GetAttributes(HttpContext.Current.Server.MapPath(path));

        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
        {
            DirectoryInfo DirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));

            sb.AppendLine("{");
            sb.AppendLine("\"Path\": \"" + RemoveRootBeforeFormResponse(path) + "\",");
            sb.AppendLine("\"Filename\": \"" + DirInfo.Name + "\",");
            sb.AppendLine("\"File Type\": \"dir\",");
            sb.AppendLine("\"Preview\": \"" + IconDirectory + "_Open.png\",");
            sb.AppendLine("\"Properties\": {");
            sb.AppendLine("\"Date Created\": \"" + DirInfo.CreationTime.ToString() + "\", ");
            sb.AppendLine("\"Date Modified\": \"" + DirInfo.LastWriteTime.ToString() + "\", ");
            sb.AppendLine("\"Height\": 0,");
            sb.AppendLine("\"Width\": 0,");
            sb.AppendLine("\"Size\": 0 ");
            sb.AppendLine("},");
            sb.AppendLine("\"Error\": \"\",");
            sb.AppendLine("\"Code\": 0	");
            sb.AppendLine("}");
        }
        else
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(path));

            sb.AppendLine("{");
            sb.AppendLine("\"Path\": \"" + RemoveRootBeforeFormResponse(path) + "\",");
            sb.AppendLine("\"RealPath\": \"" + path + "\",");
            sb.AppendLine("\"Filename\": \"" + fileInfo.Name + "\",");
            sb.AppendLine("\"File Type\": \"" + fileInfo.Extension.Replace(".", "") + "\",");
            
            if (IsImage(fileInfo))
            {
                sb.AppendLine("\"Preview\": \"" + path + "\",");
            }
            else
            {
                sb.AppendLine("\"Preview\": \"" + String.Format("{0}{1}.png", IconDirectory, fileInfo.Extension.Replace(".", "")) + "\",");
            }
            
            sb.AppendLine("\"Properties\": {");
            sb.AppendLine("\"Date Created\": \"" + fileInfo.CreationTime.ToString() + "\", ");
            sb.AppendLine("\"Date Modified\": \"" + fileInfo.LastWriteTime.ToString() + "\", ");

            if (IsImage(fileInfo)) 
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(path)))
                {
                    sb.AppendLine("\"Height\": " + img.Height.ToString() + ",");
                    sb.AppendLine("\"Width\": " + img.Width.ToString() + ",");
                }
            }
            
            sb.AppendLine("\"Size\": " + fileInfo.Length.ToString() + " ");
            sb.AppendLine("},");
            sb.AppendLine("\"Error\": \"\",");
            sb.AppendLine("\"Code\": 0	");
            sb.AppendLine("}");
        }

        return sb.ToString();
        
    }

    private string Rename(string path, string newName)
    {
        FileAttributes attr = File.GetAttributes(HttpContext.Current.Server.MapPath(path));

        StringBuilder sb = new StringBuilder();
        
        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));
            Directory.Move(HttpContext.Current.Server.MapPath(path), Path.Combine(dirInfo.Parent.FullName, newName));

            DirectoryInfo fileInfo2 = new DirectoryInfo(Path.Combine(dirInfo.Parent.FullName, newName));

            sb.AppendLine("{");
            sb.AppendLine("\"Error\": \"No error\",");
            sb.AppendLine("\"Code\": 0,");
            sb.AppendLine("\"Old Path\": \"" + RemoveRootBeforeFormResponse(path) + "\",");
            sb.AppendLine("\"Old Name\": \"" + newName + "\",");
            sb.AppendLine("\"New Path\": \"" +
                fileInfo2.FullName.Replace(HttpRuntime.AppDomainAppPath, "/").Replace(Path.DirectorySeparatorChar, '/') + "\",");
            sb.AppendLine("\"New Name\": \"" + fileInfo2.Name + "\"");
            sb.AppendLine("}");
            
        }
        else 
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(path));
            File.Move(HttpContext.Current.Server.MapPath(path), Path.Combine(fileInfo.Directory.FullName, newName));

            FileInfo fileInfo2 = new FileInfo(Path.Combine(fileInfo.Directory.FullName, newName));
            
            sb.AppendLine("{");
            sb.AppendLine("\"Error\": \"No error\",");
            sb.AppendLine("\"Code\": 0,");
            sb.AppendLine("\"Old Path\": \"" + RemoveRootBeforeFormResponse(path) + "\",");
            sb.AppendLine("\"Old Name\": \"" + newName + "\",");
            sb.AppendLine("\"New Path\": \"" + 
                fileInfo2.FullName.Replace(HttpRuntime.AppDomainAppPath, "/").Replace(Path.DirectorySeparatorChar, '/') + "\",");
            sb.AppendLine("\"New Name\": \"" + fileInfo2.Name + "\"");
            sb.AppendLine("}");
        }

        return sb.ToString();
    }

    private string Delete(string path) 
    {
        FileAttributes attr = File.GetAttributes(HttpContext.Current.Server.MapPath(path));

        StringBuilder sb = new StringBuilder();

        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
        {
            Directory.Delete(HttpContext.Current.Server.MapPath(path), true);
        }
        else
        {
            File.Delete(HttpContext.Current.Server.MapPath(path));
        }

        sb.AppendLine("{");
        sb.AppendLine("\"Error\": \"No error\",");
        sb.AppendLine("\"Code\": 0,");
        sb.AppendLine("\"Path\": \"" + RemoveRootBeforeFormResponse(path) + "\"");
        sb.AppendLine("}");
        
        return sb.ToString();
    }

    private string AddFolder(string path, string NewFolder)
    {
        StringBuilder sb = new StringBuilder();

        Directory.CreateDirectory(Path.Combine(HttpContext.Current.Server.MapPath(path), NewFolder));

        sb.AppendLine("{");
        sb.AppendLine("\"Parent\": \"" + RemoveRootBeforeFormResponse(path) + "\",");
        sb.AppendLine("\"Name\": \"" + NewFolder + "\",");
        sb.AppendLine("\"Error\": \"No error\",");
        sb.AppendLine("\"Code\": 0");
        sb.AppendLine("}");
        
        return sb.ToString();
    }


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.Clear();

        context.Response.ContentType = "plain/text";
        context.Response.ContentEncoding = Encoding.UTF8;

        string path = this.StorageDirectory + (context.Request["path"] ?? ""),
                oldPath = this.StorageDirectory + (context.Request["old"] ?? ""),
                currentPath = this.StorageDirectory + (context.Request["currentpath"] ?? "");

        switch (context.Request["mode"])
        {
            case "getinfo":
                context.Response.Write(getInfo(path));
                break;
            case "getfolder":
                context.Response.Write(getFolderInfo(path));
                break;
            case "rename":
                context.Response.Write(Rename(oldPath, context.Request["new"]));
                break;
            case "delete":
                context.Response.Write(Delete(path));
                break;
            case "addfolder":
                context.Response.Write(AddFolder(path, context.Request["name"]));
                break;
            case "download":
                FileInfo fi = new FileInfo(context.Server.MapPath(path));
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + context.Server.UrlPathEncode(fi.Name));
                context.Response.AddHeader("Content-Length", fi.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.TransmitFile(fi.FullName);
                break;
            case "add":
                System.Web.HttpPostedFile file = context.Request.Files[0];
                file.SaveAs(context.Server.MapPath(Path.Combine(currentPath, Path.GetFileName(file.FileName))));
                context.Response.ContentType = "text/html";

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("{");
                sb.AppendLine("\"Path\": \"" + RemoveRootBeforeFormResponse(currentPath) + "\",");
                sb.AppendLine("\"Name\": \"" + Path.GetFileName(file.FileName) + "\",");
                sb.AppendLine("\"Error\": \"No error\",");
                sb.AppendLine("\"Code\": 0");
                sb.AppendLine("}");

                System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
                txt.TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine;
                txt.Text = sb.ToString();
                StringWriter sw = new StringWriter();
                System.Web.UI.HtmlTextWriter writer = new System.Web.UI.HtmlTextWriter(sw);
                txt.RenderControl(writer);
                context.Response.Write(sw.ToString());

                break;
            default:
                break;
        }
    }

 
    public bool IsReusable {
        get {
            return false;
        }
    }

}