using System.Text;
using System.Collections;
namespace SingleResponsability
{
    public static class ExportHelper
    {

        public static void Export<T>(this IEnumerable<T> source) where T : class
        {
            var sb = new StringBuilder();
            var properties = typeof(T).GetProperties();
            var headers = string.Join(";",properties.Select(p => p.Name));
            sb.AppendLine(headers);
            foreach (var item in source)
            {
                string line = string.Empty;
                foreach (var prop in properties)
                {
                    object? value = prop.GetValue(item, null);

                    if (value is null) continue;
                    
                    if (value is not string && value is IEnumerable valuearray){
                        var values = valuearray.Cast<object>().Select(v => v); 
                        line += string.Join("|",values);
                        continue;
                    }

                    line += $"{value};";
                }
                sb.AppendLine(line);
                System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Students.csv"), sb.ToString(), Encoding.Unicode);
            }
        }
    }
}