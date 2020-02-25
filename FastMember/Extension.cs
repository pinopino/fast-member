using System;
using System.Linq;

namespace FastMember
{
    public static class Extension
    {
        public static void AssignValue(this ObjectAccessor accessor, string name, object value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var index = name.IndexOf('.');
            if (index == -1)
            {
                var member = accessor.TypeAccessor
                    .GetMembers()
                    .FirstOrDefault(
                        m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
                if (member != null)
                {
                    var type = member.Type;
                    type = Nullable.GetUnderlyingType(type) ?? type;
                    value = value == null ? GetDefault(type) : Convert.ChangeType(value, type);
                    accessor[name] = value;
                }
            }
            else
            {
                accessor = ObjectAccessor.Create(accessor[name.Substring(0, index)]);
                AssignValue(accessor, name.Substring(index + 1), value);
            }
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
