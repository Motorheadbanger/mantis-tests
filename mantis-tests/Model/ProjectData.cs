using System;

namespace mantis_tests
{
    public class ProjectData : IComparable<ProjectData>, IEquatable<ProjectData>
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public ProjectData()
        {
        }

        public int CompareTo(ProjectData other)
        {
            return Name.CompareTo(other.Name);
        }

        public bool Equals(ProjectData other)
        {
            return Name.Equals(other.Name);
        }
    }
}
