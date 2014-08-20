using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Expression.Encoder;

namespace WpfApplication1
{
    class DataSetting
    {
        static public string fileDirection{get;set;}

        static public Preset filePreset { get; set; }

        static public void initialize()
        {
            fileDirection = @"D:\UserList";
            filePreset = Presets.VC1HD1080pVBR;
            System.IO.Directory.CreateDirectory(fileDirection);
            System.IO.Directory.CreateDirectory(@"D:\tmp");
        }

    }
}
