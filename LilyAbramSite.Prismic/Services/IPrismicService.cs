using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LilyAbramSite.Prismic.Models;
using prismic;

namespace LilyAbramSite.Prismic.Services
{
    public interface IPrismicService
    {
        Task<Globals> GetGlobals();
        Task<Project> GetProject(string uid);
        Task<IList<Project>> GetProjects();
    }
}
