using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ShopApp.Services
{
    public class ValidationService
    {
        
        public bool IsUnique(object objectToCompare, List<object> allObjects, string nameOfPropertyToCheck)
        {
            bool result = true;
            PropertyInfo property = objectToCompare.GetType().GetProperty(nameOfPropertyToCheck);
            PropertyInfo idProperty = objectToCompare.GetType().GetProperty("Id");
            var targetObjectValue = property.GetValue(objectToCompare, null);

            if (allObjects.Count > 0)
            {
                CheckObjectsMaches(objectToCompare, allObjects[0]);
            }
            
            foreach(var obj in allObjects)
            {
                var compareToValue = property.GetValue(obj, null);
                
                if(compareToValue.Equals(targetObjectValue) 
                    && !idProperty.GetValue(objectToCompare, null).Equals(idProperty.GetValue(obj, null)))
                {
                    result = false;
                    break;
                }
            }
            
            return result;
        }

        private void CheckObjectsMaches(object objectOne, object objectTwo)
        {
            if (objectOne.GetType() != objectTwo.GetType())
            {
                throw new Exception("Object to compare type doesn't match with object type defined in List.");
            }
        }
    }
}
