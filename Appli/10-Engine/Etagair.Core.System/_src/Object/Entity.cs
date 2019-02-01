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

        // add the property under the root properties
        public bool AddProperty(PropertyGroup propertyParent, PropertyBase property)
        {
            if (propertyParent == null)
            {
                PropertyRoot.AddProperty(property);
                return true;
            }
            PropertyRoot.AddProperty(property);
            return true;
        }

    }
}
