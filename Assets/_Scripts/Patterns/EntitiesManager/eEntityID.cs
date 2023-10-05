using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Patterns.EntitiesManager
{
    public static class EntityID
    {
        public static IEnumerable<string> GetAllEntitiesNames()
        {
            return typeof(EntityID).GetFields().Select(fieldInfo => fieldInfo.Name);
        }
    }
}