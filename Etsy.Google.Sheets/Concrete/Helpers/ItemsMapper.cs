using DomainLayer.Concrete.Google.Sheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etsy.Google.Sheets.Concrete.Helpers
{
    public static class ItemsMapper
    {
        public static List<Item> MapFromRangeData(IList<IList<object>> values)
        {
            var items = new List<Item>();
            foreach (var value in values)
            {
                
                Item item = new()
                {
                    
                };
                items.Add(item);
            }
            return items;
        }
        public static IList<IList<object>> MapToRangeData(Item item)
        {
            var objectList = new List<object>() {};
            var rangeData = new List<IList<object>> { objectList };
            return rangeData;
        }
    }
}
