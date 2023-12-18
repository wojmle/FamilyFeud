using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using FamilyFeud.MVVM.Interfaces;

namespace FamilyFeud.MVVM.Service
{
    public class DataService : IDataStorage
    {
        public bool Create(string data, string filePath)
        {
            if (this.Exists(filePath))
                return this.Update(data, filePath);
            try
            {
                using (FileStream fileStream = File.Create(filePath))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                    {
                        streamWriter.Write(data);
                        streamWriter.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateAsList(string[] data, string filePath)
        {
            if (this.Exists(filePath))
                return this.UpdateAllLines(data, filePath);
            try
            {
                File.WriteAllLines(filePath, data);
                return true;
            }
            catch
            {
                int num = (int)MessageBox.Show(filePath + " couldn't be loaded. Check if file exists.", "FamilyFeud", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        public bool Delete(string filePath)
        {
            if (!this.Exists(filePath))
                return false;
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Read(string filePath, out string data)
        {
            data = (string)null;
            if (!this.Exists(filePath))
            {
                int num = (int)MessageBox.Show(filePath + " couldn't be loaded. Check if file exists.", "FamilyFeud", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            try
            {
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)fileStream))
                        data = streamReader.ReadToEnd();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReadAllLines(string filePath, out string[] data)
        {
            data = (string[])null;
            if (!this.Exists(filePath))
            {
                int num = (int)MessageBox.Show(filePath + " couldn't be loaded. Check if file exists.", "FamilyFeud", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            try
            {
                data = File.ReadAllLines(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(string data, string filePath)
        {
            if (!this.Exists(filePath))
            {
                int num = (int)MessageBox.Show(filePath + " couldn't be loaded. Check if file exists.", "FamilyFeud", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Truncate, FileAccess.ReadWrite))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                    {
                        streamWriter.Write(data);
                        streamWriter.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAllLines(string[] data, string filePath)
        {
            if (!this.Exists(filePath))
            {
                int num = (int)MessageBox.Show(filePath + " couldn't be loaded. Check if file exists.", "FamilyFeud", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            try
            {
                File.WriteAllText(filePath, string.Empty);
                File.WriteAllLines(filePath, data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exists(string filePath) => File.Exists(filePath);

        public bool GetObjectFromFile<T>(string filePath, out T dto)
        {
            dto = default(T);
            string data;
            if (!this.Read(filePath, out data))
                return false;
            dto = JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return (object)dto != null;
        }

        public bool SaveObjectToFile(object dto, string filePath) => this.Create(JsonConvert.SerializeObject(dto, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        }), filePath);

        public bool GetAttributesFromXml(string file, out Dictionary<string, object> attributes)
        {
            attributes = new Dictionary<string, object>();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);
                if (xmlDocument.DocumentElement == null)
                    return false;
                foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
                    attributes.Add(childNode.Name, (object)childNode.InnerText);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
