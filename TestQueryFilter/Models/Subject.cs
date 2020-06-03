using System.Collections.Generic;
using System.Text;

namespace TestQueryFilter.Models
{
    public class Subject : Identity
    {
        public Teacher Teacher { get; set; }
        public IList<Student> StudentList { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Subject: ");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append(base.ToString());
            stringBuilder.Append(System.Environment.NewLine);

            stringBuilder.Append("\tTeacher: ");
            stringBuilder.Append(System.Environment.NewLine);
            stringBuilder.Append($"\t{Teacher.ToString()}");
            stringBuilder.Append(System.Environment.NewLine);

            stringBuilder.Append("\t\tStudent List:");
            stringBuilder.Append(System.Environment.NewLine);

            for (int i = 0; i < StudentList.Count; i++)
            {
                stringBuilder.Append($"\t\t{StudentList[i].ToString()}");
                stringBuilder.Append(System.Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
