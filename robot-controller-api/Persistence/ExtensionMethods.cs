using FastMember;   // library used to quickly map database data to object properties
using Npgsql;       // library used to work with PostgreSQL database

namespace robot_controller_api.Persistence
{
    // this class contains helper methods used by the repository
    public static class ExtensionMethods
    {
        // this method maps data from the database reader to a C# object
        public static void MapTo<T>(this NpgsqlDataReader dr, T entity)
        {
            // if the object is null, stop the program and show error
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // FastMember creates a tool that lets us access object properties easily
            var fastMember = TypeAccessor.Create(entity.GetType());

            // get all property names from the object
            var props = fastMember.GetMembers()
                .Select(x => x.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // loop through all columns returned from the database
            for (int i = 0; i < dr.FieldCount; i++)
            {
                // check if a column name matches a property name in the object
                var prop = props.FirstOrDefault(x =>
                    x.Equals(dr.GetName(i), StringComparison.OrdinalIgnoreCase));

                // if the property exists, assign the value from database to the object
                if (!string.IsNullOrEmpty(prop))
                    fastMember[entity, prop] =
                        dr.IsDBNull(i) ? null : dr.GetValue(i);
            }
        }
    }
}