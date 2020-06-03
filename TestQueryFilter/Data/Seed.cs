using System.Collections.Generic;
using TestQueryFilter.Models;

namespace TestQueryFilter.Data
{
    public class Seed
    {
        public IList<Subject> GetSubjectList()
        {
            List<Subject> subjectList = new List<Subject>
            {
                new Subject {
                    Id = 0,
                    Name = "Mathematics",
                    Teacher = new Teacher {
                        Id = 0,
                        Name = "Daniel Maldonado"
                    },
                    StudentList= new List<Student> {
                        new Student {
                            Id = 0,
                            Name="David Maldonado"
                        },
                        new Student {
                            Id = 1,
                            Name="Renata Torres"
                        },
                        new Student {
                            Id = 2,
                            Name="Chiristian Zapata"
                        },
                    }
                },
                new Subject {
                    Id = 1,
                    Name = "Language Arts",
                    Teacher = new Teacher {
                        Id = 1,
                        Name = "Sofia Jimenez"
                    },
                    StudentList= new List<Student> {
                        new Student {
                            Id = 0,
                            Name="David Maldonado"
                        },
                        new Student {
                            Id = 1,
                            Name="Renata Torres"
                        },
                        new Student {
                            Id = 3,
                            Name="Dannah Pimbo"
                        },
                    }
                },
                new Subject {
                    Id = 2,
                    Name = "Science",
                    Teacher = new Teacher {
                        Id = 2,
                        Name = "Martha Mera"
                    },
                    StudentList= new List<Student> {
                        new Student {
                            Id = 1,
                            Name="Renata Torres"
                        },
                        new Student {
                            Id = 3,
                            Name="Dannah Pimbo"
                        },
                         new Student {
                            Id = 4,
                            Name="Sebastian Morales"
                        },
                    }
                },
                new Subject {
                    Id = 3,
                    Name = "Health",
                    Teacher = new Teacher {
                        Id = 3,
                        Name = "Ximena Sanchez"
                    },
                    StudentList= new List<Student> {
                        new Student {
                            Id = 3,
                            Name="Dannah Pimbo"
                        },
                         new Student {
                            Id = 4,
                            Name="Sebastian Morales"
                        },
                        new Student {
                            Id = 5,
                            Name="Robin Mena"
                        },
                    }
                }
            };

            return subjectList;
        }
    }
}
