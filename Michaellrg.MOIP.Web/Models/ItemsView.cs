using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Michaellrg.MOIP.Web.Models
{
    public class ItemsView
    {
        private List<Item> _items;
        public ItemsView()
        {
            this._items = new List<Item>();
        }
        public List<Item> Load()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Item>));
            string strAppPath = AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = new FileStream(strAppPath+"/Utils/List.xml", FileMode.OpenOrCreate);
            try
            {
                this._items = ser.Deserialize(fs) as List<Item>;
                return _items;
            }
            catch (InvalidOperationException ex)
            {
                ser.Serialize(fs, this._items);
                return null;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}