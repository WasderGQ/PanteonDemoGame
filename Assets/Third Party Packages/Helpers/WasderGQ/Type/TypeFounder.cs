using System.Reflection;

namespace Third_Party_Packages.Helpers.WasderGQ.Type
{
    public class TypeFounder 
    {
        private int HowMuchProductTypeYouHave<T>(object SearchObject)//Every ICreater creating one product type.
        {
            System.Type factorType = SearchObject.GetType();
            int variableCount = 0;

            foreach (FieldInfo field in factorType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.FieldType == typeof(T))
                {
                    variableCount++;
                }
            }

            return variableCount;
        }
    }
}
