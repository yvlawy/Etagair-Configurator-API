using System;
using System.Collections.Generic;
using System.Text;

namespace Etagair.Core.System
{
    /// <summary>
    /// An entity, can be:  product or a service object.
    /// Has properties, (key-value).
    /// </summary>
    public class Entity: ObjectBase
    {
        public Entity()
        {
            EntityTemplId = null;
            // create an empty property root: missing key.
            PropertyRoot = new PropertyGroup();
        }

        public string EntityTemplId { get; set; }

        /// <summary>
        /// The root of properties of the entity.
        /// </summary>
        public PropertyGroup PropertyRoot { get; private set; }

        public void SetCreatedFromTempl(EntityTempl entityTempl)
        {
            EntityTemplId = entityTempl.Id;
            BuildFrom = BuildFrom.Template;
            BuildFinished = false;
        }

        /// <summary>
        /// Add the property under the property parent.
        /// </summary>
        /// <param name="propertyParent"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public bool AddProperty(PropertyGroup propertyParent, PropertyBase property)
        {
            if (propertyParent == null)
            {
                propertyParent.AddProperty(property);
                return true;
            }
            propertyParent.AddProperty(property);
            return true;
        }

    }
}
