using System.Collections.Generic;
using portfolio.entity;

namespace portfolio.webui.Models
{
    public class IndexListViewModel
    {
        public List<Resume> Resumes { get; set; }
        public About About { get; set; }
        public List<Skill> Skills { get; set; }
        public ContactModel Contact { get; set; }
    }
}