using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFeud.MVVM.Interfaces
{
    public interface IDataStorage
    {
        bool Create(string data, string filePath);

        bool CreateAsList(string[] data, string filePath);

        bool Delete(string filePath);

        bool Read(string filePath, out string data);

        bool ReadAllLines(string filePath, out string[] data);

        bool Update(string data, string filePath);

        bool UpdateAllLines(string[] data, string filePath);

        bool Exists(string filePath);

        bool GetObjectFromFile<T>(string filePath, out T dto);

        bool SaveObjectToFile(object dto, string filePath);

        bool GetAttributesFromXml(string file, out Dictionary<string, object> attributes);
    }
}
