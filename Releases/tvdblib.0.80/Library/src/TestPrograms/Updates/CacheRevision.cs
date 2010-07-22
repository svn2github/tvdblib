using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace TestPrograms.Updates
{
  public class CacheRevision
  {
    public DateTime Date { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public FastZip Data { get; set; }

    public override string ToString()
    {
      return Name;
    }

    public String CreateFileName()
    {
      String fName = "Rev_" + this.Date.ToShortDateString().Replace('/', '-') +
                     "_" + this.Name + ".zip";

      return fName;
    }

    public static CacheRevision CreateFromFile(FileInfo _file)
    {
      try
      {
        String[] details = _file.Name.Split('_');
        CacheRevision rev = new CacheRevision();
        rev.Date = DateTime.Parse(details[1]);
        rev.Name = details[2].Replace(".zip", "");//remove .zip
        if (File.Exists(_file.FullName + ".desc.txt"))
        {
          rev.Description = File.ReadAllText(_file.FullName + ".desc.txt");
        }
        return rev;
      }
      catch (Exception ex)
      {
        return null;
      }
      
    }

  }
}
